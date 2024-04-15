using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
    public class RegisterController : Controller
    {
        public readonly IRegister _register;

        public RegisterController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _register = bl.GetRegisterBL();
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
            if (ModelState.IsValid)
            {
                UserRegisterData userRegisterData = new UserRegisterData()
                {
                    Email = register.Email,
                    FullName = register.FullName,
                    Password = register.Password,
                    Ip = Request.UserHostAddress,
                    LoginTime = DateTime.Now
                };

                var userRegister = _register.UserRegisterAction(userRegisterData);
                if (userRegister.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }

            return View();
        }
    }
}