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
    required this.isend,
    required this.istsart,
  });
  final double numOfStars;
  final String name;
  final String review;
  final bool isend;
  final bool istsart;
  @override
  Widget build(BuildContext context) {
    final Color selectedcolor = Color.fromRGBO(60, 152, 203, 1);
    final formatter = DateFormat.yMMMEd();
    // TODO: implement build
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(
          children: [
            Container(
              padding: EdgeInsets.all(11),
              decoration: BoxDecoration(
                shape: BoxShape.circle,
                color: selectedcolor,
              ),
              child: Icon(
                Icons.person_outline,
                color: Colors.white,
              ),
            ),
            SizedBox(
              width: 5,
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
                          color: selectedcolor,
                        ),
                      for (int i = 0; i < 5 - numOfStars.floor(); i++)
                        Icon(
                          Icons.star_border,
                          color: selectedcolor,
                        )
                    ] else ...[
                      for (int i = 0; i < numOfStars.floor(); i++)
                        Icon(
                          Icons.star,
                          color: selectedcolor,
                        ),
                      Icon(
                        Icons.star_half,
                        color: selectedcolor,
                      ),
                      for (int i = 0; i < 5 - numOfStars.floor() - 1; i++)
                        Icon(
                          Icons.star_border,
                          color: selectedcolor,
                        ),
                    ],
                    Text(
                      formatter.format(DateTime.now()),
                    )
                  ],
                ),
              ],
            ),
            Spacer(),
            Card(
              shape: istsart
                  ? RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(20),
                side: BorderSide(color: selectedcolor, width: 3),
              )
                  : RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(20),
              ),
              child: Padding(
                padding: const EdgeInsets.symmetric(
                  horizontal: 14,
                  vertical: 8,
                ),
                child: Row(
                  children: [
                    Icon(
                      Icons.thumb_up,
                      color: selectedcolor,
                    ),
                    SizedBox(
                      width: 5,
                    ),
                    Text('55'),
                  ],
                ),
              ),
            )
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
        if (!isend) ...[
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
