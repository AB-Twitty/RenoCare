import 'dart:async';

import 'package:app/services/token_service.dart';

import 'navigation_service.dart';

class SessionManager {
  static final SessionManager _instance = SessionManager._internal();

  factory SessionManager() => _instance;

  SessionManager._internal();

  Timer? _timer;

  void startSessionCheck(NavigationService navigationService) {
    _timer = Timer.periodic(Duration(seconds: 1), (timer) async {
      bool isValid = await LoginDataManager2().isSessionValid();
      if (!isValid) {
        await LoginDataManager2().clearLoginData();
        navigationService.removeAndNavigateToRoute2('/login');
      }
    });
  }

  void stopSessionCheck() {
    _timer?.cancel();
  }
}
