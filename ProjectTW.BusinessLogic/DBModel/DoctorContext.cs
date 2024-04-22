using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.DBModel
{
    public class DoctorContext : DbContext
    {
        public DoctorContext() :
            base("name=ProjectTW")
        {
        }

        public virtual DbSet<DoctorDbTable> Doctors { get; set; }
    }
}
