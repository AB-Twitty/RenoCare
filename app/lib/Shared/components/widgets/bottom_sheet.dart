import 'package:flutter/material.dart';

class SortBottomSheet extends StatefulWidget {
  final Function(String) onSortSelected;

  SortBottomSheet({required this.onSortSelected});

  @override
  State<SortBottomSheet> createState() => _SortBottomSheetState();
}

class _SortBottomSheetState extends State<SortBottomSheet> {
  String? _selectedSortOption;

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 20.0, vertical: 30.0),
        child: Column(
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(
                  "Sorted by",
                  style: TextStyle(
                    fontSize: 16,
                    color: Colors.black,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                IconButton(
                  onPressed: () {
                    Navigator.pop(context);
                  },
                  icon: Icon(Icons.close_outlined),
                ),
              ],
            ),
            SizedBox(height: 25),
            RadioListTile<String>(
              title: const Text(
                'Name',
                style: TextStyle(
                  fontSize: 18,
                ),
              ),
              value: 'name',
              groupValue: _selectedSortOption,
              onChanged: (value) {
                setState(() {
                  _selectedSortOption = value;
                });
              },
            ),
            RadioListTile<String>(
              title: const Text(
                'Rating',
                style: TextStyle(
                  fontSize: 18,
                ),
              ),
              value: 'rating',
              groupValue: _selectedSortOption,
              onChanged: (value) {
                setState(() {
                  _selectedSortOption = value;
                });
              },
            ),
            RadioListTile<String>(
              title: const Text(
                'Price',
                style: TextStyle(
                  fontSize: 18,
                ),
              ),
              value: 'price',
              groupValue: _selectedSortOption,
              onChanged: (value) {
                setState(() {
                  _selectedSortOption = value;
                });
              },
            ),
            SizedBox(
              height: 10,
            ),
            Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                Padding(
                  padding: const EdgeInsets.symmetric(horizontal: 15.0),
                  child: ElevatedButton(
                    style: ElevatedButton.styleFrom(
                        padding: EdgeInsets.symmetric(
                          horizontal: 30,
                          vertical: 15,
                        ),
                        shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(20))),
                    onPressed: () {
                      widget.onSortSelected(_selectedSortOption!);
                      Navigator.pop(context);
                    },
                    child: Text(
                      "Apply",
                      style:
                      TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                    ),
                  ),
                )
              ],
            )
          ],
        ),
      ),
    );
  }
}
