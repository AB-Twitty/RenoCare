import 'package:flutter/material.dart';

import 'chat_page.dart';
import 'model/chat_model.dart';

class ChatHomePage extends StatelessWidget {

  final List<Chat> chats = [
    Chat(id: '1', name: 'Mohamed gamal', lastMessage: 'Hey!'),
    Chat(id: '2', name: 'ahmed', lastMessage: 'What\'s up?'),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Chats'),
      ),
      body: ListView.builder(
        itemCount: chats.length,
        itemBuilder: (context, index) {
          final chat = chats[index];
          return ListTile(

            leading: Image.asset("assets/images/profile.png",height: 30,),
            title: Text(chat.name),
            subtitle: Text(chat.lastMessage),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => ChatPage(chatId: chat.id, chatName: chat.name),
                ),
              );
            },
          );
        },
      ),
    );
  }
}
