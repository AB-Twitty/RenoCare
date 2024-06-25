import 'package:app/services/token_service.dart';
import 'package:flutter/material.dart';

import 'chat_page.dart';
import 'model/chat_model.dart';

class ChatHomePage extends StatelessWidget {
  final loginManager = LoginDataManager();


  final List<Chat> chats = [
    Chat(id: '1', name: 'Mohamed gamal', lastMessage: 'Hey!'),
    Chat(id: '2', name: 'ahmed', lastMessage: 'What\'s up?'),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Chats'),
        actions: [
          IconButton(onPressed: ()async{

            final loadedData = await loginManager.loadLoginData();
            print('User ID: ${loadedData['id']}');
            print('First Name: ${loadedData['firstName']}');
            print('Last Name: ${loadedData['lastName']}');
            print('Access Token: ${loadedData['accessToken']}');

          }, icon:Icon(Icons.add) )
        ],
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
