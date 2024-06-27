import 'package:app/models/home_page_center_card/CenterModel.dart';
import 'package:app/models/home_page_center_card/center_card.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../../../services/navigation_service.dart';

class CenterCard extends StatelessWidget {
  final Items centerModel;
  final NavigationService _navigation = NavigationService();

  CenterCard({required this.centerModel});

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
            border: Border.all(color: Colors.grey, width: 0.6),
            borderRadius: BorderRadius.only(
                topRight: Radius.circular(32), topLeft: Radius.circular(32))),
        child: Column(
          children: [
            Container(
              decoration: BoxDecoration(),
              child: ClipRRect(
                  borderRadius: BorderRadius.only(
                      topRight: Radius.circular(32),
                      topLeft: Radius.circular(32)),
                  child:Image.asset("assets/images/center.png")


              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: 8, left: 22),
              child: Row(
                children: [Text(centerModel.name??"No Name")],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: 8, left: 18),
              child: Row(
                children: [
                  Icon(
                    CupertinoIcons.location_solid,
                    size: 16,
                    color: Colors.grey,
                  ),
                  Text(
                    "${centerModel.address},${centerModel.city},${centerModel.country}",
                    style: TextStyle(fontSize: 10, color: Colors.grey),
                  ),
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(top: 8, left: 22),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  Text(
                    (centerModel.isHdSupported==true && centerModel.isHdfSupported==true)
                        ? "HD \$${centerModel.hdPrice??125} -- HDF \$${centerModel.hdfPrice??230}"
                        : (centerModel.isHdSupported==true)
                            ? "HD \$${centerModel.hdPrice??132}"
                            : "HDF \$${centerModel.hdfPrice??226}",
                    style: TextStyle(
                      fontSize: 13,
                    ),
                  ),
                  Row(
                    children: [
                      Text(
                        centerModel.rating.toString(),
                        style: TextStyle(fontSize: 13),
                      ),
                      Text(
                        "${centerModel.reviewsCnt}",
                        style: TextStyle(color: Colors.grey, fontSize: 8),
                      ),
                      Icon(
                        CupertinoIcons.star,
                        color: Color(0xff3C98CB),
                      )
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
