using ProjectTW.BusinessLogic.Core;
using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.AppBL
{
    public class AdminBL : AdminAPI, IAdmin
    {
        public HospitalData GetDoctorAndPatientNumber()
        {
            return DoctorAndPatientNumber();
        }
    }
}
