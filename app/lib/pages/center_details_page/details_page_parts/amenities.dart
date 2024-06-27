import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

class AmenitiesPart extends StatelessWidget {
  const AmenitiesPart({super.key});
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    final Color selectedcolor = Color.fromRGBO(60, 152, 203, 1);
    return Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
      Text(
        'Amenities',
        style: TextStyle(
          fontSize: 18,
          fontWeight: FontWeight.bold,
        ),
      ),
      SizedBox(
        height: 16,
      ),
      Wrap(
        spacing: 35,
        runSpacing: 16,
        children: [
          Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              FaIcon(
                FontAwesomeIcons.glassWater,
                color: selectedcolor,
              ),
              SizedBox(
                width: 5,
              ),
              Text('Refreshments'),
            ],
          ),
          Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Icon(
                Icons.wifi,
                color: selectedcolor,
              ),
              SizedBox(
                width: 5,
              ),
              Text('Wifi'),
            ],
          ),
          Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Icon(
                Icons.tv,
                color: selectedcolor,
              ),
              SizedBox(
                width: 5,
              ),
              Text('TV Screens'),
            ],
          ),
          Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Icon(
                Icons.local_parking,
                color: selectedcolor,
              ),
              SizedBox(
                width: 5,
              ),
              Text('Parking'),
            ],
          ),
        ],
      ),
    ]);
  }
}
