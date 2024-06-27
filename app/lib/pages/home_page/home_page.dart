import 'package:app/pages/home_page/home_cubit.dart';
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
            body: cubit.tabs[cubit.current_indx],
            bottomNavigationBar: BottomNavigationBar(
              selectedItemColor: Color(0xff45B3EF),
              showUnselectedLabels: true,
              unselectedItemColor: const Color.fromARGB(255, 251, 219, 219),
              currentIndex: cubit.current_indx,
              onTap: (index) {
                switch (index) {
                  case 0:
                    Navigator.pushNamed(context, '/appointment');
                    break;
                  case 1:
                    Navigator.pushNamed(context, '/home');
                    break;
                  case 2:
                    Navigator.pushNamed(context, '/profile');
                    break;
                  case 3:
                    Navigator.pushNamed(context, '/chatHomePage');
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
