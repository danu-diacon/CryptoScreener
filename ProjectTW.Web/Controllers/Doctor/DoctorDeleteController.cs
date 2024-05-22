using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Web.Extension;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
    public class DoctorDeleteController : Controller
    {
        public readonly IAdmin _admin;

        public DoctorDeleteController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _admin = bl.GetAdminBL();
        }

        // GET: DoctorEdit
        public ActionResult Index()
        {
            var user = System.Web.HttpContext.Current.GetMySessionObject();

            var allDoctors = _admin.GetAllDoctors();

            GlobalData globalData = new GlobalData()
            {
                Email = user.Email,
                FullName = user.FullName,
                Speciality = user.Speciality,
                Biography = user.Biography,
                ProfileImagePath = user.ProfileImagePath,
                Level = user.Level,
                DoctorList = allDoctors,
            };

            return View(globalData);
        }

        [HttpPost]
        public JsonResult DeleteDoctor(int id)
        {
            try
            {
                _admin.DeleteDoctorById(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}