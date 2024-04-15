using ProjectTW.BusinessLogic.DBModel;
using ProjectTW.Domain.Entities.Response;
using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTW.BusinessLogic.Core
{
    public class UserAPI
    {
        public UserLoginResponse LoginResponse(UserLoginData loginData)
        {
            //SQL request

            UserDbTable result;

            using (var db = new UserContext())
            {
                result = db.Users.FirstOrDefault(u => u.Email == loginData.Credential && u.Password == loginData.Password);
            }

            if (result == null)
            {
                return new UserLoginResponse() { Success = false, Message = "The Username or Password is Incorrect" };
            }

            return new UserLoginResponse() { Success = true };
        }

        public UserRegisterResponse RegisterResponse(UserRegisterData registerData)
        {
            UserDbTable result;

            using (var db = new UserContext())
            {
                result = db.Users.FirstOrDefault(u => u.Email == registerData.Email);
            }

            if (result != null)
            {
                return new UserRegisterResponse() { Success = false, Message = "A user with this email already exists" };
            }

            var todo = new UserDbTable
            {

                FullName = registerData.FullName,
                Password = registerData.Password,
                Email = registerData.Email,
                LastLogin = registerData.LoginTime,

            };

            using (var db = new UserContext())
            {
                db.Users.Add(todo);
                db.SaveChanges();
            }

            return new UserRegisterResponse { Success = true };
        }
    }
}
