import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../../../services/navigation_service.dart';

class CenterCard extends StatelessWidget {
  final NavigationService _navigation=NavigationService();
  @override
  Widget build(BuildContext context) {
    return InkWell(
      
      onTap: () {
        _navigation.navigateToRoute('/detailsPage');
        
      },
      child: Container(
        
        margin: EdgeInsets.all(12),
        width: MediaQuery.of(context).size.width * 0.95,
        height: MediaQuery.of(context).size.height * 0.40,
        decoration: BoxDecoration(
          color: Colors.transparent,
          border: Border.all(color: Colors.grey,width: 0.6),
          borderRadius: BorderRadius.only(topRight: Radius.circular(32),topLeft: Radius.circular(32))
        ),
        child: Column(
          children: [
            Container(
              decoration: BoxDecoration(
      
              ),
              child: ClipRRect(
                borderRadius: BorderRadius.only(topRight: Radius.circular(32),topLeft: Radius.circular(32)),
                child: Image(
                  image: AssetImage("assets/images/center.png"),
                  height: MediaQuery.of(context).size.height * 0.27,
      
                ),
              ),
            ),
            Padding(
      
              padding: const EdgeInsets.only(top: 8,left: 22),
              child: Row(
                children: [Text("center of renal-dialysis unit")],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: 8,left: 18),
              child: Row(
                children: [
                  Icon(CupertinoIcons.location_solid,size: 16,color: Colors.grey,),
                  Text(
                      "123 Main Street, AnyTown, USA 12345",
                    style: TextStyle(
                      fontSize: 10,
                      color: Colors.grey
                    ),
                  ),
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: 8,left: 22),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    "HD \$340 -- HDF \$340",
                    style: TextStyle(
                      fontSize: 13,
                    ),
                  ),
                  
                  Row(
                    children: [
                      Text("4.5",style: TextStyle(
                        fontSize: 13
                      ),),
                      Text("(23)",style: TextStyle(
                        color: Colors.grey,
                        fontSize: 8
                      ),),
                      Icon(CupertinoIcons.star,color:Color(0xff3C98CB) ,)
                    ],
                  )
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
