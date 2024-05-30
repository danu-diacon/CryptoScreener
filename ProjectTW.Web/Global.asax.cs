using ProjectTW.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Optimization;

namespace ProjectTW.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
           AreaRegistration.RegisterAllAreas();
           RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Bundles
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;

            if (httpException != null)
            {
                int errorCode = httpException.GetHttpCode();

                if (errorCode == 404)
                {
                    Response.Clear();
                    Server.ClearError();
                    Response.Redirect("/Error");
                }
            }
        }

    }
}