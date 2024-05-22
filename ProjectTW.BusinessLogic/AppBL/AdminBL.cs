using ProjectTW.BusinessLogic.Core;
using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.Admin;
using ProjectTW.Domain.Entities.User;
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

        public List<UserMinimal> GetAllDoctors()
        {
            return AllDoctors();
        }

        public bool DeleteDoctorById(int id)
        {
            return DeleteDoctor(id);
        }
    }
}
