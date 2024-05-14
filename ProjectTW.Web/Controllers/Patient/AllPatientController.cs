using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Web.Extension;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
    public class AllPatientController : Controller
    {
        public readonly IPatient _patient;

        public AllPatientController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _patient = bl.GetPatientBL();
        }

        // GET: AllPatient
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            var patients = _patient.GetAllPatientsData();

            GlobalData globalData = new GlobalData()
            {
                Email = user.Email,
                FullName = user.FullName,
                Speciality = user.Speciality,
                Biography = user.Biography,
                ProfileImagePath = user.ProfileImagePath,
                Level = user.Level,
                PatientList = patients,
            };

            return View(globalData);
        }
    }
}