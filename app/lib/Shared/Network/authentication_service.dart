import 'package:dio/dio.dart';

class AuthenticationService {
  Dio _dio;

  AuthenticationService(this._dio);

  Future<void> login(String email, String password) async {
    final url = 'https://renocareapi.azurewebsites.net/Api/V1/Login';
    final requestBody = {
      "email": email,
      "password": password,
      "rememberMe": true
    };

    try {
      final response = await _dio.post(url, data: requestBody);
      if (response.statusCode == 200 && response.data['succeded']) {
        // Handle successful login
        print('Logged in Successfully');
        // You can now access the user's details and accessToken from response.data
      } else {
        // Handle error, the login was not successful
        print('Login failed');
      }
    } on DioError catch (e) {
      // Handle DioError, which may occur due to request cancellation, timeout, or an error from Dio
      print('DioError: ${e.message}');
    } catch (e) {
      // Handle any other type of error
      print('Error: ${e.toString()}');
    }
  }
}
