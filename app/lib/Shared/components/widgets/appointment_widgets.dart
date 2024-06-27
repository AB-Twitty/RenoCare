import 'package:app/Shared/components/widgets/commentScreen.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

import '../../../models/center_model/center_model.dart';

class cardLists extends StatelessWidget {
  cardLists(this.Hospitals, {super.key});
  hospitalsClass Hospitals;
  @override
  Widget build(BuildContext context) {
    final formatter = DateFormat.yMMMEd();
    bool activeselect = Hospitals.category == Category.upcoming ||
        Hospitals.category == Category.completed;
    bool activeButton = Hospitals.category == Category.upcoming;
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
                                Hospitals.name,
                                style: TextStyle(
                                  fontSize: 16.0,
                                ),
                                overflow: TextOverflow.ellipsis,
                              ),
                            ),
                            Icon(
                              color: Color.fromARGB(255, 109, 153, 222),
                              size: 30.0,
                              IconData(0xe5c9, fontFamily: 'MaterialIcons'),
                            ),
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
                                Hospitals.address,
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
                              Hospitals.category.name,
                              textAlign: TextAlign.end,
                              style: TextStyle(
                                  color: Hospitals.category == Category.upcoming
                                      ? Color.fromARGB(255, 199, 183, 38)
                                      : Hospitals.category == Category.cancelled
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
                  Text('10:00 am'),
                  Spacer(),
                  Icon(Icons.calendar_month),
                  Text(formatter.format(DateTime.now())),
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
                              showModalBottomSheet(
                                  shape: const RoundedRectangleBorder(
                                    borderRadius: BorderRadius.vertical(
                                      top: Radius.circular(20),
                                    ),
                                  ),
                                  context: context,
                                  builder: (context) => CommentScreen());
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
                                activeButton ? 'Reshedule' : 'View Report')),
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
