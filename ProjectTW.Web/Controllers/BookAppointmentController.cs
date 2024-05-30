using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Web.ActionFilters;
using ProjectTW.Web.Extension;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
     public class BookAppointmentController : BaseController
     {
          public readonly IPatient _patient;
          public readonly ISession _session;

        public BookAppointmentController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _patient = bl.GetPatientBL();
               _session = bl.GetSessionBL();
          }
        // GET: BookAppointment
        [PatientMod]
        public ActionResult Index()
        {
        SessionStatus();
        if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
        {
            return RedirectToAction("Index", "Login");
        }

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

            return View(globalData);
        }

        [HttpPost]
        public ActionResult DoctorsBySpeciality(GlobalData globalData)
        {
            var doctors = _patient.GetDoctorsBySpeciality(globalData.Speciality);

            return Json(doctors);
        }

        [HttpPost]
        public ActionResult AvailableTimeByDoctorId(GlobalData globalData)
        {
            NewAppointmentData partitialData = new NewAppointmentData()
            {
                DoctorId = globalData.DoctorId,
                AppointmentDate = globalData.AppointmentDateTime
            };

            var availableTime = _patient.GetAvailableTimeByDoctorId(partitialData);

            return Json(availableTime);
        }
        [HttpPost]
        public ActionResult SaveAppointment(GlobalData globalData)
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            NewAppointmentData newAppointmentData = new NewAppointmentData()
            {
                DoctorId = globalData.DoctorId,
                PatientId = user.Id,
                AppointmentDate = globalData.AppointmentDateTime
            };

            _session.NewAppointment(newAppointmentData);

            return Json(newAppointmentData);
        }

     }
}