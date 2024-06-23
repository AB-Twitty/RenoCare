
import 'dart:io';

import 'package:firebase_storage/firebase_storage.dart';
import 'package:cloud_firestore/cloud_firestore.dart';
import 'package:file_picker/file_picker.dart';
class FirebaseUtil {
  final FirebaseStorage _storage = FirebaseStorage.instance;
  final FirebaseFirestore _firestore = FirebaseFirestore.instance;

  Future<String?> uploadFile(PlatformFile file) async {
    try {
      // Create a reference to the location you want to upload the file to
      Reference ref = _storage.ref().child('uploads/${file.name}');

      // Upload the file
      UploadTask uploadTask = ref.putFile(File(file.path!));

      // Wait for the upload to complete
      TaskSnapshot snapshot = await uploadTask;

      // Get the download URL
      String downloadUrl = await snapshot.ref.getDownloadURL();

      // Save file metadata to Firestore
      await _firestore.collection('files').add({
        'name': file.name,
        'url': downloadUrl,
        'createdAt': Timestamp.now(),
      });

      return downloadUrl;
    } catch (e) {
      print('Error uploading file: $e');
      return null;
    }
  }
}