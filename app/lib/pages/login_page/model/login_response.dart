class LoginResponse {
  final int statusCode;
  final String? meta;
  final String message;
  final bool succeeded;
  final UserData data;
  final List<String>? errors;

  LoginResponse({
    required this.statusCode,
    this.meta,
    required this.message,
    required this.succeeded,
    required this.data,
    this.errors,
  });

  factory LoginResponse.fromJson(Map<String, dynamic> json) {
    return LoginResponse(
      statusCode: json['statusCode'],
      meta: json['meta'],
      message: json['message'],
      succeeded: json['succeeded'],
      data: UserData.fromJson(json['data']),
      errors: json['errors'] != null ? List<String>.from(json['errors']) : null,
    );
  }
}

class UserData {
  final String id;
  final String firstName;
  final String lastName;
  final String accessToken;

  UserData({
    required this.id,
    required this.firstName,
    required this.lastName,
    required this.accessToken,
  });

  factory UserData.fromJson(Map<String, dynamic> json) {
    return UserData(
      id: json['id'],
      firstName: json['firstName'],
      lastName: json['lastName'],
      accessToken: json['accessToken'],
    );
  }
}
