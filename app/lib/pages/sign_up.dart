import 'package:app/Shared/components/widgets/custom_input_fields.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'second_SignUp_Page.dart';

class SignUp extends StatefulWidget {
  const SignUp({Key? key}) : super(key: key);

  @override
  State<SignUp> createState() => _SignUpState();
}

class _SignUpState extends State<SignUp> {
  final _formKey = GlobalKey<FormState>();
  final formatter = DateFormat("yyyy-MM-dd");
  String? _firstName;
  String? _secondName;
  String? _email;
  String? _password;
  String? _gender;
  DateTime? _selectedDate;
  List<String> Gender = ["Male", "Female"];
  String _selectedGender = "Male";
  bool _obsecureText = true;
  Color passwordLabelColor = Colors.black;
  Color confirmPasswordLabelColor = Colors.black;
  TextEditingController _dateController = TextEditingController();

  @override
  void dispose() {
    _dateController.dispose();
    super.dispose();
  }

  void _presentDatePicker() async {
    final now = DateTime.now();
    final pickedDate = await showDatePicker(
        context: context,
        initialDate: now,
        firstDate: DateTime(1944),
        lastDate: now,
        barrierColor: Colors.white);
    setState(() {
      _selectedDate = pickedDate;
      _dateController.text = formatter.format(pickedDate!);
    });
  }

  @override
  Widget build(BuildContext context) {
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
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  const Text(
                    "Sign up",
                    style: TextStyle(
                        fontSize: 30,
                        fontWeight: FontWeight.bold,
                        color: Color(0xff45B3EF)),
                  ),
                  const SizedBox(height: 20),
                  Text(
                    "Create your account",
                    style: TextStyle(fontSize: 15, color: Colors.black),
                  ),
                  const SizedBox(height: 40),
                  Form(
                    key: _formKey,
                    child: Column(
                      children: [
                        CustomTextFormField1(
                          onSaved: (_value) => _firstName = _value,
                          regEX: r'[a-zA-Z]',
                          hintText: "First Name",
                          obscureText: false,
                          textReg: 'Please Enter Valid Name',
                          textnull: 'Please Enter Your first name',
                          icon: Icons.person,
                        ),
                        const SizedBox(height: 20),
                        CustomTextFormField1(
                          onSaved: (_value) => _secondName = _value,
                          regEX: r'[a-zA-Z]',
                          hintText: "Last Name",
                          obscureText: false,
                          textReg: 'Please Enter Valid Name',
                          textnull: 'Please Enter Your last name',
                          icon: Icons.person,
                        ),
                        const SizedBox(height: 20),
                        CustomTextFormField1(
                          onSaved: (_value) => _email = _value,
                          regEX:
                          r"^[a-zA-Z0-9.a-zA-Z0-9.!#$%&'*+-/=?^_{|}~]+@[a-zA-Z0-9]+\.[a-zA-Z]+",
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
                              _password = value;
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
                            labelText: 'Confirm password',
                            labelStyle: TextStyle(
                              color: confirmPasswordLabelColor,
                              fontSize: 12,
                            ),
                          ),
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              setState(() {
                                confirmPasswordLabelColor = Colors.red;
                              });
                              return 'Please confirm your password';
                            } else if (value != _password) {
                              setState(() {
                                confirmPasswordLabelColor = Colors.red;
                              });
                              return 'Passwords do not match';
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
                              padding: EdgeInsets.symmetric(horizontal: 20),
                              decoration: BoxDecoration(
                                  border: Border.all(
                                      color: Color(0xffB8E8F7), width: 2),
                                  borderRadius: BorderRadius.circular(15)),
                              child: DropdownButton(
                                borderRadius: BorderRadius.circular(15),
                                dropdownColor: Colors.white,
                                value: _selectedGender,
                                underline: SizedBox(),
                                items: Gender.map(
                                      (category) => DropdownMenuItem(
                                    value: category,
                                    child: Text(
                                      category,
                                      style: TextStyle(fontSize: 14),
                                    ),
                                  ),
                                ).toList(),
                                onChanged: (value) {
                                  if (value == null) return;
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
                        const SizedBox(height: 20),
                        SizedBox(
                          width: 330,
                          child: ElevatedButton(
                            style: ElevatedButton.styleFrom(
                              padding: EdgeInsets.symmetric(
                                  horizontal: 20, vertical: 15),
                              shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(20),
                              ),
                            ),
                            child: Text(
                              "Next",
                              style: TextStyle(
                                  fontSize: 18, fontWeight: FontWeight.bold),
                            ),
                            onPressed: () {
                              if (_formKey.currentState!.validate()) {
                                _formKey.currentState!.save();
                                Map<String, dynamic> userData = {
                                  'firstName': _firstName,
                                  'lastName': _secondName,
                                  'email': _email,
                                  'password': _password,
                                  'gender': _selectedGender == 'Male' ? 1 : 2,
                                  'birthDate': _selectedDate != null
                                      ? formatter.format(_selectedDate!)
                                      : null,
                                };
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                    builder: (ctx) =>
                                        SecondSignUpPage(userData: userData),
                                  ),
                                );
                              }
                            },
                          ),
                        ),
                        const SizedBox(height: 10),
                        Row(
                          mainAxisAlignment: MainAxisAlignment.center,
                          children: [
                            Text(
                              "Already have an account?",
                              style: TextStyle(
                                  color: Colors.grey.shade600, fontSize: 14),
                            ),
                            TextButton(
                              onPressed: () {
                                Navigator.pushNamed(context, '/login');
                              },
                              child: Text(
                                "Sign in",
                                style: TextStyle(color: Color(0xff45B3EF)),
                              ),
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
  }
}
