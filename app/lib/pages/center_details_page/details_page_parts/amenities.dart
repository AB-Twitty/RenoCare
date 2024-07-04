import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

class AmenitiesPart extends StatelessWidget {
  final List<String> Amenities;
  const AmenitiesPart({super.key, required this.Amenities});
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
        children: [
          ...Amenities.map((amenity) => Padding(
            padding: const EdgeInsets.only(bottom: 10),
            child: Card(
              color: Colors.grey[200],
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(20),
              ),
              child: Padding(
                padding: const EdgeInsets.symmetric(
                    horizontal: 20, vertical: 10),
                child: Row(
                  mainAxisSize: MainAxisSize.min,
                  children: [
                    FaIcon(
                      FontAwesomeIcons.glassWater,
                      color: selectedcolor,
                    ),
                    SizedBox(
                      width: 10,
                    ),
                    Text(amenity),
                  ],
                ),
              ),
            ),
          )),
        ],
      )

      // Wrap(
      //   spacing: 35,
      //   runSpacing: 16,
      //   children: [
      //     Row(
      //       mainAxisSize: MainAxisSize.min,
      //       children: [
      //         FaIcon(
      //           FontAwesomeIcons.glassWater,
      //           color: selectedcolor,
      //         ),
      //         SizedBox(
      //           width: 5,
      //         ),
      //         Text('Refreshments'),
      //       ],
      //     ),
      //     Row(
      //       mainAxisSize: MainAxisSize.min,
      //       children: [
      //         Icon(
      //           Icons.email,
      //           color: selectedcolor,
      //         ),
      //         SizedBox(
      //           width: 5,
      //         ),
      //         Text('Wifi'),
      //       ],
      //     ),
      //     Row(
      //       mainAxisSize: MainAxisSize.min,
      //       children: [
      //         Icon(
      //           Icons.tv,
      //           color: selectedcolor,
      //         ),
      //         SizedBox(
      //           width: 5,
      //         ),
      //         Text('TV Screens'),
      //       ],
      //     ),
      //     Row(
      //       mainAxisSize: MainAxisSize.min,
      //       children: [
      //         Icon(
      //           Icons.local_parking,
      //           color: selectedcolor,
      //         ),
      //         SizedBox(
      //           width: 5,
      //         ),
      //         Text('Parking'),
      //       ],
      //     ),
      //   ],
      // ),
    ]);
  }
}