using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.DBModel
{
    public class PatientContext : DbContext
    {
        public PatientContext() :
            base("name=ProjectTW")
        {
        }

        public virtual DbSet<PatientDbTable> Patients { get; set; }
    }
}
