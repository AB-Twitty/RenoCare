import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:google_fonts/google_fonts.dart';

import 'package:table_calendar/table_calendar.dart';

import '../../Shared/components/widgets/book_widgets/time.dart';
import '../../Shared/components/widgets/book_widgets/treatmentType.dart';

class BookScreen extends StatefulWidget {
  const BookScreen({super.key});

  @override
  State<BookScreen> createState() => _BookScreenState();
}

class _BookScreenState extends State<BookScreen> {
  CalendarFormat _format = CalendarFormat.month;
  DateTime _focusedDay = DateTime.now();
  DateTime _currentDay = DateTime.now();
  int? currentIndex;
  bool isWeekEnd = false;
  bool dateSelected = false;
  bool timeSelected = false;
  Color selectedcolor = Color.fromRGBO(60, 152, 203, 1);
  Color unselectedColor = Colors.black;
  int? _selectedIconIndex;
  List<String> timeText = [
    "10:00 AM",
    "11:00 AM",
    "12:00 AM",
    "13:00 PM",
    "14:00 PM",
    "15:00 PM",
    "16:00 PM",
    "18:00 PM",
    "19:00 PM",
    "20:00 PM",
  ];
  void _onIconPressed(int index) {
    setState(() {
      _selectedIconIndex = index;
    });
  }

  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Scaffold(
      bottomNavigationBar: BottomAppBar(
        child: Container(
          height: 70,
          child: ElevatedButton(
            onPressed: () {},
            child: Text(
              textAlign: TextAlign.center,
              'Shedule',
              style: TextStyle(fontSize: 18),
            ),
            style: ElevatedButton.styleFrom(
              backgroundColor: selectedcolor,
              foregroundColor: Colors.white,
              elevation: 0,
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.only(
                  topLeft: Radius.circular(10),
                  topRight: Radius.circular(10),
                ),
              ),
            ),
          ),
        ),
      ),
      appBar: AppBar(
        title: Text('Get an appointment'),
      ),
      body: Padding(
        padding: const EdgeInsets.all(20.0),
        child: SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Date',
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
              SizedBox(
                height: 10,
              ),
              TableCalendar(
                focusedDay: _focusedDay,
                firstDay: DateTime.now(),
                lastDay: DateTime(2024, 12, 31),
                calendarFormat: _format,
                currentDay: _currentDay,
                headerStyle: HeaderStyle(
                  titleCentered: true,
                  headerMargin: EdgeInsets.only(
                    bottom: 10,
                  ),
                  titleTextStyle: GoogleFonts.courgette(
                    color: selectedcolor,
                    fontSize: 16,
                  ),
                ),
                daysOfWeekStyle: DaysOfWeekStyle(
                  weekdayStyle: TextStyle(
                    color: selectedcolor,
                  ),
                  weekendStyle: TextStyle(
                    color: Colors.red,
                  ),
                ),
                calendarStyle: CalendarStyle(
                  defaultTextStyle: TextStyle(
                    color: unselectedColor,
                  ),
                  todayDecoration: BoxDecoration(
                    color: selectedcolor,
                    shape: BoxShape.circle,
                  ),
                  cellMargin: EdgeInsets.only(
                      top: 16), // Add space between days and dates
                  selectedTextStyle: TextStyle(
                    color: Colors.white,
                  ),
                ),
                availableCalendarFormats: const {
                  CalendarFormat.month: 'Month',
                },
                onFormatChanged: (format) {
                  setState(() {
                    _format = format;
                  });
                },
                onDaySelected: (selectedDay, focusedDay) {
                  setState(() {
                    _currentDay = selectedDay;
                    _focusedDay = focusedDay;
                    dateSelected = true;
                    if (selectedDay.weekday == 6 || selectedDay.weekday == 7) {
                      isWeekEnd = true;
                      timeSelected = false;
                      currentIndex = null;
                    } else {
                      isWeekEnd = false;
                    }
                  });
                },
              ),
              SizedBox(
                height: 15,
              ),
              Divider(
                color: Colors.grey,
                height: 2,
                endIndent: 5,
                indent: 5,
              ),
              SizedBox(
                height: 16,
              ),
              Row(
                children: [
                  Text(
                    'Time',
                    style: TextStyle(
                      fontSize: 18,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  Spacer(),
                  Container(
                    padding: EdgeInsets.symmetric(horizontal: 10, vertical: 5),
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(
                        10,
                      ),
                      color: Colors.lightBlue[50],
                    ),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        _buildIcon(0, Icons.sunny),
                        _buildVerticalDivider(),
                        _buildIcon(1, FontAwesomeIcons.cloudSun),
                        _buildVerticalDivider(),
                        _buildIcon(2, FontAwesomeIcons.moon),
                      ],
                    ),
                  ),
                ],
              ),
              SizedBox(
                height: 15,
              ),
              TimePart(timetext: timeText),
              SizedBox(
                height: 15,
              ),
              Divider(
                color: Colors.grey,
                height: 2,
                endIndent: 5,
                indent: 5,
              ),
              SizedBox(
                height: 16,
              ),
              TreatmentType(),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildIcon(int index, IconData icon) {
    return GestureDetector(
      onTap: () => _onIconPressed(index),
      child: Container(
        decoration: BoxDecoration(
          color: _selectedIconIndex == index ? Colors.blue : Colors.transparent,
          shape: BoxShape.circle,
        ),
        padding: EdgeInsets.all(8.0),
        child: Icon(
          size: 20,
          icon,
          color: _selectedIconIndex == index ? Colors.white : Colors.black,
        ),
      ),
    );
  }

  Widget _buildVerticalDivider() {
    return Container(
      width: 2,
      height: 30,
      color: Colors.black,
      margin: EdgeInsets.symmetric(horizontal: 10),
    );
  }
}
