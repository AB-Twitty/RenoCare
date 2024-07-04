import 'package:app/services/notification_service.dart';
import 'package:app/services/signalR_service.dart';
import 'package:app/services/token_service.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart'
    as not;
import 'package:intl/intl.dart';
import 'package:loading_animation_widget/loading_animation_widget.dart';
import 'package:signalr_core/signalr_core.dart' as signalR;

import 'chat_page.dart';
import 'model/message_model.dart';
import 'model/chat_response.dart';

class ChatHomePage extends StatefulWidget {
  @override
  State<ChatHomePage> createState() => _ChatHomePageState();
}

class _ChatHomePageState extends State<ChatHomePage> {
  final loginManager = LoginDataManager2();
  final not.FlutterLocalNotificationsPlugin flutterLocalNotificationsPlugin =
      not.FlutterLocalNotificationsPlugin();

  late final signalR.HubConnection _hubConnection;

  String accessToken = "";

  String prv_active_id = "";
  final Dio _dio = Dio();
  List<Contact> contacts = [];
  String currUserId = "";
  late Future<void> _contactsFuture;

  @override
  void initState() {
    super.initState();
    _contactsFuture = _getContacts();
    initSignalR();
  }

  Future<void> _getContacts() async {
    accessToken = await loginManager.getAccessToken() ?? "";

    try {
      final response = await _dio.get(
        "https://renocareapi.azurewebsites.net/chat/contacts",
        options: Options(
          headers: {
            'Authorization': 'Bearer $accessToken',
          },
        ),
      );

      if (response.statusCode == 200) {
        final chatResponse = ChatResponse.fromJson(response.data);
        if (chatResponse.succeded) {
          setState(() {
            contacts = chatResponse.data;
            for (int i = 0; i < contacts.length; i++) {
              if (contacts[i].lastMsg.senderId != currUserId) // i am sender
              {
                contacts[i].lastMsg.status = 0;
              }
            }
          });

          for (int i = 0; i < contacts.length; i++) {
            print("===================================================");
            print("Name: ${contacts[i].name}");

            print("Name: ${contacts[i].lastMsg.message}");
            print("Name: ${contacts[i].unreadMsgCount}");
            print("Name: ${contacts[i].lastMsg.status}");
          }
        } else {
          print("Failed to fetch contacts: ${chatResponse.message}");
        }
      } else {
        print("Failed to fetch contacts: ${response.statusCode}");
      }
    } catch (e) {
      print(e);
    }
  }

  Future<void> initSignalR() async {
    SignalRUtil signalRUtil = SignalRUtil();

    _hubConnection = await signalRUtil.startConnection();

    _hubConnection.on("ReceiveMessage", _handleReceivedMessage);
    _hubConnection.on("MarkedAsReceived", _markMessageAsReceived);
    _hubConnection.on("MarkedAsRead", _markMessageAsRead);
  }

  String _statusText(int status) {
    switch (status) {
      case 1:
        return 'Sent';
      case 2:
        return 'Delivered';
      case 3:
        return 'Seen';
      default:
        return '';
    }
  }

  void _markMessageAsReceived(List<Object?>? arguments) {
    if (arguments == null || arguments.isEmpty) return;

    final messageJson = arguments[0] as Map<String, dynamic>;
    final receivedMessage = Message.fromJson(messageJson);
    setState(() {
      final index = contacts.indexWhere((chat) =>
          (currUserId == chat.lastMsg.senderId) &&
          chat.userId == receivedMessage.receiverId);
      if (index != -1) {
        if (currUserId == contacts[index].lastMsg.senderId)
          contacts[index].lastMsg.status = 2; // 2 means delivered
      }
      print(
          "============================The Index Derliverd: $index====================");
    });
  }

  void _markMessageAsRead(List<Object?>? arguments) {
    if (arguments == null || arguments.isEmpty) return;

    final messageId = arguments[0] as int;
    setState(() {
      final index = contacts.indexWhere((chat) =>
          (chat.lastMsg.Id == messageId) &&
          (currUserId == chat.lastMsg.senderId));
      if (index != -1) {
        contacts[index].lastMsg.status = 3; // 3 means seen
      }
      print(
          "============================The IndexSeen: $index====================");
    });
  }

