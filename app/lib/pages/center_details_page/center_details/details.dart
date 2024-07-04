import 'dart:convert';
import 'package:app/pages/book_page/book_page.dart';
import 'package:app/pages/center_details_page/details_page_parts/about_us.dart';
import 'package:app/pages/center_details_page/details_page_parts/amenities.dart';
import 'package:app/pages/center_details_page/details_page_parts/map_part.dart';
import 'package:app/pages/center_details_page/details_page_parts/review_part.dart';
import 'package:app/pages/chat_module/page/chat_home/chat_page.dart';
import 'package:flutter/material.dart';
import 'package:dio/dio.dart';
import 'package:carousel_slider/carousel_controller.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'package:smooth_page_indicator/smooth_page_indicator.dart';
import 'package:shared_preferences/shared_preferences.dart';

class DetailsScreen extends StatefulWidget {
  final int unitId;

  DetailsScreen({required this.unitId, Key? key}) : super(key: key);

  @override
  State<DetailsScreen> createState() => _DetailsScreenState();
}

class _DetailsScreenState extends State<DetailsScreen> {
  String? uId;
  String? name;
  late Future<DetailModel> futureDetail;
  var myCurrentIndex = 0;
  CarouselController carouselController = CarouselController();
  List<String> days = [
    "Saturday",
    "Sunday",
    "Monday",
    "Tuesday",
    "Wednesday",
    "Thursday",
    "Friday",
  ];
  String? selectedDay;
  List<String> selectedTimes = [];

