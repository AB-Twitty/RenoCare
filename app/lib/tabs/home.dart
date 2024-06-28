import 'package:app/Shared/Network/getDataFromBackend/api_handler_for_centers.dart';
import 'package:app/pages/map_page.dart';
import 'package:draggable_fab/draggable_fab.dart';
import 'package:easy_search_bar/easy_search_bar.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

// import 'package:google_fonts/google_fonts.dart';
import '../models/home_page_center_card/CenterModel.dart';
import '../models/home_page_center_card/center_card.dart';
import '../services/navigation_service.dart';
import '../Shared/components/widgets/bottom_sheet.dart';
import '../Shared/components/widgets/center_card.dart';
import '../Shared/components/widgets/fliter_drawer.dart';
import '../services/token_service.dart';

class Home extends StatefulWidget {
  const Home({Key? key}) : super(key: key);

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  final GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>();
  late NavigationService _navigation;
  final loginDataManager2 = LoginDataManager2();
  String accessToken = "";
  String searchValue = '';
  final ApiHandler _apiService = ApiHandler();
  int page=1;
  // late Future<List<CenterModel>> futureCenterUnit;
  late Future<CenterModel> futureCenterUnit;
  List<Items> units = [];

  final controller = ScrollController();

  final List<String> _suggestions = [
    'Afeganistan',
    'Albania',
    'Algeria',
    'Australia',
    'Brazil',
    'German',
    'Madagascar',
    'Mozambique',
    'Portugal',
    'Zambia'
  ];

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    // _fetchData();
    controller.addListener(() {
      if(controller.position.maxScrollExtent==controller.offset)
        {

          page++;
          _fetchData(page);
        }
    });
    futureCenterUnit = ApiHandler().getCenterData(page);
  }

  Future<void> _fetchData(int page) async {
    try{

      CenterModel centerModel =await _apiService.getCenterData(page);
      setState(() {
        units.addAll(centerModel.data?.items??[]);

      });
    }catch(e)
    {
      print("Error Fetching data: $e");
    }
  }

  @override
  Widget build(BuildContext context) {
    _navigation = NavigationService();
    return Scaffold(
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          _navigation.navigateToRoute("/chatHomePage");
        },
        child: Icon(
          Icons.chat,
        ),
        backgroundColor: Color(0xff3C98CB),
      ),

      key: _scaffoldKey,

      drawer: FilterDrawer(page),
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
            searchValue = value;
          });
        },
        suggestions: _suggestions,
      ),

      // drawer: FilterDrawer(),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                InkWell(
                  onTap: () {
                    displayBottomSheet(context);
                  },
                  child: Card(
                    color: Colors.grey[100],
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(15),
                    ),
                    child: Padding(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 20,
                        vertical: 10,
                      ),
                      child: Row(
                        children: [
                          Icon(
                            Icons.sort,
                            color: Color(0xff3C98CB),
                          ),
                          SizedBox(
                            width: 5,
                          ),
                          Text("Sort", style: TextStyle(fontSize: 12))
                        ],
                      ),
                    ),
                  ),
                ),
                InkWell(
                  onTap: () {
                    _scaffoldKey.currentState?.openDrawer();
                  },
                  child: Card(
                    color: Colors.grey[100],
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(15),
                    ),
                    child: Padding(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 20,
                        vertical: 10,
                      ),
                      child: Row(
                        children: [
                          Icon(
                            Icons.filter_alt,
                            color: Color(0xff3C98CB),
                          ),
                          SizedBox(
                            width: 5,
                          ),
                          Text("Filter", style: TextStyle(fontSize: 12))
                        ],
                      ),
                    ),
                  ),
                ),
                InkWell(
                  onTap: () {
                    _navigation.navigateToPage(MapPage());
                  },
                  child: Card(
                    color: Colors.grey[100],
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(15),
                    ),
                    child: Padding(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 20,
                        vertical: 10,
                      ),
                      child: Row(
                        children: [
                          Icon(
                            Icons.map,
                            color: Color(0xff3C98CB),
                          ),
                          SizedBox(
                            width: 5,
                          ),
                          Text("Maps", style: TextStyle(fontSize: 12))
                        ],
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
          Expanded(
            child: FutureBuilder<CenterModel>(
              future: futureCenterUnit,
              builder: (context, snapshot) {
                if (snapshot.connectionState == ConnectionState.waiting) {
                  return Center(child: CircularProgressIndicator());
                } else if (snapshot.hasError) {
                  return Center(child: Text('Error: ${snapshot.error}'));
                } else if (snapshot.hasData) {
                  units = snapshot.data!.data?.items??[];
                  return ListView.builder(
                    controller: controller,
                    itemCount: units.length + 1,
                    itemBuilder: (context, index) {
                      if (index < units.length) {
                        final unit = units[index];
                        return CenterCard(centerModel: unit);
                      } else {
                        return Padding(
                          padding: EdgeInsets.symmetric(vertical: 32),
                          child: Center(
                            child: CircularProgressIndicator(),
                          ),
                        );
                      }

                      print(index);
                      return CenterCard(
                        centerModel: units[index],
                      );
                    },
                  );
                } else {
                  return Center(child: Text('No data available'));
                }
              },
            ),
          ),
        ],
      ),
    );
  }

  displayBottomSheet(BuildContext context) {
    return showBottomSheet(
      context: context,
      builder: (context) => SortBottomSheet(),
      shape: OutlineInputBorder(
          borderSide: BorderSide(color: Colors.black26),
          borderRadius: BorderRadius.only(
              topLeft: Radius.circular(16), topRight: Radius.circular(16))),
    );
  }
}
