import 'package:flutter/material.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';

import '../../services/notification_service.dart';


final FlutterLocalNotificationsPlugin flutterLocalNotificationsPlugin=FlutterLocalNotificationsPlugin();

class SplashScreen extends StatefulWidget {
  final VoidCallback onInitializationComplete;

  const SplashScreen({
    required Key key,
    required this.onInitializationComplete,
  }) : super(key: key);

  @override
  State<SplashScreen> createState() => _SplashScreenState();
}

class _SplashScreenState extends State<SplashScreen> {
  void initState() {
    super.initState();
    NotificationService.initialize(flutterLocalNotificationsPlugin);
    Future.delayed(Duration(seconds: 3)).then((_) {
      widget.onInitializationComplete();
    });
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData(

        scaffoldBackgroundColor: Color.fromRGBO(255, 255, 255, 1.0),
      ),
      home: Scaffold(
        body: Stack(
          alignment: Alignment.center,
          children: [
            Container(
              width: MediaQuery.of(context).size.width,
              height: MediaQuery.of(context).size.height,
              color: Color.fromRGBO(147, 210, 243, 1.0),
            ),

            Container(
              height: 300,
              width: 300,
              decoration: BoxDecoration(
                shape: BoxShape.circle,

              ),
              child: ClipRRect(
                borderRadius: BorderRadius.circular(200),
                child: Image.asset("assets/images/Logo.PNG",fit: BoxFit.cover,),
              ),
            ),
            SizedBox(
              height: 30,
            ),
          ],
        ),
      ),
    );
  }
}
