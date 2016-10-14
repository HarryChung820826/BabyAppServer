using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyApp_Server.Filter
{
    public class Auth
    {
        public static IUser getUser()
        {
            return (IUser)HttpContext.Current.Items["_user"];
        }
    }
}