using Newtonsoft.Json;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Infrastructure
{
    public class MonkeyController : Controller
    {
        string hiddenToken = "hiddenToken";

        /// <summary>
        /// 频繁提交过滤，页面上需要加隐藏域
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext) {
            string httpMethod = filterContext.RequestContext.HttpContext.Server.HtmlEncode(filterContext.RequestContext.HttpContext.Request.HttpMethod);
            if (httpMethod == "POST" && filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()) {
                string cacheToken = filterContext.HttpContext.Request[hiddenToken];
                if (cacheToken != null) {
                    if (System.Web.HttpContext.Current.Cache[cacheToken] == null) {
                        System.Web.HttpContext.Current.Cache.Insert(cacheToken, cacheToken, null, DateTime.MaxValue, TimeSpan.FromSeconds(1));
                    } else {                        
                        var context = filterContext.HttpContext;
                        string callback = context.Request["callback"];
                        string content = string.Empty;
                        JsonMessage jsonMessage = new JsonMessage();
                        jsonMessage.Flag = false;
                        jsonMessage.Message = "请不要频繁提交，太快了，臣妾受不了";
                        if (!string.IsNullOrEmpty(callback)) {
                            string jsoncallback = context.Request["callback"];
                            content = jsoncallback + "(" + JsonConvert.SerializeObject(jsonMessage) + ")";
                        } else {
                            content = JsonConvert.SerializeObject(jsonMessage);
                        }
                        context.Response.Clear();
                        context.Response.ContentEncoding = Encoding.UTF8;
                        context.Response.ContentType = "application/json";
                        context.Response.Expires = 0;
                        context.Response.Cache.SetNoStore();
                        context.Response.Write(content);
                        context.Response.End();
                        filterContext.Result = new ContentResult();
                    }
                } else {
                    // 如果post请求中没有带token，则识为非法请求
                    var context = filterContext.HttpContext;
                    string callback = context.Request["callback"];
                    string content = string.Empty;
                    JsonMessage jsonMessage = new JsonMessage();
                    jsonMessage.Flag = false;
                    jsonMessage.Message = "非法请求";
                    if (!string.IsNullOrEmpty(callback)) {
                        string jsoncallback = context.Request["callback"];
                        content = jsoncallback + "(" + JsonConvert.SerializeObject(jsonMessage) + ")";
                    } else {
                        content = JsonConvert.SerializeObject(jsonMessage);
                    }
                    context.Response.Clear();
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.ContentType = "application/json";
                    context.Response.Expires = 0;
                    context.Response.Cache.SetNoStore();
                    context.Response.Write(content);
                    context.Response.End();
                    filterContext.Result = new ContentResult();
                }
            }
        }
    }
}