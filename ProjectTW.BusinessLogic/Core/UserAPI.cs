using ProjectTW.BusinessLogic.DBModel;
using ProjectTW.Domain.Entities.Response;
using ProjectTW.Domain.Entities.User;
using ProjectTW.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectTW.BusinessLogic.Core
{
     public class UserAPI
     {
        public UserLoginResponse LoginResponse(UserLoginData loginData)
        {
            UserDbTable result;
            var HashPassword = LoginHelper.HashGen(loginData.Password);

            using (var db = new UserContext())
            {
                result = db.Users.FirstOrDefault(u => u.Email == loginData.Email && u.Password == HashPassword);
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

            var HashPassword = LoginHelper.HashGen(registerData.Password);

            var todo = new UserDbTable
            {

                FullName = registerData.FullName,
                Password = HashPassword,
                Email = registerData.Email,
                LastLogin = registerData.LoginTime,
                Level = registerData.Level
            };

            using (var db = new UserContext())
            {
                db.Users.Add(todo);
                db.SaveChanges();
            }

            return new UserRegisterResponse { Success = true };
        }

        public HttpCookie Cookie(string loginEmail)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginEmail)
            };

            using (var db = new SessionContext())
            {
                UserSession current;
                var validate = new EmailAddressAttribute();

                current = (from e in db.Sessions where e.Email == loginEmail select e).FirstOrDefault();

                if (current != null)
                {
                    current.CookieString = apiCookie.Value;
                    current.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(current).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new UserSession { Email = loginEmail,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60) });
                    db.SaveChanges();
                }
            }
            return apiCookie;
        }

        public UserMinimal UserCookie(string cookie)
        {
            UserSession session;
            UserDbTable currentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) { return null; }
            using (var db = new UserContext())
            {
                currentUser = db.Users.FirstOrDefault(u => u.Email == session.Email);
            }

            if (currentUser == null) { return null; };

            var userMinimal = new UserMinimal()
            {
                Id = currentUser.Id,
                FullName = currentUser.FullName,
                Email = currentUser.Email,
                LastLogin = currentUser.LastLogin,
                Level = currentUser.Level

            };

            return userMinimal;
        }
     }
}
