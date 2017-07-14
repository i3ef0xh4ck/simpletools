using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Web.Infrastructure
{
    public class AppUtils
    {
        #region 获取客户端IP
        /// <summary>
        /// 获取客户端IP。
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP() {
            return GetClientIP(System.Web.HttpContext.Current.Request);
        }
        /// <summary>
        /// 获取客户端IP。
        /// </summary>
        /// <param name="request">上下文请求对象</param>
        /// <returns></returns>
        public static string GetClientIP(System.Web.HttpRequest request) {
            var svars = request.ServerVariables;
            return GetIPFromServerVars(svars);
        }

        /// <summary>
        /// 获取客户端IP。
        /// </summary>
        /// <param name="request">上下文请求对象</param>
        /// <returns></returns>
        public static string GetClientIP(System.Web.HttpRequestBase request) {
            var svars = request.ServerVariables;
            return GetIPFromServerVars(svars);
        }

        private static string GetIPFromServerVars(System.Collections.Specialized.NameValueCollection svars) {
            var result = svars["REMOTE_ADDR"];
            if (!string.IsNullOrEmpty(svars["HTTP_X_FORWARDED_FOR"]) && IsIP(svars["HTTP_X_FORWARDED_FOR"])) {
                result = svars["HTTP_X_FORWARDED_FOR"];
            } else if (!string.IsNullOrEmpty(svars["HTTP_X_SURFCACHE_FOR"]) && IsIP(svars["HTTP_X_SURFCACHE_FOR"])) {
                result = svars["HTTP_X_SURFCACHE_FOR"];
            } else if (!string.IsNullOrEmpty(svars["HTTP_X_REAL_IP"]) && IsIP(svars["HTTP_X_REAL_IP"])) {
                result = svars["HTTP_X_REAL_IP"];
            } else if (!string.IsNullOrEmpty(svars["HTTP_CLIENT_IP"]) && IsIP(svars["HTTP_CLIENT_IP"])) {
                result = svars["HTTP_CLIENT_IP"];
            } else if (!string.IsNullOrEmpty(svars["HTTP_REMOTE_HOST"]) && IsIP(svars["HTTP_REMOTE_HOST"])) {
                result = svars["HTTP_REMOTE_HOST"];
            }
            return result;
        }

        public static bool IsIP(string host) {
            return System.Text.RegularExpressions.Regex.IsMatch(host, @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
        }
        #endregion
    }
}