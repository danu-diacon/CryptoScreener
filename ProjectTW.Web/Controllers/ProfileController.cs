using ProjectTW.Web.Extension;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            GlobalData globalData = new GlobalData()
            {
                Email = user.Email,
                FullName = user.FullName,
                Speciality = user.Specilality,
                Biography = user.Biography,
                ProfileImagePath = user.ProfileImagePath,
                Level = user.Level
            };

            return View(globalData);
        }
    }
}