import 'package:animated_splash_screen/animated_splash_screen.dart';
import 'package:flutter/material.dart';
import 'package:lottie/lottie.dart';

import '../login_page/login_page.dart';

class SplashScreen2 extends StatelessWidget {
  final VoidCallback onInitializationComplete;

  const SplashScreen2({
    required Key key,
    required this.onInitializationComplete,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      home: AnimatedSplashScreen(
        splash: Expanded(
          child: Lottie.asset("assets/animation/Flow 2.json"),
        ),
        nextScreen: LoginPage(),
        duration: 2250,
        backgroundColor: Colors.white,
        splashIconSize: MediaQuery.of(context).size.width*MediaQuery.of(context).size.height,

      ),
    );
  }
}
