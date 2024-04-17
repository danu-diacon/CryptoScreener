using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Domain.Enums;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProjectTW.Web.Controllers
{
    public class RegisterController : Controller
    {
        public readonly IRegister _register;
        public readonly ISession _session;

        public RegisterController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _register = bl.GetRegisterBL();

            _session = bl.GetSessionBL();
        }

        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(NewUserData register)
        {
            if(ModelState.IsValid)
            {
                UserRegisterData userRegisterData = new UserRegisterData()
                {
                    Email = register.Email,
                    FullName = register.FullName,
                    Password = register.Password,
                    Ip = Request.UserHostAddress,
                    LoginTime = DateTime.Now,
                    Level = UserRole.Doctor
                };

                var userRegister = _register.UserRegisterAction(userRegisterData);
                if(userRegister.Success)
                {
                    HttpCookie cookie = _session.GenCookie(register.Email);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userRegister.Message);
                    return View();
                }
            }

            return View();
        }
    }
}