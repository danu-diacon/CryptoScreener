using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Web.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectTW.Web.ActionFilters
{
    public class DoctorOrPatientMod : ActionFilterAttribute
    {
        private readonly ISession _session;

        public DoctorOrPatientMod()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
                if (profile != null && (profile.Level == Domain.Enums.UserRole.Doctor || profile.Level == Domain.Enums.UserRole.Patient))
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Error", action = "Index" }));
                }
            }
        }
    }
}