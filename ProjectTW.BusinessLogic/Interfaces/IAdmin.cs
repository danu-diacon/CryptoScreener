using ProjectTW.Domain.Entities.Admin;
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
    }
}
