import 'package:date_picker_timeline/date_picker_timeline.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:group_button/group_button.dart';

import 'filter/divider.dart';
import 'filter/groupButton.dart';

class FilterDrawer extends StatefulWidget {
  @override
  State<FilterDrawer> createState() => _FilterDrawerState();
}

class _FilterDrawerState extends State<FilterDrawer> {
  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 20.0, vertical: 30.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                IconButton(
                  icon: const Icon(
                    Icons.cancel,
                    size: 33,
                    color: Colors.grey,
                  ),
                  onPressed: () {
                    Navigator.pop(context);
                  },
                ),
                const Text(
                  'Filter by',
                  style: TextStyle(
                    fontSize: 20,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                TextButton(
                    onPressed: () {},
                    child: const Text(
                      'Clear',
                      style: TextStyle(fontSize: 20),
                    ))
              ],
            ),
            const RepeatedDivider(text: 'Treatment type'),
            Center(
              child: const GroupButton(
                options: GroupButtonOptions(
                    mainGroupAlignment: MainGroupAlignment.center,
                    crossGroupAlignment: CrossGroupAlignment.center,
                    spacing: 0,
                    buttonWidth: 110,
                    buttonHeight: 50,
                    selectedBorderColor: Colors.black54,
                    unselectedBorderColor: Colors.black54,
                    selectedColor: Color.fromRGBO(60, 152, 203, 1)),
                buttons: ["All types", "HD", "HDF"],
              ),
            ),
            const SizedBox(
              height: 10,
            ),
            const RepeatedDivider(text: 'Accepts patients with'),
            RepeatedGroupButton(
              content: const ["HIV", "Heaptitis B", "Hepatitis C"],
            ),
            const SizedBox(
              height: 10,
            ),
            const RepeatedDivider(text: 'Amenities'),
            GroupButton(
              isRadio: false,
              options: GroupButtonOptions(
                mainGroupAlignment: MainGroupAlignment.start,
                crossGroupAlignment: CrossGroupAlignment.start,
                spacing: 20,
                textPadding: const EdgeInsets.symmetric(horizontal: 20),
                buttonHeight: 40,
                selectedBorderColor: Colors.black54,
                unselectedBorderColor: Colors.black54,
                borderRadius: BorderRadius.circular(20),
                selectedColor: Color.fromRGBO(60, 152, 203, 1),
                unselectedTextStyle: TextStyle(
                  color: Colors.grey[600],
                ),
              ),
              buttons: const [
                "Refreshments",
                "WIFI",
                "TV screens",
                "Free parking"
              ],
            ),
            const SizedBox(
              height: 10,
            ),
            const RepeatedDivider(text: 'Days'),
            Container(
              child: DatePicker(
                DateTime.now(),
                height: 100,
                width: 80,
                initialSelectedDate: DateTime.now(),
                selectionColor: Color.fromRGBO(60, 152, 203, 1),
                dateTextStyle: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.w600,
                  color: Colors.grey,
                ),
              ),
            ),
            const SizedBox(
              height: 10,
            ),
            const Divider(
              indent: 5,
              endIndent: 5,
              thickness: 2,
            ),
            const SizedBox(
              height: 10,
            ),
            SizedBox(
              width: double.infinity,
              child: ElevatedButton(
                onPressed: () {},
                child: Text(
                  'Apply',
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    fontSize: 18,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                style: ElevatedButton.styleFrom(
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(20),
                    ),
                    backgroundColor: Color.fromRGBO(60, 152, 203, 1),
                    padding: EdgeInsets.all(20)),
              ),
            )
          ],
        ),
      ),
    );
  }
}
