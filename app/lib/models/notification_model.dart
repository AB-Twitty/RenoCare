class NotificationModel {
  final String userId;
  final String date;
  final String title;
  final String body;

  NotificationModel({
    required this.userId,
    required this.date,
    required this.title,
    required this.body,
  });

  factory NotificationModel.fromJson(Map<String, dynamic> json) {
    return NotificationModel(
        userId: json['userId'],
        date: json['formattedDate'],
        title: json['title'],
        body: json['body']);
  }
}
