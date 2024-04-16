using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext(): 
            base("name=ProjectTW")
        { 
        }

        public virtual DbSet<UserSession> Sessions { get; set; }
    }
}
