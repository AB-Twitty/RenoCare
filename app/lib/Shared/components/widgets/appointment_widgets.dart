import 'package:app/models/center_model/center_model.dart';
import 'package:app/tabs/appointment.dart';
import 'package:awesome_dialog/awesome_dialog.dart';
import 'package:flutter/material.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:dio/dio.dart';
import 'package:path_provider/path_provider.dart';
import 'dart:io';

class AppointmentCard extends StatefulWidget {
  final AppointmentModel appointment;
  final Function fetchAppointments;
  const AppointmentCard(this.appointment,
      {super.key, required this.fetchAppointments});

  @override
  State<AppointmentCard> createState() => _AppointmentCardState();
}

class _AppointmentCardState extends State<AppointmentCard> {
  void popup() {
    AwesomeDialog(
            context: context,
            dialogType: DialogType.warning,
            title: 'Attention',
            descTextStyle: TextStyle(
              fontSize: 16,
              height: 1.5,
            ),
            desc: "Are you sure that you want to cancel this appointment?",
            btnOkColor: Color.fromRGBO(60, 152, 203, 1),
            buttonsTextStyle: TextStyle(
              fontSize: 18,
            ),
            btnOkOnPress: () {
              ScaffoldMessenger.of(context).showSnackBar(SnackBar(
                content: Text('Appointment cancelled successfully'),
              ));
              // Call fetchAppointments to refresh the list
              widget.fetchAppointments();
            },
            btnCancelOnPress: () {},
            btnOkText: "Yes",
            btnCancelText: "No")
        .show();
  }

  Future<void> cancelAppointment(BuildContext context) async {
    final Dio _dio = Dio();
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? token = prefs.getString('token');

    if (token == null) {
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
        content: Text('No token found. Please login again.'),
      ));
      return;
    }

    String cancelUrl =
        'https://renocareapi.azurewebsites.net/Api/V1/Medication/Request/Status-Update';

    try {
      final response = await _dio.post(
        cancelUrl,
        options: Options(
          headers: {
            'Authorization': 'Bearer $token',
          },
        ),
        data: {"id": widget.appointment.id, "status": "Cancelled"},
      );

      if (response.statusCode == 200) {
        popup();
        // ScaffoldMessenger.of(context).showSnackBar(SnackBar(
        //   content: Text('Appointment cancelled successfully'),
        // ));
        // // Call fetchAppointments to refresh the list
        // widget.fetchAppointments();
      } else {
        ScaffoldMessenger.of(context).showSnackBar(SnackBar(
          duration: const Duration(seconds: 3),
          content: Text('Failed to cancel appointment'),
        ));
      }
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
        duration: const Duration(seconds: 3),
        content: Text('Failed to cancel appointment: $e'),
      ));
    }
  }

  @override
  Widget build(BuildContext context) {
    bool activeselect = widget.appointment.category == Category.upcoming ||
        widget.appointment.category == Category.completed;
    bool activeButton = widget.appointment.category == Category.completed;

    return Padding(
      padding: const EdgeInsets.all(4.0),
      child: Card(
        elevation: 4,
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: Column(
            children: [
              Row(
                children: [
                  Image.asset(
                    'assets/images/Rectangle 175.png',
                    width: 100,
                  ),
                  SizedBox(width: 8.0),
                  Expanded(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.end,
                      children: [
                        Row(
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Expanded(
                              child: Text(
                                widget.appointment.dialysisUnitName,
                                style: TextStyle(
                                  fontSize: 16.0,
                                ),
                                overflow: TextOverflow.ellipsis,
                              ),
                            ),
                            if (widget.appointment.category ==
                                    Category.upcoming ||
                                widget.appointment.category ==
                                    Category.completed) ...{
                              Icon(
                                color: Color.fromARGB(255, 109, 153, 222),
                                size: 30.0,
                                IconData(0xe5c9, fontFamily: 'MaterialIcons'),
                              ),
                            }
                          ],
                        ),
                        SizedBox(
                          height: 5,
                        ),
                        Row(
                          crossAxisAlignment: CrossAxisAlignment.center,
                          children: [
                            Icon(
                              Icons.fmd_good_sharp,
                              size: 20.0,
                            ),
                            SizedBox(width: 4.0),
                            Expanded(
                              child: Text(
                                widget.appointment.formattedAddress,
                                style: TextStyle(fontSize: 10),
                                overflow: TextOverflow.ellipsis,
                              ),
                            ),
                          ],
                        ),
                        Card(
                          color: Color.fromARGB(255, 201, 225, 245),
                          child: Padding(
                            padding: const EdgeInsets.all(7.0),
                            child: Text(
                              widget.appointment.category.name,
                              textAlign: TextAlign.end,
                              style: TextStyle(
                                  color: widget.appointment.category ==
                                          Category.upcoming
                                      ? Color.fromARGB(255, 199, 183, 38)
                                      : widget.appointment.category ==
                                              Category.cancelled
                                          ? Colors.red
                                          : Colors.green),
                            ),
                          ),
                        )
                      ],
                    ),
                  ),
                ],
              ),
              SizedBox(
                height: 10,
              ),
              Row(
                children: [
                  Icon(IconData(0xee2d, fontFamily: 'MaterialIcons')),
                  SizedBox(
                    width: 5,
                  ),
                  Text(widget.appointment.time),
                  Spacer(),
                  Icon(Icons.calendar_month),
                  Text(widget.appointment.date),
                ],
              ),
              SizedBox(
                height: 15,
              ),
              activeselect
                  ? Row(
                      children: [
                        OutlinedButton(
                          style: OutlinedButton.styleFrom(
                            side: BorderSide(
                              color: Color.fromARGB(255, 109, 153, 222),
                            ),
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(20),
                            ),
                          ),
                          onPressed: () {
                            if (activeButton) {
                              // Function to enter review (if exists)
                            } else {
                              cancelAppointment(context);
                            }
                          },
                          child: Text(
                            activeButton ? 'Enter Review' : 'Cancel',
                            style: TextStyle(
                              fontWeight: FontWeight.bold,
                              color: Color.fromARGB(255, 109, 153, 222),
                            ),
                          ),
                        ),
                        Spacer(),
                        if (activeButton) ...{
                          ElevatedButton.icon(
                            icon: Icon(Icons.download),
                            style: OutlinedButton.styleFrom(
                              shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(20),
                              ),
                            ),
                            onPressed: () {
                              // Function to view report (if exists)
                            },
                            label: Text('View Report'),
                          ),
                        }
                      ],
                    )
                  : SizedBox(
                      height: 15,
                    ),
            ],
          ),
        ),
      ),
    );
  }
}
