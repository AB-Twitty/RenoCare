// // dialysis_unit.dart
//
// class CenterModel {
//   final int id;
//   final String name;
//   final String address;
//   final String country;
//   final String city;
//   final String contactNumber;
//   final double rating;
//   final int reviewsCnt;
//   final bool isHdSupported;
//   final double? hdPrice;
//   final bool isHdfSupported;
//   final double? hdfPrice;
//   final String? thumbnailImage;
//
//   CenterModel({
//     required this.id,
//     required this.name,
//     required this.address,
//     required this.country,
//     required this.city,
//     required this.contactNumber,
//     required this.rating,
//     required this.reviewsCnt,
//     required this.isHdSupported,
//     this.hdPrice,
//     required this.isHdfSupported,
//     this.hdfPrice,
//     this.thumbnailImage,
//   });
//
//   factory CenterModel.fromJson(Map<String, dynamic> json) {
//     return CenterModel(
//       id: json['id'] ?? 0, // Provide default values if fields are null
//       name: json['name'] ?? '',
//       address: json['address'] ?? '',
//       country: json['country'] ?? '',
//       city: json['city'] ?? '',
//       contactNumber: json['contactNumber'] ?? '',
//       rating: json['rating'] != null ? json['rating'].toDouble() : 0.0,
//       reviewsCnt: json['reviewsCnt'] ?? 0,
//       isHdSupported: json['isHdSupported'] ?? false,
//       hdPrice: json['hdPrice'] != null ? json['hdPrice'].toDouble() : 100,
//       isHdfSupported: json['isHdfSupported'] ?? false,
//       hdfPrice: json['hdfPrice'] != null ? json['hdfPrice'].toDouble() : 100,
//       thumbnailImage: json['thumbnailImage'],
//     );
//   }
// }
