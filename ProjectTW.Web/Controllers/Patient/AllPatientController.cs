using ProjectTW.Web.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
    public class AllPatientController : Controller
    {
        // GET: AllPatient
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            return View(user);
        }
    }
}