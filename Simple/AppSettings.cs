using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple
{
    public class AppSettings
    {
        public static string CDNDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["CDNDomain"];
            }
        }
        
        public static string RedisHost
        {
            get { return ConfigurationManager.AppSettings.Get("RedisHost"); }
        }

        public static int RedisTimeOut
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("RedisTimeOut").ToInt32(60);
            }
        }

        public static bool IsDevelopment
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("IsDevelopment").ToBool(false);
            }
        }

        public static int ManagePageSize
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("ManagePageSize").ToInt32(10);
            }
        }

        public static int WebPageSize
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("WebPageSize").ToInt32(15);
            }
        }

        public static string CookieName
        {
            get
            {
                return "simple_cookie";
            }
        }

        public static string SessionName
        {
            get
            {
                return "simple_admin";
            }
        }

        public static string DesKey
        {
            get
            {
                return "tzbyghyn";
            }
        }
    }
}
