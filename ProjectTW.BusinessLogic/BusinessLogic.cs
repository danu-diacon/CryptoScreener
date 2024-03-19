using ProjectTW.BusinessLogic.AppBL;
using ProjectTW.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic
{
     public class BusinessLogic
     {
          public ILogin GetLoginBL()
          {
               return new LoginBL();
          }
     }
}
