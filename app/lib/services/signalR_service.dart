import 'package:signalr_core/signalr_core.dart';

class SignalRUtil {
  late final HubConnection _hubConnection;
  var accessToken ="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhNmQ2ZjQ5MS0xOTU3LTRlNzAtOThjNy05OTdlYjBkMzI1NmYiLCJlbWFpbCI6ImFkbWluQGxvY2FsaG9zdC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiU3lzdGVtIEFkbWluIiwianRpIjoiMmFhZWM2YjYtZjVhZi00ZWM4LTkxOTMtNjdkMjU3YTcwOTAyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3MTkxMTMwNzUsImlzcyI6IlJlbm9DYXJlIiwiYXVkIjoiUmVub0NhcmUifQ.lZc30ZhI5enfGB5tlMii8865si4KvUOzW4C4Gz_euuk";
  SignalRUtil() {
    _hubConnection = HubConnectionBuilder()
        .withUrl(
      "https://renocareapi.azurewebsites.net/chat",
      HttpConnectionOptions(

        logging: (level, message) => print(message),
         accessTokenFactory: () async => accessToken,


        transport: HttpTransportType.longPolling,
        // skipNegotiation: true,
      ),
    )
        .withAutomaticReconnect()
        .build();

    // _hubConnection.serverTimeoutInMilliseconds = 1200000;
  }

  Future<void> startConnection() async {
    print("====================================Start Connecting=======================");
    try {
      await _hubConnection.start();
      print("Connection started");
    } catch (e) {
      print("Connection is not in the 'connected' state.");
      print(e.toString());
    }

    if (_hubConnection.state == HubConnectionState.connected) {
      print("=================Connected============");
    } else {
      print("Connection is not in the 'connected' state.");
    }
  }

  void stopConnection() {
    _hubConnection.stop();
  }

  void onMessageReceived(Function(String, String) callback) {
    print("+=======================================Message Received");
    _hubConnection.on("ReceiveMessage", (arguments) {
      final user = arguments?[0] as String;
      final message = arguments?[1] as String;
      callback(user, message);
    });
  }

  void sendMessage(String user, String message) {
    print("+=======================================Message send");
    print("+=======================================Message Content=========");
    print("====================$message=================");
    _hubConnection.invoke("SendMessage", args: <Object>[user, message]);
  }

  void _reconnect() async {
    while (_hubConnection.state != HubConnectionState.connected) {
      try {
        await _hubConnection.start();
        print("Reconnected to SignalR server.");
      } catch (e) {
        print("Reconnection failed: $e. Retrying...");
        await Future.delayed(Duration(seconds: 5));
      }
    }
  }
}
