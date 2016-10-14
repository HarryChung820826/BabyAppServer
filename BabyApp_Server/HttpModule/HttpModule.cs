using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyApp_Server.Filter;

namespace BabyApp_Server.HttpModule
{
    public class HttpModule : IHttpModule
    {
        public void Dispose()
        {
 
        }

        public const string cookieName = "LoginUser";
        public const string User_Name = "Name";

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            var context = app.Context;
            var cookie1 = context.Request.Cookies[cookieName];

            IUser _user = null;

            if (cookie1 != null)
            {
                string Username = cookie1.Values[User_Name];
                User s_user = new User();
                s_user.username = Username;
                _user = s_user;
            }
            else {
                _user = new AnonymousUser();
            }

            context.Items["_user"] = _user;
        }
    }
}