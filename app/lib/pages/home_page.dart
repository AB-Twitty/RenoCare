import 'package:app/pages/cubit/home_cubit.dart';
import 'package:app/tabs/appointment.dart';
import 'package:app/tabs/home.dart';
import 'package:app/tabs/profile_tab.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class HomePage extends StatefulWidget {
  static const String routeName = "home_page";
  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => HomeCubit(),
      child: BlocConsumer<HomeCubit, HomeState>(
        listener: (context, state) {
          // TODO: implement listener
        },
        builder: (context, state) {
          var cubit = HomeCubit.get(context);
          return Scaffold(
            appBar: AppBar(
              automaticallyImplyLeading: false,
              title: Text('Hemodialysis App'),
            ),
            body: cubit.tabs[cubit.current_indx],

            bottomNavigationBar: BottomNavigationBar(
                selectedItemColor: Color(0xff45B3EF),
                currentIndex: cubit.current_indx,
                onTap: (index) {
                  switch(index)
                  {
                    case 0:
                      Navigator.pushNamed(context, '/appointment');
                      break;
                    case 1:

                      Navigator.pushNamed(context, '/home');
                      break;
                    case 2:
                      Navigator.pushNamed(context, '/profile');
                      break;
                  }

                },
                items: cubit.bottomItems,
            ),
          );
        },
      ),
    );
  }



}
