using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Simple
{
    public static class RequestUtility
    {
        /// <summary>
        /// 使用Get方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Encoding encoding = null)
        {
            WebClient wc = new WebClient();
            wc.Encoding = encoding ?? Encoding.UTF8;
            return wc.DownloadString(url);
        }

        /// <summary>
        /// 使用Post方法获取字符串结果，常规提交
        /// </summary>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null, int timeOut = 10000)
        {
            string dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return HttpPost(url, cookieContainer, ms, null, null, encoding, timeOut);
        }

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Stream postStream = null, Dictionary<string, string> fileDictionary = null, string refererUrl = null, Encoding encoding = null, int timeOut = 100000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = timeOut;

            #region 处理Form表单文件上传


            request.ContentType = "application/x-www-form-urlencoded";
            #endregion
            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;

            if (!string.IsNullOrEmpty(refererUrl))
            {
                request.Referer = refererUrl;
            }
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;

                //直接写入流
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                postStream.Close();//关闭文件访问
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.GetEncoding("utf-8")))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

        /// <summary>
        /// 组装QueryString的方法
        /// 参数之间用&连接，首位没有符号，如：a=1&b=2&c=3
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static string GetQueryString(Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }

        public static Stream GetQueryStream(string queryString) {
            var formDataBytes = queryString == null ? new byte[0] : Encoding.UTF8.GetBytes(queryString);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

    }

    public static class Get
    {
        public static T GetJson<T>(string url, Encoding encoding = null)
        {
            string returnText = RequestUtility.HttpGet(url, encoding);
            return JsonConvert.DeserializeObject<T>(returnText);
        }

        public static string GetString(string url, Encoding encoding = null)
        {
            return RequestUtility.HttpGet(url, encoding);
        }

    }

    public static class Post
    {
        /// <summary>
        /// 获取Post结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText"></param>
        /// <returns></returns>
        public static T GetResult<T>(string returnText)
        {
            T result = JsonConvert.DeserializeObject<T>(returnText);
            return result;
        }

        /// <summary>
        /// 发起Post请求
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <returns></returns>
        public static T PostFileGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Encoding encoding = null)
        {
            string returnText = RequestUtility.HttpPost(url, cookieContainer, null, fileDictionary, null, encoding);
            var result = GetResult<T>(returnText);
            return result;
        }

        /// <summary>
        /// 发起Post请求
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="fileStream">文件流</param>
        /// <returns></returns>
        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null)
        {
            string returnText = RequestUtility.HttpPost(url, cookieContainer, fileStream, null, null, encoding);
            var result = GetResult<T>(returnText);
            return result;
        }

        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null)
        {
            string returnText = RequestUtility.HttpPost(url, cookieContainer, formData, encoding);
            var result = GetResult<T>(returnText);
            return result;
        }

        public static T PostGetJson<T>(string url, Dictionary<string, string> formData = null, Encoding encoding = null)
        {
            string returnText = RequestUtility.HttpPost(url, null, formData, encoding);
            var result = GetResult<T>(returnText);
            return result;
        }

        public static T PostGetJson<T>(string url, bool haveCookie, Dictionary<string, string> formData = null, Encoding encoding = null)
        {
            CookieContainer container = null;
            if (haveCookie)
            {
                container = new CookieContainer();
                var cookie = System.Web.HttpContext.Current.Request.Cookies.Get("UserInfo");
                if (cookie != null)
                    container.Add(new Cookie { Name = cookie.Name, Domain = ".tsingming.com", Value = cookie.Value, Expires = cookie.Expires });
                //foreach (string key in list.Keys)
                //{
                //    container.Add(new Cookie { Name = key, Domain = System.Configuration.ConfigurationManager.AppSettings.Get("RootDomain"), Value = list[key].Value, Expires = list[key].Expires });
                //}
            }
            string returnText = RequestUtility.HttpPost(url, container, formData, encoding);
            var result = GetResult<T>(returnText);
            return result;
        }

        public static string PostGetString(string url, Dictionary<string, string> formData = null, Encoding encoding = null)
        {
            string returnText = RequestUtility.HttpPost(url, null, formData, encoding);
            //var result = GetResult<T>(returnText);
            return returnText;
        }
    }
}
