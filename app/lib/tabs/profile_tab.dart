import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class ProfileTab extends StatelessWidget {
  const ProfileTab({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {

      return Scaffold(
        backgroundColor: Colors.white,

        appBar: AppBar(
          toolbarHeight: MediaQuery.of(context).size.height*0.15,
          centerTitle: true,
          title: Text('Profile',style: TextStyle(
            fontSize: 14,
            fontWeight: FontWeight.bold,
          ),),
          backgroundColor: Color(0xffB8E8F7),

        ),

      );
  }
}

// ClipRRect(
// child:Image(
// image: AssetImage("assets/images/profile.png"),
// height: 100,
//
// ),
// ),