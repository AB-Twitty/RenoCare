import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:group_button/group_button.dart';

class TreatmentType extends StatefulWidget {
  final List<String> types;
  final Function(int) onSelectionChanged;

  TreatmentType(
      {super.key, required this.types, required this.onSelectionChanged});

  @override
  State<StatefulWidget> createState() {
    return _TreatmentTypeState();
  }
}

class _TreatmentTypeState extends State<TreatmentType> {
  int _selectedIndices = 0;

  @override
  Widget build(BuildContext context) {
    return Center(
      child: GroupButton(
        options: GroupButtonOptions(
          spacing: 0,
          buttonWidth: 160,
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
          ),
        ),
        buttons: widget.types,
        onSelected: (String text, int index, bool isSelected) {
          setState(() {
            if (isSelected) {
              _selectedIndices = index;
            }
            int value = 0;
            if (_selectedIndices == 0) {
              value = 1;
            } else if (_selectedIndices == 1) {
              value = 2;
            }
            widget.onSelectionChanged(value);
          });
        },
      ),
    );
  }
}
