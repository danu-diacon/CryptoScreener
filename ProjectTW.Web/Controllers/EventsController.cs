using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Web.Extension;
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
                _session.NewAppointment(1, user.Id, DateTime.Now.AddMinutes(10));
                return View(user);
            }        
            
            if(user.Level == Domain.Enums.UserRole.Doctor)
            {
                var allAppointmets = _session.GetAllAppointments(user.Id);
                return View(allAppointmets);
            }

            return View(user);
        }
    }
}