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
    public class DoctorBL : AdminAPI, IDoctor
    {
        public UserRegisterResponse DoctorRegisterAction(DoctorRegisterData doctorRegisterData)
        {
            return DoctorRegisterResponse(doctorRegisterData);
        }

        public List<UserMinimal> GetAllDoctorsData()
        {
            return AllDoctorsData();
        }
    }
}
