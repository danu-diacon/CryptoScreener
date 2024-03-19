using ProjectTW.BusinessLogic.Core;
using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.Response;
using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.AppBL
{
     public class LoginBL : UserAPI, ILogin
     {
          public UserLoginResponse UserLoginAction(UserLoginData loginData)
          {
               return LoginResponse(loginData);
          }
     }
}
