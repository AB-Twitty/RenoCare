import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:flutter/widgets.dart';
import 'package:get_it/get_it.dart';
import 'package:intl/intl.dart';
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
  final formatter = DateFormat.yMd();
  bool passwordVisiable = false;
  bool passwordVisiable2 = false;
  String? _name;
  String? _email;
  String? _password;
  String? _gender;
  TextEditingController _dateController = TextEditingController();
  late NavigationService _navigation;
  final _registeFormKey = GlobalKey<FormState>();
  var _formKey = GlobalKey<FormState>();
  bool _obsecureText = true;
  Color passwordLabelColor = Colors.black;
  Color confirmPasswordLabelColor = Colors.black;
  String? password = " ";
  String? confirmPassword = " ";
  DateTime? _selectedDate;
  List<String> Gender = ["Male", "Female"];
  String _selectedGender = "Male";
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

  void _presentDatePicker() async {
    final now = DateTime.now();
    final pickedDate = await showDatePicker(
        context: context,
        initialDate: now,
        firstDate: now,
        lastDate: DateTime(2025),
        barrierColor: Colors.white);
    setState(() {
      _selectedDate = pickedDate;
    });
  }

  @override
  Widget build(BuildContext context) {
    _navigation = NavigationService();
    _deviceHeight = MediaQuery.of(context).size.height;
    _deviceWidth = MediaQuery.of(context).size.width;

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
                padding: const EdgeInsets.all(30),
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    Column(
                      children: <Widget>[
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
                    SizedBox(
                      height: 40,
                    ),
                    Form(
                      key: _formKey,
                      child: Column(
                        children: [
                          CustomTextFormField1(
                            onSaved: (_value) {
                              setState(() {
                                _name = _value;
                              });
                            },
                            regEX: r'.{8,}',
                            hintText: "Full Name",
                            obscureText: false,
                            textReg: 'Please Enter Valid Name',
                            textnull: 'Please Enter Your full name',
                            icon: Icons.person,
                          ),
                          const SizedBox(height: 20),
                          CustomTextFormField1(
                            onSaved: (_value) {
                              setState(() {
                                _email = _value;
                              });
                            },
                            regEX:
                            r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+",
                            hintText: "Email",
                            obscureText: false,
                            textReg: "Please Enter Valid Email",
                            textnull: "Please Enter Your Email",
                            icon: Icons.email,
                          ),
                          const SizedBox(height: 20),
                          TextFormField(
                            obscureText: _obsecureText,
                            decoration: InputDecoration(
                              suffixIcon: GestureDetector(
                                onTap: () {
                                  setState(() {
                                    _obsecureText = !_obsecureText;
                                  });
                                },
                                child: Icon(
                                  _obsecureText == true
                                      ? Icons.visibility_off
                                      : Icons.visibility,
                                  color: passwordLabelColor,
                                ),
                              ),
                              prefixIcon: Icon(
                                Icons.lock,
                                color: passwordLabelColor,
                              ),
                              errorStyle: TextStyle(
                                color: Colors.red,
                              ),
                              errorBorder: OutlineInputBorder(
                                borderRadius: BorderRadius.all(Radius.circular(20)),
                                borderSide: BorderSide(
                                  color: Colors.red,
                                ),
                              ),
                              focusedErrorBorder: OutlineInputBorder(
                                borderRadius: BorderRadius.all(Radius.circular(20)),
                                borderSide: BorderSide(
                                  color: Colors.red,
                                ),
                              ),
                              floatingLabelStyle:
                              TextStyle(color: passwordLabelColor),
                              focusedBorder: OutlineInputBorder(
                                borderRadius: BorderRadius.all(Radius.circular(20)),
                                borderSide: BorderSide(
                                  color: Color(
                                    0xffB8E8F7,
                                  ),
                                  width: 3,
                                ),
                              ),
                              enabledBorder: OutlineInputBorder(
                                borderRadius: BorderRadius.all(Radius.circular(20)),
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
                              setState(() {
                                password = value;
                              });
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
                          const SizedBox(height: 20),
                          TextFormField(
                            obscureText: _obsecureText,
                            decoration: InputDecoration(
                              suffixIcon: GestureDetector(
                                onTap: () {
                                  setState(() {
                                    _obsecureText = !_obsecureText;
                                  });
                                },
                                child: Icon(
                                  _obsecureText == true
                                      ? Icons.visibility_off
                                      : Icons.visibility,
                                  color: confirmPasswordLabelColor,
                                ),
                              ),
                              prefixIcon: Icon(
                                Icons.lock,
                                color: confirmPasswordLabelColor,
                              ),
                              errorStyle: TextStyle(
                                color: Colors.red,
                              ),
                              errorBorder: OutlineInputBorder(
                                borderRadius: BorderRadius.all(Radius.circular(20)),
                                borderSide: BorderSide(
                                  color: Colors.red,
                                ),
                              ),
                              focusedErrorBorder: OutlineInputBorder(
                                borderRadius: BorderRadius.all(Radius.circular(20)),
                                borderSide: BorderSide(
                                  color: Colors.red,
                                ),
                              ),
                              floatingLabelStyle:
                              TextStyle(color: passwordLabelColor),
                              focusedBorder: OutlineInputBorder(
                                borderRadius: BorderRadius.all(Radius.circular(20)),
                                borderSide: BorderSide(
                                  color: Color(
                                    0xffB8E8F7,
                                  ),
                                  width: 3,
                                ),
                              ),
                              enabledBorder: OutlineInputBorder(
                                borderRadius: BorderRadius.all(Radius.circular(20)),
                                borderSide: BorderSide(
                                  color: Color(0xffB8E8F7),
                                  width: 3,
                                ),
                              ),
                              labelText: 'Confirm password',
                              labelStyle: TextStyle(
                                color: confirmPasswordLabelColor,
                                fontSize: 12,
                              ),
                            ),
                            validator: (value) {
                              setState(() {
                                confirmPassword = value;
                              });
                              if (confirmPassword != password) {
                                setState(() {
                                  confirmPasswordLabelColor = Colors.red;
                                });
                                return 'Wrong Password';
                              }
                              if (value == null || value.isEmpty) {
                                setState(() {
                                  confirmPasswordLabelColor = Colors.red;
                                });
                                return 'Please confirm your password';
                              } else {
                                setState(() {
                                  confirmPasswordLabelColor = Colors.black;
                                });
                              }
                              return null;
                            },
                          ),
                          const SizedBox(height: 20),
                          Row(
                            children: [
                              Container(
                                padding: EdgeInsets.only(left: 20, right: 20),
                                decoration: BoxDecoration(
                                    border: Border.all(
                                        color: Color(0xffB8E8F7), width: 2),
                                    borderRadius: BorderRadius.circular(15)),
                                child: DropdownButton(
                                  dropdownColor: Colors.white,
                                  value: _selectedGender,
                                  underline: SizedBox(),
                                  items: Gender.map(
                                        (category) => DropdownMenuItem(
                                      value: category,
                                      child: Text(
                                        category,
                                        style: TextStyle(
                                          fontSize: 14,
                                        ),
                                      ),
                                    ),
                                  ).toList(),
                                  onChanged: (value) {
                                    if (value == null) {
                                      return;
                                    }
                                    setState(() {
                                      _selectedGender = value;
                                    });
                                  },
                                ),
                              ),
                              Expanded(
                                child: Row(
                                  mainAxisAlignment: MainAxisAlignment.end,
                                  crossAxisAlignment: CrossAxisAlignment.center,
                                  children: [
                                    Text(
                                      _selectedDate == null
                                          ? 'No date selected'
                                          : formatter.format(_selectedDate!),
                                    ),
                                    IconButton(
                                      onPressed: _presentDatePicker,
                                      icon: const Icon(
                                        Icons.calendar_month,
                                        color: Color.fromRGBO(60, 152, 203, 1),
                                      ),
                                    ),
                                  ],
                                ),
                              ),
                            ],
                          ),
                          SizedBox(
                            height: 20,
                          ),
                          SizedBox(
                            width: 330,
                            child: ElevatedButton(
                                style: ElevatedButton.styleFrom(
                                    padding: EdgeInsets.symmetric(
                                        horizontal: 20, vertical: 15),
                                    shape: RoundedRectangleBorder(
                                        borderRadius: BorderRadius.circular(20))),
                                child: Text(
                                  "Sign up",
                                  style: TextStyle(
                                      fontSize: 18, fontWeight: FontWeight.bold),
                                ),
                                onPressed: () {
                                  _formKey.currentState!.validate();
                                  print(
                                      "===================================================");
                                  print(_name);
                                  print(_gender);
                                  print(
                                      "===================================================");
                                  // _navigation.removeAndNavigateToRoute('/home');
                                }),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
            ),
          )),
    );
  }
}