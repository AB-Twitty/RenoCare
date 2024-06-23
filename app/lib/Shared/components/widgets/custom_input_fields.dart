import 'package:flutter/material.dart';

class CustomTextFormField extends StatelessWidget {
  final Function(String) onSaved;
  final String regEX;
  final String hintText;
  final bool obscureText;

  CustomTextFormField(
      {required this.onSaved,
        required this.regEX,
        required this.hintText,
        required this.obscureText});

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      onSaved: (newValue) => onSaved(newValue!),
      cursorColor: Colors.white,
      style: TextStyle(
        color: Colors.black,
      ),
      obscureText: obscureText,
      validator: (value) {
        return RegExp(regEX).hasMatch(value!) ? null : 'Enter Valid value';
      },
      decoration: InputDecoration(
          fillColor: Color(0xffB8E8F7),
          filled: true,
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10.0),
            borderSide: BorderSide.none,
          ),
          hintText: hintText,
          hintStyle: TextStyle(color: Colors.white54)),
    );
  }
}

//====================================================================================
class CustomTextFormField1 extends StatelessWidget {
  final Function(String) onSaved;
  final String regEX;
  final String hintText;
  final bool obscureText;
  final Icon icon;

  CustomTextFormField1(
      {required this.onSaved,
        required this.regEX,
        required this.hintText,
        required this.obscureText,
        required this.icon
      });

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      onSaved: (newValue) => onSaved(newValue!),
      cursorColor: Colors.white,
      style: TextStyle(
        color: Colors.black,
      ),
      obscureText: obscureText,
      validator: (value) {
        return RegExp(regEX).hasMatch(value!) ? null : 'Enter Valid value';
      },
      decoration: InputDecoration(
        fillColor: Color(0xffB8E8F7),
        filled: true,
        border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(18),
            borderSide: BorderSide.none),
        hintText: hintText,
        hintStyle: TextStyle(
          fontSize: 12,
        ),
        prefixIcon: icon,

      ),

    );
  }
}