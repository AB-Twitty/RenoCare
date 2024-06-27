import 'package:app/models/home_page_center_card/CenterModel.dart';
import 'package:app/models/home_page_center_card/center_card.dart';
import 'package:app/pages/center_details_page/center_details/details.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

import '../../../services/navigation_service.dart';

class CenterCard extends StatelessWidget {
  final Items centerModel;
  final NavigationService _navigation = NavigationService();

  CenterCard({required this.centerModel});

  @override
  Widget build(BuildContext context) {
    // return Container(
    //   margin: EdgeInsets.all(12),
    //   width: MediaQuery.of(context).size.width * 0.95,
    //   height: MediaQuery.of(context).size.height * 0.40,
    //   decoration: BoxDecoration(
    //     color: Colors.transparent,
    //     border: Border.all(color: Colors.grey,width: 0.6),
    //     borderRadius: BorderRadius.only(topRight: Radius.circular(32),topLeft: Radius.circular(32))
    //   ),
    return Container(
      padding: EdgeInsets.all(10),
      child: InkWell(
        onTap: () {
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) => DetailsScreen(),
            ),
          );
        },
        child: Card(
          margin: const EdgeInsets.all(8),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(10),
          ),
          clipBehavior: Clip.hardEdge,
          elevation: 2,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Image(
                fit: BoxFit.cover,
                width: double.infinity,
                image: AssetImage("assets/images/center.png"),
              ),
              SizedBox(
                height: 15,
              ),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 8.0),
                child: Text(
                  centerModel.name ?? "No Name",
                  style: TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 16,
                  ),
                ),
              ),
              SizedBox(
                height: 10,
              ),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: 8.0),
                child: Row(
                  children: [
                    Icon(
                      CupertinoIcons.location_solid,
                      size: 20,
                      color: Colors.grey,
                    ),
                    SizedBox(
                      width: 5,
                    ),
                    Text(
                      "${centerModel.address},${centerModel.city},${centerModel.country}",
                      style: TextStyle(fontSize: 12, color: Colors.grey),
                    ),
                  ],
                ),
              ),
              SizedBox(
                height: 10,
              ),
              Padding(
                padding:
                    const EdgeInsets.only(left: 8.0, right: 8.0, bottom: 10.0),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      (centerModel.isHdSupported == true &&
                              centerModel.isHdfSupported == true)
                          ? "HD \$${centerModel.hdPrice ?? 125} -- HDF \$${centerModel.hdfPrice ?? 230}"
                          : (centerModel.isHdSupported == true)
                              ? "HD \$${centerModel.hdPrice ?? 132}"
                              : "HDF \$${centerModel.hdfPrice ?? 226}",
                      style: TextStyle(
                        fontSize: 14,
                      ),
                    ),
                    Row(
                      crossAxisAlignment: CrossAxisAlignment.center,
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text(
                          centerModel.rating.toString(),
                          style: TextStyle(fontSize: 14),
                        ),
                        SizedBox(
                          width: 8,
                        ),
                        Text(
                          "(${centerModel.reviewsCnt})",
                          style:
                              TextStyle(color: Colors.grey[700], fontSize: 12),
                        ),
                        SizedBox(
                          width: 8,
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
      ),
    );
  }
}
