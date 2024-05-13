class LoginService {
  int? statusCode;
  dynamic meta;
  String? message;
  bool? succeded;
  Data? data;
  dynamic errors;

  LoginService({
    this.statusCode,
    this.meta,
    this.message,
    this.succeded,
    this.data,
    this.errors,
  });

  factory LoginService.fromJson(Map<String, dynamic> json) {
    return LoginService(
      statusCode: json['statusCode'],
      meta: json['meta'],
      message: json['message'],
      succeded: json['succeded'],
      data: json['data'] != null ? Data.fromJson(json['data']) : null,
      errors: json['errors'],
    );
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['statusCode'] = this.statusCode;
    data['meta'] = this.meta;
    data['message'] = this.message;
    data['succeded'] = this.succeded;
    if (this.data != null) {
      data['data'] = this.data!.toJson();
    }
    data['errors'] = this.errors;
    return data;
  }
}

class Data {
  String? id;
  String? firstName;
  String? lastName;
  String? accessToken;

  Data({
    this.id,
    this.firstName,
    this.lastName,
    this.accessToken,
  });

  factory Data.fromJson(Map<String, dynamic> json) {
    return Data(
      id: json['id'],
      firstName: json['firstName'],
      lastName: json['lastName'],
      accessToken: json['accessToken'],
    );
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['id'] = this.id;
    data['firstName'] = this.firstName;
    data['lastName'] = this.lastName;
    data['accessToken'] = this.accessToken;
    return data;
  }
}
