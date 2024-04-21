using ProjectTW.Web.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
           SessionStatus();
           if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
           {
                return RedirectToAction("Index", "Login");
            }
            
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            return View(user);
        }
    }
}