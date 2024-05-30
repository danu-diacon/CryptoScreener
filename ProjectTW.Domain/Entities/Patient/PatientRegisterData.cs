using ProjectTW.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.Domain.Entities.Patient
{
    public class PatientRegisterData
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string Biography { get; set; }

        public string ProfileImagePath { get; set; }

        public UserRole Level { get; set; }
    }
}
