using ProjectTW.BusinessLogic.AppBL;
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
        public readonly IPatient _patient;

        public EventsController() 
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
            _patient = bl.GetPatientBL();
        }
        // GET: Events
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            GlobalData globalData = new GlobalData()
            {
                Email = user.Email,
                FullName = user.FullName,
                Speciality = user.Speciality,
                Biography = user.Biography,
                ProfileImagePath = user.ProfileImagePath,
                Level = user.Level
            };

            if (user.Level == Domain.Enums.UserRole.Patient)
            {
                var allAppointmets = _patient.GetAllPatientAppointments(user.Id);
                List<Appointment> appointments = new List<Appointment>();

                foreach (var appointment in allAppointmets)
                {
                     var Doctor = _session.GetDoctorById(appointment.DoctorId);

                     Appointment appointment_temp = new Appointment()
                     {
                          Id = appointment.Id,
                          FullName = Doctor.FullName,
                          AppointmentDateTime = appointment.AppointmentDate,
                          AppointmentStatus = appointment.AppointmentStatus,
                     };

                     appointments.Add(appointment_temp);
                }

                globalData.AllAppointments = appointments;

                return View(globalData);
            }        
            
            if(user.Level == Domain.Enums.UserRole.Doctor)
            {
                var allAppointmets = _session.GetAllDoctorAppointments(user.Id);
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

                globalData.AllAppointments = appointments;

                return View(globalData);
            }

            return View(globalData);
        }
    }
}