import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class CenterCard extends StatelessWidget {
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
                  "center of renal-dialysis unit",
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
                      "123 Main Street, AnyTown, USA 12345",
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
                      "HD \$340 -- HDF \$340",
                      style: TextStyle(
                        fontSize: 14,
                      ),
                    ),
                    Row(
                      crossAxisAlignment: CrossAxisAlignment.center,
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Text(
                          "4.5",
                          style: TextStyle(fontSize: 14),
                        ),
                        SizedBox(
                          width: 5,
                        ),
                        Text(
                          "(23)",
                          style: TextStyle(color: Colors.grey, fontSize: 12),
                        ),
                        SizedBox(
                          width: 5,
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
