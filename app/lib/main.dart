import 'package:app/Shared/components/widgets/bootmnavigationbar.dart';
import 'package:app/pages/book_page/book_page.dart';
import 'package:app/pages/center_details_page/center_details/details.dart';
import 'package:app/pages/chat_module/page/chat_home/chat_home_page.dart';
import 'package:app/pages/chat_module/page/chat_home/chat_page.dart';
import 'package:app/pages/chat_module/page/chat_home/model/message_model.dart';
import 'package:app/pages/home_page/home_page.dart';
import 'package:app/pages/login_page/login_page.dart';
import 'package:app/pages/sign_up.dart';
import 'package:app/pages/splash_page/splash_screen.dart';
import 'package:app/services/navigation_service.dart';
import 'package:app/services/notification_service.dart';
import 'package:app/services/session_maneger.dart';
import 'package:app/services/signalR_service.dart';
import 'package:app/tabs/appointment.dart';

import 'package:app/tabs/home.dart';
import 'package:app/tabs/profile_tab.dart';
import 'package:bloc/bloc.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';
import 'package:flutter_downloader/flutter_downloader.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart' as not;

import 'bloc.dart';
import 'firebase_options.dart';


void main() async {
  Bloc.observer = MyBlocObserver();
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );

  await FlutterDownloader.initialize(
    debug: true,
  );
  runApp(
    SplashScreen(
      key: UniqueKey(),
      onInitializationComplete: () {
        runApp(
          MyApp(),
        );
      },
    ),
  );
}

class MyApp extends StatelessWidget {
    final not.FlutterLocalNotificationsPlugin flutterLocalNotificationsPlugin =
        not.FlutterLocalNotificationsPlugin();
  final SignalRUtil signalRUtil=SignalRUtil();

  Future<void> SignalR_init()async{

    var myHub= await signalRUtil.startConnection();
    myHub.on("ReceiveMessage", _handleReceivedMessage);
  }
  @override
  Widget build(BuildContext context) {

    SignalR_init();
    return MaterialApp(
      theme: ThemeData(
        useMaterial3: false,
        scaffoldBackgroundColor: Colors.white,
        //backgroundColor: Colors.white,
      ),
      debugShowCheckedModeBanner: false,
      home: LoginPage(),
      navigatorKey: NavigationService.navigatorKey,
      initialRoute: '/login',
      routes: {
        '/login': (context) => LoginPage(),
        '/home_page': (context) => HomePage(),
        '/signup': (context) => SignUp(),
        '/profile': (context) => ProfileTab(),
        '/appointment': (context) => Appointment(),
        '/home': (context) => Home(),
        '/chatHomePage': (context) => ChatHomePage(),
        '/bottomnav': (context) => TabsScreen(),

      },
    );
  }



}

void _handleReceivedMessage(List<Object?>? arguments) {
  if (arguments == null || arguments.isEmpty) return;

  final Message msg = Message.fromJson(arguments[0] as Map<String, dynamic>);


    NotificationService.showMessageNotification(
                 title: arguments[1]as String ,
                 body: msg.message,
                 fln: flutterLocalNotificationsPlugin);
}

