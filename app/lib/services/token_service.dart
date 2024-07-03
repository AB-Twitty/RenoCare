import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';

// class TokenService {
//   static final TokenService _instance = TokenService._internal();
//
//   factory TokenService() {
//     return _instance;
//   }
//
//   TokenService._internal();
//
//   static const _tokenKey = 'accessToken';
//   static const _responseDataKey = 'responseData';
//   static const _sessionExpiryKey = 'sessionExpiry';
//
//
//   //==============================================================================
//   //==============================================================================
//   //                        Handel Saving Token
//   //==============================================================================
//   //==============================================================================
//   Future<void> saveToken(String token) async {
//     final prefs = await SharedPreferences.getInstance();
//     await prefs.setString(_tokenKey, token);
//   }
//
//   Future<String?> getToken() async {
//     final prefs = await SharedPreferences.getInstance();
//     return prefs.getString(_tokenKey);
//   }
//
//   Future<void> clearToken() async {
//     final prefs = await SharedPreferences.getInstance();
//     await prefs.remove(_tokenKey);
//   }
//
//
//   //==============================================================================
//   //==============================================================================
//   //                        Handel Saving User Data
//   //==============================================================================
//   //==============================================================================
//   Future<void> saveResponseData(Map<String, dynamic> responseData) async {
//     print("========================Eneter Save Response=================");
//     final prefs = await SharedPreferences.getInstance();
//     String jsonString = jsonEncode(responseData);
//     await prefs.setString(_responseDataKey, jsonString);
//     print("========================Save Response=================");
//
//   }
//
//   Future<Map<String, dynamic>?> getResponseData() async {
//     final prefs = await SharedPreferences.getInstance();
//     String? jsonString = prefs.getString(_responseDataKey);
//     if (jsonString != null) {
//       return jsonDecode(jsonString) as Map<String, dynamic>;
//     }
//     return null;
//   }
//
//   Future<void> clearResponseData() async {
//     final prefs = await SharedPreferences.getInstance();
//     await prefs.remove(_responseDataKey);
//   }
//
//
//
//   //==============================================================================
//   //==============================================================================
//   //                        Handel Session Time
//   //==============================================================================
//   //==============================================================================
//
//   Future<void> saveSessionExpiry(DateTime expiry) async {
//     final prefs = await SharedPreferences.getInstance();
//     await prefs.setString(_sessionExpiryKey, expiry.toIso8601String());
//   }
//
//   Future<DateTime?> getSessionExpiry() async {
//     final prefs = await SharedPreferences.getInstance();
//     String? expiryString = prefs.getString(_sessionExpiryKey);
//     if (expiryString != null) {
//       return DateTime.parse(expiryString);
//     }
//     return null;
//   }
//
//   Future<bool> isSessionValid() async {
//     final expiry = await getSessionExpiry();
//     if (expiry != null && expiry.isAfter(DateTime.now())) {
//       return true;
//     }
//     return false;
//   }
//
//   Future<void> clearSession() async {
//     await clearToken();
//     await clearResponseData();
//     final prefs = await SharedPreferences.getInstance();
//     await prefs.remove(_sessionExpiryKey);
//   }
//
//
// }





class LoginDataManager2 {
  // Singleton instance
  static final LoginDataManager2 _instance = LoginDataManager2._internal();
  factory LoginDataManager2() => _instance;
  LoginDataManager2._internal();

  static const _idKey = 'id';

  static const _firstNameKey = 'firstName';
  static const _lastNameKey = 'lastName';
  static const _accessTokenKey = 'accessToken';
  static const _sessionExpiryKey = 'sessionExpiryKey';

  // Save login data to shared preferences
  Future<void> saveLoginData(Map<String, dynamic> responseData, DateTime expiry) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString(_idKey, responseData['data']['id']);
    await prefs.setString(_firstNameKey, responseData['data']['firstName']);
    await prefs.setString(_lastNameKey, responseData['data']['lastName']);
    await prefs.setString(_accessTokenKey, responseData['data']['accessToken']);
    await prefs.setString(_sessionExpiryKey, expiry.toIso8601String());
  }

  // Load login data from shared preferences
  Future<Map<String, String>> loadLoginData() async {
    final prefs = await SharedPreferences.getInstance();
    final id = prefs.getString(_idKey) ?? '';
    final firstName = prefs.getString(_firstNameKey) ?? '';
    final lastName = prefs.getString(_lastNameKey) ?? '';
    final accessToken = prefs.getString(_accessTokenKey) ?? '';
    final sessionExpiry = prefs.getString(_sessionExpiryKey) ?? '';



    return {
      'id': id,
      'firstName': firstName,
      'lastName': lastName,
      'accessToken': accessToken,
      'sessionExpiryKey': sessionExpiry,
    };
  }

  // Check if the session has expired
  Future<bool> isSessionValid() async {
    final prefs = await SharedPreferences.getInstance();
    String? sessionExpiryString = prefs.getString(_sessionExpiryKey);
    if (sessionExpiryString != null) {
      DateTime sessionExpiry = DateTime.parse(sessionExpiryString);
      return DateTime.now().isBefore(sessionExpiry);
    }
    return false;
  }

  // Clear login data from shared preferences
  Future<void> clearLoginData() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove(_idKey);
    await prefs.remove(_firstNameKey);
    await prefs.remove(_lastNameKey);
    await prefs.remove(_accessTokenKey);
    await prefs.remove(_sessionExpiryKey);
  }

  Future<String?> getAccessToken()async{
    final pref=await SharedPreferences.getInstance();
    return pref.getString('accessToken');
  }

  Future<String?> getId()async{
    final pref=await SharedPreferences.getInstance();
    return pref.getString('id');
  }
}