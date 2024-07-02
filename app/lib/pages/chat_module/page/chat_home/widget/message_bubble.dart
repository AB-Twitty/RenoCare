

import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:url_launcher/url_launcher_string.dart';
import 'package:flutter_downloader/flutter_downloader.dart';
import 'package:path_provider/path_provider.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:chat_bubbles/chat_bubbles.dart';

import '../model/message_model.dart';

class MessageBubble extends StatelessWidget {
  final Message message;
  final bool isMe;


  MessageBubble({required this.message, required this.isMe});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 5.0, horizontal: 10.0),
      child: Column(
        crossAxisAlignment: isMe ? CrossAxisAlignment.end : CrossAxisAlignment.start,
        children: [
          BubbleSpecialThree(

            delivered: message.status==2 && isMe,
            seen: message.status==3&& isMe,
            sent: message.status==1&& isMe,
            text: message.message,
            isSender: isMe,

            color: isMe ? Color(0xff025144) : Colors.grey[300]!,
            tail: true,
            textStyle: TextStyle(
              color: isMe ? Colors.white : Colors.black,
              fontSize: 16,
            ),
          ),
          if (message.isFile == true) _buildFileMessage(context),
          SizedBox(height: 5),
          Text(
            message.sendingTime.toString(),
            style: TextStyle(
              color:   Colors.black54,

              fontSize: 10,
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildFileMessage(BuildContext context) {
    final fileName = message.fileLink ?? "Unknown File";
    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      children: [
        // Text(
        //   'File Name: $fileName',
        //   style: TextStyle(
        //     fontWeight: FontWeight.bold,
        //     color: isMe ? Colors.white : Colors.black,
        //   ),
        // ),
        // SizedBox(height: 8),
        GestureDetector(
          onTap: () {
            _downloadFile(context, message.fileLink, fileName);
          },
          child: Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Text(
                'Download',
                style: TextStyle(
                  fontSize: 14,
                  color: Colors.blue,
                  decoration: TextDecoration.underline,
                ),
              ),
              Icon(Icons.download_rounded, size: 34),
            ],
          ),
        ),
      ],
    );
  }

  void _downloadFile(BuildContext context, String? url, String fileName) async {
    if (url != null) {
      final status = await Permission.storage.request();
      if (status.isGranted) {
        final externalDir = await getExternalStorageDirectory();
        final taskId = await FlutterDownloader.enqueue(
          url: url,
          savedDir: externalDir!.path,
          fileName: fileName,
          showNotification: true,
          openFileFromNotification: true,
        );
        print('Download started with taskId: $taskId');
      } else {
        print('Permission denied to access storage');
      }
    } else {
      print('Invalid URL');
    }
  }
}
