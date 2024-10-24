# RenoCare

RenoCare is an innovative online healthcare platform designed to streamline the hemodialysis experience for patients with end-stage renal disease (ESRD) and healthcare providers. The platform enhances patient mobility and operational efficiency by providing a comprehensive directory of hemodialysis centers, a booking system, secure medical record storage, and real-time communication features.

## Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Demo](#demo)
- [Account Login](#account-login)
- [Technologies](#technologies)
- [License](#license)

## Project Overview

RenoCare is designed to address the challenges faced by hemodialysis patients and healthcare providers. It offers a centralized platform where patients can find and book hemodialysis sessions, transfer medical records, and manage their treatment schedules efficiently. For healthcare providers, it streamlines patient record management and enhances communication.

**Key Objectives:**
- Improve patient mobility and quality of life.
- Enhance operational efficiency for hemodialysis centers.
- Provide a user-friendly interface for both patients and healthcare providers.

## Features

### For Hemodialysis Patients:
- **Search and Booking:** View, filter, and book dialysis sessions based on various criteria.
- **Automated Reminders:** Receive reminders via email or mobile app before scheduled appointments.
- **Session Reports:** Download or print professional PDF reports of dialysis sessions.
- **File Upload:** Share documents and medical records through the chat function.
- **Feedback:** Provide ratings and reviews for hemodialysis centers.
- **Real-Time Communication:** Chat with healthcare providers for updates and support.

### For Hemodialysis Care Providers:
- **Record Management:** Access, filter, and export patient records and session reports.
- **Session Scheduling:** Schedule, modify, and manage dialysis sessions.
- **Patient Communication:** Receive and respond to patient inquiries and feedback.
- **Dashboard:** View statistical data and charts related to patient and session metrics.

### For Application Administrators:
- **Provider Management:** Add and authenticate new healthcare providers.
- **Data Export:** Filter, sort, and export records of patients, dialysis units, and bookings.
- **Dashboard:** Access statistical data and charts for comprehensive management.

## Installation

### Web Application

1. **Clone the Repository:**
    ```bash
    git clone https://github.com/yourusername/renocare.git
    ```
2. **Navigate to the Project Directory:**
    ```bash
    cd renocare
    ```
3. **Install Dependencies:**
    - Ensure that you have [.NET SDK](https://dotnet.microsoft.com/download) installed.
    - Restore NuGet packages:
      ```bash
      dotnet restore
      ```
4. **Run the Application:**
    ```bash
    dotnet run
    ```
   The application will be hosted locally. For deployment, use your preferred hosting service.

### Mobile Application

1. **Download the APK:**
    - [Download RenoCare APK](https://drive.google.com/file/d/1OEBsasn0__JdRGOnOkc-KswENBCZMOAb/view?usp=drive_link)
2. **Install on Android Devices:**
    - Transfer the APK to your device and install it. Make sure to enable installation from unknown sources in your device settings.

## Usage

### Access the Web Application
- Visit: [RenoCare](https://renocare.azurewebsites.net) to access the web application.
- **Login Credentials:**
  - **Admin Account:**
    - **Username:** admin@localhost.com
    - **Password:** 123456
  - **Healthcare Provider Account:**
    - **Username:** pmacnishfm@virginia.edu
    - **Password:** 123456

### Mobile Application
- Open the RenoCare app on your mobile device to start managing your hemodialysis sessions and communicate with healthcare providers.

## Demo

Explore the following demo videos to get a comprehensive understanding of the RenoCare platform:

- **Web Application Demo:**

https://github.com/user-attachments/assets/3d192a34-0607-4ad2-8ad4-058712e32f90
- **Mobile Application Demo:**

https://github.com/user-attachments/assets/988c4398-0214-46f6-a537-d84b63b06065

## Technologies

- **Backend:** ASP.NET Web API, ASP.NET MVC, ASP.NET Identity
- **Frontend:** HTML, CSS, JavaScript, jQuery, Bootstrap
- **Mobile Development:** Flutter
- **Real-Time Communication:** SignalR
- **Email Handling:** FluentEmail
- **Validation:** FluentValidation
- **Data Management:** DataTables, 
- **Design Pattern:** Mediator
- **Data Storage:** .NET Entity Framework, Microsoft SQL Server

## License

RenoCare is licensed under the [GPL-3.0 License](https://opensource.org/licenses/GPL-3.0).
