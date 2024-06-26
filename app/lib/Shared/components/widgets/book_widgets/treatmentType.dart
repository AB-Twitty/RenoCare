import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:group_button/group_button.dart';

class TreatmentType extends StatefulWidget {
  const TreatmentType({super.key});
  @override
  State<StatefulWidget> createState() {
    // TODO: implement createState
    return _TreatmentTypeState();
  }
}

class _TreatmentTypeState extends State<TreatmentType> {
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return GroupButton(
      options: GroupButtonOptions(
          mainGroupAlignment: MainGroupAlignment.center,
          crossGroupAlignment: CrossGroupAlignment.center,
          spacing: 0,
          buttonWidth: 220,
          buttonHeight: 50,
          selectedBorderColor: Colors.black54,
          unselectedBorderColor: Colors.black54,
          selectedColor: Color.fromRGBO(60, 152, 203, 1),
          selectedTextStyle: TextStyle(
            fontSize: 18,
            fontWeight: FontWeight.bold,
          ),
          unselectedTextStyle: TextStyle(
            color: Colors.black,
            fontSize: 18,
            fontWeight: FontWeight.bold,
          )),
      buttons: ["HD", "HDF"],
    );
  }
}
