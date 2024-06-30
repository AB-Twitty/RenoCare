import 'package:app/services/signalR_service.dart';
import 'package:app/services/token_service.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:signalr_core/signalr_core.dart';

import 'chat_page.dart';
import 'model/chat_model.dart';
import 'model/message_model.dart';

class ChatHomePage extends StatefulWidget {
  @override
  State<ChatHomePage> createState() => _ChatHomePageState();
}

class _ChatHomePageState extends State<ChatHomePage> {
  final loginManager = LoginDataManager();
  final loginDataManager2 = LoginDataManager2();

  late final HubConnection _hubConnection;

  String accessToken = "";

  final Dio _dio = Dio();

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _getContacts();
    initSignalR();
  }

  final List<Chat> chats = [];

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
        final List<dynamic> data = response.data['data'];
        List<Chat> fetchedChats =
            data.map((json) => Chat.fromJson(json)).toList();

        print(fetchedChats[0].name);
        print(fetchedChats[0].lastMessage);

        setState(() {
          chats.addAll(fetchedChats);
        });
      } else {
        print("Faild to fetch contacts ${response.statusCode}");
      }
    } catch (e) {
      print(e);
    }
  }

  Future<void> initSignalR() async {
    SignalRUtil signalRUtil = SignalRUtil();

    _hubConnection = await signalRUtil.startConnection();

    _hubConnection.on("ReceiveMessage", _handleReceivedMessage);
  }

  void _handleReceivedMessage(List<Object?>? arguments) async {
    if (arguments == null || arguments.isEmpty) return;

    final String currUserId = await loginDataManager2.getId() ?? "";
    print(
        "===========================A message received==========================");
    print("==============================SEnder ID $currUserId");
    final Message msg = Message.fromJson(arguments[0] as Map<String, dynamic>);
    String curr_user_id = currUserId;

    setState(() {
      final Index = chats.indexWhere(
          (chat) => chat.id == msg.senderId || chat.id == msg.receiverId);
      if (Index != -1) {
        chats[Index].lastMessage = msg.message;
        chats[Index].lastMessageTime = msg.sendingTime;
        if (msg.senderId != currUserId) {
          chats[Index].unreadMsgCount++;
        }

        // last date
        // counter of unread messages
        //
      } else {
        String name = arguments[1] as String;
        Chat chat = Chat(
            id: msg.senderId,
            name: name,
            lastMessage: msg.message,
            unreadMsgCount: 1);
        chats.add(chat);
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
      appBar: AppBar(
        title: Text('Chats'),
        actions: [
          IconButton(
              onPressed: () async {
                final loadedData = await loginManager.loadLoginData();
                print('User ID: ${loadedData['id']}');
                print('First Name: ${loadedData['firstName']}');
                print('Last Name: ${loadedData['lastName']}');
                print('Access Token: ${loadedData['accessToken']}');
              },
              icon: Icon(Icons.add))
        ],
      ),
      body: ListView.builder(
        itemCount: chats.length,
        itemBuilder: (context, index) {
          final chat = chats[index];
          return ListTile(
            leading: Image.asset(
              "assets/images/profile2.jpeg",
              height: 30,
            ),
            title: Text(chat.name),
            subtitle: Column(
              children: [
                Text(chat.lastMessage),
                if (chat.lastMessageTime != null)
                  Text(
                    _formatDate(chat.lastMessageTime!),
                    style: TextStyle(fontSize: 12, color: Colors.grey),
                  ),
              ],
            ),
            trailing: chat.unreadMsgCount > 0
                ? CircleAvatar(
                    radius: 10,
                    backgroundColor: Colors.red,
                    child: Text(
                      chat.unreadMsgCount.toString(),
                      style: TextStyle(color: Colors.white, fontSize: 12),
                    ),
                  )
                : null,
            onTap: () {
              setState(() {
                chat.unreadMsgCount = 0;
              });
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) =>
                      ChatPage(active_chat_Id: chat.id, chatName: chat.name),
                ),
              );
            },
          );
        },
      ),
    );
  }
}

//
//
// ListView.builder(
// itemCount: chats.length,
// itemBuilder: (context, index) {
// final chat = chats[index];
// return ListTile(
//
// leading: Image.asset("assets/images/profile.png",height: 30,),
// title: Text(chat.name),
// subtitle: Text(chat.lastMessage),
// onTap: () {
// Navigator.push(
// context,
// MaterialPageRoute(
// builder: (context) => ChatPage(active_chat_Id: chat.id, chatName: chat.name),
// ),
// );
// },
// );
// },
// )
