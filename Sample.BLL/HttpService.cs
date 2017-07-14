using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sample.BLL
{
    public class HttpService
    {
        public static HttpModel GetHostInfo() {
            HttpModel slt = new HttpModel();
            var request = HttpContext.Current.Request;
            slt.IP = GetIP();
            slt.Language = string.Join(", ",request.UserLanguages);
            slt.UserAgent = request.UserAgent;
            slt.BrowserType = request.Browser.Browser;
            slt.BrowserID = request.Browser.Id;
            slt.BrowserVersion = request.Browser.Version;
            slt.OperSystem = GetHoverTreeOSName(request.UserAgent);
            return slt;
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP() {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty) {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (null == result || result == String.Empty) {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (null == result || result == String.Empty) {
                return "0.0.0.0";
            }
            return result;
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip) {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 根据 User Agent 获取操作系统名称
        /// </summary>
        private static string GetHoverTreeOSName(string userAgent) {
            string m_hvtOsVersion = "未知";
            if (userAgent.Contains("NT 6.4") || userAgent.Contains("NT 10.0")) {
                m_hvtOsVersion = "Windows 10";
            }
            else
            if (userAgent.Contains("NT 6.3")) {
                m_hvtOsVersion = "Windows 8.1";
            }
            else
            if (userAgent.Contains("NT 6.2")) {
                m_hvtOsVersion = "Windows 8";
            }
            else
            if (userAgent.Contains("NT 6.1")) {
                m_hvtOsVersion = "Windows 7";
            }
            else
            if (userAgent.Contains("NT 6.0")) {
                m_hvtOsVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2")) {
                m_hvtOsVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1")) {
                m_hvtOsVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5")) {
                m_hvtOsVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4")) {
                m_hvtOsVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me")) {
                m_hvtOsVersion = "Windows Me";
            }
            else if (userAgent.Contains("98")) {
                m_hvtOsVersion = "Windows 98";
            }
            else if (userAgent.Contains("95")) {
                m_hvtOsVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac")) {
                m_hvtOsVersion = "Mac";
            }
            else if (userAgent.Contains("Unix")) {
                m_hvtOsVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux")) {
                m_hvtOsVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS")) {
                m_hvtOsVersion = "SunOS";
            }
            return m_hvtOsVersion;
        }
    }

    public class HttpModel
    {
        public string IP { get; set; }

        public string MD5 { get; set; }

        public string Language { get; set; }

        public string UserAgent { get; set; }

        public string BrowserType { get; set; }

        public string BrowserID { get; set; }

        public string BrowserVersion { get; set; }

        public string OperSystem { get; set; }

        public string Ratio { get; set; }
    }
}
