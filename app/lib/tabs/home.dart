import 'package:app/Shared/Network/getDataFromBackend/api_handler_for_centers.dart';
import 'package:app/Shared/components/widgets/bottom_sheet.dart';
import 'package:app/pages/center_details_page/center_details/details.dart';
import 'package:app/pages/map_page.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:dio/dio.dart';
import 'package:app/Shared/components/widgets/center_card.dart';
import 'package:app/services/navigation_service.dart';
import 'package:app/Shared/components/widgets/fliter_drawer.dart';
import 'package:google_fonts/google_fonts.dart';
import '../models/home_page_center_card/CenterModel.dart';
import 'package:shared_preferences/shared_preferences.dart';

class Home extends StatefulWidget {
  const Home({Key? key}) : super(key: key);

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  final GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>();
  late NavigationService _navigation;
  String accessToken = "";
  String searchValue = '';
  final ApiHandler _apiService = ApiHandler();
  int page = 1;
  late Future<CenterModel> futureCenterUnit;
  List<Items> units = [];
  Map<String, dynamic> filters = {};
  String? _selectedSortOption;
  String searchQuery = ''; // Variable to store the search query
  bool isSearchBarVisible =
      false; // State variable to manage the visibility of the search bar

  final controller = ScrollController();
  Map<String, dynamic> appliedFilters = {};
  @override
  void initState() {
    super.initState();
    controller.addListener(() {
      if (controller.position.maxScrollExtent == controller.offset) {
        page++;
        _fetchData(page, filters);
      }
    });
    futureCenterUnit = _fetchData(page, filters);
  }

  Future<CenterModel> _fetchData(int page, Map<String, dynamic> filters) async {
    try {
      // Add search query to filters if it exists
      if (searchQuery.isNotEmpty) {
        filters['search'] = searchQuery;
      }

      CenterModel centerModel = await _apiService.getCenterData(page, filters);
      setState(() {
        units.addAll(centerModel.data?.items ?? []);
      });
      return centerModel;
    } catch (e) {
      print("Error Fetching data: $e");
      throw Exception("Error Fetching data: $e");
    }
  }

  void _applySortOption(String sortBy) {
    setState(() {
      _selectedSortOption = sortBy;
      filters['sortBy'] = sortBy;
      units.clear();
      page = 1;
      futureCenterUnit = _fetchData(page, filters);
    });
  }

