using ProjectTW.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectTW.Domain.Entities.User
{
    public class NewProfileData
    {
        public string NewFullName { get; set; }

        public string NewBiography { get; set; }

        public HttpPostedFileBase NewProfileImage { get; set; }

        public string NewPassword { get; set; }

        public int UserID { get; set; }

        public UserRole UserLevel { get; set; }

        //Server Path
        public string BasePhysicalPath { get; set; }
    }
}
