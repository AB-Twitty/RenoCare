import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class TimePart extends StatefulWidget {
  const TimePart({super.key, required this.timetext});
  final List<String> timetext;
  @override
  State<TimePart> createState() => _TimePartState();
}

class _TimePartState extends State<TimePart> {
  int? selectedCardIndex;
  Color selectedcolor = Color.fromRGBO(60, 152, 203, 1);

  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Wrap(
      spacing: 16.0,
      runSpacing: 16.0,
      children: List.generate(widget.timetext.length, (index) {
        return InkWell(
          onTap: () {
            setState(() {
              selectedCardIndex = index;
            });
          },
          child: Card(
            shape:
            RoundedRectangleBorder(borderRadius: BorderRadius.circular(10)),
            color:
            selectedCardIndex == index ? selectedcolor : Colors.grey[100],
            child: Padding(
              padding: const EdgeInsets.all(10.0),
              child: Text(
                widget.timetext[index],
                style: TextStyle(
                  color:
                  selectedCardIndex == index ? Colors.white : Colors.black,
                ),
              ),
            ),
          ),
        );
      }),
    );
  }
}
