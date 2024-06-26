import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class RepeatedDivider extends StatelessWidget {
  const RepeatedDivider({super.key, required this.text});
  final String text;
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Divider(
          indent: 5,
          endIndent: 5,
          thickness: 2,
        ),
        const SizedBox(
          height: 10,
        ),
        Text(
          text,
          style: const TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
        ),
        const SizedBox(
          height: 16,
        ),
      ],
    );
  }
}
