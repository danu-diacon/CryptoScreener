using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.Patient;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Web.ActionFilters;
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
    public class PatientAddController : BaseController
    {
        public readonly IPatient _patient;

        public PatientAddController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _patient = bl.GetPatientBL();
        }

        // GET: PatientAdd
        [AdminOrDoctorMod]
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
        [ValidateAntiForgeryToken]
        public ActionResult AddNewPatient(GlobalData patientRegisterData)
        {
            if (ModelState.IsValid && patientRegisterData.PatientAddProfileImage != null && patientRegisterData.PatientAddProfileImage.ContentLength > 0)
            {
                //Save Profile Image
                string uploadFolderPath = Path.Combine(Server.MapPath("~/ProfileImages"), Guid.NewGuid().ToString());

                //Create new Folder
                DirectoryInfo directoryInfo = Directory.CreateDirectory(uploadFolderPath);
                string directory = directoryInfo.ToString();

                //Save uploaded image to the new created folder
                string imagePath = Path.Combine(uploadFolderPath, Path.GetFileName(patientRegisterData.PatientAddProfileImage.FileName));
                patientRegisterData.PatientAddProfileImage.SaveAs(imagePath);

                var dbImagePath = "~/ProfileImages/" + directory + "/" + patientRegisterData.PatientAddProfileImage.FileName;

                PatientRegisterData patientRegisterData1 = new PatientRegisterData()
                {
                    Email = patientRegisterData.PatientAddEmail,
                    Password = patientRegisterData.PatientAddPassword,
                    FullName = patientRegisterData.PatientAddFullName,
                    Biography = patientRegisterData.PatientAddBiography,
                    ProfileImagePath = dbImagePath,
                    Level = Domain.Enums.UserRole.Patient
                };

                var patientRegister = _patient.PatientRegisterAction(patientRegisterData1);

                if (!patientRegister.Success)
                {
                    ModelState.AddModelError("", patientRegister.Message);
                    return Json(new { success = false, message = patientRegister.Message });
                }
                else
                {
                    return Json(new { success = true, message = "Patient registered successfully." });
                }
            }

            return Json(new { success = false, message = "Model state is invalid or profile image is missing." });
        }
    }
}