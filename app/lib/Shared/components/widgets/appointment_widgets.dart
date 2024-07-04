import 'package:app/Shared/components/widgets/commentScreen.dart';
import 'package:app/models/center_model/center_model.dart';
import 'package:app/tabs/appointment.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class AppointmentCard extends StatelessWidget {
  final AppointmentModel appointment;
  const AppointmentCard(this.appointment, {super.key});

  @override
  Widget build(BuildContext context) {
    final formatter = DateFormat.yMMMEd();
    bool activeselect = appointment.category == Category.upcoming ||
        appointment.category == Category.completed;
    bool activeButton = appointment.category == Category.upcoming;
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
                                appointment.dialysisUnitName,
                                style: TextStyle(
                                  fontSize: 16.0,
                                ),
                                overflow: TextOverflow.ellipsis,
                              ),
                            ),
                            if (appointment.category == Category.upcoming ||
                                appointment.category == Category.completed) ...{
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
                                appointment.formattedAddress,
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
                              appointment.category.name,
                              textAlign: TextAlign.end,
                              style: TextStyle(
                                  color:
                                  appointment.category == Category.upcoming
                                      ? Color.fromARGB(255, 199, 183, 38)
                                      : appointment.category ==
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
                  Text(appointment.time),
                  Spacer(),
                  Icon(Icons.calendar_month),
                  Text(appointment.date),
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
                              color: Color.fromARGB(255, 109, 153, 222)),
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(20))),
                      onPressed: () {
                        if(!activeButton)
                          {
                            showModalBottomSheet(
                                isScrollControlled: true,
                                shape: const RoundedRectangleBorder(
                                  borderRadius: BorderRadius.vertical(
                                    top: Radius.circular(20),
                                  ),
                                ),
                                context: context,
                                builder: (context) => CommentScreen());
                          }
                        else
                          {
                            // logic to cancel appointment

                          }


                      },
                      child: Text(
                        activeButton ? 'Cancel' : 'Enter Review',
                        style: TextStyle(
                          fontWeight: FontWeight.bold,
                          color: Color.fromARGB(255, 109, 153, 222),
                        ),
                      )),
                  Spacer(),
                  ElevatedButton(
                      style: OutlinedButton.styleFrom(
                          shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(20))),
                      onPressed: () {},
                      child: Text(
                          activeButton ? 'Reschedule' : 'View Report')),
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
