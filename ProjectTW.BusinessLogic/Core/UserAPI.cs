using ProjectTW.Domain.Entities.Response;
using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.Core
{
     public class UserAPI
     {
          public UserLoginResponse LoginResponse(UserLoginData loginData)
          {
               //SQL request
               if(loginData.Credential == "admin" && loginData.Password == "admin")
               {
                    return new UserLoginResponse { Success = true };
               }

               return new UserLoginResponse { Success = false };
          }
     }
}
