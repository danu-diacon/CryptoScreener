using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.Domain.Entities.User
{
     public class UserLoginData
     {
          public string Credential { get; set; }

          public string Password { get; set; }

          public string Ip { get; set; }

          public DateTime LoginTime { get; set; }
     }
}
