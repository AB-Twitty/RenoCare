import 'package:flutter/material.dart';

class DropDownButtonSingUp extends StatefulWidget {
  DropDownButtonSingUp({
    super.key,
    required this.items,
    required this.selectedItem,
    required this.label,
  });
  List<String> items;
  String selectedItem;
  String label;
  @override
  State<StatefulWidget> createState() {
    // TODO: implement createState
    return _DropDownButtonSingUpState();
  }
}

class _DropDownButtonSingUpState extends State<DropDownButtonSingUp> {
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 5.0),
          child: Text(
            widget.label,
            style: TextStyle(
              fontWeight: FontWeight.bold,
              fontSize: 16,
            ),
          ),
        ),
        SizedBox(
          height: 10,
        ),
        Container(
          padding: EdgeInsets.only(
            left: 20,
            right: 20,
          ),
          decoration: BoxDecoration(
            border: Border.all(
              color: Color(0xffB8E8F7),
              width: 2,
            ),
            borderRadius: BorderRadius.circular(15),
          ),
          child: DropdownButton(
            borderRadius: BorderRadius.circular(15),
            isExpanded: true,
            dropdownColor: Colors.white,
            value: widget.selectedItem,
            icon: Icon(
              Icons.arrow_drop_down_circle,
            ),
            underline: SizedBox(),
            items: widget.items
                .map(
                  (category) => DropdownMenuItem(
                    value: category,
                    child: Text(
                      category,
                      style: TextStyle(
                        fontSize: 16,
                      ),
                    ),
                  ),
                )
                .toList(),
            onChanged: (value) {
              if (value == null) {
                return;
              }
              setState(() {
                widget.selectedItem = value;
              });
            },
          ),
        ),
        SizedBox(
          height: 15.0,
        ),
      ],
    );
  }
}
