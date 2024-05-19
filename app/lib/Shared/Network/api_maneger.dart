
import 'dart:convert';
import 'package:http/http.dart' as http;

import '../../models/login_service.dart';
class ApiManager{


  static Future<LoginService> loginUser(String email,String password)async{
    final url=Uri.parse('http://localhost:6982/Api/V1/Login');

    final body=jsonEncode({
      'email':email,
      'password':password,
      'rememberMe':true
    });

    try {
      final response = await http.post(
          url,
          body: body

      );
      if (response.statusCode == 200) {
        final jsonResponse = json.decode(response.body);
        print("====================================");
        print(jsonResponse);
        return LoginService.fromJson(jsonResponse);
      } else {
        print('==================================================\n===============================');

        throw Exception('Failed to load login response');
      }
    }catch(e)
    {
      print(e.toString());
    }
    return LoginService();

  }


 static Future<void> login(String email, String password, bool rememberMe) async {
    final url = Uri.parse('http://192.168.43.6:6982/Api/V1/Login');
    try {
      final response = await http.post(
        url,
        body: json.encode({
          'email': email,
          'password': password,
          'rememberMe': rememberMe,
        }),
      );

      if (response.statusCode == 200) {
        // If the server returns an OK response, parse the JSON.
        print('===================================');
        print("A7a I am tired");
        print('===================================');

      } else {
        // If the server did not return a 200 OK response,
        // then throw an exception.
        print('===================================');
        print(response.statusCode);
        print(response.body);
        print("Fuck Api's");
        print('===================================');
        throw Exception('Failed to load login');
      }
    } catch (e) {
      // For any errors, print them or handle them appropriately.
      print(e.toString());
      return null;
    }
  }

}