class CenterModel {
  CenterModel({
    int? statusCode,
    dynamic meta,
    String? message,
    bool? succeeded,
    Data? data,
    dynamic errors,
  }) {
    _statusCode = statusCode;
    _meta = meta;
    _message = message;
    _succeeded = succeeded;
    _data = data;
    _errors = errors;
  }

  CenterModel.fromJson(dynamic json) {
    _statusCode = json['statusCode'];
    _meta = json['meta'];
    _message = json['message'];
    _succeeded = json['succeeded'];
    _data = json['data'] != null ? Data.fromJson(json['data']) : null;
    _errors = json['errors'];
  }

  int? _statusCode;
  dynamic _meta;
  String? _message;
  bool? _succeeded;
  Data? _data;
  dynamic _errors;

  CenterModel copyWith({
    int? statusCode,
    dynamic meta,
    String? message,
    bool? succeeded,
    Data? data,
    dynamic errors,
  }) => CenterModel(
    statusCode: statusCode ?? _statusCode,
    meta: meta ?? _meta,
    message: message ?? _message,
    succeeded: succeeded ?? _succeeded,
    data: data ?? _data,
    errors: errors ?? _errors,
  );

  int? get statusCode => _statusCode;
  dynamic get meta => _meta;
  String? get message => _message;
  bool? get succeeded => _succeeded;
  Data? get data => _data;
  dynamic get errors => _errors;
}

class Data {
  Data({
    int? indexFrom,
    int? pageIndex,
    int? pageSize,
    int? totalCount,
    int? filterCount,
    int? totalPages,
    List<Items>? items,
    bool? hasPreviousPage,
    bool? hasNextPage,
  }) {
    _indexFrom = indexFrom;
    _pageIndex = pageIndex;
    _pageSize = pageSize;
    _totalCount = totalCount;
    _filterCount = filterCount;
    _totalPages = totalPages;
    _items = items;
    _hasPreviousPage = hasPreviousPage;
    _hasNextPage = hasNextPage;
  }

  Data.fromJson(dynamic json) {
    _indexFrom = json['indexFrom'];
    _pageIndex = json['pageIndex'];
    _pageSize = json['pageSize'];
    _totalCount = json['totalCount'];
    _filterCount = json['filterCount'];
    _totalPages = json['totalPages'];
    if (json['items'] != null) {
      _items = [];
      json['items'].forEach((v) {
        _items?.add(Items.fromJson(v));
      });
    }
    _hasPreviousPage = json['hasPreviousPage'];
    _hasNextPage = json['hasNextPage'];
  }

  int? _indexFrom;
  int? _pageIndex;
  int? _pageSize;
  int? _totalCount;
  int? _filterCount;
  int? _totalPages;
  List<Items>? _items;
  bool? _hasPreviousPage;
  bool? _hasNextPage;

  Data copyWith({
    int? indexFrom,
    int? pageIndex,
    int? pageSize,
    int? totalCount,
    int? filterCount,
    int? totalPages,
    List<Items>? items,
    bool? hasPreviousPage,
    bool? hasNextPage,
  }) => Data(
    indexFrom: indexFrom ?? _indexFrom,
    pageIndex: pageIndex ?? _pageIndex,
    pageSize: pageSize ?? _pageSize,
    totalCount: totalCount ?? _totalCount,
    filterCount: filterCount ?? _filterCount,
    totalPages: totalPages ?? _totalPages,
    items: items ?? _items,
    hasPreviousPage: hasPreviousPage ?? _hasPreviousPage,
    hasNextPage: hasNextPage ?? _hasNextPage,
  );

  int? get indexFrom => _indexFrom;
  int? get pageIndex => _pageIndex;
  int? get pageSize => _pageSize;
  int? get totalCount => _totalCount;
  int? get filterCount => _filterCount;
  int? get totalPages => _totalPages;
  List<Items>? get items => _items;
  bool? get hasPreviousPage => _hasPreviousPage;
  bool? get hasNextPage => _hasNextPage;
}

class Items {
  Items({
    int? id,
    String? name,
    String? address,
    String? country,
    String? city,
    String? contactNumber,
    num? rating, // Changed to num
    int? reviewsCnt,
    bool? isHdSupported,
    num? hdPrice, // Changed to num
    bool? isHdfSupported,
    num? hdfPrice, // Changed to num
    dynamic thumbnailImage,
  }) {
    _id = id;
    _name = name;
    _address = address;
    _country = country;
    _city = city;
    _contactNumber = contactNumber;
    _rating = rating;
    _reviewsCnt = reviewsCnt;
    _isHdSupported = isHdSupported;
    _hdPrice = hdPrice;
    _isHdfSupported = isHdfSupported;
    _hdfPrice = hdfPrice;
    _thumbnailImage = thumbnailImage;
  }

  Items.fromJson(dynamic json) {
    _id = json['id'];
    _name = json['name'];
    _address = json['address'];
    _country = json['country'];
    _city = json['city'];
    _contactNumber = json['contactNumber'];
    _rating = json['rating'];
    _reviewsCnt = json['reviewsCnt'];
    _isHdSupported = json['isHdSupported'];
    _hdPrice = json['hdPrice'];
    _isHdfSupported = json['isHdfSupported'];
    _hdfPrice = json['hdfPrice'];
    _thumbnailImage = json['thumbnailImage'];
  }

  int? _id;
  String? _name;
  String? _address;
  String? _country;
  String? _city;
  String? _contactNumber;
  num? _rating; // Changed to num
  int? _reviewsCnt;
  bool? _isHdSupported;
  num? _hdPrice; // Changed to num
  bool? _isHdfSupported;
  num? _hdfPrice; // Changed to num
  dynamic _thumbnailImage;

  Items copyWith({
    int? id,
    String? name,
    String? address,
    String? country,
    String? city,
    String? contactNumber,
    num? rating, // Changed to num
    int? reviewsCnt,
    bool? isHdSupported,
    num? hdPrice, // Changed to num
    bool? isHdfSupported,
    num? hdfPrice, // Changed to num
    dynamic thumbnailImage,
  }) => Items(
    id: id ?? _id,
    name: name ?? _name,
    address: address ?? _address,
    country: country ?? _country,
    city: city ?? _city,
    contactNumber: contactNumber ?? _contactNumber,
    rating: rating ?? _rating,
    reviewsCnt: reviewsCnt ?? _reviewsCnt,
    isHdSupported: isHdSupported ?? _isHdSupported,
    hdPrice: hdPrice ?? _hdPrice,
    isHdfSupported: isHdfSupported ?? _isHdfSupported,
    hdfPrice: hdfPrice ?? _hdfPrice,
    thumbnailImage: thumbnailImage ?? _thumbnailImage,
  );

  int? get id => _id;
  String? get name => _name;
  String? get address => _address;
  String? get country => _country;
  String? get city => _city;
  String? get contactNumber => _contactNumber;
  num? get rating => _rating; // Changed to num
  int? get reviewsCnt => _reviewsCnt;
  bool? get isHdSupported => _isHdSupported;
  num? get hdPrice => _hdPrice; // Changed to num
  bool? get isHdfSupported => _isHdfSupported;
  num? get hdfPrice => _hdfPrice; // Changed to num
  dynamic get thumbnailImage => _thumbnailImage;
}
