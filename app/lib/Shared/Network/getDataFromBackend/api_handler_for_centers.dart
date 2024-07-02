import 'package:app/Shared/components/constant/app_constant.dart';
import 'package:app/models/home_page_center_card/CenterModel.dart';
import 'package:dio/dio.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ApiHandler {
  final Dio _dio = Dio();

  Future<CenterModel> getCenterData(
      int page, Map<String, dynamic> filters) async {
    const int pageSize = 10;
    SharedPreferences prefs = await SharedPreferences.getInstance();
    String? token = prefs.getString('token');

    Map<String, dynamic> queryParams = {
      'page': page,
      'pageSize': pageSize,
      ...filters,
    };

    try {
      final response = await _dio.get(
        '$baseUrl$GetCenterDataEndPoint',
        queryParameters: queryParams,
        options: Options(
          headers: {
            'Authorization': 'Bearer $token',
          },
        ),
      );

      if (response.statusCode == 200) {
        final data = CenterModel.fromJson(response.data);
        return data;
      } else {
        throw Exception("Failed to load data");
      }
    } on DioError catch (e) {
      throw Exception('Failed to load Center Data: ${e.message}');
    }
  }
}
