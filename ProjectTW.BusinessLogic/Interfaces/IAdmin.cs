using ProjectTW.Domain.Entities.Admin;
using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        HospitalData GetDoctorAndPatientNumber();

        List<UserMinimal> GetAllDoctors();

        bool DeleteDoctorById(int id);
    }
}
