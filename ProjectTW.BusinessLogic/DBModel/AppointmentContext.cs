using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.DBModel
{
    public class AppointmentContext : DbContext
    {
        public AppointmentContext() :
            base("name=ProjectTW")
        {
        }

        public virtual DbSet<AppointmentsDbTable> Appointments { get; set; }
    }
}
