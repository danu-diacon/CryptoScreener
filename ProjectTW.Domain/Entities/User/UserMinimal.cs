using ProjectTW.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.Domain.Entities.User
{
    public class UserMinimal
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DoctorSpeciality Specilality { get; set; }
        public string Biography { get; set; }
        public DateTime LastLogin { get; set; }
        public UserRole Level { get; set; }
    }
}
