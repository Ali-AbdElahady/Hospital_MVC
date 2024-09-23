$('#Apppointment_Stepper #hospitalSelect').change(function () {
    var hospitalId = $(this).val();
    $('#Apppointment_Stepper #departmentSelect').empty().append('<option value="">Select Department</option>');
    $('#Apppointment_Stepper #doctorSelect').empty().append('<option value="">Select Doctor</option>');

    if (hospitalId) {


        $.ajax({
            url: 'GetDepartments',
            type: 'GET', // HTTP method
            data: { hospitalId: hospitalId }, // Data sent to the server
            dataType: 'json', // Expected response type
            success: function (data) {
                // Handle success: Populate the department dropdown
                $.each(data, function (index, department) {
                    $('#Apppointment_Stepper #departmentSelect').append('<option value="' + department.departmentId + '">' + department.department_Name + '</option>');
                });
                $('#Apppointment_Stepper #departmentSection').show(); // Show the department section
            },
            error: function (xhr, status, error) {
                // Handle errors if the request fails
                console.error("An error occurred while fetching departments: ", error);
                alert('Failed to retrieve departments. Please try again.');
            }
        });

    } else {
        $('#Apppointment_Stepper #departmentSection').hide();
        $('#Apppointment_Stepper #doctorSection').hide();
    }
});

$('#Apppointment_Stepper #departmentSelect').change(function () {
    var departmentId = $(this).val();
    $('#Apppointment_Stepper #doctorSelect').empty().append('<option value="">Select Doctor</option>');

    if (departmentId) {
        $.ajax({
            url: 'GetDoctors',        // URL of the endpoint
            type: 'GET',              // Request type
            dataType: 'json',         // Expecting JSON data in return
            data: { departmentId: departmentId },  // Data sent to the server
            success: function (data) {
                $('#doctorSelect').empty();
                $.each(data, function (index, doctor) {
                    $('#doctorSelect').append('<option value="' + doctor.id + '">' + doctor.name + '</option>');
                });
                $('#doctorSection').show();
            },
            error: function (xhr, status, error) {
                console.error('Error fetching doctors:', error);
                // Handle errors here if necessary (e.g., showing a message to the user)
            }
        });

    } else {
        $('#doctorSection').hide();
    }
});

$('#Apppointment_Stepper #doctorSelect').change(function () {
    if ($(this).val()) {
        $('#Apppointment_Stepper #submitAppointment').show();
    } else {
        $('#Apppointment_Stepper #submitAppointment').hide();
    }
});

$('#Apppointment_Stepper #submitAppointment').click(function () {
    var appointment = {
        DoctorID: $('#Apppointment_Stepper #doctorSelect').val(),
        DepartmentId: $('#Apppointment_Stepper #departmentSelect').val(),
        HospitalId: $('#Apppointment_Stepper #hospitalSelect').val(),
        // Add more fields as necessary (e.g., PatientId, Date)
    };

    $.post('@Url.Action("CreateAppointment", "Appointment")', appointment, function (response) {
        // Handle success (e.g., redirect or show message)
    });
});