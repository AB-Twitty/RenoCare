import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:group_button/group_button.dart';

class RepeatedGroupButton extends StatelessWidget {
  final List<String> content;
  final bool isRadio;
  final Function(List<String>) onSelected;
  final GroupButtonController controller;

  RepeatedGroupButton({
    Key? key,
    required this.content,
    required this.isRadio,
    required this.onSelected,
    required this.controller,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
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
      isRadio: isRadio,
      onSelected: (String value, int index, bool isSelected) {
        List<String> selectedItems =
        controller.selectedIndexes.map((i) => content[i]).toList();
        onSelected(selectedItems);
      },
      controller: controller,
    );
  }
}