  @override
  void initState() {
    super.initState();
    futureDetail = fetchDetails(widget.unitId);
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
        print(unitId);
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
          return DetailModel.fromJson(detailData);
        } else {
          detailData['reviews'] = [];
          return DetailModel.fromJson(detailData);
        }
      } else {
        throw Exception('Failed to load details');
      }
    } on DioError catch (e) {
      throw Exception('Failed to load details: ${e.message}');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Details Page'),
        backgroundColor: Color.fromRGBO(60, 152, 203, 1),
      ),
      bottomNavigationBar: BottomAppBar(
        child: Container(
          height: 70,
          child: ElevatedButton(
            onPressed: () {
              Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) => BookScreen(unitId: widget.unitId)));
            },
            child: Text(
              'Schedule Appointment',
              style: TextStyle(fontSize: 18),
            ),
            style: ElevatedButton.styleFrom(
              backgroundColor: Color.fromRGBO(60, 152, 203, 1),
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
      floatingActionButton: FloatingActionButton(
        onPressed: () {
          // go to chat

          print("===============================================");
          print("Name : $name");
          print("Udi : $uId");
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) => ChatPage(
                active_chat_Id: uId!,
                chatName: name!,
              ),
            ),
          );
        },
        backgroundColor: Color.fromRGBO(60, 152, 203, 1),
        child: Icon(Icons.message),
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
            name = detail.name;
            uId = detail.uId;
            return SingleChildScrollView(
              child: Padding(
                padding: const EdgeInsets.all(20.0),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    CarouselSlider(
                      items: detail.images.map((image) {
                        return Image.network(image.path);
                      }).toList(),
                      options: CarouselOptions(
                        height: 250.0,
                        enlargeCenterPage: true,
                        enableInfiniteScroll: true,
                        viewportFraction: 0.8,
                        onPageChanged: (index, reason) {
                          setState(() {
                            myCurrentIndex = index;
                          });
                        },
                      ),
                      carouselController: carouselController,
                    ),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        IconButton(
                          onPressed: () => carouselController.previousPage(
                            duration: Duration(milliseconds: 500),
                            curve: Curves.fastOutSlowIn,
                          ),
                          icon: Icon(Icons.navigate_before_rounded),
                          iconSize: 40,
                          color: Color.fromRGBO(60, 152, 203, 1),
                        ),
                        SizedBox(width: 16),
                        AnimatedSmoothIndicator(
                          activeIndex: myCurrentIndex,
                          count: detail.images.length,
                          effect: WormEffect(
                            dotHeight: 10,
                            dotWidth: 10,
                            spacing: 10,
                            dotColor: Colors.grey.shade200,
                            activeDotColor: Color.fromRGBO(60, 152, 203, 1),
                            paintStyle: PaintingStyle.fill,
                          ),
                        ),
                        SizedBox(width: 16),
                        IconButton(
                          onPressed: () => carouselController.nextPage(
                            duration: Duration(milliseconds: 500),
                            curve: Curves.fastOutSlowIn,
                          ),
                          icon: Icon(Icons.navigate_next_rounded),
                          iconSize: 40,
                          color: Color.fromRGBO(60, 152, 203, 1),
                        ),
                      ],
                    ),
                    SizedBox(height: 10),
                    Text(
                      detail.name,
                      style: TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                    SizedBox(height: 10),
                    Row(
                      children: [
                        Icon(Icons.location_on_outlined),
                        SizedBox(width: 5),
                        Text(
                            '${detail.address}, ${detail.country}, ${detail.city}'),
                      ],
                    ),
                    SizedBox(height: 10),
                    Row(
                      children: [
                        Icon(Icons.call),
                        SizedBox(width: 5),
                        Text('${detail.phoneNumber}'),
                      ],
                    ),
                    SizedBox(height: 10),
                    Row(
                      children: [
                        if (detail.isHdSupported && detail.isHdfSupported) ...{
                          Text(
                            'HD \$${detail.hdPrice} - HDF \$${detail.hdfPrice}',
                            style: TextStyle(fontWeight: FontWeight.bold),
                          ),
                        } else if (detail.isHdSupported) ...{
                          Text(
                            'HD \$${detail.hdPrice}',
                            style: TextStyle(fontWeight: FontWeight.bold),
                          ),
                        } else if (detail.isHdfSupported) ...{
                          Text(
                            'HDF \$${detail.hdfPrice}',
                            style: TextStyle(fontWeight: FontWeight.bold),
                          ),
                        },
                        Spacer(),
                        Text(
                          '${detail.rating}',
                          style: TextStyle(fontWeight: FontWeight.bold),
                        ),
                        SizedBox(width: 5),
                        Text(
                          '(${detail.reviewCnt})',
                          style: TextStyle(fontSize: 11),
                        ),
                        SizedBox(width: 5),
                        Icon(
                          Icons.star_border,
                          color: Color.fromRGBO(60, 152, 203, 1),
                        ),
                      ],
                    ),
                    SizedBox(height: 10),
                    Divider(thickness: 2, endIndent: 5, indent: 5),
                    SizedBox(height: 10),
                    AboutUs(
                      description: detail.description,
                      viruses: detail.viruses,
                    ),
                    SizedBox(height: 10),
                    Divider(thickness: 2, endIndent: 5, indent: 5),
                    SizedBox(height: 10),
                    Text(
                      "Days",
                      style: TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                    SizedBox(height: 10),
                    Column(
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
                          child: DropdownButton<String>(
                            hint: Text('Select a day'),
                            borderRadius: BorderRadius.circular(15),
                            isExpanded: true,
                            dropdownColor: Colors.white,
                            value: selectedDay,
                            icon: Icon(
                              Icons.arrow_drop_down_circle,
                              color: Color.fromRGBO(60, 152, 203, 1),
                            ),
                            underline: SizedBox(),
                            items:
                                detail.groupedSessions.keys.map((String day) {
                              return DropdownMenuItem<String>(
                                value: day,
                                child: Text(day),
                              );
                            }).toList(),
                            onChanged: (String? newValue) {
                              setState(() {
                                selectedDay = newValue;
                                selectedTimes = newValue != null
                                    ? detail.groupedSessions[newValue]!
                                    : [];
                              });
                            },
                          ),
                        ),
                        SizedBox(
                          height: 15.0,
                        ),
                        if (selectedDay != null)
                          Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                                'Sessions',
                                style: TextStyle(
                                    fontSize: 18, fontWeight: FontWeight.bold),
                              ),
                              SizedBox(height: 10),
                              Wrap(
                                runSpacing: 16,
                                spacing: 10,
                                children: selectedTimes.map((time) {
                                  return Card(
                                    shape: RoundedRectangleBorder(
                                        borderRadius:
                                            BorderRadius.circular(20)),
                                    color: Color.fromRGBO(60, 152, 203, 1),
                                    child: Padding(
                                      padding: const EdgeInsets.symmetric(
                                          vertical: 10.0, horizontal: 20),
                                      child: Text(
                                        time,
                                        style: TextStyle(color: Colors.white),
                                      ),
                                    ),
                                  );
                                }).toList(),
                              ),
                            ],
                          )
                        else
                          Text(
                            'Please select a day',
                            style: TextStyle(fontSize: 18),
                          ),
                      ],
                    ),
                    SizedBox(height: 10),
                    Divider(thickness: 2, endIndent: 5, indent: 5),
                    SizedBox(height: 10),
                    if (detail.amenities.isNotEmpty) ...{
                      AmenitiesPart(
                        Amenities: detail.amenities,
                      ),
                      SizedBox(height: 10),
                      Divider(thickness: 2, endIndent: 5, indent: 5),
                      SizedBox(height: 10),
                    },
                    MapTest(),
                    SizedBox(height: 10),
                    if (detail.reviews.isNotEmpty)
                      Text(
                        'Reviews',
                        style: TextStyle(
                          fontSize: 18,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    SizedBox(height: 16),
                    Divider(thickness: 2, endIndent: 5, indent: 5),
                    SizedBox(height: 10),
                    for (int i = 0; i < detail.reviews.length; i++)
                      ReviewPart(
                        numOfStars: detail.reviews[i].rating,
                        name: detail.reviews[i].patientName,
                        review: detail.reviews[i].comment,
                        creationDate: detail.reviews[i].creationDate,
                        isEnd: i == detail.reviews.length - 1,
                        isStart: i == 0,
                      )
                  ],
                ),
              ),
            );
          } else {
            return Center(child: Text('No data available'));
          }
        },
      ),
    );
  }
}