  TextEditingController searchController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    _navigation = NavigationService();
    return Scaffold(
      floatingActionButton: FloatingActionButton.extended(
        onPressed: () {
          _navigation.navigateToRoute('/chatHomePage');
        },
        label: Text("Chats"),
        icon: Icon(Icons.chat),
        backgroundColor: Color(0xff3C98CB),
      ),
      key: _scaffoldKey,
      // EasySearchBar(
      //   backgroundColor: Colors.transparent,
      //   elevation: 0.0,
      //   title: Text(
      //     'Search',
      //     style: TextStyle(fontSize: 14),
      //   ),
      //   onSearch: (value) {
      //     setState(() {
      //       searchQuery = value; // تحديث استعلام البحث
      //       units.clear(); // مسح البيانات الحالية
      //       page = 1; // إعادة تعيين الصفحة
      //       futureCenterUnit = _fetchData(
      //           page, filters); // استدعاء البيانات مع التصفية الجديدة
      //     });
      //   },
      //   suggestions: _suggestions,
      // ),
      appBar: PreferredSize(
        preferredSize: Size.fromHeight(kToolbarHeight),
        child: isSearchBarVisible
            ? _buildSearchBar()
            : AppBar(
                backgroundColor: Color.fromARGB(255, 255, 255, 255),
                title: Text(
                  "RenoCare",
                  style: GoogleFonts.lato(color: Colors.black),
                ),
                elevation: 0,
                actions: [
                  IconButton(
                    padding: EdgeInsets.only(right: 20.0),
                    onPressed: () {
                      setState(() {
                        isSearchBarVisible = true; // Show search bar
                      });
                    },
                    icon: Icon(
                      Icons.search,
                      color: Colors.black,
                    ),
                  )
                ],
              ),
      ),
      body: Column(
        children: [
          Padding(
            padding:
                const EdgeInsets.symmetric(horizontal: 10.0, vertical: 20.0),
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
                          SizedBox(width: 5),
                          Text("Sort", style: TextStyle(fontSize: 12))
                        ],
                      ),
                    ),
                  ),
                ),
                InkWell(
                  onTap: () {
                    showModalBottomSheet(
                      isScrollControlled: true,
                      shape: const RoundedRectangleBorder(
                        borderRadius: BorderRadius.vertical(
                          top: Radius.circular(30),
                        ),
                      ),
                      context: context,
                      builder: (ctx) => FilterDrawer(
                        onApplyFilters: (selectedFilters) {
                          setState(() {
                            appliedFilters = Map.from(selectedFilters);
                            filters = selectedFilters;
                            units.clear();
                            page = 1;
                            futureCenterUnit = _fetchData(page, filters);
                          });
                        },
                        initialFilters: appliedFilters,
                      ),
                    );
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
                          SizedBox(width: 5),
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
                          SizedBox(width: 5),
                          Text("Maps", style: TextStyle(fontSize: 12))
                        ],
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
          FutureBuilder<CenterModel>(
            future: futureCenterUnit,
            builder: (context, snapshot) {
              if (snapshot.connectionState == ConnectionState.waiting) {
                return Center(child: CircularProgressIndicator());
              } else if (snapshot.hasError) {
                return Center(child: Text('Error: ${snapshot.error}'));
              } else if (!snapshot.hasData ||
                  snapshot.data!.data!.items!.isEmpty) {
                return Center(child: Text('No data available'));
              } else {
                units = snapshot.data!.data!.items!;
                if (units.length == 1) {
                  return Flexible(
                    child: SingleChildScrollView(
                      physics: ScrollPhysics(),
                      child: CenterCard(
                        centerModel: units[0],
                        onTap: () {
                          if (units[0].id != null) {
                            _navigation.navigateToPage(
                              DetailsScreen(unitId: units[0].id!),
                            );
                          } else {
                            print('Unit ID is null');
                          }
                        },
                      ),
                    ),
                  );
                } else {
                  return Expanded(
                    child: ListView.builder(
                      controller: controller,
                      itemCount: units.length +
                          (snapshot.connectionState == ConnectionState.waiting
                              ? 1
                              : 0),
                      itemBuilder: (context, index) {
                        if (index < units.length) {
                          final unit = units[index];
                          return CenterCard(
                            centerModel: unit,
                            onTap: () {
                              if (unit.id != null) {
                                _navigation.navigateToPage(
                                  DetailsScreen(unitId: unit.id!),
                                );
                              } else {
                                print('Unit ID is null');
                              }
                            },
                          );
                        } else {
                          return Padding(
                            padding: EdgeInsets.symmetric(vertical: 32),
                            child: Center(
                              child: CircularProgressIndicator(),
                            ),
                          );
                        }
                      },
                    ),
                  );
                }
              }
            },
          ),
        ],
      ),
    );
  }

  Widget _buildSearchBar() {
    return SafeArea(
      child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Container(
          color: Colors.white,
          child: Row(
            children: [
              IconButton(
                icon: Icon(Icons.arrow_back),
                onPressed: () {
                  setState(() {
                    searchQuery = ''; // Clear search query
                    filters.clear(); // Clear filters
                    units.clear(); // Clear current data
                    page = 1; // Reset page number
                    futureCenterUnit =
                        _fetchData(page, filters); // Fetch initial data
                    isSearchBarVisible =
                        false; // Hide search bar // Hide search bar
                  });
                },
              ),
              Expanded(
                child: TextField(
                  controller: searchController,
                  onChanged: (value) {
                    setState(() {
                      searchQuery = value; // Update search query
                      units.clear(); // Clear current data
                      page = 1; // Reset page number
                      futureCenterUnit = _fetchData(
                          page, filters); // Fetch data with new filter
                    });
                  },
                  decoration: InputDecoration(
                    hintText: "Search...",
                    hintStyle: TextStyle(fontSize: 16),
                    // border: InputBorder.none,
                    focusColor: Color(0xff3C98CB),
                    suffixIcon: searchQuery.isNotEmpty
                        ? IconButton(
                            icon: Icon(
                              Icons.clear,
                              color: Colors.black,
                            ),
                            onPressed: () {
                              setState(() {
                                searchController.clear();
                                searchQuery = ''; // Clear search query
                                units.clear();
                                filters.clear(); // Clear current data
                                page = 1; // Reset page number
                                futureCenterUnit = _fetchData(
                                    page, filters); // Fetch initial data
                              });
                            },
                          )
                        : null,
                  ),
                ),
              ),
              IconButton(
                icon: Icon(Icons.search),
                onPressed: () {
                  setState(() {
                    units.clear(); // Clear current data
                    page = 1; // Reset page number
                    futureCenterUnit =
                        _fetchData(page, filters); // Fetch data with new filter
                  });
                },
              ),
            ],
          ),
        ),
      ),
    );
  }

  displayBottomSheet(BuildContext context) {
    return showModalBottomSheet(
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.vertical(
          top: Radius.circular(30),
        ),
      ),
      context: context,
      builder: (context) => SortBottomSheet(onSortSelected: _applySortOption),
    );
  }
}
