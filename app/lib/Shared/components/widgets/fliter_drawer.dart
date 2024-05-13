import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class FilterDrawer extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Container(
          width: MediaQuery
              .of(context)
              .size
              .width,

          decoration: BoxDecoration(
              color: Colors.white
          ),
          child: Column(

            children: [
              Row(

                mainAxisAlignment: MainAxisAlignment.start,
                children: [

                  Padding(
                    padding: const EdgeInsets.only(left: 12,right: 50),
                    child: IconButton(
                        onPressed: () {
                          Navigator.pop(context);
                        },
                        icon: Icon(Icons.close)
                    ),
                  ),

                  Text(
                    "Filter By",
                    style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.bold,
                        color: Colors.black
                    ),
                  ),

                  Padding(
                    padding: const EdgeInsets.only(left: 70,right: 10),
                    child: TextButton(
                        onPressed:() {

                        },
                        child: Text(
                          "Clear",
                          style: TextStyle(
                              color: Color(0xff45B3EF),
                            fontSize: 16
                          ),
                        ),
                    ),
                  ),
                ],
              ),
              Padding(
                padding: const EdgeInsets.only(left:28,right: 28,top: 12,bottom: 12),
                child: Divider(height: 2,color: Colors.black,),
              ),
              Padding(
                padding: const EdgeInsets.only(left: 24,right: 50,bottom: 12),
                child: Row(
                  children: [
                    Text("Treatment type",
                    style: TextStyle(
                      fontSize: 16,
                    ),
                    ),

                  ],
                ),
              ),

              
              
              
            ],
          ),
        )
    );
  }
}
