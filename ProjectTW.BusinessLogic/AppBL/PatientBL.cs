using ProjectTW.BusinessLogic.Core;
using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.Patient;
using ProjectTW.Domain.Entities.Response;
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

          public List<DateTime> GetAvailableTimeByDoctorId(NewAppointmentData partitialData)
          {
               return AvailableTimeByDoctorId(partitialData);
          }

          public List<UserMinimal> GetAllPatientsData()
          {
            return AllPatientsData();
          }

          public List<AppointmentsDbTable> GetAllPatientAppointments(int patientID)
          {
               return AllPatientAppointments(patientID);
          }

          public UserRegisterResponse PatientRegisterAction(PatientRegisterData patientRegisterData)
          {
              return PatientRegister(patientRegisterData);
          }
     }
}
