import 'package:dio/dio.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../Shared/components/widgets/appointment_widgets.dart';
import '../models/center_model/center_model.dart';

class Appointment extends StatefulWidget {
  const Appointment({super.key});

  @override
  State<Appointment> createState() {
    return _AppointmentCardState();
  }
}

class _AppointmentCardState extends State<Appointment> {
  List<Category> selectedCategories = [];
  Future<List<AppointmentModel>>? futureAppointments;

  Future<List<AppointmentModel>> fetchAppointments() async {
    final Dio _dio = Dio();
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? token = prefs.getString('token');

    try {
      final response = await _dio.get(
        'https://renocareapi.azurewebsites.net/Api/V1//Medication/Request/For-Patient',
        options: Options(
          headers: {
            'Authorization': 'Bearer $token',
          },
        ),
      );

      if (response.statusCode == 200) {
        List<dynamic> data = response.data['data'];
        return data.map((item) => AppointmentModel.fromJson(item)).toList();
      } else {
        throw Exception('Failed to load appointments');
      }
    } on DioError catch (e) {
      throw Exception('Failed to load appointments: ${e.message}');
    }
  }

  void refreshAppointments() {
    setState(() {
      futureAppointments = fetchAppointments();
    });
  }

  @override
  void initState() {
    super.initState();
    futureAppointments = fetchAppointments();
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        body: Padding(
          padding: const EdgeInsets.symmetric(vertical: 20.0, horizontal: 16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Appointment',
                style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.bold,
                ),
              ),
              SizedBox(
                height: 13,
              ),
              Card(
                shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(20.0)),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                  children: Category.values
                      .map((category) => FilterChip(
                          backgroundColor: Color.fromARGB(255, 65, 171, 246),
                          selected: selectedCategories.contains(category),
                          label: Text(
                            category.name,
                            style: TextStyle(color: Colors.white),
                          ),
                          onSelected: (selected) {
                            setState(() {
                              if (selected) {
                                selectedCategories.add(category);
                              } else {
                                selectedCategories.remove(category);
                              }
                            });
                          }))
                      .toList(),
                ),
              ),
              SizedBox(
                height: 13,
              ),
              Expanded(
                child: FutureBuilder<List<AppointmentModel>>(
                  future: futureAppointments,
                  builder: (context, snapshot) {
                    if (snapshot.connectionState == ConnectionState.waiting) {
                      return Center(child: CircularProgressIndicator());
                    } else if (snapshot.hasError) {
                      return Center(child: Text('Error: ${snapshot.error}'));
                    } else if (snapshot.hasData) {
                      final appointments = snapshot.data!;
                      final filteredAppointments =
                          appointments.where((appointment) {
                        return selectedCategories.isEmpty ||
                            selectedCategories.contains(appointment.category);
                      }).toList();
                      return ListView.builder(
                        scrollDirection: Axis.vertical,
                        shrinkWrap: true,
                        itemCount: filteredAppointments.length,
                        itemBuilder: ((context, index) {
                          final appointment = filteredAppointments[index];
                          return AppointmentCard(
                            appointment,
                            fetchAppointments: refreshAppointments,
                          );
                        }),
                      );
                    } else {
                      return Center(child: Text('No data available'));
                    }
                  },
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}

class AppointmentModel {
  final int id;
  final int unitId;
  final String dialysisUnitName;
  final String status;
  final String time;
  final String date;
  final String formattedAddress;
  final int? reportId;
  final Category category;

  AppointmentModel({
    required this.id,
    required this.unitId,
    required this.dialysisUnitName,
    required this.status,
    required this.time,
    required this.date,
    required this.formattedAddress,
    this.reportId,
    required this.category,
  });

  factory AppointmentModel.fromJson(Map<String, dynamic> json) {
    return AppointmentModel(
      id: json['id'],
      unitId: json['unitId'],
      dialysisUnitName: json['dialysisUnitName'],
      status: json['status'],
      time: json['time'],
      date: json['date'],
      formattedAddress: json['formattedAdress'],
      reportId: json['reportId'],
      category: CategoryExtension.fromString(json['status']),
    );
  }
}
