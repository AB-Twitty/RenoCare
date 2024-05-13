class UserModel{

  String? name;
  String? email;
  String? phone;
  String? token;
  UserModel({
   required this.name,
   required this.email,
   required this.phone,
   required this.token
  });
  // factory usermodel.fromjson(jsondata) {
  //   return usermodel(
  //     name: jsondata['name'],
  //     email: jsondata['email'],
  //     phone: jsondata['phone'],
  //     image: jsondata['image'],
  //     token: jsondata['token'],
  //   );

  UserModel.fromjson({required Map<String, dynamic> data}) {
    name = data['name'];
    email = data['email'];
    phone = data['phone'];
    token = data['token'];
  }

  Map<String, dynamic> tojson() {
    return {
      'name': name,
      'email': email,
      'phone': phone,
      'token': token,
    };
  }
}