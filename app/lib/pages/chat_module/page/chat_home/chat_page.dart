import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';

import '../../../../Shared/components/widgets/message_bubble.dart';
import '../../../../services/firebase_service.dart';
import '../../../../services/media_service.dart';
import '../../../../services/signalR_service.dart';
import 'model/message_model.dart';

class ChatPage extends StatefulWidget {
  final String chatId;
  final String chatName;

  ChatPage({required this.chatId, required this.chatName});

  @override
  _ChatPageState createState() => _ChatPageState();
}

class _ChatPageState extends State<ChatPage> {
  final FilePickerUtil filePickerUtil = FilePickerUtil();
  final List<Message> messages = [
    Message(
        sender: 'mohamed',
        content: 'Hey!',
        timestamp: DateTime.now().subtract(Duration(minutes: 2)),
        isMe: false),
    Message(
        sender: 'You',
        content: 'Hello!',
        timestamp: DateTime.now().subtract(Duration(minutes: 1)),
        isMe: true),
  ];
  final TextEditingController _controller = TextEditingController();
  final FirebaseUtil firebaseUtil = FirebaseUtil();

  final SignalRUtil signalRUtil = SignalRUtil();

  @override
  void initState() {
    print("======================hmada====================");
    super.initState();
     _initilizeSignalR();
  }

  @override
  void dispose() {
    signalRUtil.stopConnection();
    super.dispose();
  }

  void _initilizeSignalR() async {
    print("==============initilize signalR============");
    await signalRUtil.startConnection();
    signalRUtil.onMessageReceived((user, message) {
      setState(() {
        messages.add(Message(
            sender: user,
            content: message,
            timestamp: DateTime.now(),
            isMe: user == 'a6d6f491-1957-4e70-98c7-997eb0d3256f'));
      });
    });
  }

  void _sendMessage(String content, {String? fileUrl}) {
    print("==============================Enter SendMessage Func");
    if (content.isNotEmpty) {
      setState(() {
        // messages.add(
        //   Message(sender: 'You', content: content, timestamp: DateTime.now(), isMe: true),
        // );
        messages.add(
          Message(
            sender: 'a6d6f491-1957-4e70-98c7-997eb0d3256f',
            content: content,
            timestamp: DateTime.now(),
            isMe: true,
            fileUrl: fileUrl,
          ),
        );
        print("+++++++++++++++++++++++++====Message to send");
        signalRUtil.sendMessage('a6d6f491-1957-4e70-98c7-997eb0d3255f', content);
        print("=====================================Message Sent===========================");
      });


      _controller.clear();
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
                return MessageBubble(message: message);
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
    PlatformFile? file = await filePickerUtil.pickFile();

    if (file != null) {
      String? downloadUrl = await firebaseUtil.uploadFile(file);

      if (downloadUrl != null) {
        print('File uploaded: $downloadUrl');
        _sendMessage('', fileUrl: downloadUrl);
        // You can now send a message with the file URL or handle it as needed
      } else {
        print('File upload failed');
      }
    } else {
      // User canceled the picker
      print('File picker canceled');
    }
  }


}
