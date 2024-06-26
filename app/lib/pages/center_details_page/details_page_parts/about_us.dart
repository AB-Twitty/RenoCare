import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:readmore/readmore.dart';

class AboutUs extends StatelessWidget {
  const AboutUs({super.key});
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    final Color color = Color.fromRGBO(60, 152, 203, 1);
    final String content =
        "At Diaverum, we offer our patients consistently high standards of care in every one of our clinics worldwide. Our care excellence delivers superior medical outcomes, based on a highly standardised system of clinical governance and practice  ";
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
        Row(
          children: [
            Text(
              'Accepting Patients',
              style: TextStyle(
                fontWeight: FontWeight.bold,
              ),
            ),
            Spacer(),
            Icon(
              Icons.check,
              color: color,
            ),
            SizedBox(
              width: 5,
            ),
            const Text(
              'HV',
            ),
            SizedBox(
              width: 5,
            ),
            Icon(
              Icons.check,
              color: color,
            ),
            SizedBox(
              width: 5,
            ),
            const Text(
              'HBV',
            ),
            SizedBox(
              width: 5,
            ),
            Icon(
              Icons.check,
              color: color,
            ),
            SizedBox(
              width: 5,
            ),
            const Text(
              'HCV',
            ),
          ],
        ),
        SizedBox(
          height: 8,
        ),
        ReadMoreText(
          content,
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
