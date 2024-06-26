// import 'dart:convert';
//
// import 'package:app/pages/chat_module/page/chat_home/model/chat_model.dart';
// import 'package:app/pages/chat_module/page/chat_home/model/message_model.dart';
// import 'package:app/services/token_service.dart';
// import 'package:signalr_core/signalr_core.dart';
//
// class SignalRUtil {
//   late final HubConnection _hubConnection;
//   final loginDataManager2=LoginDataManager2();
//
//
//   var accessToken ="";
//   SignalRUtil() {
//
//     _hubConnection = HubConnectionBuilder()
//         .withUrl(
//           "https://renocareapi.azurewebsites.net/chat",
//           HttpConnectionOptions(
//             logging: (level, message) => print(message),
//             accessTokenFactory: () async => accessToken,
//
//             transport: HttpTransportType.longPolling,
//           ),
//         )
//         .withAutomaticReconnect()
//         .build();
//
//     print(accessToken);
//
//     // _hubConnection.serverTimeoutInMilliseconds = 1200000;
//   }
//
//   Future<void>init()async{
//     await loginDataManager2.loadLoginData();
//     accessToken=await loginDataManager2.getAccessToken()??"";
//     print("====================================Access Token From SignalR==========================================");
//     print(accessToken);
//   }
//
//
//   Future<void> startConnection() async {
//     //init();
//     print(
//         "====================================Start Connecting=======================");
//     try {
//       await _hubConnection.start();
//       print("Connection started");
//     } catch (e) {
//       print("Connection is not in the 'connected' state.");
//       print(e.toString());
//     }
//
//     if (_hubConnection.state == HubConnectionState.connected) {
//       print("=================Connected============");
//     } else {
//       print("Connection is not in the 'connected' state.");
//     }
//   }
//
//   void stopConnection() {
//     _hubConnection.stop();
//   }
//
//   Future<void> onMessageReceived(Function(Message) callback)async {
//     String ?id=await loginDataManager2.getId();
//
//     print("============================$id");
//     print("+=======================================Message Received");
//     _hubConnection.on("ReceiveMessage", (args) {
//       if (args != null && args is List && args.isNotEmpty) {
//         var message = Message.fromJson(args[0]); // Assuming msg[0] is the JSON representation of the Message class
//
//         if(message.senderId==id && message.receiverId== )
//       }
//     });
//   }
//
//
//   //Complete
//   void sendMessage(String user, String message) {
//     print("+=======================================Message send");
//     print("+=======================================Message Content=========");
//     print("====================$message=================");
//
//     if(message.isNotEmpty){
//       _hubConnection.invoke("SendMessage", args: <Object>[user, message]);
//     }
//
//   }
//
//   Future<List<Map<String, String>>> loadPreviousMessages() async {
//     try {
//       final result = await _hubConnection.invoke("GetPreviousMessages");
//       final messages = jsonDecode(result) as List;
//       return messages.map((msg) {
//         return {
//           'user': msg['user'] as String,
//           'message': msg['message'] as String,
//         };
//       }).toList();
//     } catch (e) {
//       print("Failed to load previous messages: $e");
//       return [];
//     }
//   }
// }
//
//
