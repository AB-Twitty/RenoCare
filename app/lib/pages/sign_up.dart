import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:get_it/get_it.dart';
import '../Shared/components/widgets/custom_input_fields.dart';
import '../Shared/components/widgets/rounded_button.dart';
import '../services/navigation_service.dart';


class SignUp extends StatefulWidget {

  const SignUp({Key? key}) : super(key: key);

  @override
  State<SignUp> createState() => _SignUpState();
}

class _SignUpState extends State<SignUp> {
  late double _deviceHeight;
  late double _deviceWidth;

  bool passwordVisiable = false;
  bool passwordVisiable2 = false;
  String? _name;
  String? _email;
  String? _password;
  String? _gender;
  TextEditingController _dateController = TextEditingController();
  late NavigationService _navigation;
  final _registeFormKey = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
    passwordVisiable = true;
    passwordVisiable2 = true;
    _dateController.text = "1/12/2002";
  }

  void dispose() {
    _dateController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    _navigation = NavigationService();
    _deviceHeight = MediaQuery.of(context).size.height;
    _deviceWidth = MediaQuery.of(context).size.width;

    return Scaffold(
        body: SingleChildScrollView(
      child: Container(
        padding: const EdgeInsets.symmetric(horizontal: 10),
        height: MediaQuery.of(context).size.height - 80,
        width: double.infinity,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Column(
              children: <Widget>[
                const SizedBox(height: 60.0),
                const Text(
                  "Sign up",
                  style: TextStyle(
                      fontSize: 30,
                      fontWeight: FontWeight.bold,
                      color: Color(0xff45B3EF)),
                ),
                const SizedBox(
                  height: 20,
                ),
                Text(
                  "Create your account",
                  style: TextStyle(
                    fontSize: 15,
                    color: Colors.black,
                  ),
                )
              ],
            ),
            Column(
              children: [
                Container(
                    height: 47,
                    width: 344,
                    child: CustomTextFormField1(
                      onSaved: (_value) {
                          setState(() {
                            _name = _value;
                          });

                      },
                      regEX: r'.{8,}',
                      hintText: "Full Name",
                      obscureText: false,
                      icon: Icon(Icons.person),
                    )
                ),
                const SizedBox(height: 20),
                Container(
                    height: 47,
                    width: 344,
                    child: CustomTextFormField1(
                      onSaved: (_value) {
                        setState(() {
                          _email=_value;
                        });
                      },
                      regEX:
                          r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+",
                      hintText: "Email",
                      obscureText: false,
                      icon: Icon(Icons.email),
                    )),
                const SizedBox(height: 20),
                Container(
                  height: 47,
                  width: 344,
                  child: TextField(

                    decoration: InputDecoration(
                      hintText: "Password",
                      hintStyle: TextStyle(
                        fontSize: 12,
                      ),
                      border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(18),
                          borderSide: BorderSide.none),
                      fillColor: Color(0xff45B3EF).withOpacity(0.4),
                      filled: true,
                      prefixIcon: IconButton(
                        icon: Icon(!passwordVisiable
                            ? Icons.visibility
                            : Icons.visibility_off),
                        onPressed: () {
                          setState(() {
                            passwordVisiable = !passwordVisiable;
                          });
                        },
                      ),
                    ),
                    obscureText: passwordVisiable,
                  ),
                ),
                const SizedBox(height: 20),
                Container(
                  height: 47,
                  width: 344,
                  child: TextField(
                    decoration: InputDecoration(
                      hintText: "Password",
                      hintStyle: TextStyle(
                        fontSize: 12,
                      ),
                      border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(18),
                          borderSide: BorderSide.none),
                      fillColor: Color(0xff45B3EF).withOpacity(0.4),
                      filled: true,
                      prefixIcon: IconButton(
                        icon: Icon(!passwordVisiable2
                            ? Icons.visibility
                            : Icons.visibility_off),
                        onPressed: () {
                          setState(() {
                            passwordVisiable2 = !passwordVisiable2;
                          });
                        },
                      ),
                    ),
                    obscureText: passwordVisiable2,
                  ),
                ),
                const SizedBox(height: 20),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    Expanded(
                      child: Padding(
                        padding: const EdgeInsets.only(left: 20,right: 50),
                        child: DropdownButtonFormField<String>(
                          value: _gender,
                          items: ['Male', 'Female'].map((String value) {
                            return DropdownMenuItem<String>(
                              value: value,
                              child: Text(value),
                            );
                          }).toList(),
                          onChanged: (newValue) {
                            setState(() {
                              _gender = newValue;
                            });
                          },
                          decoration: InputDecoration(
                              labelText: 'Gender', border: InputBorder.none),
                        ),
                      ),
                    ),


                    // ToDo { add date picker }
                    Expanded(
                      child: Container(

                        padding: EdgeInsets.only(right: 20,left: 50),
                        height: 47,

                        child: Text(
                          "Date"
                        ),
                      ),
                    ),
                  ],
                ),
                SizedBox(height: 20,),
                Container(
                  width: 335,
                  height: 57,
                  child: RoundedButton(
                      name: "SignUp",
                      height: _deviceHeight * 0.075,
                      width: _deviceWidth * 0.80,
                      onPressed:(){

                        print("===================================================");
                        print(_name);
                        print(_gender);
                        print("===================================================");

                        _navigation.removeAndNavigateToRoute('/home');
                      }
                  ),
                ),
                Row(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  mainAxisSize: MainAxisSize.max,
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Text(
                      "Already have an account?",
                      style: TextStyle(
                          color: Colors.grey,
                          fontSize: 12
                      ),

                    ),

                    TextButton(
                        onPressed: () {

                          _navigation.removeAndNavigateToRoute('/login');
                        },
                        child: Text(
                          "Login",
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
    ));
  }
}
