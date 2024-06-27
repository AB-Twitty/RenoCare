import 'package:app/pages/home_page/home_page.dart';
import 'package:app/tabs/home.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class BottomTab extends StatefulWidget {
  const BottomTab({super.key});
  @override
  State<StatefulWidget> createState() {
    // TODO: implement createState
    return _BottomTabState();
  }
}

class _BottomTabState extends State<BottomTab> {
  int _selectedPage = 1;
  void SelectPage(int index) {
    setState(() {
      _selectedPage = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    Widget activePage = Home();
    // TODO: implement build
    return Scaffold(
      body: activePage,
      bottomNavigationBar: BottomNavigationBar(
        items: [
          BottomNavigationBarItem(
            icon: Icon(Icons.calendar_month),
            label: "Appointment",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: "Home",
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.person),
            label: "Profile",
          ),
        ],
      ),
    );
  }
}
