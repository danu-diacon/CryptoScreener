using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectTW.BusinessLogic.Interfaces
{
    public interface ISession
    {
        HttpCookie GenCookie(string loginEmail);

        UserMinimal GetUserByCookie(string apiCookieValue);

        bool NewAppointment(NewAppointmentData AppointmentData);

        List<AppointmentsDbTable> GetAllAppointments(int doctorID);

        UserMinimal GetPatientById(int Id);

        UserMinimal GetDoctorById(int Id);
     }
}
