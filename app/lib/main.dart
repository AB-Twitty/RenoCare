import 'package:app/pages/home_page.dart';
import 'package:app/pages/login_page.dart';
import 'package:app/pages/splash_screen.dart';
import 'package:app/services/navigation_service.dart';
import 'package:flutter/material.dart';

void main() {
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
        backgroundColor: Colors.white,
      ),
      debugShowCheckedModeBanner: false ,
      home: LoginPage(),
      navigatorKey: NavigationService.navigatorKey,
      initialRoute: '/login',

      routes: {
        '/login':(context)=>LoginPage(),
        '/home':(context)=>HomePage(),

      },
    );
  }



}