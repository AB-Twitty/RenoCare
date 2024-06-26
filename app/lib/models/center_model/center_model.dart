enum Category { upcoming, completed, cancelled }

class hospitalsClass {
  final String name;
  final String address;
  final Category category;

  hospitalsClass(this.name, this.address, this.category);

  static List<hospitalsClass> hospitals = [
    hospitalsClass('center of renal-dialysis unit',
        '123 Main Street, Anytown, USA 12345', Category.upcoming),
    hospitalsClass('center of renal-dialysis unit',
        '123 Main Street, Anytown, USA 12345', Category.upcoming),
    hospitalsClass('center of renal-dialysis unit',
        '123 Main Street, Anytown, USA 12345', Category.upcoming),
    hospitalsClass('center of renal-dialysis unit',
        '123 Main Street, Anytown, USA 12345', Category.upcoming),
    hospitalsClass('center of renal-dialysis unit',
        '123 Main Street, Anytown, USA 12345', Category.completed),
    hospitalsClass('center of renal-dialysis unit',
        '123 Main Street, Anytown, USA 12345', Category.completed),
    hospitalsClass('center of renal-dialysis unit',
        '123 Main Street, Anytown, USA 12345', Category.cancelled),
    hospitalsClass('center of renal-dialysis unit',
        '123 Main Street, Anytown, USA 12345', Category.cancelled),
  ];
}
