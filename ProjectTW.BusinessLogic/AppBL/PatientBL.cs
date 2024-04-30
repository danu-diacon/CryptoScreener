using ProjectTW.BusinessLogic.Core;
using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.AppBL
{
     public class PatientBL : UserAPI, IPatient
     {
          public List<UserMinimal> GetDoctorsBySpeciality(DoctorSpeciality doctorSpeciality)
          {
               return DoctorsBySpeciality(doctorSpeciality);
          }
     }
}
