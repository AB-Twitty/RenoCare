class Message {
  final String senderId;
  final String receiverId;
  final int Id;
  final String message;
  final DateTime sendingTime;

  int? status;

  //final bool isMe;

  final String? fileLink;
  final bool? isFile;

  Message({
    required this.senderId,
    required this.Id,
    required this.receiverId,
    required this.message,
    required this.sendingTime,
    //required this.isMe,
    this.fileLink,
    this.isFile,
    this.status,
  });

  factory Message.fromJson(Map<String, dynamic> json) {
    return Message(
      senderId: json['senderId'],
      Id: json['id'],
      receiverId: json['receiverId'],
      message: json['message'],
      sendingTime: DateTime.parse(json['sendingTime']),
      fileLink: json['fileLink'],
      isFile: json['isFile'],
      status: json['status'],
      //isMe: json['isMe'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'senderId': senderId,
      'id': Id,
      'receiverId': receiverId,
      'message': message,
      'timestamp': sendingTime.toIso8601String(),
      //'isMe': isMe,
      'fileLink': fileLink,
      'isFile': isFile,
      'status': status,
    };
  }

  Message copyWith({
    String? senderId,
    String? receiverId,
    int? Id,
    String? message,
    DateTime? sendingTime,
    int? status,
    String? fileLink,
    bool? isFile,
  }) {
    return Message(
      senderId: senderId ?? this.senderId,
      Id: Id ?? this.Id,
      receiverId: receiverId ?? this.receiverId,
      message: message ?? this.message,
      sendingTime: sendingTime ?? this.sendingTime,
      status: status ?? this.status,
      fileLink: fileLink ?? this.fileLink,
      isFile: isFile ?? this.isFile,
    );
  }
}
