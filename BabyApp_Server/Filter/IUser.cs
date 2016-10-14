using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyApp_Server.Filter
{
    public interface IUser
    {
        bool isAuthentication();
        string username { get; }
    }

    public class AnonymousUser : IUser
    {
        public bool isAuthentication()
        {
            return false;
        }
        public string username { get { return "遊客"; } }
    }

    public class User : IUser
    {
        public bool isAuthentication()
        {
            return true;
        }
        public string username { get; set; }

    }
}