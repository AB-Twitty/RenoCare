import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:google_maps_flutter/google_maps_flutter.dart';

class MapTest extends StatefulWidget {
  const MapTest({super.key});

  @override
  State<MapTest> createState() => _MapTestState();
}

class _MapTestState extends State<MapTest> {
  static const LatLng _pgoogleplex = LatLng(37.773972, -122.431297);
  @override
  Widget build(BuildContext context) {
    final Color selectedcolor = Color.fromRGBO(60, 152, 203, 1);
    // TODO: implement build
    return Column(
      children: [
        Row(
          children: [
            Text(
              'Location',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.bold,
              ),
            ),
            Spacer(),
            TextButton(
              onPressed: () {},
              child: Text('View Map'),
              style: TextButton.styleFrom(foregroundColor: selectedcolor),
            ),
          ],
        ),
        SizedBox(
          height: 10,
        ),
        Image.asset(
          'assets/images/mapExample.png',
          width: 350,
        )
      ],
    );
  }
}
