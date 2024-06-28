import 'package:app/Shared/components/constant/app_constant.dart';
import 'package:app/models/filter_part_models/amenity.dart';
import 'package:app/models/filter_part_models/viruses.dart';
import 'package:dio/dio.dart';

import '../../../services/token_service.dart';

class ApiHandlerGetFilterParts {
  final Dio _dio = Dio();

  Future<List<Amenity>> GetAmenity() async {
    final LoginDataManager2 loginDataManager2 = LoginDataManager2();
    final accessToken = await loginDataManager2.getAccessToken();
    print("===============access token from GetAmenity $accessToken===========");

    final response = await _dio.get(
      "$baseUrl$GetAmenityEndPoint",
      options: Options(
        headers: {
          'Authorization': 'Bearer $accessToken',
        },
      ),
    );
    List<Amenity> amenities = [];
    if (response.statusCode == 200) {
      amenities = (response.data['data'] as List)
          .map((i) => Amenity.fromJson(i))
          .toList();
      return amenities;
    }else{
      throw Exception("Error For loading amnity");
    }

  }


  Future<List<Viruses>> GetViruses()async{
    final LoginDataManager2 loginDataManager2 = LoginDataManager2();
    final accessToken = await loginDataManager2.getAccessToken();
    print("===============access token from GetViruses $accessToken===========");
    final response = await _dio.get(
      "https://renocareapi.azurewebsites.net/Api/V1/Viruses",
      options: Options(
        headers: {
          'Authorization': 'Bearer $accessToken',
        },
      ),
    );


    if (response.statusCode == 200) {
      List<Viruses> viruses = (response.data['data'] as List)
          .map((i) => Viruses.fromJson(i))
          .toList();

      // print(response.data['data']);
      print(viruses[1].abbreviation);
      print(viruses[0].abbreviation);
      return viruses;
    } else {
      throw Exception("Error loading viruses");
    }
  }
}
