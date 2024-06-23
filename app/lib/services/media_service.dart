import 'package:file_picker/file_picker.dart';

class FilePickerUtil {
  Future<PlatformFile?> pickFile() async {
    FilePickerResult? result = await FilePicker.platform.pickFiles();

    if (result != null) {
      return result.files.first;
    } else {
      return null;
    }
  }
}
