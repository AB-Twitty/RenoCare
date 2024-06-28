import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
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
  List<String> selectedCategories = [];
  @override
  Widget build(BuildContext context) {
    final filteredCategories = hospitalsClass.hospitals.where((product) {
      return selectedCategories.isEmpty ||
          selectedCategories.contains(product.category.name);
    }).toList();

    return Scaffold(
        body: Padding(
      padding: const EdgeInsets.symmetric(vertical: 40.0, horizontal: 16.0),
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
            height: 10,
          ),
          Card(
            shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(20.0)),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: Category.values
                  .map((category) => FilterChip(
                      backgroundColor: Color.fromARGB(255, 65, 171, 246),
                      selected: selectedCategories.contains(category.name),
                      label: Text(
                        category.name,
                        style: TextStyle(color: Colors.white),
                      ),
                      onSelected: (selected) {
                        setState(() {
                          if (selected) {
                            selectedCategories.add(category.name);
                          } else {
                            selectedCategories.remove(category.name);
                          }
                        });
                      }))
                  .toList(),
            ),
          ),
          Expanded(
            child: ListView.builder(
              scrollDirection: Axis.vertical,
              shrinkWrap: true,
              itemCount: filteredCategories.length,
              itemBuilder: ((context, index) {
                final product = filteredCategories[index];
                return cardLists(product);
              }),
            ),
          )
        ],
      ),
    ));
  }
}