  void _handleReceivedMessage(List<Object?>? arguments) async {
    if (arguments == null || arguments.isEmpty) return;

    currUserId = await loginManager.getId() ?? "";
    final Message msg = Message.fromJson(arguments[0] as Map<String, dynamic>);

    final String senderId = msg.senderId;
    final String receiverId = msg.receiverId;

    setState(() {
      final index = contacts.indexWhere((contact) =>
          contact.userId == (senderId != currUserId ? senderId : receiverId));

      if (index != -1) {
        final contact = contacts[index];
        contact.lastMsg = msg;
        if (senderId != currUserId) {
          contact.unreadMsgCount++;
          NotificationService.showMessageNotification(
              title: contact.name,
              body: msg.message,
              fln: flutterLocalNotificationsPlugin);
          contact.lastMsg.status = 0;
        }
      } else {
        String name = arguments[1] as String;
        Contact contact = Contact(
          name: name,
          userId: senderId,
          contactId: 0,
          lastMsg: msg,
          unreadMsgCount: senderId != currUserId ? 1 : 0,
        );
        contacts.add(contact);
      }
    });
    await _hubConnection.invoke("MarkReceived", args: <Object>[msg.Id]);
  }

  String _formatDate(DateTime dateTime) {
    return DateFormat('yyyy-MM-dd HH:mm').format(dateTime);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[200],
      appBar: AppBar(
        title: Text('Chats'),
        backgroundColor: Color.fromRGBO(60, 152, 203, 1),
      ),
      body: FutureBuilder<void>(
        future: _contactsFuture,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(
              child: LoadingAnimationWidget.threeArchedCircle(
                  color: Colors.blue, size: 50),
            );
          } else if (snapshot.hasError) {
            return Center(child: Text('Error fetching contacts'));
          } else if (contacts.isEmpty) {
            return Center(child: Text('No contacts available'));
          }
          return Padding(
            padding: const EdgeInsets.symmetric(horizontal: 15),
            child: ListView.builder(
              itemCount: contacts.length,
              itemBuilder: (context, index) {
                final contact = contacts[index];
                return Container(
                  margin: EdgeInsets.symmetric(vertical: 12),
                  padding: EdgeInsets.all(9),
                  decoration: BoxDecoration(
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(20)),
                  child: ListTile(
                    leading: Image.asset(
                      "assets/images/profile2.jpeg",
                      width: 40,
                    ),
                    title: Text(
                      contact.name,
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                      ),
                      overflow: TextOverflow.ellipsis,
                    ),
                    subtitle: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        SizedBox(
                          height: 7,
                        ),
                        Text(
                          contact.lastMsg.message,
                          style: TextStyle(
                            overflow: TextOverflow.ellipsis,
                            fontSize: 16,
                          ),
                        ),
                        SizedBox(
                          height: 7,
                        ),
                        Text(
                          _formatDate(contact.lastMsg.sendingTime),
                          style: TextStyle(
                            fontSize: 14,
                            color: Colors.grey,
                            overflow: TextOverflow.ellipsis,
                          ),
                        ),
                        Text(
                          _statusText(contact.lastMsg.status!),
                          style: TextStyle(fontSize: 12, color: Colors.grey),
                        ),
                      ],
                    ),
                    trailing: contact.unreadMsgCount > 0
                        ? CircleAvatar(
                            radius: 10,
                            backgroundColor: Colors.blueAccent,
                            child: Text(
                              contact.unreadMsgCount.toString(),
                              style:
                                  TextStyle(color: Colors.white, fontSize: 12),
                            ),
                          )
                        : null,
                    onTap: () async {
                      setState(() {
                        contact.unreadMsgCount = 0;
                      });
                      await Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => ChatPage(
                            active_chat_Id: contact.userId,
                            chatName: contact.name,
                          ),
                        ),
                      ).then((value) {
                        prv_active_id = value;
                        fun();
                      });
                      setState(() {}); // Refresh the chat list on return
                    },
                  ),
                );
              },
            ),
          );
        },
      ),
    );
  }

  void fun() {
    final index =
        contacts.indexWhere((contact) => contact.userId == prv_active_id);

    if (index != -1) {
      contacts[index].unreadMsgCount = 0;
    }
  }
}
