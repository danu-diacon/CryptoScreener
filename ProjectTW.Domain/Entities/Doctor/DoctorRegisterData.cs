using ProjectTW.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectTW.Domain.Entities.User
{
    public class DoctorRegisterData
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public DoctorSpeciality Speciality { get; set; }

        public string Biography { get; set; }

        public string ProfileImagePath { get; set; }

        public UserRole Level { get; set; }
    }
}
