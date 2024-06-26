class CenterModel {
  final int id;
  final String name;
  final String address;
  final String country;
  final String city;
  final String contactNumber;
  final double rating;
  final int reviewsCnt;
  final bool isHdSupported;
  final double hdPrice;
  final bool isHdfSupported;
  final double hdfPrice;
  final String thumbnailImage;

  CenterModel({
    required this.id,
    required this.name,
    required this.address,
    required this.country,
    required this.city,
    required this.contactNumber,
    required this.rating,
    required this.reviewsCnt,
    required this.isHdSupported,
    required this.hdPrice,
    required this.isHdfSupported,
    required this.hdfPrice,
    required this.thumbnailImage,
  });

  factory CenterModel.fromJson(Map<String, dynamic> json) {
    return CenterModel(
      id: json['id'],
      name: json['name'],
      address: json['address'],
      country: json['country'],
      city: json['city'],
      contactNumber: json['contactNumber'],
      rating: json['rating'],
      reviewsCnt: json['reviewsCnt'],
      isHdSupported: json['isHdSupported'],
      hdPrice: json['hdPrice'],
      isHdfSupported: json['isHdfSupported'],
      hdfPrice: json['hdfPrice'],
      thumbnailImage: json['thumbnailImage'],
    );
  }
}
