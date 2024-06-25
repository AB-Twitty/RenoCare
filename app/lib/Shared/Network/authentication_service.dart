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

  Future<Response> Login(String email, String password) async {
    try {
      final response = await _dio.post('https://renocareapi.azurewebsites.net/Api/V1/Login', data: {
        'email': email,
        'password': password,
        'rememberMe':true,
      });

      if (response.statusCode == 200) {
        return response; // Return the entire response
      } else {
        throw DioError(
          requestOptions: response.requestOptions,
          response: response,
          type: DioErrorType.badResponse,
          error: 'Invalid credentials',
        );
      }
    } catch (e) {
      if (e is DioError) {
        throw e; // Rethrow Dio errors to be handled by the cubit
      } else {
        throw Exception('Unexpected error');
      }
    }
  }

}
