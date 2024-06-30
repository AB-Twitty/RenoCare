class Chat {
  final String id;
  final String name;
   String lastMessage;
   int unreadMsgCount ;
   DateTime? lastMessageTime;

  Chat({
    required this.id,
    required this.name,
    required this.lastMessage,
    this.unreadMsgCount = 0,
    this.lastMessageTime
  });



  factory Chat.fromJson(Map<String,dynamic>json)
  {

    return Chat(
        id: json['userId'],
        name: json['name'],
        unreadMsgCount: json['unreadMsgCount'],
        lastMessage:json['lastMsg']['message'],
      lastMessageTime: json['lastMsg'] != null ? DateTime.parse(json['lastMsg']['sendingTime']) : null,
    );
  }

}
