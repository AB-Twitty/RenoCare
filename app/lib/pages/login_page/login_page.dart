import 'package:app/Shared/Network/authentication_service.dart';
import 'package:app/pages/login_page/login_cubit.dart';
import 'package:dio/dio.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:google_fonts/google_fonts.dart';

import '../../services/navigation_service.dart';

import 'package:http/http.dart' as http;

import '../../services/token_service.dart';

class LoginPage extends StatefulWidget {
  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  Color emailLabelColor = Colors.black;
  Color passwordLabelColor = Colors.black;
  bool _obsecureText = true;
  late NavigationService _navigation;
  final AuthenticationService auth = AuthenticationService(Dio());
  late double deviceHeight;
  late double deviceWidth;
  bool? is_checked;
  var _formKey = GlobalKey<FormState>();
  var emailController = TextEditingController();
  var passwordController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    deviceHeight = MediaQuery.of(context).size.height;
    deviceWidth = MediaQuery.of(context).size.width;
    _navigation = NavigationService();
    return BlocProvider(
      create: (context) => LoginCubit(),
      child: BlocConsumer<LoginCubit, LoginState>(
        listener: (context, state) {
          if (state is LoginSuccessState) {
            _navigation.removeAndNavigateToRoute2('/bottomnav');
          } else if (state is LoginErrorState) {
            ScaffoldMessenger.of(context).showSnackBar(
              SnackBar(
                  content:
                      Text('Login Failed: The email or password is wrong')),
            );
          }
        },
        builder: (context, state) {
          var cubit = LoginCubit.get(context);
          return GestureDetector(
            onTap: () {
              FocusScopeNode currentFocus = FocusScope.of(context);
              if (!currentFocus.hasPrimaryFocus) {
                currentFocus.unfocus();
              }
            },
            child: Scaffold(
              body: SafeArea(
                child: SingleChildScrollView(
                  physics: BouncingScrollPhysics(),
                  child: Padding(
                    padding: const EdgeInsets.symmetric(
                        horizontal: 30, vertical: 25),
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      crossAxisAlignment: CrossAxisAlignment.center,
                      children: [
                        Image.asset(
                          "assets/images/DoctorsImage.jpg",
                          height: 250,
                        ),
                        const SizedBox(
                          height: 30,
                        ),
                        Form(
                          key: _formKey,
                          child: Column(
                            children: [
                              TextFormField(
                                controller: cubit.emailController,
                                cursorColor: Colors.black,
                                decoration: InputDecoration(
                                  errorStyle: TextStyle(
                                    color: Colors.red,
                                  ),
                                  errorBorder: OutlineInputBorder(
                                    borderRadius:
                                        BorderRadius.all(Radius.circular(20)),
                                    borderSide: BorderSide(
                                      color: Colors.red,
                                    ),
                                  ),
                                  focusedErrorBorder: OutlineInputBorder(
                                    borderRadius:
                                        BorderRadius.all(Radius.circular(20)),
                                    borderSide: BorderSide(
                                      color: Colors.red,
                                    ),
                                  ),
                                  floatingLabelStyle:
                                      TextStyle(color: emailLabelColor),
                                  focusedBorder: OutlineInputBorder(
                                    borderRadius:
                                        BorderRadius.all(Radius.circular(20)),
                                    borderSide: BorderSide(
                                      color: Color(0xffB8E8F7),
                                      width: 3,
                                    ),
                                  ),
                                  enabledBorder: OutlineInputBorder(
                                    borderRadius:
                                        BorderRadius.all(Radius.circular(20)),
                                    borderSide: BorderSide(
                                      color: Color(0xffB8E8F7),
                                      width: 3,
                                    ),
                                  ),
                                  prefixIcon: Icon(
                                    Icons.email,
                                    color: emailLabelColor,
                                  ),
                                  labelText: 'Enter your Email',
                                  labelStyle: TextStyle(
                                    color: emailLabelColor,
                                    fontSize: 12,
                                  ),
                                ),
                                validator: (value) {
                                  if (value == null || value.isEmpty) {
                                    setState(() {
                                      emailLabelColor = Colors.red;
                                    });
                                    return 'Please enter your email';
                                  } else {
                                    setState(() {
                                      emailLabelColor = Colors.black;
                                    });
                                  }
                                  final bool emailValid = RegExp(
                                          r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+")
                                      .hasMatch(value);
                                  if (!emailValid) {
                                    setState(() {
                                      emailLabelColor = Colors.red;
                                    });
                                    return "Please Enter Valid Email";
                                  } else {
                                    setState(() {
                                      emailLabelColor = Colors.black;
                                    });
                                  }
                                  return null;
                                },
                              ),
                              const SizedBox(
                                height: 30,
                              ),
                              TextFormField(
                                obscureText: _obsecureText,
                                controller: cubit.passwordController,
                                decoration: InputDecoration(
                                  prefixIcon: Icon(
                                    Icons.lock,
                                    color: passwordLabelColor,
                                  ),
                                  suffixIcon: GestureDetector(
                                    onTap: () {
                                      setState(() {
                                        _obsecureText = !_obsecureText;
                                      });
                                    },
                                    child: Icon(
                                      _obsecureText
                                          ? Icons.visibility_off
                                          : Icons.visibility,
                                      color: passwordLabelColor,
                                    ),
                                  ),
                                  errorStyle: TextStyle(
                                    color: Colors.red,
                                  ),
                                  errorBorder: OutlineInputBorder(
                                    borderRadius:
                                        BorderRadius.all(Radius.circular(20)),
                                    borderSide: BorderSide(
                                      color: Colors.red,
                                    ),
                                  ),
                                  focusedErrorBorder: OutlineInputBorder(
                                    borderRadius:
                                        BorderRadius.all(Radius.circular(20)),
                                    borderSide: BorderSide(
                                      color: Colors.red,
                                    ),
                                  ),
                                  floatingLabelStyle:
                                      TextStyle(color: passwordLabelColor),
                                  focusedBorder: OutlineInputBorder(
                                    borderRadius:
                                        BorderRadius.all(Radius.circular(20)),
                                    borderSide: BorderSide(
                                      color: Color(
                                        0xffB8E8F7,
                                      ),
                                      width: 3,
                                    ),
                                  ),
                                  enabledBorder: OutlineInputBorder(
                                    borderRadius:
                                        BorderRadius.all(Radius.circular(20)),
                                    borderSide: BorderSide(
                                      color: Color(0xffB8E8F7),
                                      width: 3,
                                    ),
                                  ),
                                  labelText: 'Enter your password',
                                  labelStyle: TextStyle(
                                    color: passwordLabelColor,
                                    fontSize: 12,
                                  ),
                                ),
                                validator: (value) {
                                  if (value == null || value.isEmpty) {
                                    setState(() {
                                      passwordLabelColor = Colors.red;
                                    });
                                    return 'Please enter your password';
                                  } else {
                                    setState(() {
                                      passwordLabelColor = Colors.black;
                                    });
                                  }
                                  return null;
                                },
                              ),
                              const SizedBox(
                                height: 30,
                              ),
                              state is LoginLoadingState
                                  ? Center(
                                      child: CircularProgressIndicator(),
                                    )
                                  : SizedBox(
                                      width: 250,
                                      child: ElevatedButton(
                                        style: ElevatedButton.styleFrom(
                                          shape: RoundedRectangleBorder(
                                              borderRadius:
                                                  BorderRadius.circular(20)),
                                          padding: EdgeInsets.symmetric(
                                            horizontal: 40,
                                            vertical: 17,
                                          ),
                                          backgroundColor: Color(0xff019AED),
                                          textStyle: TextStyle(
                                            color: Colors.white,
                                            fontSize: 20,
                                          ),
                                        ),
                                        onPressed: () async {
                                          if (_formKey.currentState
                                                  ?.validate() ??
                                              false) {
                                            print(cubit.emailController.text);
                                            cubit.login(
                                                cubit.emailController.text,
                                                cubit.passwordController.text,
                                                true);
                                          }
                                        },
                                        child: const Text(
                                          'Login',
                                        ),
                                      ),
                                    ),
                              SizedBox(
                                height: 25,
                              ),
                              Row(
                                children: [
                                  Expanded(
                                    child: Divider(
                                      endIndent: 10,
                                      indent: 10,
                                      height: 2,
                                      color: Colors.black,
                                      thickness: 1.5,
                                    ),
                                  ),
                                  Text(
                                    "OR",
                                    style: TextStyle(
                                        color: Colors.black, fontSize: 12),
                                  ),
                                  Expanded(
                                    child: Divider(
                                      endIndent: 10,
                                      indent: 10,
                                      height: 2,
                                      color: Colors.black,
                                      thickness: 1.5,
                                    ),
                                  ),
                                ],
                              ),
                              SizedBox(
                                height: 10,
                              ),
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  InkWell(
                                    child: Row(
                                      mainAxisAlignment:
                                          MainAxisAlignment.center,
                                      children: [
                                        Container(
                                          child: Image.asset(
                                              "assets/images/google_logo.png"),
                                          width: 50,
                                          height: 50,
                                        ),
                                        Text("Continue with Google")
                                      ],
                                    ),
                                  ),
                                  Row(
                                    crossAxisAlignment:
                                        CrossAxisAlignment.center,
                                    mainAxisSize: MainAxisSize.max,
                                    mainAxisAlignment: MainAxisAlignment.center,
                                    children: [
                                      Text(
                                        "Don't have an account?",
                                        style: TextStyle(
                                            color: Colors.grey.shade600,
                                            fontSize: 14),
                                      ),
                                      TextButton(
                                          style: TextButton.styleFrom(),
                                          onPressed: () {
                                            _navigation
                                                .removeAndNavigateToRoute(
                                                    '/signup');
                                          },
                                          child: Text(
                                            "sign up",
                                            style: TextStyle(
                                                color: Color(0xff45B3EF)),
                                          )),
                                    ],
                                  ),
                                ],
                              ),
                            ],
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              ),
            ),
          );
        },
      ),
    );
  }
}
