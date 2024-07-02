
import 'package:flutter/material.dart';

Stack buildUserImage(Size size,context) {
  return Stack(
    children: [
      Container(
        width: size.width,
        height: 180,
        color: Colors.transparent,
        padding: EdgeInsets.only(bottom: 150 / 3.5),
        child: Container(
          width: size.width,
          height: 140,
          decoration: BoxDecoration(

            color: Color(0xffB8E8F7)
          ),
        ),
      ),

      // * 1 backbutton (Row widget)
      Row(

        children: [
          IconButton(
            color: Colors.white,
            onPressed: () {
              Navigator.pop(context);
            },
            icon: Icon(Icons.arrow_back,color: Colors.black,),
          ),

        ],
      ),

      // * user profile image
      Positioned(
        top:90,
        left: size.width / 2.8,
        child: Container(
          width: 100,
          height: 90,
          decoration: BoxDecoration(
              color: Colors.white,
              border: Border.all(color: Colors.white, width: 3),
              shape: BoxShape.circle),
          child: CircleAvatar(
            backgroundColor: Colors.transparent,
            backgroundImage:AssetImage("assets/images/profile2.jpeg")  ),
        ),
      ),
    ],
  );
}






// Stack(
// children: [
// Align(
// alignment: Alignment.topCenter,
// child: Container(
// height: 200,
// color: Color(0xffB8E8F7),
//
// child: Center(
// child: Padding(
// padding: const EdgeInsets.all(8.0),
// child: Row(
//
// children: [
// IconButton(
// onPressed: () {
// _navigation.goBack();
// },
// icon: Icon(
// Icons.arrow_back,
// color: Colors.black,
// )
//
//
// ),
// Spacer(),
// // Text('Profile',style: TextStyle(
// //    fontSize: 14,
// //    fontWeight: FontWeight.bold,
// //  ),
// // ),
// Spacer(),
// Icon(Icons.notifications)
// ],
// ),
// ),
// ),
// ),
// ),
// Positioned(
// top: 130,
// left: 0.0,
// right: 0.0,
//
// child:Align(
// alignment: Alignment.center,
// child: Container(
// padding: EdgeInsets.all(2),
// decoration: BoxDecoration(
// border: Border.all(width: 3,color: Colors.white),
// color: Colors.white,
// shape: BoxShape.circle
// ),
// child: CircleAvatar(
// radius: 50.5,
// backgroundImage:  AssetImage("assets/images/profile.png"),
// ),
// ),
// )
// ),
//
//
// ],
// ),
