using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sample.Web.Infrastructure
{
    public class AdminValidationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext) {
            object cert = filterContext.HttpContext.Session["admin"];
            if (cert == null) {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(new { controller = "account", action = "login" }));
                return;
            }
            return;
        }
    }
}