import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class Appointment extends StatelessWidget {

  late double deviceHeight;
  late double deviceWidth;


  @override
  Widget build(BuildContext context) {
    deviceHeight = MediaQuery.of(context).size.height;
    deviceWidth = MediaQuery.of(context).size.width;
    return Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.grey,
      ),
    );
    // return DefaultTabController(
    //   length: 3,
    //   child: Column(
    //
    //
    //     children: [
    //       Padding(
    //         padding: const EdgeInsets.only(top: 12,left: 16),
    //         child: Row(
    //           children: [
    //             Text("Appointment",
    //                 style: TextStyle(
    //                   fontSize: 20,
    //                   color: Colors.black
    //                 ),
    //
    //             ),
    //           ],
    //         ),
    //       ),
    //
    //       TabBar(
    //           indicatorColor: Color(0xff3C98CB),
    //           tabs: [
    //             Tab(
    //               icon: Text(
    //                 "Upcoming",
    //                 style: TextStyle(
    //                   color: Colors.black54,
    //                   fontSize: 12,
    //                 ),
    //               ),
    //             ),
    //             Tab(
    //               icon: Text(
    //                 "Completed",
    //                 style: TextStyle(
    //                   color: Colors.black54,
    //                   fontSize: 12,
    //                 ),
    //               ),
    //             ),
    //             Tab(
    //               icon: Text(
    //                 "Cancelled",
    //                 style: TextStyle(
    //                   color: Colors.black54,
    //                   fontSize: 12,
    //                 ),
    //               ),
    //             ),
    //           ],
    //
    //
    //       ),
    //
    //       Expanded(
    //         child: TabBarView(
    //             children: [
    //               // SingleChildScrollView(
    //               //
    //               //   child: Column(
    //               //     mainAxisAlignment: MainAxisAlignment.spaceBetween,
    //               //     children: [
    //               //
    //               //     ],
    //               //   ),
    //               // ),
    //
    //               Container(color: Colors.green,),
    //               Container(color: Colors.black,),
    //               Container(color: Colors.red,),
    //
    //
    //             ]
    //
    //         ),
    //       ),
    //
    //
    //
    //     ],
    //
    //
    //
    //   ),
    // );
  }
}
