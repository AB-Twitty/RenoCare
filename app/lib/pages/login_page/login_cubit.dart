import 'package:app/services/session_maneger.dart';
import 'package:app/services/signalR_service.dart';
import 'package:bloc/bloc.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart' as not;
import 'package:intl/intl.dart';
import 'package:meta/meta.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../../Shared/Network/authentication_service.dart';
import '../../models/notification_model.dart';
import '../../services/navigation_service.dart';
import '../../services/notification_service.dart' ;
import '../../services/token_service.dart';
import '../chat_module/page/chat_home/model/message_model.dart';

part 'login_state.dart';

class LoginCubit extends Cubit<LoginState> {
  LoginCubit() : super(LoginInitial());

  String currId="";
  static LoginCubit get(context) => BlocProvider.of(context);
  final not.FlutterLocalNotificationsPlugin flutterLocalNotificationsPlugin =
  not.FlutterLocalNotificationsPlugin();
  late NavigationService _navigation;
  final AuthenticationService auth = AuthenticationService(Dio());
  late double deviceHeight;
  late double deviceWidth;
  var _formKey = GlobalKey<FormState>();
  var emailController = TextEditingController();
  var passwordController = TextEditingController();
  final loginManager = LoginDataManager2();
  final SessionService _sessionService=SessionService();

  SignalRUtil signalRUtil=SignalRUtil();

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
       await LoginDataManager2().saveLoginData(response.data, sessionExpiration);

//=====================================For Test ======================

        final loadedData = await loginManager.loadLoginData();
        print('User ID: ${loadedData['id']}');
        print('First Name: ${loadedData['firstName']}');
        print('Last Name: ${loadedData['lastName']}');
        print('Access Token: ${loadedData['accessToken']}');
        print("======================================================");
        print('sessionExpiryKey: ${loadedData['sessionExpiryKey']}');
        await _sessionService.saveToken(loadedData['accessToken']!);

        currId=loadedData['id']!;
        //=======================================================


        await initConnection();

        //========================================================
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


  Future<void> initConnection()async{
    var myHub=await signalRUtil.startConnection();
    myHub.on("ReceiveMessage", _handleReceivedMessage);
    myHub.on("OnNotified", (arguments) {
      NotificationModel notificationModel = NotificationModel.fromJson(arguments![0]);

      NotificationService.showAppointmentNotification(
          title:  notificationModel.title,
          body: notificationModel.body,
          data: {
            'Message': notificationModel.body
          },
          fln: flutterLocalNotificationsPlugin);
    });
  }
  void _handleReceivedMessage(List<Object?>? arguments)async {

    if (arguments == null || arguments.isEmpty) return;

    final Message msg = Message.fromJson(arguments[0] as Map<String, dynamic>);

    if(msg.senderId == currId ){
      return;
    }
    NotificationService.showMessageNotification(
        title: arguments[1]as String ,
        body: msg.message,
        fln: flutterLocalNotificationsPlugin);
    var myHub=await SignalRUtil().startConnection();
    await myHub.invoke("MarkReceived", args: <Object>[msg.Id]);
  }



}

