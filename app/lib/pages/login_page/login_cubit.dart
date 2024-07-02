import 'package:app/pages/login_page/model/login_response.dart';
import 'package:app/services/session_maneger.dart';
import 'package:bloc/bloc.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:meta/meta.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../Shared/Network/authentication_service.dart';
import '../../services/navigation_service.dart';
import '../../services/token_service.dart';

part 'login_state.dart';

class LoginCubit extends Cubit<LoginState> {
  LoginCubit() : super(LoginInitial());

  static LoginCubit get(context) => BlocProvider.of(context);

  late NavigationService _navigation;
  final AuthenticationService auth = AuthenticationService(Dio());
  late double deviceHeight;
  late double deviceWidth;
  var _formKey = GlobalKey<FormState>();
  var emailController = TextEditingController();
  var passwordController = TextEditingController();
  final loginManager = LoginDataManager2();
  final SessionService _sessionService=SessionService();

  Future<void> login(String email, String password, bool rememberMe) async {
  // await loginManager.clearLoginData();
    emit(LoginLoadingState());
    try {
      final response = await auth.Login(email, password);
      print("=============the Token============");
      print(response.data['accessToken']);
      // Use the full response data as needed
      if(response.statusCode==200)
      {
        final jsonData = response.data;
        SharedPreferences prefs = await SharedPreferences.getInstance();
        await prefs.setString('token', jsonData['data']['accessToken']);

        final sessionExpiration = DateTime.now().add(Duration(seconds: 10));
        //loginManager.saveLoginData(response.data, sessionExpiration);

        LoginDataManager2().saveLoginData(response.data, sessionExpiration);

//=====================================For Test ======================

        final loadedData = await loginManager.loadLoginData();
        print('User ID: ${loadedData['id']}');
        print('First Name: ${loadedData['firstName']}');
        print('Last Name: ${loadedData['lastName']}');
        print('Access Token: ${loadedData['accessToken']}');
        print("======================================================");
        print('sessionExpiryKey: ${loadedData['sessionExpiryKey']}');
        await _sessionService.saveToken(loadedData['accessToken']!);
        //=======================================================
        emit(LoginSuccessState(response: response));
      }

    } catch (e) {
      if (e is DioError) {
        emit(LoginErrorState(error: e.response?.data['message'] ?? e.message));
      } else {
        emit(LoginErrorState(error: 'An unexpected error occurred'));
      }
    }
  }
}
