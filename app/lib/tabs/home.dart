
import 'package:easy_search_bar/easy_search_bar.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';

import '../Shared/components/widgets/bottom_sheet.dart';
import '../Shared/components/widgets/center_card.dart';
import '../Shared/components/widgets/fliter_drawer.dart';


class Home extends StatefulWidget {

  const Home({Key? key}) : super(key: key);

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  final GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>();
  String searchValue='';
  final List<String> _suggestions = ['Afeganistan', 'Albania', 'Algeria', 'Australia', 'Brazil', 'German', 'Madagascar', 'Mozambique', 'Portugal', 'Zambia'];
  @override
  Widget build(BuildContext context) {
    return Scaffold(

      key: _scaffoldKey,

    drawer: FilterDrawer(),
      appBar: EasySearchBar(

        backgroundColor: Colors.transparent,
          elevation: 0.0,

          title: Text(
              'Search',
            style: TextStyle(
              fontSize: 14,
            ),


          ),
          onSearch: (value) {
            setState(() {

              searchValue=value;
            });
          },
        suggestions: _suggestions,

      ),

      // drawer: FilterDrawer(),
      body: Column(

        children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [

                Row(
                  children: [
                    IconButton(
                        onPressed: () {
                          displayBottomSheet(context);
                        },
                        icon: Icon(Icons.sort,color:Color(0xff3C98CB) ,)
                    ),
                    Text(
                      "Sort",
                      style: TextStyle(
                        fontSize: 12
                      )

                    )
                  ],
                ),

                Row(
                  children: [
                    IconButton(
                        onPressed: () {
                          _scaffoldKey.currentState?.openDrawer();
                        },
                        icon: Icon(Icons.filter_alt,color:Color(0xff3C98CB) ,)
                    ),
                    Text(
                      "Filter",
                      style: TextStyle(
                        fontSize: 12
                      )

                    )
                  ],
                ),
                Row(
                  children: [
                    IconButton(
                        onPressed: () {},
                        icon: Icon(Icons.map,color:Color(0xff3C98CB) ,)
                    ),
                    Text(
                      "Maps",
                      style: TextStyle(
                        fontSize: 12
                      )

                    )
                  ],
                ),

              ],
            ),
          Divider(height: 2,color: Colors.black,),
          Expanded(
            child: ListView.builder(
              itemCount: 20,
                itemBuilder:(context, index){
                  return CenterCard();
                },
            ),
          ),

        ],
      ),

    );
  }

   displayBottomSheet(BuildContext context)
  {
    return showBottomSheet(
        context: context,
        builder: (context)=>SortBottomSheet(),
      shape: OutlineInputBorder(
        borderSide: BorderSide(
            color: Colors.black26),
        borderRadius: BorderRadius.only(topLeft: Radius.circular(16),topRight: Radius.circular(16))
      ),
    );
  }
}
