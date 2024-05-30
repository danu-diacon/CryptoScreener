using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Web.Extension;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
    public class SettingsController : BaseController
    {
        public readonly ISession _session;

        public SettingsController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }

        // GET: Settings
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            var user = System.Web.HttpContext.Current.GetMySessionObject();

            GlobalData globalData = new GlobalData()
            {
                Email = user.Email,
                FullName = user.FullName,
                Speciality = user.Speciality,
                Biography = user.Biography,
                ProfileImagePath = user.ProfileImagePath,
                Level = user.Level
            };

            return View(globalData);
        }

        [HttpPost]
        public JsonResult UpdateProfile(GlobalData newData)
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            if ((string.IsNullOrEmpty(newData.NewFullName) || string.IsNullOrEmpty(newData.NewBiography)) && user.Level != Domain.Enums.UserRole.Admin)
            {
                return Json(new { success = false, message = "Full Name and Biography are required." });
            }

            NewProfileData newProfileData = new NewProfileData()
            {
                NewFullName = newData.NewFullName,
                NewBiography = newData.NewBiography,
                NewProfileImage = newData.NewProfileImage,
                UserID = user.Id,
                UserLevel = user.Level,
                BasePhysicalPath = Server.MapPath("~/")
            };

            if(_session.UpdateProfileData(newProfileData))
            {
                return Json(new { success = true, message = "Profile updated successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Something went wrong!!!" });
            }
            
        }

        [HttpPost]
        public JsonResult UpdatePassword(GlobalData newData)
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            if(user.Email != newData.Email)
            {
                return Json(new { success = false, message = "The email is wrong!!!" });
            }

            NewProfileData newProfileData = new NewProfileData()
            {
                NewPassword = newData.NewPassword,
                UserID = user.Id,
                UserLevel = user.Level
            };

            if (_session.UpdatePassword(newProfileData))
            {
                return Json(new { success = true, message = "Password updated successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Something went wrong!!!" });
            }
        }
    }
}