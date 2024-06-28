import 'package:app/Shared/components/widgets/DropDownButton.dart';
import 'package:app/Shared/components/widgets/multiSelect.dart';
import 'package:app/services/navigation_service.dart';
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
  List<String> _selectedItems = [];

  void _showMultiSelect() async {
    // a list of selectable items
    // these items can be hard-coded or dynamically fetched from a database/API
    final List<String> items = [
      'Type 1',
      'Type 2',
      'Type 3',
    ];
    final List<String>? results = await showDialog(
      context: context,
      builder: (BuildContext context) {
        return MultiSelect(items: items);
      },
    );
    // Update UI
    if (results != null) {
      setState(() {
        _selectedItems = results;
      });
    }
  }

  List<List<String>> choices = [
    ["Type1", "Type2"],
    ["Type1", "Type2"],
    ["Type1", "Type2"],
  ];
  List<String> selectedChoice = [
    "Type1",
    "Type1",
    "Type1",
  ];
  List<String> labels = [
    "Diabetes",
    "Hypertension",
    "Smoking status",
  ];
  final TextEditingController _textController = TextEditingController();
  late NavigationService _navigation;
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    _navigation = NavigationService();

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
                mainAxisAlignment: MainAxisAlignment.center,
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
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 5.0),
                    child: Text(
                      "viruses",
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 16,
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 10,
                  ),
                  ElevatedButton.icon(
                    icon: Icon(
                      Icons.arrow_drop_down,
                      color: Colors.black,
                    ),
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.white,
                      padding:
                          EdgeInsets.symmetric(horizontal: 40, vertical: 15),
                      shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(15),
                          side: BorderSide(color: Color(0xffB8E8F7), width: 2)),
                    ),
                    onPressed: _showMultiSelect,
                    label: const Text(
                      'Select Your virus',
                      style: TextStyle(
                        color: Colors.black,
                      ),
                    ),
                  ),
                  const Divider(
                    height: 30,
                  ),
                  // display selected items
                  Wrap(
                    children: _selectedItems
                        .map((e) => Chip(
                              backgroundColor: Color(0xff45B3EF),
                              label: Text(
                                e,
                                style: TextStyle(color: Colors.white),
                              ),
                            ))
                        .toList(),
                  ),
                  SizedBox(
                    height: 15,
                  ),
                  DropDownButtonSingUp(
                    items: choices[2],
                    selectedItem: selectedChoice[2],
                    label: labels[2],
                  ),
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 5.0),
                    child: Text(
                      textAlign: TextAlign.start,
                      "Kidney failure cause",
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 16,
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 10,
                  ),
                  TextFormField(
                    controller: _textController,
                    minLines: 2,
                    maxLines: 5,
                    keyboardType: TextInputType.multiline,
                    decoration: InputDecoration(
                        hintText: 'Enter your cause',
                        hintStyle: TextStyle(color: Colors.grey),
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(10),
                        )),
                  ),
                  SizedBox(
                    height: 20,
                  ),
                  Center(
                    child: ElevatedButton(
                        style: ElevatedButton.styleFrom(
                            backgroundColor: Color(0xff45B3EF),
                            padding: EdgeInsets.symmetric(
                                horizontal: 40, vertical: 15),
                            shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(20))),
                        child: Text(
                          "Sign up",
                          style: TextStyle(
                              fontSize: 18, fontWeight: FontWeight.bold),
                        ),
                        onPressed: () {
                          _navigation.removeAndNavigateToRoute('/bottomnav');
                        }),
                  ),
                  SizedBox(
                    height: 10,
                  ),
                  Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisSize: MainAxisSize.max,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        "Already have an account?",
                        style: TextStyle(
                            color: Colors.grey.shade600, fontSize: 14),
                      ),
                      TextButton(
                          style: TextButton.styleFrom(),
                          onPressed: () {
                            _navigation.removeAndNavigateToRoute('/login');
                          },
                          child: Text(
                            "sign in",
                            style: TextStyle(color: Color(0xff45B3EF)),
                          )),
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
