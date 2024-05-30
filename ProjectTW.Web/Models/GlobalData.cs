using ProjectTW.Domain.Entities.User;
using ProjectTW.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTW.Web.Models
{
     public class GlobalData
     {
          //GLobal Data
          public string Email { get; set; }

          public string Password { get; set; }

          public string FullName { get; set; }

          public string Biography { get; set; }

          public HttpPostedFileBase ProfileImage { get; set; }

          public string ProfileImagePath { get; set; }

          public UserRole Level { get; set; }


          //New Doctor Data
          public string DoctorAddEmail { get; set; }

          public string DoctorAddPassword { get; set; }

          public string DoctorAddFullName { get; set; }

          public string DoctorAddBiography { get; set; }

          public HttpPostedFileBase DoctorAddProfileImage { get; set; }

          public DoctorSpeciality DoctorAddSpeciality { get; set; }

          public DoctorSpeciality Speciality { get; set; }

          public List<UserMinimal> DoctorList { get; set; }

          //New Patient Data
          public string PatientAddEmail { get; set; }

          public string PatientAddPassword { get; set; }

          public string PatientAddFullName { get; set; }

          public string PatientAddBiography { get; set; }

          public HttpPostedFileBase PatientAddProfileImage { get; set; }

        //Patient Data
        public List<UserMinimal> PatientList { get; set; }


          //Appointment
          public int Id { get; set; }

          public int DoctorId { get; set; }

          public DateTime AppointmentDateTime { get; set; }

          public AppointmentStatus AppointmentStatus { get; set; }

          public List<Appointment> AllAppointments { get; set; }

          //Settings
          public string NewFullName { get; set; }

          public string NewBiography { get; set; }

          public HttpPostedFileBase NewProfileImage { get; set; }

          public string NewPassword { get; set; }
    }
}