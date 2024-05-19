
import 'package:app/Shared/Network/authentication_service.dart';
import 'package:dio/dio.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';

import '../Shared/Network/api_maneger.dart';
import '../services/navigation_service.dart';


import 'dart:convert';
import 'package:http/http.dart' as http;
class LoginPage extends StatefulWidget {



  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  late NavigationService _navigation;
  final AuthenticationService auth=AuthenticationService(Dio());
  late double deviceHeight;
  late double deviceWidth;
  var _formKey =GlobalKey<FormState>();
  var emailController=TextEditingController();
  var passwordController=TextEditingController();
  @override
  Widget build(BuildContext context) {
    deviceHeight = MediaQuery.of(context).size.height;
    deviceWidth = MediaQuery.of(context).size.width;
    _navigation=NavigationService();
    return Scaffold(
      resizeToAvoidBottomInset: false,
      body: Column(
        mainAxisSize: MainAxisSize.max,

        children: [
          Container(
            width: 390,
            height: 280,
            child: Image.asset("assets/images/DoctorsImage.jpg"),
          ),
          Padding(
            padding: EdgeInsets.all(20),
            child: Form(
              key: _formKey,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  Container(
                    height: 47,
                    width: 344,
                    child: TextFormField(
                      controller: emailController,
                      decoration: const InputDecoration(
                        fillColor: Color(0xffB8E8F7),
                        filled: true,
                        focusColor: Color(0xffB8E8F7),
                        focusedBorder: OutlineInputBorder(
                          borderRadius: BorderRadius.all(Radius.circular(16)),
                          borderSide: BorderSide(
                              color: Color(0xffB8E8F7)
                          ),
                        ),
                        enabledBorder: OutlineInputBorder(
                          borderRadius: BorderRadius.all(Radius.circular(16)),
                            borderSide: BorderSide(
                                color: Color(0xffB8E8F7)
                            ),
                        ),
                          labelText: 'Enter your Email',
                          labelStyle: TextStyle(
                          fontSize: 12,

                      ),
                      ),
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return 'Please enter your email';
                        }
                        final bool emailValid =
                        RegExp(r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+")
                            .hasMatch(value);

                        if(!emailValid)
                        {
                          return "Please Enter Valid Email";
                        }
                        return null;
                      },
                    ),
                  ),
                  Container(
                    margin: EdgeInsets.only(top: 20),
                    width: 339,
                    height: 47,
                    child: TextFormField(
                      controller: passwordController,
                      obscureText: true,
                      decoration: const InputDecoration(
                        fillColor: Color(0xffB8E8F7),
                        filled: true,
                        focusColor: Color(0xffB8E8F7),
                        focusedBorder: OutlineInputBorder(
                          borderRadius: BorderRadius.all(Radius.circular(16)),
                          borderSide: BorderSide(
                              color: Color(0xffB8E8F7)
                          ),
                        ),

                        enabledBorder: OutlineInputBorder(
                          borderRadius: BorderRadius.all(Radius.circular(16)),
                          borderSide: BorderSide(
                              color: Color(0xffB8E8F7)
                          ),
                        ),
                          labelText: 'Enter your password',
                          labelStyle: TextStyle(
                            fontSize: 12,
                          ),

                      ),
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return 'Please enter your password';
                        }
                        return null;
                      },
                    ),
                  ),
                  const SizedBox(height: 20),
                  Container(
                    width: 335,
                    height: 57,
                    child: ElevatedButton(
                      style: ElevatedButton.styleFrom(

                        backgroundColor: Color(0xff019AED),
                        textStyle: TextStyle(
                          fontSize: 20,
                          color: Colors.black,
                        ),


                      ),
                      onPressed: () {

                            // print("user loged sucess");
                            // auth.login('admn@localhost.com', '121515151');
                            _navigation.removeAndNavigateToRoute('/home_page');
                      },
                      child: const Text(
                        'Login',
                        style: TextStyle(
                              color: Colors.black,


                        ),
                      ),
                    ),
                  ),

                  SizedBox(height: 20,),
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
                          color: Colors.black,
                          fontSize: 12
                        ),
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
                  SizedBox(height: 10,),

                  Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      InkWell(
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [

                            Container(
                              child: Image.asset("assets/images/google_logo.png"),
                              width: 50,
                              height: 50,
                            ),

                            Text("Continue with Google")
                          ],
                        ),
                      ),
                      Row(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        mainAxisSize: MainAxisSize.max,
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Text(
                              "Don't have an account?",
                            style: TextStyle(
                              color: Colors.grey,
                              fontSize: 12
                            ),

                          ),

                          TextButton(
                              onPressed: () {

                                _navigation.removeAndNavigateToRoute('/signup');
                              },
                              child: Text(
                                "sign up",
                                style: TextStyle(
                                    color: Color(0xff45B3EF)
                                ),
                              )
                          ),
                        ],
                      ),
                    ],
                  ),

                ],


              ),
            ),

          ),
        ],
      ),
    );
  }
}
