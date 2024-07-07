import 'package:app/Shared/components/widgets/book_widgets/time.dart';
import 'package:app/Shared/components/widgets/book_widgets/time.dart';
import 'package:app/Shared/components/widgets/book_widgets/treatmentType.dart';
import 'package:app/pages/center_details_page/center_details/details.dart';
import 'package:awesome_dialog/awesome_dialog.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:intl/intl.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:table_calendar/table_calendar.dart';

class BookScreen extends StatefulWidget {
  final int unitId;

  const BookScreen({super.key, required this.unitId});

  @override
  State<BookScreen> createState() => _BookScreenState();
}

class _BookScreenState extends State<BookScreen> {
  final TextEditingController _textController = TextEditingController();
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
  late Future<DetailModel> futureDetail;
  List<String> selectedTimes = [];
  List<DateTime> availableDates = [];
  List<Map<String, dynamic>> bookingTypes = [];
  int sessionId = 7;
  int? selectedBookingType;
  int treatmentTypeValue = 0;

  @override
  void initState() {
    super.initState();
    futureDetail = fetchDetails(widget.unitId);
    fetchBookingTypes();
  }

  Future<DetailModel> fetchDetails(int unitId) async {
    final Dio _dio = Dio();
    final String detailsUrl =
        'https://renocareapi.azurewebsites.net/Api/V1/Dialysis-Unit/Details/$unitId';
    final String reviewsUrl =
        'https://renocareapi.azurewebsites.net/Api/V1/Reviews?unitId=$unitId&page=1';

    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? token = prefs.getString('token');
    try {
      final response = await _dio.get(
        detailsUrl,
        options: Options(
          headers: {
            'Authorization': 'Bearer $token',
          },
        ),
      );
      if (response.statusCode == 200) {
        final detailData = response.data['data'];
        final reviewsResponse = await _dio.get(
          reviewsUrl,
          options: Options(
            headers: {
              'Authorization': 'Bearer $token',
            },
          ),
        );

        if (reviewsResponse.statusCode == 200) {
          detailData['reviews'] = reviewsResponse.data['data']['items'];
        } else {
          detailData['reviews'] = [];
        }

        final detailModel = DetailModel.fromJson(detailData);

        // تعبئة availableDates هنا
        availableDates = [];
        detailModel.groupedSessions.forEach((weekday, sessions) {
          if (sessions.isNotEmpty) {
            final int weekdayIndex =
            DateFormat.EEEE().dateSymbols.WEEKDAYS.indexOf(weekday);
            DateTime now = DateTime.now();
            DateTime nextDate =
            now.add(Duration(days: (weekdayIndex - now.weekday + 7) % 7));
            while (nextDate.isBefore(DateTime(2024, 12, 31))) {
              availableDates.add(nextDate);
              nextDate = nextDate.add(Duration(days: 7));
            }
          }
        });

        return detailModel;
      } else {
        throw Exception('Failed to load details');
      }
    } on DioError catch (e) {
      throw Exception('Failed to load details: ${e.message}');
    }
  }

  Future<void> fetchBookingTypes() async {
    final Dio _dio = Dio();
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? token = prefs.getString('token');

    try {
      final response = await _dio.get(
        'https://renocareapi.azurewebsites.net/Api/V1/Medication/Requests/Types?active=true',
        options: Options(
          headers: {
            'Authorization': 'Bearer $token',
          },
        ),
      );
      if (response.statusCode == 200) {
        setState(() {
          bookingTypes = List<Map<String, dynamic>>.from(response.data['data']);
        });
      } else {
        throw Exception('Failed to load booking types');
      }
    } on DioError catch (e) {
      throw Exception('Failed to load booking types: ${e.message}');
    }
  }

  bool isSameDay(DateTime day1, DateTime day2) {
    return day1.year == day2.year &&
        day1.month == day2.month &&
        day1.day == day2.day;
  }

  String getWeekday(DateTime date) {
    return DateFormat.EEEE()
        .format(date); // Returns the weekday name (e.g., "Mon", "Tue", etc.)
  }

