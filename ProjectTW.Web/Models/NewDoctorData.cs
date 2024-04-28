using ProjectTW.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTW.Web.Models
{
    public class NewDoctorData
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public DoctorSpeciality Speciality { get; set; }

        public string Biography { get; set; }

        public HttpPostedFileBase ProfileImage { get; set; }

        public UserRole Level { get; set; }
    }
}