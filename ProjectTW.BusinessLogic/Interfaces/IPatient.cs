using ProjectTW.Domain.Entities.User;
using ProjectTW.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.Interfaces
{
     public interface IPatient
     {
          List<UserMinimal> GetDoctorsBySpeciality(DoctorSpeciality doctorSpeciality);

          List<DateTime> GetAvailableTimeByDoctorId(NewAppointmentData partitialData);

          List<UserMinimal> GetAllPatientsData();
     }
}
