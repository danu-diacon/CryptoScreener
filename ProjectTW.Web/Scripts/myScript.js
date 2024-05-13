$(document).ready(function () {
     // La schimbarea specialității
     $('select[name="Speciality"]').change(function () {
          // Șterge conținutul câmpurilor de mai jos
          $('#timeSelect').empty();
          $('select[name="timeSelect"]').val('');
          $('#doctorSelect').empty();
          $('select[name="doctorSelect"]').val('');
          $('.datepicker').val(''); // Șterge valoarea câmpului de dată

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
     $('select[name="doctorSelect"]').change(function () {
          // Șterge conținutul câmpurilor de mai jos
          $('.datepicker').val(''); // Șterge valoarea câmpului de dată
          $('#timeSelect').val(''); // Șterge valoarea câmpului pentru ora
          $('select[name="timeSelect"]').val('');
          $('#timeSelect').empty();

          // Obține ID-ul doctorului selectat
          var selectedDoctorId = $(this).val();
          // Actualizează DoctorId cu ID-ul doctorului selectat în modelul GlobalData
          $('input[name="doctorSelect"]').val(selectedDoctorId);
     });

     // La selectarea unei date
     $('input[name="dataSelect"]').change(function () {
          // Șterge conținutul câmpurilor de mai jos
          $('select[name="timeSelect"]').val('');
          $('#timeSelect').val(''); // Șterge valoarea câmpului pentru ora
          $('#timeSelect').empty();

          // Obține data selectată
          var selectedDate = $(this).val();

          // Descompune data în componente: lună, zi și an
          var dateComponents = selectedDate.split('/');
          var month = parseInt(dateComponents[0], 10); // Convertiți luna în întreg folosind radixul 10
          var day = parseInt(dateComponents[1], 10); // Convertiți ziua în întreg folosind radixul 10
          var year = parseInt(dateComponents[2], 10); // Convertiți anul în întreg folosind radixul 10

          // Crează un obiect Date în JavaScript
          var jsDate = new Date(year, month - 1, day); // lunile încep de la 0, deci scădem 1 din valoarea lunii

          // Formatează data într-un format corespunzător pentru a fi trimisă la server
          var formattedDate = jsDate.toISOString(); // obține un șir ISO8601: YYYY-MM-DDTHH: mm: ss.sssZ

          $('input[name="dataSelect"]').val(selectedDate)

          // Obține ID-ul doctorului selectat
          var selectedDoctorId = $('select[name="doctorSelect"]').val();


          // Verificăm dacă data și ID-ul doctorului sunt valide
          if (selectedDate && selectedDoctorId != null) {
               // Creăm un obiect GlobalData cu datele selectate
               var globalData = {
                    DoctorId: selectedDoctorId,
                    AppointmentDateTime: formattedDate
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
                         $('#timeSelect').append('<option></option>');

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
     $('select[name="timeSelect"]').change(function () {
          // Obține ora selectată
          var selectedTime = $(this).val();
          // Actualizează ora în modelul GlobalData
          $('select[name="timeSelect"]').val(selectedTime);
     });

     // La apăsarea butonului "Save"
     $('button.btn-primary').click(function () {
          // Obține toate valorile selectate
          var speciality = $('select[name="Speciality"]').val();
          var doctorId = $('select[name="doctorSelect"]').val();
          var selectedDate = $('input[name="dataSelect"]').val();
          var selectedTime = $('select[name="timeSelect"]').val();


          // Validare: asigură-te că toate câmpurile sunt completate
          if (speciality && doctorId && selectedDate && selectedTime) {

               // Descompune data în componente: lună, zi și an
               var dateComponents = selectedDate.split('/');
               var month = parseInt(dateComponents[0], 10); // Convertiți luna în întreg folosind radixul 10
               var day = parseInt(dateComponents[1], 10); // Convertiți ziua în întreg folosind radixul 10
               var year = parseInt(dateComponents[2], 10); // Convertiți anul în întreg folosind radixul 10

               // Descompune ora în componente: ore și minute
               var timeComponents = selectedTime.split(':');
               var hours = parseInt(timeComponents[0], 10); // Convertiți orele în întreg folosind radixul 10
               var minutes = parseInt(timeComponents[1], 10); // Convertiți minutele în întreg folosind radixul 10

               // Crează un obiect Date în JavaScript pentru data selectată
               var jsDate = new Date(year, month - 1, day, hours, minutes); // lunile încep de la 0, deci scădem 1 din valoarea lunii

               // Converteste obiectul Date într-un obiect DateTime compatibil cu modelul global
               var appointmentDateTime = jsDate.toISOString();


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
                         // Șterge conținutul câmpurilor de mai jos
                         $('select[name = "Speciality"]').val('');
                         $('#timeSelect').empty();
                         $('#doctorSelect').empty();
                         $('input[name="doctorSelect"]').val('');
                         $('.datepicker').val('');

                         // Mesaj de confirmare sau alte acțiuni după salvare
                        //alert('Programarea a fost salvată cu succes!');

                        // Afișarea alertei de succes dacă cererea AJAX se încheie cu succes
                        Swal.fire({
                            title: "Programare salvată cu succes!",
                            text: "Vă așteptăm!",
                            icon: "success",
                            confirmButtonClass: "btn btn-confirm mt-2"
                        });
                    },
                    error: function (xhr, status, error) {
                         // În caz de eroare, afișează un mesaj de eroare
                         console.error(error);
                    }
               });
          } else {
               // Dacă nu sunt completate toate câmpurile, afișează un mesaj de eroare
              //alert('Vă rugăm să completați toate câmpurile!');
              Swal.fire({
                  title: "Oops...",
                  text: "Vă rugăm să completați toate câmpurile!",
                  icon: "error",
                  confirmButtonClass: "btn btn-confirm mt-2"
              });

          }
     });
});
