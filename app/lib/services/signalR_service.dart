import 'dart:convert';

import 'package:app/pages/chat_module/page/chat_home/model/chat_model.dart';
import 'package:app/pages/chat_module/page/chat_home/model/message_model.dart';
import 'package:app/services/token_service.dart';
import 'package:signalr_core/signalr_core.dart';

class SignalRUtil {
   HubConnection? _hubConnection;
  final loginDataManager2=LoginDataManager2();


  var accessToken ="";


  Future<void>init()async{
    await loginDataManager2.loadLoginData();
    accessToken=await loginDataManager2.getAccessToken()??"";
    print("====================================Access Token From SignalR==========================================");
    print(accessToken);
  }


  Future<HubConnection> startConnection() async {

    init();
    print(
        "====================================Start Connecting=======================");
    try {

      if(_hubConnection!=null){
        print("=========================================Already Initialized ====================");
        return _hubConnection!;
      }
      print("=========================================Initializind ====================");

      _hubConnection = HubConnectionBuilder()
          .withUrl(
        "https://renocareapi.azurewebsites.net/chat",
        HttpConnectionOptions(
          logging: (level, message) => print(message),
          accessTokenFactory: () async => accessToken,
          transport: HttpTransportType.longPolling,
        ),
      )
          .withAutomaticReconnect()
          .build();

      await _hubConnection!.start();
    } catch (e) {
      print(" Error establishing signalR connection");
      _reconnect();
    }
    return _hubConnection!;
  }

  void stopConnection() {
    if(_hubConnection==null)
      {
        print("Stopping but Null");
        return;
      }
    _hubConnection!.stop();
  }

  void _reconnect() async {
    while (_hubConnection!.state != HubConnectionState.connected) {
      try {
        await _hubConnection!.start();
        print("Reconnected to SignalR server.");
      } catch (e) {
        print("Reconnection failed: $e. Retrying...");
        await Future.delayed(Duration(seconds: 5));
      }
    }
  }
}


