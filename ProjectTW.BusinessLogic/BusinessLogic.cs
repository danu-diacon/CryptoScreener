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

        public IRegister GetRegisterBL()
        {
            return new RegisterBL();
        }

        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IDoctor GetDoctorBL()
        {
            return new DoctorBL();
        }
          
        public IPatient GetPatientBL()
        {
            return new PatientBL();
        }

        public IAdmin GetAdminBL()
        {
            return new AdminBL();
        }
     }
}
