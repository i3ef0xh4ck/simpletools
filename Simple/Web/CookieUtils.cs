using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Simple
{
    public class CookieUtils
    {
        private const int Expire = 30; //cookie保存天数

        public static bool IsRequestWithCookie()
        {
            if (HttpContext.Current == null)
            {
                return false;
            }

            for (var i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)
            {
                if (HttpContext.Current.Request.Cookies[i].Expires == DateTime.MinValue)
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetCookie(string cookieName)
        {
            if (HttpContext.Current == null)
            {
                return null;
            }
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            return cookie == null ? null : cookie.Value;
        }
        public static void SetCookie(string cookieName, string val, Action<HttpCookie> extopt = null)
        {
            SetCookie(cookieName, val, DateTime.Now.AddDays(Expire), extopt);
        }
        public static void SetCookie(string cookieName, string val, DateTime? expires, Action<HttpCookie> extopt = null)
        {
            if (HttpContext.Current == null)
            {
                return;
            }
            var cookie = HttpContext.Current.Request.Cookies[cookieName] ?? new HttpCookie(cookieName);
            cookie.Expires = expires != null ? expires.Value : DateTime.Now.AddDays(Expire);
            cookie.Value = val;
            if (extopt != null)
                extopt(cookie);
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
    }
}
