using ProjectTW.BusinessLogic.Core;
using ProjectTW.BusinessLogic.Interfaces;
using ProjectTW.Domain.Entities;
using ProjectTW.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectTW.BusinessLogic.AppBL
{
    public class SessionBL : UserAPI, ISession
    {
        public HttpCookie GenCookie(string loginEmail)
        {
            return Cookie(loginEmail);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
    }
}
