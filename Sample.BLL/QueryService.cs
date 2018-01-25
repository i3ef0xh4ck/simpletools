using Hebust;
using Newtonsoft.Json;
using Simple;
using Simple.网络;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Sample.BLL
{
    public class QueryService
    {
        static readonly string today_url = "http://www.ipip5.com/today/api.php?type=json";

        public static History GetToday() {
            History slt = null;
            try {
                string data = HttpUtils.HttpGet(today_url, null);
                if (!string.IsNullOrEmpty(data)) {
                    slt = JsonConvert.DeserializeObject<History>(data);
                }
            }
            catch {}
            return slt;
        }

        public static string GetIPFromDomain(string domain) {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(domain) && RegexUtils.IsDomain(domain)) {
                try {
                    string host = GetHostFromDomain(domain);
                    IPAddress[] IPs = Dns.GetHostAddresses(host);
                    foreach (IPAddress item in IPs) {
                        ip = item.ToString();
                        break;
                    }
                }
                catch {}
            }
            return ip;
        }

        private static string GetHostFromDomain(string domain) {
            string host = domain.ToLower().Replace("http://", "").Replace("https://", "");
            if (host.EndsWith("/")) {
                host = host.Substring(0, host.Length - 1);
            }
            return host;
        }

        public static IPModel GetIPInfo(string word) {
            if (!string.IsNullOrEmpty(word)) {
                IPModel slt = null;
                string key = "host_" + word;
                object cache = CacheUtils.GetCache(key);
                if (cache == null) {
                    if (RegexUtils.IsDomain(word)) {
                        string ip = GetIPFromDomain(word);
                        slt = GetIP(ip);
                    }
                    else {
                        slt = GetIP(word);
                    }
                    if(slt != null) {
                        CacheUtils.SetCache(key, slt, new TimeSpan(0,30,0));
                    }
                }
                else {
                    slt = cache as IPModel;
                }                
                return slt;
            }
            return null;
        }

        public static IPModel GetIP(string ip) {
            IPModel model = null;
            string fileName = HttpContext.Current.Server.MapPath("~/App_Data/CoralWry.dat");
            var slt = IpLocator.GetIpLocation(fileName, ip);
            model = new IPModel() {
                ip = ip,
                country = slt.Country,
                city = slt.City,
                start = IpLocator.IntToIpString(slt.IpStart),
                end = IpLocator.IntToIpString(slt.IpEnd)
            };
            return model;
        }

        public static MobileModel GetMobile(string mobile) {
            if (!string.IsNullOrEmpty(mobile)) {
                MobileModel slt = null;
                string key = "phone_" + mobile;
                object cache = CacheUtils.GetCache(key);
                if (cache == null) {
                    if (RegexUtils.IsMobile(mobile)) {
                        string filename = HttpContext.Current.Server.MapPath("~/App_Data/MpData.dat");
                        if (mobile.Length > 7) {
                            mobile = mobile.Substring(0, 7);
                        }
                        MpLocation mpl = MpLocator.GetMpLocation(filename, mobile.ToInt32());
                        slt = new MobileModel() {
                            start = mpl.NumStart.ToString(),
                            end = mpl.NumEnd.ToString(),
                            location = mpl.Location
                        };
                    } 
                    if (slt != null) {
                        CacheUtils.SetCache(key, slt, new TimeSpan(0, 30, 0));
                    }
                } else {
                    slt = cache as MobileModel;
                }
                return slt;
            }
            return null;
        }
    }

    public class MobileModel
    {
        public string start { get; set; }

        public string end { get; set; }

        public string location { get; set; }
    }

    public class IPModel
    {
        public int ret { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public string country { get; set; }

        public string province { get; set; }

        public string city { get; set; }

        public string district { get; set; }

        public string isp { get; set; }

        public string type { get; set; }

        public string desc { get; set; }

        public string ip { get; set; }
    }

    public class History
    {
        public string today { get; set; }

        public List<result> result { get; set; }
    }

    public class result
    {
        public string year { get; set; }

        public string title { get; set; }
    }
}
