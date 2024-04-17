using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Web.Models;

namespace ProjectTW.Web.Controllers
{
    public class LoginController : Controller
    {
        public readonly ILogin _login;
        public readonly ISession _session;

        public LoginController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _login = bl.GetLoginBL();

            _session = bl.GetSessionBL();
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserData login)
        {
            if(ModelState.IsValid)
            {
                UserLoginData userLoginData = new UserLoginData()
                {
                    Email = login.Email,
                    Password = login.Password,
                    Ip = Request.UserHostAddress,
                    LoginTime = DateTime.Now
                };

                var userLogin = _login.UserLoginAction(userLoginData);
                if(userLogin.Success)
                {
                    HttpCookie cookie = _session.GenCookie(login.Email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.Message);
                    return View();
                }
            }

            return View();
        }
    }
}