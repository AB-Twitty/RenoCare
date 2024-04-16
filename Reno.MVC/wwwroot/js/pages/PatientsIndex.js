$('document').ready(function() {
    var table = $('#PatientTable').dataTable({
        pageLength: 25,
        ordering: true,
        scrollX: true,
        ajax: {
            url: "/Patients",
            type: "POST",
            datatype: "json"
        },
        serverSide: true,
        filter: true,
        processing: true,
        language: {
            processing: "Processing, Please Wait..."
        },
        columns: [
            { data: "patientName", name: "PatientName" },
            { data: "reportsSameUnit", name: "ReportsSameUnit" },
            { data: "reportsOverral", name: "ReportsOverral" },
            { data: "gender", name: "Gender" },
            { data: "age", name: "Age" },
            { data: "diabetes", name: "Diabetes" },
            { data: "hypertension", name: "Hypertension" },
            { data: "smoking", name: "Smoking" }
        ]
    });
});