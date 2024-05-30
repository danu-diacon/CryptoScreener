using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Web.ActionFilters;
using ProjectTW.Web.Extension;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ProjectTW.Web.Controllers
{
    public class HospitalReportsController : BaseController
    {
        public readonly IAdmin _admin;

        public HospitalReportsController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _admin = bl.GetAdminBL();
        }

        // GET: HospitalReports
        [AdminMod]
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            //Get data about Doctors and Patients
            var hospitalData = _admin.GetDoctorAndPatientNumber();

            var data = new[]
            {
                new { label = "Nr.Doctors", value = hospitalData.DoctorCount },
                new { label = "Nr.Patients", value = hospitalData.PatientCount }
            };

            var serializer = new JavaScriptSerializer();
            string jsonData = serializer.Serialize(data);
            ViewData["ChartData"] = jsonData;

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
    }
}