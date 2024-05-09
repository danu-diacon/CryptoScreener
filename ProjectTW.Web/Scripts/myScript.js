$(document).ready(function () {
     // La schimbarea specialității
     $('select[name="Speciality"]').change(function () {
          // Șterge conținutul câmpurilor de mai jos
          $('#timeSelect').empty();
          $('textarea#field-6').val('');
          $('.datepicker').val(''); // Șterge valoarea câmpului de dată
          $('input[name="AppointmentDateTime"]').val('');

          // Obține valoarea specialității selectate
          var speciality = $(this).val();

          // Face cererea AJAX către controller pentru a obține lista de medici
          $.ajax({
               type: 'POST',
               url: 'BookAppointment/DoctorsBySpeciality',
               data: { Speciality: speciality },
               dataType: 'json',
               success: function (data) {
                    // Șterge toate opțiunile existente
                    $('#doctorSelect').empty();
                    $('#doctorSelect').append('<option></option>');

                    // Adaugă opțiunile pentru fiecare doctor
                    $.each(data, function (index, doctor) {
                         $('#doctorSelect').append('<option value="' + doctor.Id + '">' + doctor.FullName + '</option>');
                    });
               },
               error: function (xhr, status, error) {
                    // În caz de eroare, afișează un mesaj de eroare
                    console.error(error);
               }
          });
     });

     // La selectarea unui doctor
     $('#doctorSelect').change(function () {
          // Șterge conținutul câmpurilor de mai jos
          $('textarea#field-6').val('');
          $('.datepicker').val(''); // Șterge valoarea câmpului de dată
          $('#timeSelect').val(''); // Șterge valoarea câmpului pentru ora
          $('#timeSelect').empty();
          $('input[name="AppointmentDateTime"]').val('');

          // Obține ID-ul doctorului selectat
          var selectedDoctorId = $(this).val();
          // Actualizează DoctorId cu ID-ul doctorului selectat în modelul GlobalData
          $('input[name="DoctorId"]').val(selectedDoctorId);
     });

     // La selectarea unei date
     $('.datepicker').change(function () {
          // Șterge conținutul câmpurilor de mai jos
          $('#timeSelect').val(''); // Șterge valoarea câmpului pentru ora
          $('#timeSelect').empty();

          // Obține data selectată
          var selectedDate = $(this).val();

          // Obține ID-ul doctorului selectat
          var selectedDoctorId = $('#doctorSelect').val();

          // Verificăm dacă data și ID-ul doctorului sunt valide
          if (selectedDate && selectedDoctorId) {
               // Creăm un obiect GlobalData cu datele selectate
               var globalData = {
                    DoctorId: selectedDoctorId,
                    AppointmentDateTime: selectedDate
               };

               // Apelează metoda AvailableTimeByDoctorId pentru a obține orele disponibile
               $.ajax({
                    type: 'POST',
                    url: 'BookAppointment/AvailableTimeByDoctorId',
                    data: globalData,
                    dataType: 'json',
                    success: function (data) {
                         // Șterge opțiunile existente
                         $('#timeSelect').empty();

                         // Adaugă opțiunile pentru fiecare oră disponibilă
                         $.each(data, function (index, time) {
                              // Extragem numărul de milisecunde din formatul /Date(1714975200000)/
                              var milliseconds = parseInt(time.replace(/\/Date\((\d+)\)\//, '$1'));

                              // Construim un obiect Date utilizând numărul de milisecunde
                              var date = new Date(milliseconds);

                              // Obținem orele și minutele și le convertim în formatul dorit
                              var hours = ('0' + date.getHours()).slice(-2); // Adaugăm zero în față dacă este necesar
                              var minutes = ('0' + date.getMinutes()).slice(-2); // Adaugăm zero în față dacă este necesar

                              // Construim timpul formatat în formatul dorit
                              var formattedTime = hours + ':' + minutes;

// Adăugăm opțiunea formatată în lista de selecție
$('#timeSelect').append('<option>' + formattedTime + '</option>');
                    });
                },
error: function (xhr, status, error) {
     console.error(error);
}
            });
        }
    });


// La selectarea unei ore
$('#timeSelect').change(function () {
     // Obține ora selectată
     var selectedTime = $(this).val();
     // Actualizează ora în modelul GlobalData
     var selectedDate = $('.datepicker').val();
     var selectedDateTime = selectedDate + ' ' + selectedTime;
     $('input[name="AppointmentDateTime"]').val(selectedDateTime);
});

// La apăsarea butonului "Save"
$('button.btn-primary').click(function () {
     // Obține toate valorile selectate
     var speciality = $('select[name="Speciality"]').val();
     var doctorId = $('input[name="DoctorId"]').val();
     var appointmentDateTime = $('input[name="AppointmentDateTime"]').val();

     // Validare: asigură-te că toate câmpurile sunt completate
     if (speciality && doctorId && appointmentDateTime) {
          // Adaugă valorile în modelul GlobalData și trimite-le către server
          var globalData = {
               Speciality: speciality,
               DoctorId: doctorId,
               AppointmentDateTime: appointmentDateTime
          };

          // Face cererea AJAX pentru a salva programarea
          $.ajax({
               type: 'POST',
               url: 'BookAppointment/SaveAppointment',
               data: globalData,
               dataType: 'json',
               success: function (data) {
                    // Mesaj de confirmare sau alte acțiuni după salvare
                    alert('Programarea a fost salvată cu succes!');
               },
               error: function (xhr, status, error) {
                    // În caz de eroare, afișează un mesaj de eroare
                    console.error(error);
               }
          });
     } else {
          // Dacă nu sunt completate toate câmpurile, afișează un mesaj de eroare
          alert('Vă rugăm să completați toate câmpurile!');
     }
});
});