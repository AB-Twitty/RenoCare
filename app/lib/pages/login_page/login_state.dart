part of 'login_cubit.dart';

@immutable
abstract class LoginState {}

class LoginInitial extends LoginState {}

class LoginLoadingState extends LoginState {}

class LoginSuccessState extends LoginState {
  final Response response;

  LoginSuccessState({required this.response});
}
class LoginErrorState extends LoginState {
  final String error;

  LoginErrorState({required this.error});
}
