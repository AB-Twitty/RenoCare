
import 'package:app/pages/login_page/model/login_response.dart';
import 'package:bloc/bloc.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:meta/meta.dart';

import '../../Shared/Network/authentication_service.dart';
import '../../services/navigation_service.dart';
import '../../services/token_service.dart';

part 'login_state.dart';

class LoginCubit extends Cubit<LoginState> {
  LoginCubit() : super(LoginInitial());

  static LoginCubit get(context) => BlocProvider.of(context);

  late NavigationService _navigation;
  final AuthenticationService auth=AuthenticationService(Dio());
  late double deviceHeight;
  late double deviceWidth;
  var _formKey =GlobalKey<FormState>();
  var emailController=TextEditingController();
  var passwordController=TextEditingController();



  void Login(String email,String pass,bool rememberMe)
  {
    emit(LoginLoadingState());

    try{
      auth.login(email, pass);
      emit(LoginSuccessState());
    }catch(e)
    {
      emit(LoginErrorState(e.toString()));
    }



  }



}
