class Message {
  final String sender;
  final String content;
  final DateTime timestamp;
  final bool isMe;
  final String? fileUrl;

  Message({
    required this.sender,
    required this.content,
    required this.timestamp,
    required this.isMe,
    this.fileUrl,
  });
}
