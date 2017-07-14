using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sample.BLL
{
    public class HttpRequestService
    {
        public static JsonMessage Process(HttpRequestModel model) {
            JsonMessage slt = new JsonMessage() { Flag = false};
            if (model == null) {
                slt.Message = "参数错误";
                return slt;
            }
            if (string.IsNullOrEmpty(model.Url)) {
                slt.Message = "请填写网址";
                return slt;
            }
            if (string.IsNullOrEmpty(model.Method)) {
                slt.Message = "请不要篡改请求方式";
                return slt;
            }
            model.Url = model.Url.ToLower();
            if (!model.Url.StartsWith("http://")) {
                model.Url = "http://" + model.Url;
            }
            if (!string.IsNullOrEmpty(model.Params)) {
                if (model.Params.StartsWith("?")) {
                    model.Params = model.Params.Replace("?", "");
                }
                if(model.Method == "get") {
                    if (model.Url.IndexOf("?") > 0) {
                        model.Url = model.Url + "&" + model.Params;
                    }
                    else {
                        model.Url = model.Url + "?" + model.Params;
                    }
                }
            }
            try {
                switch (model.Method) {
                    case "get":
                        slt.Flag = true;
                        slt.Data = RequestUtility.HttpGet(model.Url);
                        break;
                    case "post":
                        slt.Flag = true;
                        Stream queryStream = RequestUtility.GetQueryStream(model.Params);
                        slt.Data = RequestUtility.HttpPost(model.Url, null, queryStream);
                        break;
                }
            }
            catch(Exception ex) {
                slt.Flag = false;
                slt.Message = ex.Message;
            }
            return slt;
        }

        private static bool IsDomain(string url) {
            return Regex.IsMatch(url, @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$",
                RegexOptions.IgnoreCase);
        }
    }

    public class HttpRequestModel
    {
        public string Method { get; set; }

        public string Url { get; set; }

        public string Params { get; set; }

        public string Cookies { get; set; }

        public string Headers { get; set; }

        public string Response { get; set; }
    }
}
