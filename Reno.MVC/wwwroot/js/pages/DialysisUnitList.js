﻿$('document').ready(function () {
    var table = $('#units_table').DataTable({
        pageLength: 25,
        dom: '<"html5buttons"B>lrtip',
        ordering: true,
        scrollX: true,
        ajax: {
            url: "/Dialysis/Units",
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
            { data: "name", name: "Name" },
            { data: "healthCareProviderName", name: "HealthCareProviderName" },
            { data: "address", name: "Address" },
            { data: "country", name: "Country" },
            { data: "city", name: "City" },
            { data: "contactNumber", name: "ContacyNumber" },
            { data: "rating", name: "Rating" },
            { data: "hdTreatment", name: "HdTreatment" },
            { data: "hdfTreatment", name: "HdfTreatment" },
            { data: "amenities", name: "Amenities" },
            //{ data: "creationDate", name: "CreationDate" }
        ],
        buttons: [
            'copy', 'excel', 'csv',
            {
                extend: 'pdf',
                orientation: 'landscape',
                pageSize: 'LEGAL'
            },
            {
                extend: 'print',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }

        ],
        "fnInitComplete": function () {
            addSearchControl();
        }
    });

    function addSearchControl() {
        $("#SubmitFilter").on('click', function () {
            // Initialize an empty search object
            var searchObject = {};

            // Iterate over input fields within the form
            $("#FilterPatients input").each(function (index) {
                var inputValue = $(this).val();
                if (inputValue !== "") {
                    // Map input value to the corresponding column name
                    var columnIndex = getColumnIndexForInput(index);
                    searchObject[columnIndex] = inputValue;
                }
            });

            // Apply search filters to the DataTable
            for (var index in searchObject) {
                console.log(index);
                patientTable.column(index).search(searchObject[index]);
            }

            patientTable.draw();
        });
    }


    $("#ResetFilter").on("click", () => {
        patientTable.columns().search("").draw();
    })

    // Helper function to map input index to column name
    function getColumnIndexForInput(index) {
        switch (index) {
            case 0: return '0';
            case 1: return '5';
            case 2: return '6';
            case 3: return '7';
            // Add mappings for other input fields as needed
            default: return ""; // Return an empty string for unmapped inputs
        }
    }
});