class DetailModel {
  final int id;
  final double rating;
  final int reviewCnt;
  final List<String> amenities;
  final List<Session> sessions;
  final List<ImageModel> images;
  final List<String> viruses;
  final String name;
  final String description;
  final String phoneNumber;
  final String address;
  final String country;
  final String city;
  final bool isHdSupported;
  final double hdPrice;
  final bool isHdfSupported;
  final double? hdfPrice;
  final Map<String, List<String>> groupedSessions;
  final List<Review> reviews; // Add this line
  final String uId;
  DetailModel({
    required this.id,
    required this.rating,
    required this.reviewCnt,
    required this.amenities,
    required this.sessions,
    required this.images,
    required this.viruses,
    required this.name,
    required this.description,
    required this.phoneNumber,
    required this.address,
    required this.country,
    required this.city,
    required this.isHdSupported,
    required this.hdPrice,
    required this.isHdfSupported,
    required this.groupedSessions,
    required this.reviews, // Add this line
    this.hdfPrice,
    required this.uId,
  });

  factory DetailModel.fromJson(Map<String, dynamic> json) {
    return DetailModel(
      uId: json['userId'],
      id: json['id'] ?? 0,
      rating: (json['rating'] ?? 0).toDouble(),
      reviewCnt: json['reviewCnt'] ?? 0,
      amenities: (json['amenities'] as List? ?? [])
          .map((amenity) => Amenity.fromJson(amenity).name)
          .toList(),
      sessions: (json['sessions'] as List? ?? [])
          .map((session) => Session.fromJson(session))
          .toList(),
      groupedSessions: (json['groupedSessions'] as Map<String, dynamic>? ?? {})
          .map((key, value) =>
              MapEntry(key, (value as List).map((e) => e as String).toList())),
      images: (json['images'] as List? ?? [])
          .map((image) => ImageModel.fromJson(image))
          .toList(),
      viruses: (json['acceptingViruses'] as List? ?? [])
          .map((virus) => Virus.fromJson(virus).abbreviation)
          .toList(),
      name: json['name'] ?? '',
      description: json['description'] ?? '',
      phoneNumber: json['phoneNumber'] ?? '',
      address: json['address'] ?? '',
      country: json['country'] ?? '',
      city: json['city'] ?? '',
      isHdSupported: json['isHdSupported'] ?? false,
      hdPrice: (json['hdPrice'] ?? 0).toDouble(),
      isHdfSupported: json['isHdfSupported'] ?? false,
      hdfPrice:
          json['hdfPrice'] != null ? (json['hdfPrice'] ?? 0).toDouble() : null,
      reviews: (json['reviews'] as List? ?? [])
          .map((review) => Review.fromJson(review))
          .toList(), // Default to empty list if null
    );
  }
}

class Review {
  final String patientName;
  final String comment;
  final double rating;
  final String creationDate;

  Review({
    required this.patientName,
    required this.comment,
    required this.rating,
    required this.creationDate,
  });

  factory Review.fromJson(Map<String, dynamic> json) {
    return Review(
      patientName: json['patientName'] ?? '',
      comment: json['comment'] ?? '',
      rating: (json['rating'] ?? 0).toDouble(),
      creationDate: json['creationDate'] ?? '',
    );
  }
}

class Amenity {
  final String name;
  final String icon;

  Amenity({required this.name, required this.icon});

  factory Amenity.fromJson(Map<String, dynamic> json) {
    return Amenity(
      name: json['name'] ?? '',
      icon: json['icon'] ?? '',
    );
  }
}

class Session {
  final int day;
  final String time;

  Session({required this.day, required this.time});

  factory Session.fromJson(Map<String, dynamic> json) {
    return Session(
      day: json['day'] ?? 0,
      time: json['time'] ?? '',
    );
  }
}

class ImageModel {
  final String path;

  ImageModel({required this.path});

  factory ImageModel.fromJson(Map<String, dynamic> json) {
    return ImageModel(
      path: json['path'] ?? '',
    );
  }
}

class Virus {
  final String name;
  final String description;
  final String abbreviation;
  final int id;

  Virus({
    required this.name,
    required this.description,
    required this.abbreviation,
    required this.id,
  });

  factory Virus.fromJson(Map<String, dynamic> json) {
    return Virus(
      name: json['name'] ?? '',
      description: json['description'] ?? '',
      abbreviation: json['abbreviation'] ?? '',
      id: (json['id'] ?? 0),
    );
  }
}
