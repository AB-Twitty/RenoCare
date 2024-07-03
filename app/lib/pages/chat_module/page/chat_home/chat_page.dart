import 'package:app/pages/chat_module/page/chat_home/widget/mesasge_bubble2.dart';
import 'package:chat_bubbles/chat_bubbles.dart';
import 'package:dio/dio.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:signalr_core/signalr_core.dart';

import 'widget/message_bubble.dart';
import '../../../../services/firebase_service.dart';
import '../../../../services/media_service.dart';
import '../../../../services/signalR_service.dart';
import '../../../../services/token_service.dart';
import 'model/message_model.dart';

class ChatPage extends StatefulWidget {
  final String active_chat_Id;
  final String chatName;

  ChatPage({required this.active_chat_Id, required this.chatName});

  @override
  _ChatPageState createState() => _ChatPageState();
}

class _ChatPageState extends State<ChatPage> {
  final FilePickerUtil filePickerUtil = FilePickerUtil();
  final List<Message> messages = [];
  final TextEditingController _controller = TextEditingController();

  late final HubConnection _hubConnection;
  final loginDataManager2 = LoginDataManager2();
  String currId = "";
  String RecId = "";
  String accessToken = "";
  final Dio _dio = Dio();
  final ScrollController _scrollController = ScrollController();
  int _currentPage = 1;
  bool hasNextPage = true;
  bool getEnd=false;
  bool isMe=false;
  @override
  void initState() {
    super.initState();

    // _initSignalrConnection();
    _initialize();
    _fetchPreviousMessages();
    _scrollController.addListener(() {
      if (_scrollController.position.atEdge &&
          _scrollController.position.pixels == 0) {
        if (hasNextPage) {
          print("============================pagenation work==========");
          _fetchPreviousMessages(pageIndex: ++_currentPage);
        }
      }
    });
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _scrollToEnd();
    });
  }


  void _scrollToEnd() {
    WidgetsBinding.instance.addPostFrameCallback((_){

      if (_scrollController.hasClients) {
        _scrollController.animateTo(
          _scrollController.position.maxScrollExtent,
          duration: Duration(milliseconds: 500),
          curve: Curves.easeOut,
        );
      }
    });
  }

  @override
  void dispose() {
    //_hubConnection.stop();
    _scrollController.dispose();
    //loginDataManager2.clearLoginData();
    super.dispose();
  }

  // void _initSignalrConnection() async {
  //
  //   try {
  //     _hubConnection = HubConnectionBuilder()
  //         .withUrl(
  //           "https://renocareapi.azurewebsites.net/chat",
  //           HttpConnectionOptions(
  //             logging: (level, message) => print(message),
  //             accessTokenFactory: () async => accessToken,
  //             transport: HttpTransportType.longPolling,
  //           ),
  //         )
  //         .withAutomaticReconnect()
  //         .build();
  //
  //     await _hubConnection.start();
  //   } catch (e) {
  //     print(" Error establishing signalR connection");
  //     _reconnect();
  //   }
  // }

  Future<void> _initialize() async {
    accessToken = await loginDataManager2.getAccessToken() ?? "";

    SignalRUtil signalRUtil=SignalRUtil();
    _hubConnection=await signalRUtil.startConnection();
    if (accessToken != null) {

      currId = await loginDataManager2.getId() ?? "";
      //_initSignalrConnection();
    } else
      print("===============Error : Access Token is null");

    _hubConnection.on("ReceiveMessage", _handleReceivedMessage);
    //on message marked as received (gets the whole msg)
    _hubConnection.on("MarkedAsReceived", _markMessageAsReceived);
    //on message marked as seen (gets only the msg_id)
    _hubConnection.on("MarkedAsRead", _markMessageAsRead);
  }

  //sending message to the hub
  void _sendMessage(String message, {String? fileUrl}) async {
    if (message.isNotEmpty) {
      final currUserId = await loginDataManager2.getId() ?? "";
      currId = currUserId;

      _controller.clear();
      await _hubConnection.invoke("SendMessage",
          args: <Object>[widget.active_chat_Id, message]);
      //_scrollToEnd();
    }
  }

  void _markMessageAsReceived(List<Object?>? arguments) {
    if (arguments == null || arguments.isEmpty) return;

    final messageJson = arguments[0] as Map<String, dynamic>;
    final receivedMessage = Message.fromJson(messageJson);

    setState(() {
      final messageIndex =
          messages.indexWhere((msg) => msg.Id == receivedMessage.Id);
      if (messageIndex != -1) {
        messages[messageIndex] =
            receivedMessage.copyWith(status: 2); // 2 means delivered
      }
    });
  }

  void _markMessageAsRead(List<Object?>? arguments) {
    if (arguments == null || arguments.isEmpty) return;

    final messageId = arguments[0];
    setState(() {
      final messageIndex = messages.indexWhere((msg) => msg.Id == messageId);
      if (messageIndex != -1) {
        messages[messageIndex] =
            messages[messageIndex].copyWith(status: 3); // 3 means seen
      }

    });
  }

  void _handleReceivedMessage(List<Object?>? arguments) async {
    if (arguments == null || arguments.isEmpty) return;

    final String currUserId = await loginDataManager2.getId() ?? "";
    print(
        "===========================A message received==========================");
    print("==============================SEnder ID $currUserId");
    final Message msg = Message.fromJson(arguments[0] as Map<String, dynamic>);
    String curr_user_id = currUserId;

    bool shouldMarkRead = false;
    bool shouldMarkReceived = false;

    if (msg.senderId == curr_user_id &&
        msg.receiverId == widget.active_chat_Id) {
      // Add the message as I am the sender
      setState(() {
        messages.add(msg);
        print(
            "===========================A message Content==========================");
        print(msg);
      });
    } else if (msg.senderId == widget.active_chat_Id &&
        msg.receiverId == curr_user_id) {
      // Add the message as I am the receiver
      setState(() {
        messages.add(msg);

      });

      // As a receiver, invoke an event as received and seen, because this is the active chat
      shouldMarkRead = true;
    } else if (msg.senderId != curr_user_id) {
      // Message from another chat, just notify the user with the new message

      // As a receiver, invoke an event as received only, because this is not the active chat
      shouldMarkReceived = true;
    }
    _scrollToEnd();
    if (shouldMarkRead) {
      await _hubConnection.invoke("MarkRead", args: <Object>[msg.Id]);
    } else if (shouldMarkReceived) {
      await _hubConnection.invoke("MarkReceived", args: <Object>[msg.Id]);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(

      appBar: AppBar(

        iconTheme: IconThemeData(
          color: Colors.black
        ),
        backgroundColor: Colors.grey[200],
        centerTitle: true,
        title: Text(widget.chatName,style: TextStyle(color: Colors.black),),
        automaticallyImplyLeading: false,
        leading:  IconButton(onPressed: (){
          Navigator.pop(context,widget.active_chat_Id);

        }, icon: Icon(Icons.arrow_back)),

      ),
      body: Stack(

        children: [

          Positioned.fill(child: Image.asset("assets/images/background.jpeg",fit: BoxFit.cover,)),
          Column(
            children: [
              Expanded(
                child: ListView.builder(
                  controller: _scrollController,
                  itemCount: messages.length,
                  itemBuilder: (context, index) {
                    final message = messages[index];
                    isMe = message.senderId == currId;
                    return MessageBubble2(
                      message: message,
                      isMe: isMe,
                    );
                  },
                ),
              ),
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: Container(
                  decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.all(Radius.circular(16))
                  ),
                  child: Row(
                    children: [
                      IconButton(
                        onPressed: () {
                          _pickFile();
                        },
                        icon: Icon(Icons.attach_file_outlined),
                        color: Colors.black,
                      ),
                      Expanded(
                        child: TextField(
                          controller: _controller,
                          decoration: InputDecoration(hintText: 'Enter a message'),
                          onSubmitted: _sendMessage,
                        ),
                      ),

                      //SizedBox(width: 12,),
                      IconButton(
                        icon: Icon(Icons.send,color: Color(0xff3C98CB),),
                        onPressed: () => _sendMessage(_controller.text),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }

  Future<void> _pickFile() async {
    try {
      FilePickerResult? result = await FilePicker.platform.pickFiles();

      if (result != null) {
        PlatformFile file = result.files.first;

        FormData formData = FormData.fromMap({
          'file': await MultipartFile.fromFile(file.path!),
          'receiverId': widget.active_chat_Id,
        });

        // Replace with your API endpoint for file upload
        String uploadUrl = 'https://renocareapi.azurewebsites.net/chat/upload';

        Response response = await _dio.post(
          uploadUrl,
          data: formData,
          options: Options(
            headers: {
              "Authorization": "Bearer $accessToken",
              // Replace $accessToken with your actual access token variable
            },
          ),
        );

        // Handle success
        print('File uploaded successfully');
      } else {
        // User canceled the picker
        print('File picker canceled');
      }
    } catch (e) {
      // Handle error
      print('Error uploading file: $e');
    }
  }

//===============================================================d
  //      Get Previous messages
//===============================================================
  Future<void> _fetchPreviousMessages({int pageIndex = 1, int pageSize = 10}) async {
    final String activeChatId = widget.active_chat_Id;
    final String accessToken = await loginDataManager2.getAccessToken() ?? "";

    _scrollController.offset;
    try {
      final response = await _dio.get(
        'https://renocareapi.azurewebsites.net/chat/messages/$activeChatId',
        queryParameters: {
          'page': pageIndex,
          'pageSize': pageSize,
        },
        options: Options(
          headers: {
            "Authorization": "Bearer $accessToken",
          },
        ),
      );

      if (response.statusCode == 200) {
        final data = response.data;

        final List<Message> fetchedMessages = (data['items'] as List)
            .map((item) => Message.fromJson(item))
            .toList();

        setState(() {
          messages.insertAll(0, fetchedMessages.reversed.toList());
          hasNextPage = data['hasNextPage'];
        });

        if(!getEnd)
          {
            _scrollToEnd();
            getEnd=true;
          }
        print('Messages fetched successfully');
      } else {
        print('Error fetching messages: ${response.statusMessage}');
      }
    } catch (e) {
      print('An error occurred while fetching messages: $e');
    }
  }


  //========================================================
  //========================================================
  //========================================================
  void _reconnect() async {
    while (_hubConnection.state != HubConnectionState.connected) {
      try {
        await _hubConnection.start();
        print("Reconnected to SignalR server.");
      } catch (e) {
        print("Reconnection failed: $e. Retrying...");
        await Future.delayed(Duration(seconds: 5));
      }
    }
  }
}
