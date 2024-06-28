import 'package:chat_bubbles/chat_bubbles.dart';
import 'package:dio/dio.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:signalr_core/signalr_core.dart';

import '../../../../Shared/components/widgets/message_bubble.dart';
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
  final loginDataManager2=LoginDataManager2();
  String currId="";
  String accessToken ="";
  final Dio _dio = Dio();
  @override
  void initState() {
    super.initState();

    // _initSignalrConnection();
    _initialize();
    //add all events and their handler
    //on receiving a message event
    // _hubConnection.on("ReceiveMessage", _handleReceivedMessage);
    // //on message marked as received (gets the whole msg)
    // _hubConnection.on("MarkedAsReceived", _markMessageAsReceived);
    // //on message marked as seen (gets only the msg_id)
    // _hubConnection.on("MarkedAsRead", _markMessageAsRead);
  }






  @override
  void dispose() {
    _hubConnection.stop();
    //loginDataManager2.clearLoginData();
    super.dispose();
  }


  void _initSignalrConnection() async{
   print("=====================init access token$accessToken=====================");
    try{
      _hubConnection = HubConnectionBuilder()
          .withUrl(
        "https://renocareapi.azurewebsites.net/chat",
        HttpConnectionOptions(
          logging: (level, message) => print(message),
          accessTokenFactory: () async => accessToken,


          transport: HttpTransportType.longPolling,
        ),
      )
          .withAutomaticReconnect()
          .build();


      await _hubConnection.start();
    }
    catch(e)
    {
      print(" Error establishing signalR connection");
      _reconnect();
    }

  }

Future<void>_initialize()async{
    accessToken=await loginDataManager2.getAccessToken()??"";

    if(accessToken !=null)
      {
        print("================================Access Token is ++++++++$accessToken");
        _initSignalrConnection();
      }
    else
      print("===============Error : Access Token is null");

    _hubConnection.on("ReceiveMessage", _handleReceivedMessage);
    //on message marked as received (gets the whole msg)
    _hubConnection.on("MarkedAsReceived", _markMessageAsReceived);
    //on message marked as seen (gets only the msg_id)
    _hubConnection.on("MarkedAsRead", _markMessageAsRead);

}

  //sending message to the hub
  void _sendMessage(String message, {String? fileUrl})async {
    if(message.isNotEmpty){
      final currUserId=await loginDataManager2.getId()??"";
      currId=currUserId;
      final msg=Message(senderId: currUserId, receiverId: widget.active_chat_Id, message: message, sendingTime: DateTime.now(), Id: '');
      setState(() {
        messages.add(msg);
      });
      _controller.clear();
      await _hubConnection.invoke("SendMessage", args: <Object>[widget.active_chat_Id, message]);

    }
  }

  void _markMessageAsReceived(List<Object?>? arguments) {
    if (arguments == null || arguments.isEmpty) return;

    final messageJson = arguments[0] as Map<String, dynamic>;
    final receivedMessage = Message.fromJson(messageJson);

    setState(() {
      final messageIndex = messages.indexWhere((msg) => msg.Id == receivedMessage.Id);
      if (messageIndex != -1) {
        messages[messageIndex] = receivedMessage.copyWith(status: 2); // 2 means delivered
      } else {
        messages.add(receivedMessage.copyWith(status: 2));
      }
    });
  }

  void _markMessageAsRead(List<Object?>? arguments) {
    if (arguments == null || arguments.isEmpty) return;

    final messageId = arguments[0] as String;
    setState(() {
      final messageIndex = messages.indexWhere((msg) => msg.Id == messageId);
      if (messageIndex != -1) {
        messages[messageIndex] = messages[messageIndex].copyWith(status: 3); // 3 means seen
      }
    });
  }

  void _handleReceivedMessage(List<Object?>? arguments) async {
    if (arguments == null || arguments.isEmpty) return;

    final String currUserId = await loginDataManager2.getId() ?? "";
    print("===========================A message received==========================");
    print("==============================SEnder ID $currUserId");
    final Message msg = Message.fromJson(arguments[0] as Map<String, dynamic>);
    String curr_user_id = currUserId;

    bool shouldMarkRead = false;
    bool shouldMarkReceived = false;

    if (msg.senderId == curr_user_id && msg.receiverId == widget.active_chat_Id) {
      // Add the message as I am the sender
      setState(() {
        messages.add(msg);
        print("===========================A message Content==========================");
        print(msg);
      });
    } else if (msg.senderId == widget.active_chat_Id && msg.receiverId == curr_user_id) {
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
        title: Text(widget.chatName),
      ),
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              itemCount: messages.length,
              itemBuilder: (context, index) {
                final message = messages[index];
                return MessageBubble(message: message,isMe: message.senderId==currId,);
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8.0),
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
                  icon: Icon(Icons.send),
                  onPressed: () => _sendMessage(_controller.text),
                ),

              ],
            ),
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
              "Authorization": "Bearer $accessToken", // Replace $accessToken with your actual access token variable
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
