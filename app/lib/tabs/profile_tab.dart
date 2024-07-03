import 'package:app/Shared/components/widgets/buildUserImage.dart';
import 'package:app/services/signalR_service.dart';
import 'package:app/services/token_service.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../services/navigation_service.dart';

class ProfileTab extends StatefulWidget {
  @override
  State<ProfileTab> createState() => _ProfileTabState();
}

class _ProfileTabState extends State<ProfileTab> {
  LoginDataManager2 loginDataManager2=LoginDataManager2();

  late NavigationService _navigation;

  SignalRUtil signalRUtil=SignalRUtil();



  @override
  Widget build(BuildContext context) {

    Size size = MediaQuery.of(context).size;
    _navigation = NavigationService();
    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: false,
        actions: [
          IconButton(onPressed: ()async{
              signalRUtil.stopConnection();
            loginDataManager2.clearLoginData();
            // delete tokens
            SharedPreferences prefs = await SharedPreferences.getInstance();
            await prefs.remove('token');
            _navigation.removeAndNavigateToRoute('/login');
          }, icon: Icon(Icons.logout,color: Colors.red,))
        ],
        backgroundColor: Colors.transparent,
        elevation: 0,

      ),
        extendBodyBehindAppBar: true,
        backgroundColor: Colors.white,
        // appBar: AppBar(
        //   toolbarHeight: MediaQuery.of(context).size.height*0.15,
        //   centerTitle: true,
        //   title: Text('Profile',style: TextStyle(
        //     fontSize: 14,
        //     fontWeight: FontWeight.bold,
        //   ),),
        //   backgroundColor: Color(0xffB8E8F7),
        //
        // ),
        body: Column(
          crossAxisAlignment: CrossAxisAlignment.center,
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            buildUserImage(size, context),
            // ElevatedButton(
            //     onPressed: () {},
            //     style: ElevatedButton.styleFrom(
            //       backgroundColor: Color(0xffB8E8F7),
            //     ),
            //     child: Text(
            //       "Edit Profile",
            //       style: TextStyle(fontSize: 12, color: Colors.black),
            //     )),

            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
              child: Row(
                children: [
                  Text(
                    "Basic information",
                    style: TextStyle(fontSize: 12),
                  ),
                ],
              ),
            ),

            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12, top: 12),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text("Name"),
                  Text(
                    "mohamed gamal",
                    style: TextStyle(color: Colors.grey, fontSize: 12),
                  )
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12, top: 8),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text("Email"),
                  Text(
                    "mohamed@gmail.com",
                    style: TextStyle(color: Colors.grey, fontSize: 12),
                  )
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12, top: 8),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text("Address"),
                  Text(
                    "Giza",
                    style: TextStyle(color: Colors.grey, fontSize: 12),
                  )
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12, top: 8),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text("Phone"),
                  Text(
                    "01111111111",
                    style: TextStyle(color: Colors.grey, fontSize: 12),
                  )
                ],
              ),
            ),

            //==========================================================
            //======================Features============================
            //==========================================================
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 14),
              child: Row(
                children: [
                  Text(
                    "Features",
                    style: TextStyle(fontSize: 12),
                  ),
                ],
              ),
            ),

            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Row(
                    children: [
                      Icon(Icons.date_range_outlined),
                      SizedBox(
                        width: 6,
                      ),
                      Text("Previous Sessions"),
                    ],
                  ),
                  IconButton(
                      onPressed: () {},
                      icon: Icon(
                        Icons.arrow_forward_ios_sharp,
                        size: 20,
                      ))
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Row(
                    children: [
                      Icon(Icons.date_range_outlined),
                      SizedBox(
                        width: 6,
                      ),
                      Text("Upcoming Sessions"),
                    ],
                  ),
                  IconButton(
                      onPressed: () {},
                      icon: Icon(
                        Icons.arrow_forward_ios_sharp,
                        size: 20,
                      ))
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Row(
                    children: [
                      Icon(Icons.file_download_outlined),
                      SizedBox(
                        width: 6,
                      ),
                      Text("Export Your information"),
                    ],
                  ),
                  IconButton(
                      onPressed: () {},
                      icon: Icon(
                        Icons.arrow_forward_ios_sharp,
                        size: 20,
                      ))
                ],
              ),
            ),

            //==========================================================
            //======================Preferences============================
            //==========================================================
            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12, top: 8),
              child: Row(
                children: [
                  Text(
                    "Preferences",
                    style: TextStyle(fontSize: 12),
                  ),
                ],
              ),
            ),

            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Row(
                    children: [
                      Icon(Icons.dark_mode_outlined),
                      SizedBox(
                        width: 6,
                      ),
                      Text("Darkmode"),
                    ],
                  ),
                  IconButton(
                      onPressed: () {},
                      icon: Icon(
                        Icons.arrow_forward_ios_sharp,
                        size: 20,
                      ))
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(left: 12, right: 12),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Row(
                    children: [
                      Icon(Icons.wifi),
                      SizedBox(
                        width: 6,
                      ),
                      Text("Only Download via wifi"),
                    ],
                  ),
                  IconButton(
                      onPressed: () {},
                      icon: Icon(
                        Icons.arrow_forward_ios_sharp,
                        size: 20,
                      ))
                ],
              ),
            ),
          ],
        ));
  }
}

// ClipRRect(
// child:Image(
// image: AssetImage("assets/images/profile.png"),
// height: 100,
//
// ),
// ),