using ProjectTW.BusinessLogic.Interfaces;
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
    public class AllDoctorsController : BaseController
    {
        public readonly IDoctor _doctor;

        public AllDoctorsController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _doctor = bl.GetDoctorBL();
        }

        // GET: AllDoctors
        [AdminMod]
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();

            var all_doctors = _doctor.GetAllDoctorsData();

            GlobalData globalData = new GlobalData()
            {
                Email = user.Email,
                FullName = user.FullName,
                Speciality = user.Speciality,
                Biography = user.Biography,
                ProfileImagePath = user.ProfileImagePath,
                Level = user.Level,
                DoctorList = all_doctors,
            };

            return View(globalData);
        }
    }
}