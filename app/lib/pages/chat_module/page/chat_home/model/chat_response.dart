import 'message_model.dart';

class ChatResponse {
  final int statusCode;
  final dynamic meta;
  final String message;
  final bool succeded;
  final List<Contact> data;
  final dynamic errors;

  ChatResponse({
    required this.statusCode,
    this.meta,
    required this.message,
    required this.succeded,
    required this.data,
    this.errors,
  });

  factory ChatResponse.fromJson(Map<String, dynamic> json) {
    return ChatResponse(
      statusCode: json['statusCode'],
      meta: json['meta'],
      message: json['message'],
      succeded: json['succeded'],
      data: (json['data'] as List).map((i) => Contact.fromJson(i)).toList(),
      errors: json['errors'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'statusCode': statusCode,
      'meta': meta,
      'message': message,
      'succeded': succeded,
      'data': data.map((e) => e.toJson()).toList(),
      'errors': errors,
    };
  }
}

class Contact {
   String name;
   String userId;
  int contactId;
   Message lastMsg;
   int unreadMsgCount;

  Contact({
    required this.name,
    required this.userId,
    required this.contactId,
    required this.lastMsg,
    required this.unreadMsgCount,
  });

  factory Contact.fromJson(Map<String, dynamic> json) {
    return Contact(
      name: json['name'],
      userId: json['userId'],
      contactId: json['contactId'],
      lastMsg: Message.fromJson(json['lastMsg']),
      unreadMsgCount: json['unreadMsgCount'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'userId': userId,
      'contactId': contactId,
      'lastMsg': lastMsg.toJson(),
      'unreadMsgCount': unreadMsgCount,
    };
  }
}
