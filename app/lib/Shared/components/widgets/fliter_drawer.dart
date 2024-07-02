import 'package:flutter/material.dart';
import 'package:group_button/group_button.dart';
import 'package:dio/dio.dart';

import 'filter/divider.dart';
import 'filter/groupButton.dart';

class FilterDrawer extends StatefulWidget {
  final Function(Map<String, dynamic>) onApplyFilters;
  final Map<String, dynamic> initialFilters;

  FilterDrawer({required this.onApplyFilters, required this.initialFilters});

  @override
  State<FilterDrawer> createState() => _FilterDrawerState();
}

class _FilterDrawerState extends State<FilterDrawer> {
  Color selectedColor = Color.fromRGBO(60, 152, 203, 1);
  String _selectedDay = "All";
  List<String> days = [
    "All",
    "Saturday",
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
  ];

  List<Map<String, dynamic>> amenities = [];
  List<Map<String, dynamic>> viruses = [];

  Map<String, dynamic> selectedFilters = {};
  final GroupButtonController _amenitiesController = GroupButtonController();
  final GroupButtonController _virusesController = GroupButtonController();
  final GroupButtonController _typesController = GroupButtonController();

  @override
  void initState() {
    super.initState();
    fetchAmenities();
    fetchViruses();
    selectedFilters = widget.initialFilters;
    syncControllersWithFilters();
  }

  void syncControllersWithFilters() {
    // Sync types filter
    if (selectedFilters.containsKey('treatment')) {
      _typesController
          .selectIndex(selectedFilters['treatment'] == 'hd' ? 1 : 2);
    } else {
      _typesController.selectIndex(0);
    }

    // Sync amenities filter
    if (selectedFilters.containsKey('amenities')) {
      final selectedAmenitiesIds = selectedFilters['amenities'];
      _amenitiesController.selectIndexes(amenities
          .asMap()
          .entries
          .where((entry) =>
          selectedAmenitiesIds.contains(entry.value['id'].toString()))
          .map((entry) => entry.key)
          .toList());
    }

    // Sync viruses filter
    if (selectedFilters.containsKey('viruses')) {
      final selectedVirusesIds = selectedFilters['viruses'];
      _virusesController.selectIndexes(viruses
          .asMap()
          .entries
          .where((entry) =>
          selectedVirusesIds.contains(entry.value['id'].toString()))
          .map((entry) => entry.key)
          .toList());
    }
  }

  Future<void> fetchAmenities() async {
    try {
      final response = await Dio()
          .get('https://renocareapi.azurewebsites.net/Api/V1/Amenities');
      setState(() {
        amenities = List<Map<String, dynamic>>.from(response.data['data']);
        syncControllersWithFilters();
      });
    } catch (e) {
      print('Error fetching amenities: $e');
    }
  }

  Future<void> fetchViruses() async {
    try {
      final response = await Dio()
          .get('https://renocareapi.azurewebsites.net/Api/V1/Viruses');
      setState(() {
        viruses = List<Map<String, dynamic>>.from(response.data['data']);
        syncControllersWithFilters();
      });
    } catch (e) {
      print('Error fetching viruses: $e');
    }
  }

  void clearFilters() {
    setState(() {
      selectedFilters.clear();
      _selectedDay = "All";
      _amenitiesController.unselectAll();
      _virusesController.unselectAll();
      _typesController.unselectAll();
    });
  }

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 20.0, vertical: 30.0),
        child: Column(
          mainAxisSize: MainAxisSize.min,
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
                  onPressed: clearFilters,
                  child: const Text(
                    'Clear',
                    style: TextStyle(fontSize: 20),
                  ),
                )
              ],
            ),
            const RepeatedDivider(text: 'Treatment type'),
            Center(
              child: GroupButton(
                options: GroupButtonOptions(
                  mainGroupAlignment: MainGroupAlignment.center,
                  crossGroupAlignment: CrossGroupAlignment.center,
                  spacing: 0,
                  buttonWidth: 110,
                  buttonHeight: 50,
                  selectedBorderColor: Colors.black54,
                  unselectedBorderColor: Color.fromARGB(137, 19, 11, 11),
                  selectedColor: selectedColor,
                ),
                controller: _typesController,
                buttons: ["All types", "HD", "HDF"],
                onSelected: (dynamic value, int index, bool isSelected) {
                  setState(() {
                    selectedFilters['treatment'] =
                    index == 0 ? null : ['hd', 'hdf'][index - 1];
                  });
                },
              ),
            ),
            const SizedBox(
              height: 10,
            ),
            const RepeatedDivider(text: 'Accepts patients with'),
            RepeatedGroupButton(
              content: viruses.map((v) => v['name'] as String).toList(),
              onSelected: (selectedViruses) {
                setState(() {
                  selectedFilters['viruses'] = selectedViruses
                      .map((v) => viruses
                      .firstWhere((virus) => virus['name'] == v)['id']
                      .toString())
                      .toList();
                });
              },
              isRadio: true,
              controller: _virusesController,
            ),
            const SizedBox(
              height: 10,
            ),
            RepeatedDivider(text: 'Amenities'),
            RepeatedGroupButton(
              content: amenities.map((a) => a['name'] as String).toList(),
              isRadio: false,
              onSelected: (selectedAmenities) {
                setState(() {
                  selectedFilters['amenities'] = selectedAmenities
                      .map((a) => amenities
                      .firstWhere((amenity) => amenity['name'] == a)['id']
                      .toString())
                      .toList();
                });
              },
              controller: _amenitiesController,
            ),
            const SizedBox(
              height: 10,
            ),
            const RepeatedDivider(text: 'Days'),
            DropDownButtonFilter(
              items: days,
              selectedItem: _selectedDay,
              onChanged: (value) {
                setState(() {
                  _selectedDay = value;
                  selectedFilters['day'] = value.toLowerCase();
                });
              },
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
                onPressed: () {
                  widget.onApplyFilters(selectedFilters);
                  Navigator.pop(context);
                  },
                child: const Text(
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

class DropDownButtonFilter extends StatefulWidget {
  DropDownButtonFilter({
    super.key,
    required this.items,
    required this.selectedItem,
    required this.onChanged,
  });
  final List<String> items;
  String selectedItem;
  final ValueChanged<String> onChanged;

  @override
  State<StatefulWidget> createState() {
    return _DropDownButtonFilterState();
  }
}

class _DropDownButtonFilterState extends State<DropDownButtonFilter> {
  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
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
          child: DropdownButton(
            borderRadius: BorderRadius.circular(15),
            isExpanded: true,
            dropdownColor: Colors.white,
            value: widget.selectedItem,
            icon: Icon(
              Icons.arrow_drop_down_circle,
              color: Color.fromRGBO(60, 152, 203, 1),
            ),
            underline: SizedBox(),
            items: widget.items
                .map(
                  (category) => DropdownMenuItem(
                value: category,
                child: Text(
                  category,
                  style: TextStyle(
                    fontSize: 16,
                  ),
                ),
              ),
            )
                .toList(),
            onChanged: (value) {
              if (value == null) {
                return;
              }
              widget.onChanged(value);
              setState(() {
                widget.selectedItem = value;
              });
            },
          ),
        ),
        SizedBox(
          height: 15.0,
        ),
      ],
    );
  }
}
