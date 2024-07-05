import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:url_launcher/url_launcher.dart';
import 'package:url_launcher/url_launcher_string.dart';
import 'package:flutter_downloader/flutter_downloader.dart';
import 'package:path_provider/path_provider.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:chat_bubbles/chat_bubbles.dart';

import '../model/message_model.dart';

class MessageBubble2 extends StatelessWidget {
  final Message message;
  final bool isMe;

  MessageBubble2({required this.message, required this.isMe});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.all(10.0),
      child: Column(
        crossAxisAlignment:
            isMe ? CrossAxisAlignment.end : CrossAxisAlignment.start,
        children: [
          Material(
            borderRadius: BorderRadius.only(
              topLeft: isMe ? Radius.circular(15.0) : Radius.circular(0),
              topRight: isMe ? Radius.circular(0) : Radius.circular(15.0),
              bottomLeft: Radius.circular(15.0),
              bottomRight: Radius.circular(15.0),
            ),
            elevation: 5.0,
            color: isMe ? Color.fromRGBO(170, 221, 248, 1) : Colors.white,
            child: Padding(
              padding: EdgeInsets.symmetric(vertical: 10.0, horizontal: 20.0),
              child: message.isFile!
                  ? Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          "${message.message}",
                          style: TextStyle(
                            color: Colors.black,
                            fontSize: 15.0,
                          ),
                        ),
                        SizedBox(
                          height: 10,
                        ),
                        ElevatedButton(
                          onPressed: () {
                            _downloadFile(
                                context, message.fileLink, message.message);
                          },
                          child: Text('Download'),
                        ),
                      ],
                    )
                  : Row(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        Text(
                          message.message,
                          style: TextStyle(
                            color: Colors.black,
                            fontSize: 15.0,
                          ),
                        ),
                        SizedBox(
                          width: 6,
                        ),
                        Row(
                          mainAxisSize: MainAxisSize.min,
                          children: [if (isMe) _buildStatusCircle()],
                        ),
                      ],
                    ),
            ),
          ),
          SizedBox(
            height: 15,
          ),
          Text(
            _formatDate(message.sendingTime),
            style: TextStyle(
              fontSize: 12.0,
              color: Colors.black,
            ),
          ),
        ],
      ),
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

  String _formatDate(DateTime dateTime) {
    return DateFormat('EEE MMM dd yyyy - hh:mm a').format(dateTime);
  }

  Widget _buildStatusCircle() {
    return Padding(
      padding: const EdgeInsets.only(left: 8.0),
      child: Stack(
        alignment: Alignment.center,
        children: [
          Container(
            width: 14.0,
            height: 14.0,
            decoration: BoxDecoration(
              shape: BoxShape.circle,
              color: message.status == 3 ? Colors.blue : Colors.transparent,
              border: Border.all(
                color: message.status == 3 ? Colors.blue : Colors.black,
                color: message.status == 3 ? Colors.blue : Colors.black,
              ),
            ),
          ),
          Icon(
            message.status == 3
                ? Icons.done_all
                : message.status == 2
                    ? Icons.done_all
                    : Icons.done,
            color: message.status == 3 ? Colors.white : Colors.black,
            size: 10.0,
          ),
        ],
      ),
    );
  }
}
