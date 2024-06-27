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
// class CustomTextFormField1 extends StatelessWidget {
//   final Function(String) onSaved;
//   final String regEX;
//   final String hintText;
//   final bool obscureText;
//   final Icon icon;
//   final String textReg;
//   final String textnull;
//   CustomTextFormField1({
//     required this.onSaved,
//     required this.regEX,
//     required this.hintText,
//     required this.obscureText,
//     required this.icon,
//     required this.textReg,
//     required this.textnull,
//   });

//   @override
//   Widget build(BuildContext context) {
//     return TextFormField(
//       onSaved: (newValue) => onSaved(newValue!),
//       cursorColor: Colors.white,
//       style: TextStyle(
//         color: Colors.black,
//       ),
//       obscureText: obscureText,
//       validator: (value) {
//         if (!RegExp(regEX).hasMatch(value!)) {
//           return textReg;

//         }
//         if (value.isEmpty) {
//           return textnull;
//         }
//       },
//       decoration: InputDecoration(
//         fillColor: Color(0xffB8E8F7),
//         filled: true,
//         border: OutlineInputBorder(
//             borderRadius: BorderRadius.circular(18),
//             borderSide: BorderSide.none),
//         hintText: hintText,
//         hintStyle: TextStyle(
//           fontSize: 12,
//         ),
//         prefixIcon: icon,
//       ),
//     );
//   }
// }
//====================================================================================
class CustomTextFormField1 extends StatefulWidget {
  final Function(String) onSaved;
  final String regEX;
  final String hintText;
  final bool obscureText;
  final IconData icon;
  final String textReg;
  final String textnull;
  CustomTextFormField1({
    required this.onSaved,
    required this.regEX,
    required this.hintText,
    required this.obscureText,
    required this.icon,
    required this.textReg,
    required this.textnull,
  });

  @override
  State<CustomTextFormField1> createState() => _CustomTextFormField1State();
}

class _CustomTextFormField1State extends State<CustomTextFormField1> {
  Color labelColor = Colors.black;
  @override
  Widget build(BuildContext context) {
    return TextFormField(
      onSaved: (newValue) => widget.onSaved(newValue!),
      cursorColor: Colors.black,
      cursorHeight: 20,
      obscureText: widget.obscureText,
      validator: (value) {
        if (!RegExp(widget.regEX).hasMatch(value!)) {
          setState(() {
            labelColor = Colors.red;
          });
          return widget.textReg;
        } else if (value.isEmpty) {
          setState(() {
            labelColor = Colors.red;
          });
          return widget.textnull;
        } else {
          setState(() {
            labelColor = Colors.black;
          });
        }
      },
      decoration: InputDecoration(
        errorStyle: TextStyle(
          color: Colors.red,
        ),
        errorBorder: OutlineInputBorder(
          borderRadius: BorderRadius.all(Radius.circular(20)),
          borderSide: BorderSide(
            color: Colors.red,
          ),
        ),
        focusedErrorBorder: OutlineInputBorder(
          borderRadius: BorderRadius.all(Radius.circular(20)),
          borderSide: BorderSide(
            color: Colors.red,
          ),
        ),
        floatingLabelStyle: TextStyle(color: labelColor),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.all(Radius.circular(20)),
          borderSide: BorderSide(
            color: Color(0xffB8E8F7),
            width: 3,
          ),
        ),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.all(Radius.circular(20)),
          borderSide: BorderSide(
            color: Color(0xffB8E8F7),
            width: 3,
          ),
        ),
        labelText: widget.hintText,
        labelStyle: TextStyle(
          color: labelColor,
          fontSize: 12,
        ),
        prefixIcon: Icon(
          widget.icon,
          color: labelColor,
        ),
      ),
    );
  }
}