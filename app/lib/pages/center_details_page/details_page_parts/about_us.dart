import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:readmore/readmore.dart';

class AboutUs extends StatelessWidget {
  final String description;
  final List<String> viruses;

  const AboutUs({super.key, required this.description, required this.viruses});

  @override
  Widget build(BuildContext context) {
    final Color color = Color.fromRGBO(60, 152, 203, 1);
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          'About us',
          style: TextStyle(
            fontSize: 18,
            fontWeight: FontWeight.bold,
          ),
        ),
        SizedBox(
          height: 16,
        ),
        if (viruses.isNotEmpty) ...{
          Row(
            children: [
              Text(
                'Accepting Patients',
                style: TextStyle(
                  fontWeight: FontWeight.bold,
                ),
              ),
              Spacer(),
              ...viruses.map((virus) => Padding(
                padding: const EdgeInsets.symmetric(horizontal: 5.0),
                child: Row(
                  children: [
                    Icon(
                      Icons.check,
                      color: color,
                    ),
                    SizedBox(
                      width: 5,
                    ),
                    Text(virus),
                  ],
                ),
              )),
            ],
          ),
          SizedBox(
            height: 8,
          ),
        },
        ReadMoreText(
          description,
          trimLines: 3,
          textAlign: TextAlign.justify,
          trimMode: TrimMode.Line,
          trimCollapsedText: "read more",
          trimExpandedText: "show less",
          lessStyle: TextStyle(
            fontWeight: FontWeight.bold,
            color: color,
          ),
          moreStyle: TextStyle(fontWeight: FontWeight.bold, color: color),
          style: TextStyle(
            fontSize: 16,
            height: 1.5,
          ),
        )
      ],
    );
  }
}
