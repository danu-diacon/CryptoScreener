document.getElementById('doctorForm').addEventListener('submit', function (event) {
    event.preventDefault();

    var form = document.getElementById('doctorForm');
    var formData = new FormData(form);

    // Validare form
    var fullName = formData.get('DoctorAddFullName');
    var email = formData.get('DoctorAddEmail');
    var password = formData.get('DoctorAddPassword');
    var speciality = formData.get('DoctorAddSpeciality');
    var biography = formData.get('DoctorAddBiography');
    var profileImage = formData.get('DoctorAddProfileImage');

    if (!fullName || !email || !password || !speciality || !biography || !profileImage) {
        Swal.fire({
            title: "Error!",
            text: "Please fill in all the fields.",
            icon: "error",
            confirmButtonClass: "btn btn-confirm mt-2"
        });
        return;
    }

    fetch('@Url.Action("AddNewDoctor", "DoctorAdd")', { // Modificarea URL-ului aici
        method: 'POST',
        headers: {
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        },
        body: formData
    }).then(response => response.json())
        .then(data => {
            if (data.success) {
                Swal.fire({
                    title: "Doctor added successfully!",
                    text: "The doctor has been added.",
                    icon: "success",
                    confirmButtonClass: "btn btn-confirm mt-2"
                }).then(() => {
                    form.reset();  // Golește câmpurile formularului
                });
            } else {
                Swal.fire({
                    title: "Error!",
                    text: data.message,
                    icon: "error",
                    confirmButtonClass: "btn btn-confirm mt-2"
                });
            }
        }).catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            Swal.fire({
                title: "Error!",
                text: "There was a problem with the fetch operation.",
                icon: "error",
                confirmButtonClass: "btn btn-confirm mt-2"
            });
        });
});