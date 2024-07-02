import 'package:flutter/material.dart';

class DropDownButtonFilter extends StatefulWidget {
  DropDownButtonFilter({
    super.key,
    required this.items,
    required this.selectedItem,
  });
  final List<String> items;
  String selectedItem;
  @override
  State<StatefulWidget> createState() {
    // TODO: implement createState
    return _DropDownButtonFilterState();
  }
}

class _DropDownButtonFilterState extends State<DropDownButtonFilter> {
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        Container(
          padding: EdgeInsets.only(
            left: 20,
            right: 20,
          ),
          decoration: BoxDecoration(
            border: Border.all(
              color: Color.fromRGBO(60, 152, 203, 1),
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
              color: Color.fromRGBO(60, 152, 203, 1),
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
