import 'package:app/models/notification_model.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';

class NotificationService {
  static Future initialize(
      FlutterLocalNotificationsPlugin flutterLocalNotificationsPlugin) async {
    var androidInitialize =
        new AndroidInitializationSettings('mipmap/ic_launcher');

    var initializationsSettings =
        new InitializationSettings(android: androidInitialize);

    await flutterLocalNotificationsPlugin.initialize(initializationsSettings);
  }

  static Future showMessageNotification({
        var id = 0,
      required String title,
      required String body,
      var payload,
      required FlutterLocalNotificationsPlugin fln}) async{


    AndroidNotificationDetails androidNotificationDetails =
    new AndroidNotificationDetails(
        'you_can_name_it_whatever',
      'channel_name',

      playSound: true,
      importance: Importance.max,
      priority: Priority.high,
    );


    var not=NotificationDetails(android: androidNotificationDetails);
    await fln.show(


        0,
        title,
        body,
        not,

    );

  }


  static Future showAppointmentNotification({
    var id = 0,
    required String title,
    required String body,
    required Map<String, dynamic> data,
    var payload,
    required FlutterLocalNotificationsPlugin fln}) async{


    AndroidNotificationDetails androidNotificationDetails =
    new AndroidNotificationDetails(
      'you_can_name_it_whatever',
      'channel_name',

      playSound: true,
      importance: Importance.max,
      priority: Priority.high,

      styleInformation: BigTextStyleInformation(
        body,
        contentTitle: title,
        htmlFormatContent: true,
        htmlFormatContentTitle: true,
      ),
    );


    var not=NotificationDetails(android: androidNotificationDetails);
    await fln.show(
      0,
      title,
      body,
      not,
      payload: data.toString(),
    );

  }
}
