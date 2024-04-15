using ProjectTW.BusinessLogic.Core;
using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities.Response;
using ProjectTW.Domain.Entities.User;

namespace ProjectTW.BusinessLogic.AppBL
{
    public class RegisterBL : UserAPI, IRegister
    {
        public UserRegisterResponse UserRegisterAction(UserRegisterData userRegisterData)
        {
            return RegisterResponse(userRegisterData);
        }
    }
}
