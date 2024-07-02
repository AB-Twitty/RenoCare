enum Category { upcoming, completed, cancelled }

extension CategoryExtension on Category {
  String get name {
    switch (this) {
      case Category.upcoming:
        return 'Upcoming';
      case Category.completed:
        return 'Completed';
      case Category.cancelled:
        return 'Cancelled';
      default:
        return '';
    }
  }

  static Category fromString(String status) {
    switch (status.toLowerCase()) {
      case 'upcoming':
      case 'pending':
        return Category.upcoming;
      case 'completed':
        return Category.completed;
      case 'cancelled':
      case 'rejected':
        return Category.cancelled;
      default:
        return Category.upcoming; // Default case
    }
  }
}

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
