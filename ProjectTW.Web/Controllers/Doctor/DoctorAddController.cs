using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Web.ActionFilters;
using ProjectTW.Web.Extension;
using ProjectTW.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace ProjectTW.Web.Controllers
{
    public class DoctorAddController : Controller
    {
        public readonly IDoctor _doctor;

        public DoctorAddController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _doctor = bl.GetDoctorBL();
        }

        // GET: DoctorAdd
        [AdminMod]
        public ActionResult Index()
        {
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
        [ValidateAntiForgeryToken]
        public ActionResult AddNewDoctor(GlobalData doctorRegisterData)
        {
            if(ModelState.IsValid && doctorRegisterData.DoctorAddProfileImage != null && doctorRegisterData.DoctorAddProfileImage.ContentLength > 0)
            {
                //Save Profile Image
                string uploadFolderPath = Path.Combine(Server.MapPath("~/ProfileImages"), Guid.NewGuid().ToString());

                //Create new Folder
                DirectoryInfo directoryInfo = Directory.CreateDirectory(uploadFolderPath);
                string directory = directoryInfo.ToString();

                //Save uploaded image to the new created folder
                string imagePath = Path.Combine(uploadFolderPath, Path.GetFileName(doctorRegisterData.DoctorAddProfileImage.FileName));
                doctorRegisterData.DoctorAddProfileImage.SaveAs(imagePath);

                var dbImagePath = "~/ProfileImages/" + directory + "/" + doctorRegisterData.DoctorAddProfileImage.FileName;

                DoctorRegisterData doctorRegisterData1 = new DoctorRegisterData()
                {
                    Email = doctorRegisterData.DoctorAddEmail,
                    Password = doctorRegisterData.DoctorAddPassword,
                    FullName = doctorRegisterData.DoctorAddFullName,
                    Biography = doctorRegisterData.DoctorAddBiography,
                    Speciality = doctorRegisterData.DoctorAddSpeciality,
                    ProfileImagePath = dbImagePath,
                    Level = Domain.Enums.UserRole.Doctor
                };

                var doctorRegister = _doctor.DoctorRegisterAction(doctorRegisterData1);

                if (!doctorRegister.Success)
                {
                    ModelState.AddModelError("", doctorRegister.Message);
                    return Json(new { success = false, message = doctorRegister.Message });
                }
                else
                {
                    return Json(new { success = true, message = "Doctor registered successfully." });
                }
            }

            return Json(new { success = false, message = "Model state is invalid or profile image is missing." });
        }
    }
}