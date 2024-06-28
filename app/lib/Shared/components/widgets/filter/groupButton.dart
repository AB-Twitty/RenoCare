import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:group_button/group_button.dart';

class RepeatedGroupButton extends StatelessWidget {
  RepeatedGroupButton({super.key, required this.content});
  List<String> content;
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return GroupButton(
      options: GroupButtonOptions(
        mainGroupAlignment: MainGroupAlignment.start,
        crossGroupAlignment: CrossGroupAlignment.start,
        spacing: 15,
        textPadding: const EdgeInsets.symmetric(horizontal: 20),
        buttonHeight: 40,
        selectedBorderColor: Colors.black54,
        unselectedBorderColor: Colors.black54,
        borderRadius: BorderRadius.circular(20),
        selectedColor: Color.fromRGBO(60, 152, 203, 1),
        unselectedTextStyle: TextStyle(
          color: Colors.grey[600],
        ),
      ),
      buttons: content,
    );
  }
}