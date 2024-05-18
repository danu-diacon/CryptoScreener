using ProjectTW.BusinessLogic.AppBL;
using ProjectTW.BusinessLogic.DBModel;
using ProjectTW.Domain.Entities.Response;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Domain.Enums;
using ProjectTW.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectTW.BusinessLogic.Core
{
     public class UserAPI
     {
        public UserLoginResponse LoginResponse(UserLoginData loginData)
        {
            PatientDbTable resultPatient;
            DoctorDbTable resultDoctor;
            AdminDbTable resultAdmin;

            var HashPassword = LoginHelper.HashGen(loginData.Password);

            //Search in Patient Table
            using (var dbPatient = new PatientContext())
            {
                resultPatient = dbPatient.Patients.FirstOrDefault(u => u.Email == loginData.Email && u.Password == HashPassword);
            }
            if (resultPatient != null)
            {
                return new UserLoginResponse() { Success = true };
            }

            //Search in Doctor Table
            using (var dbDoctor = new DoctorContext())
            {
                resultDoctor = dbDoctor.Doctors.FirstOrDefault(u => u.Email == loginData.Email && u.Password == HashPassword);
            }
            if (resultDoctor != null)
            {
                return new UserLoginResponse() { Success = true };
            }

            //Search in Admin Table
            using (var dbAdmin = new AdminContext())
            {
                resultAdmin = dbAdmin.Admins.FirstOrDefault(u => u.Email == loginData.Email && u.Password == HashPassword);
            }
            if (resultAdmin != null)
            {
                return new UserLoginResponse() { Success = true };
            }

            
            return new UserLoginResponse() { Success = false, Message = "The Username or Password is Incorrect" };
          }

        public UserRegisterResponse RegisterResponse(UserRegisterData registerData)
        {
            //Check if there are any users in the system
            bool isFirstUser;
            using (var dbPatient = new PatientContext())
            using (var dbDoctor = new DoctorContext())
            using (var dbAdmin = new AdminContext())
            {
                isFirstUser = !dbPatient.Patients.Any() && !dbDoctor.Doctors.Any() && !dbAdmin.Admins.Any();
            }

            if (isFirstUser)
            {
                var AdminHashPassword = LoginHelper.HashGen(registerData.Password);

                //Add first Admin
                var firstAdmin = new AdminDbTable
                {
                    FullName = registerData.FullName,
                    Password = AdminHashPassword,
                    Email = registerData.Email,
                    LastLogin = registerData.LoginTime,
                    Level = UserRole.Admin
                };

                using (var db = new AdminContext())
                {
                    db.Admins.Add(firstAdmin);
                    db.SaveChanges();
                }

                return new UserRegisterResponse { Success = true };
            }

            PatientDbTable resultPatient;
            DoctorDbTable resultDoctor;
            AdminDbTable resultAdmin;

            //Search in Patients Table
            using (var dbPatient = new PatientContext())
            {
                resultPatient = dbPatient.Patients.FirstOrDefault(u => u.Email == registerData.Email);
            }
            if (resultPatient != null)
            {
                return new UserRegisterResponse() { Success = false, Message = "A user with this email already exists" };
            }

            //Search in Doctors Table
            using (var dbDoctor = new DoctorContext())
            {
                resultDoctor = dbDoctor.Doctors.FirstOrDefault(u => u.Email == registerData.Email);
            }
            if (resultDoctor != null)
            {
                return new UserRegisterResponse() { Success = false, Message = "A user with this email already exists" };
            }

            //Search in Admins Table
            using (var dbAdmin = new AdminContext())
            {
                resultAdmin = dbAdmin.Admins.FirstOrDefault(u => u.Email == registerData.Email);
            }
            if (resultAdmin != null)
            {
                return new UserRegisterResponse() { Success = false, Message = "A user with this email already exists" };
            }


            //Add new User Patient
            var HashPassword = LoginHelper.HashGen(registerData.Password);

            var todo = new PatientDbTable
            {
                FullName = registerData.FullName,
                Password = HashPassword,
                Email = registerData.Email,
                Biography = "Nu exista nici o biografie",
                LastLogin = registerData.LoginTime,
                Level = registerData.Level
            };

            using (var db = new PatientContext())
            {
                db.Patients.Add(todo);
                db.SaveChanges();
            } 

            return new UserRegisterResponse { Success = true };
        }

        public HttpCookie Cookie(string loginEmail)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginEmail)
            };

            using (var db = new SessionContext())
            {
                UserSession current;
                var validate = new EmailAddressAttribute();

                current = (from e in db.Sessions where e.Email == loginEmail select e).FirstOrDefault();

                if (current != null)
                {
                    current.CookieString = apiCookie.Value;
                    current.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(current).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new UserSession { Email = loginEmail,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60) });
                    db.SaveChanges();
                }
            }
            return apiCookie;
        }

        public UserMinimal UserCookie(string cookie)
        {
            UserSession session;
            PatientDbTable currentUserPatient;
            DoctorDbTable currentUserDoctor;
            AdminDbTable currentUserAdmin;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) { return null; }

            //Search in Patients Table
            using (var db = new PatientContext())
            {
                currentUserPatient = db.Patients.FirstOrDefault(u => u.Email == session.Email);
            }

            if (currentUserPatient != null)
            {
                var userMinimal = new UserMinimal()
                {
                    Id = currentUserPatient.Id,
                    FullName = currentUserPatient.FullName,
                    Email = currentUserPatient.Email,
                    Biography = currentUserPatient.Biography,
                    LastLogin = currentUserPatient.LastLogin,
                    ProfileImagePath = currentUserPatient.ProfileImage,
                    Level = currentUserPatient.Level

                };
                return userMinimal;
            }

            //Search in Doctors Table
            using (var dbDoctor = new DoctorContext())
            {
                currentUserDoctor = dbDoctor.Doctors.FirstOrDefault(u => u.Email == session.Email);
            }

            if (currentUserDoctor != null)
            {
                var userMinimal = new UserMinimal()
                {
                    Id = currentUserDoctor.Id,
                    FullName = currentUserDoctor.FullName,
                    Email = currentUserDoctor.Email,
                    Speciality = currentUserDoctor.Specilality,
                    Biography = currentUserDoctor.Biography,
                    LastLogin = currentUserDoctor.LastLogin,
                    ProfileImagePath = currentUserDoctor.ProfileImage,
                    Level = currentUserDoctor.Level

                };
                return userMinimal;
            }

            //Search in Admins Table
            using (var dbAdmin = new AdminContext())
            {
                currentUserAdmin = dbAdmin.Admins.FirstOrDefault(u => u.Email == session.Email);
            }

            if (currentUserAdmin != null)
            {
                var userMinimal = new UserMinimal()
                {
                    Id = currentUserAdmin.Id,
                    FullName = currentUserAdmin.FullName,
                    Email = currentUserAdmin.Email,
                    LastLogin = currentUserAdmin.LastLogin,
                    ProfileImagePath = currentUserAdmin.ProfileImage,
                    Level = currentUserAdmin.Level

                };
                return userMinimal;
            }

            return null;
        }

        public bool AddAppointment(NewAppointmentData AppointmentData)
        {
            var NewAppointment = new AppointmentsDbTable
            {
                DoctorId = AppointmentData.DoctorId,
                PatientId = AppointmentData.PatientId,
                AppointmentDate = AppointmentData.AppointmentDate
            };

            using (var db = new AppointmentContext())
            {
                db.Appointments.Add(NewAppointment);
                db.SaveChanges();
            }

            return true;
        }

        public List<AppointmentsDbTable> AllDoctorAppointments(int doctorID)
        {
            using (var db = new AppointmentContext())
            {
                var appointments = db.Appointments.Where(p => p.DoctorId == doctorID && p.AppointmentDate > DateTime.Now).OrderBy(p => p.AppointmentDate).ToList();

                return appointments;
            }
        }

        public List<AppointmentsDbTable> AllPatientAppointments(int patientID)
        {
            using (var db = new AppointmentContext())
            {
                var appointments = db.Appointments.Where(p => p.PatientId == patientID && p.AppointmentDate > DateTime.Now).OrderBy(p => p.AppointmentDate).ToList();

                return appointments;
            }
        }

        public UserMinimal PatientId(int Id)
        {
            using (var db = new PatientContext())
            {
                var resultPatient = db.Patients.FirstOrDefault(e => e.Id == Id);

                if (resultPatient == null)
                {
                    return null;
                }

                UserMinimal userMinimal = new UserMinimal
                {
                    Id = resultPatient.Id,
                    FullName = resultPatient.FullName,
                    Email = resultPatient.Email,
                    Biography = resultPatient.Biography,
                    LastLogin = resultPatient.LastLogin,
                    Level = resultPatient.Level
                };

                return userMinimal;
            } 
        }

          public UserMinimal DoctorId(int Id)
          {
               using (var db = new DoctorContext())
               {
                    var resultDoctor = db.Doctors.FirstOrDefault(e => e.Id == Id);

                    if (resultDoctor == null)
                    {
                         return null;
                    }

                    UserMinimal userMinimal = new UserMinimal
                    {
                         Id = resultDoctor.Id,
                         FullName = resultDoctor.FullName,
                         Email = resultDoctor.Email,
                         Biography = resultDoctor.Biography,
                         LastLogin = resultDoctor.LastLogin,
                         Level = resultDoctor.Level
                    };

                    return userMinimal;
               }
          }

          public List<UserMinimal> DoctorsBySpeciality(DoctorSpeciality doctorSpeciality)
          {
               using(var db = new DoctorContext())
               {
                    var dbDoctors = db.Doctors.Where(p => p.Specilality == doctorSpeciality).ToList();

                    var doctors = dbDoctors.Select(d => new UserMinimal { Id = d.Id, FullName = d.FullName }).ToList();

                    return doctors;
               }
          }

          public List<DateTime> AvailableTimeByDoctorId(NewAppointmentData partitialData)
          {
               List<DateTime> availableTimes = new List<DateTime>();
               DateTime startTime = DateTime.Today.AddHours(9); // Ora de început (de exemplu, 9:00)
               DateTime endTime = DateTime.Today.AddHours(16).AddMinutes(30); // Ora de sfârșit (de exemplu, 16:30)

               while (startTime <= endTime)
               {
                    availableTimes.Add(startTime);
                    startTime = startTime.AddMinutes(30); // Intervalul de 30 de minute
               }

               //Selectez din baza de date
               using (var db = new AppointmentContext())
               {
                   var appointments = db.Appointments.Where(a => a.DoctorId == partitialData.DoctorId && 
                                                                DbFunctions.TruncateTime(a.AppointmentDate) == partitialData.AppointmentDate.Date)
                                                                .Select(a => a.AppointmentDate).ToList();
                   
                   foreach (var appointment in appointments)
                   {
                       availableTimes.RemoveAll(time => time.TimeOfDay == appointment.TimeOfDay);
                   }
               }

               return availableTimes;
          }

          public List<UserMinimal> AllPatientsData()
          {
              using (var db = new PatientContext())
              {
                  var dbPatients = db.Patients.ToList();

                  var patients = dbPatients.Select(p => new UserMinimal()
                  {
                      Id = p.Id,
                      FullName = p.FullName,
                      Biography = p.Biography,
                      ProfileImagePath = p.ProfileImage
                  }).ToList();

                  return patients;
              }
          }
     }
}
