class Chat {
  final String id;
  final String name;
   String lastMessage;

  Chat({
    required this.id,
    required this.name,
    required this.lastMessage,
  });



  factory Chat.fromJson(Map<String,dynamic>json)
  {

    return Chat(
        id: json['userId'],
        name: json['name'],
        lastMessage:json['lastMsg']['message'],
    );

  }

}
