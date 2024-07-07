import 'dart:convert';
import 'package:app/Shared/components/widgets/DropDownButton.dart';
import 'package:app/Shared/components/widgets/multiSelect.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;

class SecondSignUpPage extends StatefulWidget {
  final Map<String, dynamic> userData;

  const SecondSignUpPage({required this.userData, Key? key}) : super(key: key);

  @override
  _SecondSignUpPageState createState() => _SecondSignUpPageState();
}

class _SecondSignUpPageState extends State<SecondSignUpPage> {
  List<String> _selectedItems = [];
  List<List<String>> choices = [
    [
      "Non-diabetic",
      "Type 2 diabetes",
      "Type 1 diabetes",
      "Gestational diabetes",
      "Monogenic diabetes"
    ],
    [
      "Normal",
      "Elevated",
      "Hypertension Stage 1",
      "Hypertension Stage 2",
      "Hypertensive Crisis"
    ],
    ["Non Smoker", "Former Smoker", "Current Smoker	"],
  ];
  List<String> selectedChoice = ["Non-diabetic", "Normal", "Non Smoker"];
  List<String> labels = ["Diabetes", "Hypertension", "Smoking status"];
  final TextEditingController _textController = TextEditingController();
  void _showErrorDialog(Map<String, dynamic> errors) {
    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          title: Text("Error"),
          content: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: errors.entries.map((entry) {
              return Padding(
                padding: const EdgeInsets.symmetric(vertical: 4.0),
                child: Text('${entry.key}: ${entry.value}'),
              );
            }).toList(),
          ),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: Text("Close"),
            ),
          ],
        );
      },
    );
  }

  void _showMultiSelect() async {
    final List<String> items = [
      'Human Immunodeficiency Virus	',
      'Hepatitis B Virus',
      'Hepatitis C Virus',
    ];
    final List<String>? results = await showDialog(
      context: context,
      builder: (BuildContext context) {
        return MultiSelect(items: items);
      },
    );
    if (results != null) {
      setState(() {
        _selectedItems = results;
      });
    }
  }

  void _signUp() async {
    widget.userData.addAll({
      'diabetesType': choices[0].indexOf(selectedChoice[0]) + 1,
      'hypertensionType': choices[1].indexOf(selectedChoice[1]) + 1,
      'smokingStatus': choices[2].indexOf(selectedChoice[2]) + 1,
      'viruses': _selectedItems.map((e) => choices[2].indexOf(e) + 1).toList(),
      'kidneyFailureCause': _textController.text,
    });

    final response = await http.post(
      Uri.parse('https://renocareapi.azurewebsites.net/Api/V1/Patient/Newcome'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(widget.userData),
    );

    if (response.statusCode == 200) {
      Navigator.pushNamed(context, '/bottomnav');
    } else {
      final Map<String, dynamic> responseBody = jsonDecode(response.body);
      _showErrorDialog(responseBody);
      print('Failed to sign up: ${response.body}');
    }
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
          child: Padding(
            padding: const EdgeInsets.all(30.0),
            child: SingleChildScrollView(
              physics: BouncingScrollPhysics(),
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
                  DropDownButtonSingUp(
                    items: choices[0],
                    selectedItem: selectedChoice[0],
                    label: labels[0],
                  ),
                  DropDownButtonSingUp(
                    items: choices[1],
                    selectedItem: selectedChoice[1],
                    label: labels[1],
                  ),
                  const Padding(
                    padding: EdgeInsets.symmetric(horizontal: 5.0),
                    child: Text(
                      "Viruses",
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 16,
                      ),
                    ),
                  ),
                  const SizedBox(height: 10),
                  ElevatedButton.icon(
                    icon:
                    const Icon(Icons.arrow_drop_down, color: Colors.black),
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.white,
                      padding: const EdgeInsets.symmetric(
                          horizontal: 40, vertical: 15),
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(15),
                          side: const BorderSide(
                              color: Color(0xffB8E8F7), width: 2)),
                    ),
                    onPressed: _showMultiSelect,
                    label: const Text(
                      'Select Your Virus',
                      style: TextStyle(color: Colors.black),
                    ),
                  ),
                  const Divider(height: 30),
                  Wrap(
                    children: _selectedItems
                        .map((e) => Chip(
                      backgroundColor: const Color(0xff45B3EF),
                      label: Text(
                        e,
                        style: const TextStyle(color: Colors.white),
                      ),
                    ))
                        .toList(),
                  ),
                  const SizedBox(height: 15),
                  DropDownButtonSingUp(
                    items: choices[2],
                    selectedItem: selectedChoice[2],
                    label: labels[2],
                  ),
                  const Padding(
                    padding: EdgeInsets.symmetric(horizontal: 5.0),
                    child: Text(
                      "Kidney failure cause",
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 16,
                      ),
                    ),
                  ),
                  const SizedBox(height: 10),
                  TextFormField(
                    controller: _textController,
                    minLines: 2,
                    maxLines: 5,
                    keyboardType: TextInputType.multiline,
                    decoration: InputDecoration(
                      hintText: 'Enter your cause',
                      hintStyle: const TextStyle(color: Colors.grey),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(10),
                      ),
                    ),
                  ),
                  const SizedBox(height: 20),
                  Center(
                    child: ElevatedButton(
                      style: ElevatedButton.styleFrom(
                        backgroundColor: const Color(0xff45B3EF),
                        padding: const EdgeInsets.symmetric(
                            horizontal: 40, vertical: 15),
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(20),
                        ),
                      ),
                      child: const Text(
                        "Sign up",
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      onPressed: _signUp,
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
                        child: const Text(
                          "Sign in",
                          style: TextStyle(color: Color(0xff45B3EF)),
                        ),
                      ),
                    ],
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
