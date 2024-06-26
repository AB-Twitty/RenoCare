import 'package:carousel_slider/carousel_slider.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:smooth_page_indicator/smooth_page_indicator.dart';

import '../details_page_parts/about_us.dart';
import '../details_page_parts/amenities.dart';
import '../details_page_parts/map_part.dart';
import '../details_page_parts/review_part.dart';

class DetailsScreen extends StatefulWidget {
  DetailsScreen({super.key});
  @override
  State<DetailsScreen> createState() => _DetailsScreenState();
}

class _DetailsScreenState extends State<DetailsScreen> {
  final mylist = [
    Image.asset('assets/images/center.png'),
    Image.asset('assets/images/center.png'),
    Image.asset('assets/images/center.png'),
  ];
  CarouselController carouselController = CarouselController();
  // void _navigation(BuildContext context) {
  //   Navigator.push(
  //       context, MaterialPageRoute(builder: ((ctx) => HomeScreen())));
  // }

  void _goTobookScreen(BuildContext context) {
    Navigator.pushNamed(context, '/bookScreen');
  }

  final List<String> reviewText = [
    'that is a very good hospital ,doctors are very kind and all people working there doing their job effitintly',
    'that is a very good hospital ,doctors are very kind and all people working there doing their job effitintly',
    'that is a very good hospital ,doctors are very kind and all people working there doing their job effitintly',
    'that is a very good hospital ,doctors are very kind and all people working there doing their job effitintly',
    'that is a very good hospital ,doctors are very kind and all people working there doing their job effitintly',
  ];
  final List<double> numOfStars = [5, 4.5, 4, 3.5, 3];
  final List<String> names = ['Mohamed', 'Youssef', 'Ali', "Ahmed", "Mostafa"];

  final Color color = Color.fromRGBO(60, 152, 203, 1);
  int myCurrentIndex = 0;
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return Scaffold(
      appBar: AppBar(
        title: Text('Details Page'),
        backgroundColor: color,
      ),
      bottomNavigationBar: BottomAppBar(
        child: Container(
          height: 70,
          child: ElevatedButton(
            onPressed: () {
              _goTobookScreen(context);
            },
            child: Text(
              textAlign: TextAlign.center,
              'Shedule Appointment',
              style: TextStyle(fontSize: 18),
            ),
            style: ElevatedButton.styleFrom(
              backgroundColor: color,
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
      floatingActionButton: FloatingActionButton.small(
        onPressed: () {

        },
        backgroundColor: color,
        child: Icon(Icons.message),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              CarouselSlider(
                items: mylist,
                carouselController: carouselController,
                options: CarouselOptions(
                    height: 250.0,
                    enlargeCenterPage: true,
                    enableInfiniteScroll: true,
                    viewportFraction: 0.8,
                    onPageChanged: ((index, reason) {
                      setState(() {
                        myCurrentIndex = index;
                      });
                    })),
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  IconButton(
                    onPressed: () => carouselController.previousPage(
                        duration: const Duration(milliseconds: 500),
                        curve: Curves.fastOutSlowIn),
                    icon: const Icon(Icons.navigate_before_rounded),
                    iconSize: 40,
                    splashRadius: 0.01,
                    color: color,
                  ),
                  const SizedBox(
                    width: 16,
                  ),
                  AnimatedSmoothIndicator(
                    activeIndex: myCurrentIndex,
                    count: mylist.length,
                    effect: WormEffect(
                      dotHeight: 10,
                      dotWidth: 10,
                      spacing: 10,
                      dotColor: Colors.grey.shade200,
                      activeDotColor: color,
                      paintStyle: PaintingStyle.fill,
                    ),
                  ),
                  const SizedBox(
                    width: 16,
                  ),
                  IconButton(
                    onPressed: () => carouselController.nextPage(
                        duration: Duration(milliseconds: 500),
                        curve: Curves.fastOutSlowIn),
                    icon: const Icon(Icons.navigate_next_rounded),
                    iconSize: 40,
                    splashRadius: 0.01,
                    color: color,
                  ),
                ],
              ),
              const SizedBox(
                height: 10,
              ),
              Text(
                'Central of rental-dialysis unit',
                style: TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(
                height: 10,
              ),
              Row(
                children: [
                  Icon(Icons.location_on_outlined),
                  SizedBox(
                    width: 5,
                  ),
                  Text(
                    '123 Main Street, Anytown, USA 12345',
                  ),
                ],
              ),
              const SizedBox(
                height: 10,
              ),
              Row(
                children: [
                  Text(
                    'HD \$340 - HDF \$340',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  Spacer(),
                  Text(
                    '4.5',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  SizedBox(
                    width: 5,
                  ),
                  Text(
                    '(23)',
                    style: TextStyle(
                      fontSize: 11,
                    ),
                  ),
                  SizedBox(
                    width: 5,
                  ),
                  Icon(
                    Icons.star_border,
                    color: color,
                  )
                ],
              ),
              SizedBox(
                height: 10,
              ),
              Divider(
                thickness: 2,
                endIndent: 5,
                indent: 5,
              ),
              SizedBox(
                height: 10,
              ),
              AboutUs(),
              SizedBox(
                height: 10,
              ),
              Divider(
                thickness: 2,
                endIndent: 5,
                indent: 5,
              ),
              SizedBox(
                height: 10,
              ),
              AmenitiesPart(),
              SizedBox(
                height: 10,
              ),
              Divider(
                thickness: 2,
                endIndent: 5,
                indent: 5,
              ),
              SizedBox(
                height: 10,
              ),
              MapTest(),
              SizedBox(
                height: 10,
              ),
              Divider(
                thickness: 2,
                endIndent: 5,
                indent: 5,
              ),
              SizedBox(
                height: 10,
              ),
              const Text(
                'Reviews',
                style: TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(
                height: 16,
              ),
              for (int i = 0; i < names.length; i++)
                ReviewPart(
                  numOfStars: numOfStars[i],
                  name: names[i],
                  review: reviewText[i],
                  isend: i == names.length - 1 ? true : false,
                  istsart: i == 0 ? true : false,
                )
            ],
          ),
        ),
      ),
    );
  }
}
