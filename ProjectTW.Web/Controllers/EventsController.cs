using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Web.Extension;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
    public class EventsController : Controller
    {
        public readonly ISession _session;

        public EventsController() 
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        // GET: Events
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            if(user.Level == Domain.Enums.UserRole.Patient)
            {
                NewAppointmentData AppointmentData = new NewAppointmentData()
                {
                    DoctorId = 1,
                    PatientId = user.Id,
                    AppointmentDate = DateTime.Now.AddMinutes(10)
                };

                _session.NewAppointment(AppointmentData);
                return View(user);
            }        
            
            if(user.Level == Domain.Enums.UserRole.Doctor)
            {
                var allAppointmets = _session.GetAllAppointments(user.Id);
                List<Appointment> appointments = new List<Appointment>();

                foreach (var appointment in allAppointmets)
                {
                    var Patient = _session.GetPatientById(appointment.PatientId);

                    Appointment appointment_temp = new Appointment()
                    {
                        Id = appointment.Id,
                        FullName = Patient.FullName,
                        AppointmentDateTime = appointment.AppointmentDate,
                        AppointmentStatus = appointment.AppointmentStatus,
                    };

                    appointments.Add(appointment_temp);
                }

                ViewBag.AllAppointments = appointments;
                return View(user);
            }

            return View(user);
        }
    }
}