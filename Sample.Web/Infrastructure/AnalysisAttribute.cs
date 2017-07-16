using Sample.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Infrastructure
{
    public class AnalysisAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext) {
            try {
                var context = filterContext.RequestContext.HttpContext;
                if (context != null && context.Request != null && !context.Request.IsAjaxRequest() && context.Request.RequestType == "GET") {
                    string path = context.Request.Url.AbsolutePath.ToLower();
                    var funsContainer = FunsContainer.GlobalFuns ?? new FunsContainer();                    
                    if (funsContainer.ContainsKey(path)) {
                        var fun = funsContainer[path];
                        FunsService.VisitPlus(fun.ID);
                    }
                }
                base.OnActionExecuting(filterContext);
            } catch (Exception ex) {
                base.OnActionExecuting(filterContext);
            }
        }
    }
}