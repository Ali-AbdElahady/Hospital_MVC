// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
        $('#patientSearch').on('keyup', function () {
            let searchTerm = $(this).val();

            if (searchTerm.length > 0) {
                $.ajax({
                    url: 'SearchPatients', // API URL for patient search
                    type: 'GET',
                    data: { searchTerm: searchTerm },
                    success: function (data) {
                        // Clear previous results
                        $('#patientResults').empty().show();

                        if (data.length > 0) {
                            // Populate the dropdown list with search results
                            data.forEach(function (patient) {
                                $('#patientResults').append(`<li class="list-group-item" data-id="${patient.id}">${patient.name}</li>`);
                            });
                        } else {
                            $('#patientResults').append('<li class="list-group-item disabled">No results found</li>');
                        }
                    }
                });
            } else {
                $('#patientResults').empty().hide(); // Hide dropdown if input is empty
            }
        });

        // Handle click on a result item
        $(document).on('click', '#patientResults li', function () {
            let selectedPatientName = $(this).text();
            let selectedPatientId = $(this).data('id');

            // Set the input field value to the selected patient's name
            $('#patientSearch').val(selectedPatientName);

            // Optionally, store the selected patient ID somewhere (e.g., hidden field)
            $('#selectedPatientId').val(selectedPatientId);

            // Hide the results dropdown
            $('#patientResults').hide();
        });

        // Hide dropdown if clicking outside of input or result list
        $(document).on('click', function (event) {
            if (!$(event.target).closest('#patientSearch, #patientResults').length) {
                $('#patientResults').hide();
            }
        });
    
    $('#doctorSearch').on('keyup', function () {
        let searchTerm = $(this).val();
        console.log("SearchDoctors")
        if (searchTerm.length > 0) {
            $.ajax({
                url: 'SearchDoctors', // API URL for patient search
                type: 'GET',
                data: { searchTerm: searchTerm },
                success: function (data) {
                    // Clear previous results
                    $('#doctorResults').empty().show();

                    if (data.length > 0) {
                        // Populate the dropdown list with search results
                        data.forEach(function (doctor) {
                            $('#doctorResults').append(`<li class="list-group-item" data-id="${doctor.id}">${doctor.name}</li>`);
                        });
                    } else {
                        $('#doctorResults').append('<li class="list-group-item disabled">No results found</li>');
                    }
                }
            });
        } else {
            $('#doctorResults').empty().hide(); // Hide dropdown if input is empty
        }
    });

    // Handle click on a result item
    $(document).on('click', '#doctorResults li', function () {
        let selectedPatientName = $(this).text();
        let selectedPatientId = $(this).data('id');

        // Set the input field value to the selected patient's name
        $('#doctorSearch').val(selectedPatientName);

        // Optionally, store the selected patient ID somewhere (e.g., hidden field)
        $('#selectedDoctorId').val(selectedPatientId);

        // Hide the results dropdown
        $('#doctorResults').hide();
    });

    // Hide dropdown if clicking outside of input or result list
    $(document).on('click', function (event) {
        if (!$(event.target).closest('#doctorSearch, #doctorResults').length) {
            $('#doctorResults').hide();
        }
    });
    
});