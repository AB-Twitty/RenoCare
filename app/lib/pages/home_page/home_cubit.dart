import 'package:app/pages/chat_module/page/chat_home/chat_home_page.dart';
import 'package:app/pages/chat_module/page/chat_home/chat_page.dart';
import 'package:bloc/bloc.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:meta/meta.dart';

import '../../tabs/appointment.dart';

import '../../tabs/home.dart';
import '../../tabs/profile_tab.dart';
part 'home_state.dart';

class HomeCubit extends Cubit<HomeState> {
  HomeCubit() : super(HomeInitial());
  static HomeCubit get(context) => BlocProvider.of(context);
  int current_indx=1;

  List<BottomNavigationBarItem> bottomItems= [
    BottomNavigationBarItem(
        icon: Icon(Icons.date_range_outlined),
        label: "Appointment"
    ),

    BottomNavigationBarItem(
        icon: Icon(Icons.home_filled),
        label: "Home"
    ),

    BottomNavigationBarItem(
        icon:Icon(Icons.person),
        label: "Profile"
    ),
    BottomNavigationBarItem(
        icon:Icon(Icons.chat_outlined),
        label: "Chat"
    ),
  ];

  List<Widget>tabs = [
    Appointment(),
    Home(),
    // ProfileTab(),
    ChatHomePage(),
  ];

  void changeNavBarState(int index)
  {
    current_indx=index;
    emit(HomeBottomNavBar());
  }

}
