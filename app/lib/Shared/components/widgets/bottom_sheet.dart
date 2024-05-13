
import 'package:flutter/material.dart';

class SortBottomSheet extends StatefulWidget {
  @override
  State<SortBottomSheet> createState() => _SortBottomSheetState();
}

class _SortBottomSheetState extends State<SortBottomSheet> {
  bool? isNameChecked=false;

  bool? isRatingChecked=false;

  bool? isPriceChecked=false;

  @override
  Widget build(BuildContext context) {
    return Container(

      padding: EdgeInsets.all(12),
      decoration: BoxDecoration(
        color: Colors.white54
      ),
      height: MediaQuery.of(context).size.height*0.33,
      child: Column(
        children: [

          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,

            children: [
              Text("Sorted by",style: TextStyle(
                fontSize: 16,
                color: Colors.black,
                fontWeight: FontWeight.bold
              ),),
              IconButton(onPressed: () {

                Navigator.pop(context);
              }, icon: Icon(Icons.close_outlined)),
            ],
          ),
          SizedBox(height: 25,),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text(
                "Name",
                style: TextStyle(
                  fontSize: 14,

                ),

              ),
              Checkbox(
              activeColor: Color(0xff45B3EF),
                  side: BorderSide(color: Colors.black),
                  value: isNameChecked,
                  onChanged: (value) {
                    setState(() {
                      isNameChecked=value;
                    });
                  },
              ),
            ],
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text(
                "Rating",
                style: TextStyle(
                  fontSize: 14,

                ),

              ),
              Checkbox(
                activeColor: Color(0xff45B3EF),
                side: BorderSide(color: Colors.black),
                  value: isRatingChecked,
                  onChanged: (value) {
                    setState(() {
                      isRatingChecked=value;
                    });
                  },
              ),
            ],
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Text(
                "Price",
                style: TextStyle(
                  fontSize: 14,

                ),

              ),
              Checkbox(
                activeColor: Color(0xff45B3EF),
                side: BorderSide(color: Colors.black),
                  value: isPriceChecked,
                  onChanged: (value) {
                    setState(() {
                      isPriceChecked=value;
                    });
                  },
              ),
            ],
          ),
        ],
      ),
    );
  }
}
