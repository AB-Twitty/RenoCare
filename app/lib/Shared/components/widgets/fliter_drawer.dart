import 'package:app/Shared/Network/getDataFromBackend/api_handler_for_centers.dart';
import 'package:app/models/filter_part_models/amenity.dart';
import 'package:app/models/filter_part_models/viruses.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter_xlider/flutter_xlider.dart';
import 'package:group_button/group_button.dart';

import '../../../services/navigation_service.dart';
import '../../Network/filterApi/get_amenity.dart';
import 'filter/divider.dart';
import 'filter/groupButton.dart';

class FilterDrawer extends StatefulWidget {
  int? page;
  @override
  State<FilterDrawer> createState() => _FilterDrawerState();

  FilterDrawer(this.page);
}

class _FilterDrawerState extends State<FilterDrawer> {

  String _selectedTreatmentType = "All types";
  List<String> _selectedPatients = [];
  List<Amenity> _selectedAmenityIds = [];
  List<Amenity> amenities = [];
  List<String> viruses = [];
  String ID="";
  String _selectedPriceRange = "";
  List<String> _selectedShifts = [];
  List<String> selectedVirusIds = [];
  final ApiHandlerGetFilterParts _apiHandlerGetAmenity=ApiHandlerGetFilterParts();
 final NavigationService _navigationService=NavigationService();
final ApiHandler _apiHandler=ApiHandler();
  double _minPrice=0;
  double _maxPrice=500;

  Future<void> _getAmenities()async{

    try{
      List<Amenity> loadedAmenities=await _apiHandlerGetAmenity.GetAmenity();
      setState(() {
        amenities = loadedAmenities;
      });
    }catch(e)
    {
      print(e);
    }
  }

  Future<void>_getViruses()async{
    try{
      List<Viruses> loadedViruses=await _apiHandlerGetAmenity.GetViruses();
      setState(() {
        viruses = loadedViruses.map((i)=>i.abbreviation).toList();
        Map<String, int> virusMap = {for (var v in loadedViruses) v.abbreviation: v.id};
      });
    }catch(e)
    {
      print(e);
    }
  }
  @override
  void initState() {
    super.initState();
    // call
    _getAmenities();
    _getViruses();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      color: Colors.white,
      child: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 20.0, vertical: 40.0),
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
                      onPressed: () {
                        _selectedTreatmentType = "All types";
                        _selectedPatients.clear();
                        _selectedAmenityIds.clear();
                        _selectedPriceRange = "";
                        _selectedShifts.clear();
                      },
                      child: const Text(
                        'Clear',
                        style: TextStyle(fontSize: 20),
                      ))
                ],
              ),
              RepeatedDivider(text: 'Treatment type'),
              Center(
                child:  GroupButton(
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
                  onSelected: (value, index, isSelected) {
                    setState(() {
                      _selectedTreatmentType=["All types", "HD", "HDF"][index];
                    });

                  },
                  isRadio: true,

                ),
              ),
              const SizedBox(
                height: 10,
              ),
               RepeatedDivider(text: 'Accepts patients with'),
              RepeatedGroupButton(
                content: viruses,

              ),
              const SizedBox(
                height: 10,
              ),
              RepeatedDivider(text: 'Amenities'),
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
                buttons: amenities.map((e) => e.name).toList(),
                onSelected: (value, index, isSelected) {
                  //print(value);
                  setState(() {
                    if(isSelected)
                      {
                        _selectedAmenityIds.add(amenities[index]!);


                      }else {
                      _selectedAmenityIds.remove(amenities[index]!);
                    }



                  });
                },
              ),
              const SizedBox(
                height: 10,
              ),
              const RepeatedDivider(text: 'Price Range'),
              FlutterSlider(
                values: [_minPrice, _maxPrice],
                rangeSlider: true,
                max: 500,
                min: 0,
                step:  FlutterSliderStep(step: 1),
                onDragging: (handlerIndex, lowerValue, upperValue) {
                  setState(() {
                    _minPrice = lowerValue;
                    _maxPrice = upperValue;
                  });
                },
                // tooltip: FlutterSliderTooltip(
                //   alwaysShowTooltip: true,
                //   format: (value) {
                //     return '\$${value}';
                //   },
                // ),
              ),
              Text('Price Range: \$${_minPrice.round()} - \$${_maxPrice.round()}'),
              const SizedBox(
                height: 10,
              ),
              const RepeatedDivider(text: 'Shifts'),
              RepeatedGroupButton(
                content:  [
                  "Morning",
                  "Afternoon",
                  "Evening",
                ],

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
                    // for(int i=0;i<_selectedAmenityIds.length;i++)
                    //   {
                    //
                    //     print(_selectedAmenityIds[i].id);
                    //   }

                    final List<int> ids = _selectedAmenityIds.map((amenity) => amenity.id).toList();
                    final String idString = ids.join(',');
                    print(_selectedAmenityIds.length);

                    print('IDs as a comma-separated string: $idString');
                    _apiHandler.getCenterData(amenitis: idString,widget.page!);


                    _navigationService.goBack();

                  },
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
      ),
    );
  }
}
