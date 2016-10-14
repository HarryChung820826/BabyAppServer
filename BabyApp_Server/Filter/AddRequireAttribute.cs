using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyApp_Server.Filter
{
    public class AddRequireAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var user = Auth.getUser();

            if (user == null)
            {
                HttpContext.Current.Response.Redirect("/");
            }
            else
            {
                if (!user.isAuthentication())
                {
                    HttpContext.Current.Response.Redirect("/");
                }
            }
        }
    }
}