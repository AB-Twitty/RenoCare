import 'package:app/tabs/appointment.dart';
import 'package:app/tabs/home.dart';
import 'package:app/tabs/profile_tab.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

class TabsScreen extends StatefulWidget {
  const TabsScreen({super.key});
  @override
  State<TabsScreen> createState() {
    return _TabsState();
  }
}

class _TabsState extends State<TabsScreen> {
  int selectedPage = 1;
  void _SelectPage(int index) {
    setState(() {
      selectedPage = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    Widget activeScreen = Home();
    if (selectedPage == 0) {
      activeScreen = Appointment();
    }
    if (selectedPage == 2) {
      activeScreen = ProfileTab();
    }
    // TODO: implement build
    return Scaffold(
      body: activeScreen,
      bottomNavigationBar: BottomNavigationBar(
        onTap: _SelectPage,
        currentIndex: selectedPage,
        selectedIconTheme:
            IconThemeData(color: Color.fromRGBO(60, 152, 203, 1)),
        items: [
          BottomNavigationBarItem(
            icon: Icon(Icons.calendar_month),
            label: 'Appointments',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: 'Home',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.person),
            label: 'Profile',
          ),
        ],
      ),
    );
  }
}
