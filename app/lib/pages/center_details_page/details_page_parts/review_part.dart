import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'dart:math';
import 'package:intl/intl.dart';

class ReviewPart extends StatelessWidget {
  const ReviewPart({
    super.key,
    required this.numOfStars,
    required this.name,
    required this.review,
    required this.creationDate, // Add this line
    required this.isEnd,
    required this.isStart,
  });
  final double numOfStars;
  final String name;
  final String review;
  final String creationDate; // Add this line
  final bool isEnd;
  final bool isStart;

  @override
  Widget build(BuildContext context) {
    final Color selectedColor = Color.fromRGBO(60, 152, 203, 1);
    final formatter = DateFormat.yMd().add_jm();

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(
          children: [
            Container(
              padding: EdgeInsets.all(11),
              decoration: BoxDecoration(
                shape: BoxShape.circle,
                color: selectedColor,
              ),
              child: Icon(
                Icons.person_outline,
                color: Colors.white,
              ),
            ),
            SizedBox(
              width: 10,
            ),
            Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  name,
                  style: TextStyle(
                    fontSize: 18,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                SizedBox(
                  height: 3,
                ),
                Row(
                  children: [
                    if (numOfStars == numOfStars.floor()) ...[
                      for (int i = 0; i < numOfStars.floor(); i++)
                        Icon(
                          Icons.star,
                          color: selectedColor,
                        ),
                      for (int i = 0; i < 5 - numOfStars.floor(); i++)
                        Icon(
                          Icons.star_border,
                          color: selectedColor,
                        )
                    ] else ...[
                      for (int i = 0; i < numOfStars.floor(); i++)
                        Icon(
                          Icons.star,
                          color: selectedColor,
                        ),
                      Icon(
                        Icons.star_half,
                        color: selectedColor,
                      ),
                      for (int i = 0; i < 5 - numOfStars.floor() - 1; i++)
                        Icon(
                          Icons.star_border,
                          color: selectedColor,
                        ),
                    ],
                    SizedBox(
                      width: 5,
                    ),
                    Text(
                      formatter.format(
                          DateTime.parse(creationDate)), // Change this line
                    )
                  ],
                ),
              ],
            ),
          ],
        ),
        SizedBox(
          height: 16,
        ),
        Text(
          review,
          style: TextStyle(
            height: 1.5,
          ),
        ),
        if (!isEnd) ...[
          SizedBox(
            height: 10,
          ),
          Divider(
            thickness: 2,
            endIndent: 5,
            indent: 5,
          ),
          SizedBox(
            height: 10,
          ),
        ]
      ],
    );
  }
}
