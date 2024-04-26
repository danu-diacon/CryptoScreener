using ProjectTW.BusinessLogic.DBModel;
using ProjectTW.Domain.Entities.Response;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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
                    Biography = "In the United States, the Ed.D. tends to be granted by the school of education of universities and is a terminal degree in education. Majors within the Ed.D. may include: counseling, curriculum and instruction/curriculum and teaching, educational administration, education policy, educational psychology, educational technology, higher education, human resource development, language/linguistics or leadership. ",
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
                    Specilality = currentUserDoctor.Specilality,
                    Biography = currentUserDoctor.Biography,
                    LastLogin = currentUserDoctor.LastLogin,
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

        public List<AppointmentsDbTable> AllAppointments(int doctorID)
        {
            using (var db = new AppointmentContext())
            {
                var appointments = db.Appointments.Where(p => p.DoctorId == doctorID).ToList();

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
    }
}
