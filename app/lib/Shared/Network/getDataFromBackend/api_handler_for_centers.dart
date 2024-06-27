import 'package:app/models/home_page_center_card/center_card.dart';
import 'package:app/services/token_service.dart';
import 'package:dio/dio.dart';
import '../../components/constant/app_constant.dart';

// https://renocareapi.azurewebsites.net/Api/V1//List-For-Patients?page=1&pageSize=20
class ApiHandler {

  final Dio _dio = Dio();


  Future<List<CenterModel>> getCenterData(int page) async {

    const int pageSize = 20;
    final LoginDataManager2 loginDataManager2=LoginDataManager2();
    final accessToken = await loginDataManager2.getAccessToken();
    print("===============access token from handerl $accessToken===========");

    try {
      final response = await _dio.get(
        '$baseUrl$GetCenterDataEndPoint',
        queryParameters: {
          'page':page,
          'pageSize':pageSize
        },
        options: Options(
          headers:{
            'Authorization' :'Bearer $accessToken',
          },
        ),
      );

      if (response.statusCode == 200) {
        final List<dynamic> data = response.data['data']['items'];
        List<CenterModel> units = data.map((json) => CenterModel.fromJson(json)).toList();
        print('=================response Data =====================x============');
        print(units[1].address);
        print(units[1].name);
        return units;

        return units;

      } else {
        throw Exception("Faild to load data");
      }
    } on DioError catch (e) {
      throw Exception('Faild to load Center Data: ${e.message}');
    }
  }
}
