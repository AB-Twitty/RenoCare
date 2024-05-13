import 'package:flutter/material.dart';

class RoundedButton extends StatelessWidget {
  final String name;
  final double height;
  final double width;
  final Function onPressed;

  RoundedButton(
      {required this.name,
        required this.height,
        required this.width,
        required this.onPressed});

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(height*0.25),
          color: Color(0xff019AED).withOpacity(0.6)
      ),
      height: height,
      width: width,
      child: TextButton(
        onPressed: () => onPressed(),
        child: Text(name,textAlign: TextAlign.center,style: TextStyle(
            fontSize: 20,
            color: Colors.black,
            height: 1.5
        ),),
      ),
    );
  }
}