  void _onDaySelected(DateTime selectedDay, DetailModel detail) {
    setState(() {
      _currentDay = selectedDay;
      _focusedDay = selectedDay;
      String weekday = getWeekday(selectedDay);
      selectedTimes = detail.groupedSessions[weekday] ?? [];
      dateSelected = true;
      if (selectedDay.weekday == 6 || selectedDay.weekday == 7) {
        isWeekEnd = true;
        timeSelected = false;
        currentIndex = null;
      } else {
        isWeekEnd = false;
      }
    });
  }

  void showErrorDialog(String message) {
    AwesomeDialog(
      context: context,
      dialogType: DialogType.error,
      title: 'Error',
      descTextStyle: TextStyle(
        fontSize: 16,
      ),
      desc: message,
      btnOkColor: Color.fromRGBO(60, 152, 203, 1),
      buttonsTextStyle: TextStyle(
        fontSize: 18,
      ),
      btnOkOnPress: () {},
    ).show();
  }

  void showSuccessDialog(String message) {
    AwesomeDialog(
      context: context,
      dialogType: DialogType.success,
      title: 'Success',
      descTextStyle: TextStyle(
        fontSize: 16,
      ),
      desc: message,
      btnOkColor: Color.fromRGBO(60, 152, 203, 1),
      buttonsTextStyle: TextStyle(
        fontSize: 18,
      ),
      btnOkOnPress: () {},
    ).show();
  }

