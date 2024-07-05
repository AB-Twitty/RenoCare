import 'package:flutter/material.dart';

class Pro extends StatelessWidget {
  const Pro({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: [
          Stack(
            alignment: Alignment.center,
            children: [
              Container(
                width: double.infinity,
                height: 200,
                decoration:  BoxDecoration(
                  color: Color(0xff019AED).withOpacity(0.8),
                  borderRadius: BorderRadius.only(bottomLeft: Radius.circular(125),bottomRight: Radius.circular(125))
                ),
              ),

              Positioned(
                bottom: -50, // Half of the image height (100 / 2)
                left: MediaQuery.of(context).size.width / 2 - 50, // Half of the image width (100 / 2)
                child: Image.asset(
                  'assets/images/profile2.jpeg', // Replace with your image URL
                  width: 100,
                  height: 100,
                ),
              ),
            ],


          ),
        ],

      ),



    );
  }
}

class Customshap extends CustomClipper<Path> {
  @override
  Path getClip(Size size) {
    double height = size.height;
    double width = size.width;

    final path = Path();
    path.moveTo(0, 0);
    path.lineTo(0, height - 20);
    path.quadraticBezierTo(width / 2, height, width, height - 50);

    path.close();
    return path;
  }

  @override
  bool shouldReclip(covariant CustomClipper<Path> oldClipper) {
    // TODO: implement shouldReclip
    return true;
  }
}
