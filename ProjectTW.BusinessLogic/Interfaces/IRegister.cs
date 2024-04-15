using ProjectTW.Domain.Entities.Response;
using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.Interfaces
{
    public interface IRegister
    {
        UserRegisterResponse UserRegisterAction(UserRegisterData userRegisterData);
    }
}
