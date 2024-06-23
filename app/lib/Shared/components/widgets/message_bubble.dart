import 'package:flutter/material.dart';
import '../../../pages/chat_module/page/chat_home/model/message_model.dart';

class MessageBubble extends StatelessWidget {
  final Message message;

  MessageBubble({required this.message});

  @override
  Widget build(BuildContext context) {
    final bg = message.isMe ? Colors.lightBlueAccent : Colors.white;
    final align = message.isMe ? CrossAxisAlignment.end : CrossAxisAlignment.start;
    final radius = message.isMe
        ? BorderRadius.only(
      topLeft: Radius.circular(12),
      topRight: Radius.circular(12),
      bottomLeft: Radius.circular(12),
    )
        : BorderRadius.only(
      topLeft: Radius.circular(12),
      topRight: Radius.circular(12),
      bottomRight: Radius.circular(12),
    );

    return Container(
      margin: const EdgeInsets.symmetric(vertical: 10, horizontal: 10),
      child: Column(
        crossAxisAlignment: align,
        children: <Widget>[
          Container(
            decoration: BoxDecoration(
              color: bg,
              borderRadius: radius,
            ),
            padding: const EdgeInsets.all(16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  message.sender,
                  style: TextStyle(
                    fontWeight: FontWeight.bold,
                    color: message.isMe ? Colors.white : Colors.black,
                  ),
                ),
                SizedBox(height: 5),
                if (message.content.isNotEmpty)
                  Text(
                    message.content,
                    style: TextStyle(
                      color: message.isMe ? Colors.white : Colors.black,
                    ),
                  ),
                if (message.fileUrl != null)
                  Padding(
                    padding: const EdgeInsets.only(top: 8.0),
                    child: GestureDetector(
                      onTap: () {
                        // Handle file opening logic here
                      },
                      child: Text(
                        "File: ${message.fileUrl}",
                        style: TextStyle(
                          color: Colors.blue,
                          decoration: TextDecoration.underline,
                        ),
                      ),
                    ),
                  ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
