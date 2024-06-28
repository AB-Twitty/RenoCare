import 'package:app/Shared/components/widgets/DropDownButton.dart';
import 'package:flutter/material.dart';

class SecondSignUpPage extends StatefulWidget {
  const SecondSignUpPage({super.key});
  @override
  State<StatefulWidget> createState() {
    // TODO: implement createState
    return _SecondSignUpPageState();
  }
}

class _SecondSignUpPageState extends State<SecondSignUpPage> {
  List<List<String>> choices = [
    ["Type1", "Type2"],
    ["Type1", "Type2"],
    ["Type1", "Type2"],
    ["Type1", "Type2"],
  ];
  List<String> selectedChoice = [
    "Type1",
    "Type1",
    "Type1",
    "Type1",
  ];
  List<String> labels = [
    "Diabetes",
    "Hypertension",
    "Viruses",
    "Smoking status",
  ];
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
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
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
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
                ),
                SizedBox(
                  height: 40,
                ),
                Expanded(
                    child: ListView.builder(
                  scrollDirection: Axis.vertical,
                  shrinkWrap: true,
                  itemCount: choices.length,
                  itemBuilder: ((context, index) {
                    return DropDownButtonSingUp(
                      items: choices[index],
                      selectedItem: selectedChoice[index],
                      label: labels[index],
                    );
                  }),
                ))
              ],
            ),
          ),
        ),
      ),
    );
  }
}