  Future<void> scheduleAppointment() async {
    final Dio _dio = Dio();
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? token = prefs.getString('token');
    DetailModel detailModel;

    try {
      final response = await _dio.post(
        'https://renocareapi.azurewebsites.net/Api/V1/Medication/Request/Schedule',
        data: {
          "unitId": widget.unitId,
          "date": _currentDay.toIso8601String(),
          "sessionId": sessionId,
          "patientRemarks": _textController.text,
          "medReqTypeId": selectedBookingType,
          "treatment": treatmentTypeValue,
        },
        options: Options(
          headers: {
            'Authorization': 'Bearer $token',
          },
        ),
      );

      if (response.statusCode == 200) {
        print(response.data);
        showSuccessDialog("Booking has been done successfuly");
        // Handle success, e.g., show a success message or navigate to another page
        print('Appointment scheduled successfully');
      } else {
        showErrorDialog('Failed to schedule appointment');
        throw Exception('Failed to schedule appointment');
      }
    } on DioError catch (e) {
      // Handle error, e.g., show an error message
      showErrorDialog('Failed to schedule appointment:');
      print('Failed to schedule appointment: ${e.message}');
    }
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        FocusScopeNode currentFocus = FocusScope.of(context);
        if (!currentFocus.hasPrimaryFocus) {
          currentFocus.unfocus();
        }
      },
      child: Scaffold(
        bottomNavigationBar: BottomAppBar(
          child: Container(
            height: 70,
            child: ElevatedButton(
              onPressed: () {
                // Your code to handle scheduling
                scheduleAppointment();
              },
              child: Text(
                textAlign: TextAlign.center,
                'Schedule',
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
          backgroundColor: Color.fromRGBO(60, 152, 203, 1),
        ),
        body: FutureBuilder<DetailModel>(
          future: futureDetail,
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.waiting) {
              return Center(child: CircularProgressIndicator());
            } else if (snapshot.hasError) {
              return Center(child: Text('Error: ${snapshot.error}'));
            } else if (snapshot.hasData) {
              final detail = snapshot.data!;
              return Padding(
                padding: const EdgeInsets.all(20.0),
                child: SingleChildScrollView(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Date',
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      SizedBox(height: 10),
                      TableCalendar(
                        focusedDay: _focusedDay,
                        firstDay: DateTime.now(),
                        lastDay: DateTime(2024, 12, 31),
                        calendarFormat: _format,
                        currentDay: _currentDay,
                        headerStyle: HeaderStyle(
                          titleCentered: true,
                          headerMargin: EdgeInsets.only(bottom: 10),
                          titleTextStyle: GoogleFonts.courgette(
                            color: selectedcolor,
                            fontSize: 16,
                          ),
                        ),
                        daysOfWeekStyle: DaysOfWeekStyle(
                          weekdayStyle: TextStyle(color: selectedcolor),
                          weekendStyle: TextStyle(color: Colors.red),
                        ),
                        calendarStyle: CalendarStyle(
                          defaultTextStyle: TextStyle(color: unselectedColor),
                          todayDecoration: BoxDecoration(
                            color: selectedcolor,
                            shape: BoxShape.circle,
                          ),
                          cellMargin: EdgeInsets.only(top: 16),
                          selectedTextStyle: TextStyle(color: Colors.white),
                        ),
                        availableCalendarFormats: const {
                          CalendarFormat.month: 'Month',
                        },
                        enabledDayPredicate: (day) {
                          return availableDates.any(
                                  (availableDay) => isSameDay(availableDay, day));
                        },
                        onFormatChanged: (format) {
                          setState(() {
                            _format = format;
                          });
                        },
                        onDaySelected: (selectedDay, focusedDay) {
                          _onDaySelected(selectedDay, detail);
                        },
                      ),
                      SizedBox(height: 15),
                      Divider(
                          color: Colors.grey,
                          height: 2,
                          endIndent: 5,
                          indent: 5),
                      SizedBox(height: 16),
                      Row(
                        children: [
                          Text(
                            'Time',
                            style: TextStyle(
                                fontSize: 18, fontWeight: FontWeight.bold),
                          ),
                          Spacer(),
                        ],
                      ),
                      SizedBox(height: 15),
                      TimePart(timetext: selectedTimes),
                      SizedBox(height: 15),
                      Divider(
                          color: Colors.grey,
                          height: 2,
                          endIndent: 5,
                          indent: 5),
                      SizedBox(height: 16),
                      Text(
                        'Treatment type',
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      SizedBox(height: 20),
                      if (detail.isHdfSupported && detail.isHdSupported) ...{
                        TreatmentType(
                          types: const ["HD", "HDF"],
                          onSelectionChanged: (value) {
                            print(value);
                            setState(() {
                              treatmentTypeValue = value;
                            });
                          },
                        )
                      } else if (detail.isHdSupported) ...{
                        TreatmentType(
                            types: const ["HD"],
                            onSelectionChanged: (value) {
                              print(value);
                              setState(() {
                                treatmentTypeValue = value;
                              });
                            })
                      } else ...{
                        TreatmentType(
                          types: const ["HDF"],
                          onSelectionChanged: (value) {
                            print(value);
                            setState(() {
                              treatmentTypeValue = value;
                            });
                          },
                        )
                      },
                      SizedBox(height: 15),
                      Divider(
                          color: Colors.grey,
                          height: 2,
                          endIndent: 5,
                          indent: 5),
                      SizedBox(height: 16),
                      Text(
                        'Booking Type',
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      SizedBox(height: 20),
                      Container(
                        padding: EdgeInsets.only(
                          left: 20,
                          right: 20,
                        ),
                        decoration: BoxDecoration(
                          border: Border.all(
                            color: Color.fromRGBO(60, 152, 203, 1),
                            width: 2,
                          ),
                          borderRadius: BorderRadius.circular(15),
                        ),
                        child: DropdownButton<int>(
                          hint: Text('Select booking type'),
                          borderRadius: BorderRadius.circular(15),
                          isExpanded: true,
                          dropdownColor: Colors.white,
                          value: selectedBookingType,
                          icon: Icon(
                            Icons.arrow_drop_down_circle,
                            color: Color.fromRGBO(60, 152, 203, 1),
                          ),
                          underline: SizedBox(),
                          items: bookingTypes.map((type) {
                            return DropdownMenuItem<int>(
                              value: type['id'],
                              child: Text(type['name']),
                            );
                          }).toList(),
                          onChanged: (int? newValue) {
                            setState(() {
                              selectedBookingType = newValue;
                            });
                          },
                        ),
                      ),
                      SizedBox(height: 15),
                      Divider(
                          color: Colors.grey,
                          height: 2,
                          endIndent: 5,
                          indent: 5),
                      SizedBox(height: 16),
                      Text(
                        'Problem details',
                        style: TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      SizedBox(height: 16),
                      TextFormField(
                        controller: _textController,
                        minLines: 2,
                        maxLines: 5,
                        keyboardType: TextInputType.multiline,
                        decoration: InputDecoration(
                          hintText: 'Enter your cause',
                          hintStyle: const TextStyle(color: Colors.grey),
                          border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(10),
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              );
            } else {
              return Center(child: Text('No data available'));
            }
          },
        ),
      ),
    );
  }
}
