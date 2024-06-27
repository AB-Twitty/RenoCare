import 'package:app/pages/chat_module/page/chat_home/chat_home_page.dart';
import 'package:app/pages/chat_module/page/chat_home/chat_page.dart';
import 'package:app/pages/home_page/home_page.dart';
import 'package:app/pages/login_page/login_page.dart';
import 'package:app/pages/sign_up.dart';
import 'package:app/pages/splash_page/splash_screen.dart';
import 'package:app/services/navigation_service.dart';
import 'package:app/services/session_maneger.dart';
import 'package:app/tabs/appointment.dart';
import 'package:app/tabs/home.dart';
import 'package:app/tabs/profile_tab.dart';
import 'package:bloc/bloc.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';

import 'bloc.dart';
import 'firebase_options.dart';

void main() async {
  Bloc.observer = MyBlocObserver();
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
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
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      theme: ThemeData(
        scaffoldBackgroundColor: Colors.white,
        useMaterial3: false,
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
      },
    );
  }
}
