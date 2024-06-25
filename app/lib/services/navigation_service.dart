import 'package:flutter/material.dart';

class NavigationService {
  static GlobalKey<NavigatorState> navigatorKey = GlobalKey<NavigatorState>();

  void removeAndNavigateToRoute(String _route) {
    navigatorKey.currentState?.pushReplacementNamed(_route);
  }
  void removeAndNavigateToRoute2(String route) {
    navigatorKey.currentState?.pushNamedAndRemoveUntil(route, (Route<dynamic> route) => false);
  }
  void navigateToRoute(String _route) {
    navigatorKey.currentState?.pushNamed(_route);
  }

  void navigateToPage(Widget page) {
    navigatorKey.currentState?.push(
      MaterialPageRoute(
        builder: (BuildContext _contex) {
          return page;
        },
      ),
    );
  }

  void goBack(){
    navigatorKey.currentState?.pop();
  }
}
