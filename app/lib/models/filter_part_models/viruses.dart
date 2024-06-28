class Viruses{
  final int id;
  final String abbreviation;
  Viruses({

    required this.id,
    required  this.abbreviation,
  });

  factory Viruses.fromJson(Map<String,dynamic>json){
    return Viruses(
      abbreviation: json['abbreviation'],
      id: json['id'],
    );
  }
}