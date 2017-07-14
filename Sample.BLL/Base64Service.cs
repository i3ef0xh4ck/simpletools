using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.BLL
{
    public class Base64Service
    {
        public static JsonMessage Base64Encode(string text) {
            JsonMessage jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "编码内容不能为空";
                return jsonMessage;
            }            
            try {
                jsonMessage.Flag = true;
                jsonMessage.Data = HtmlUtils.ToBase64String(text);
            }
            catch(Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }
        
        public static JsonMessage Base64Decode(string text) {
            JsonMessage jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "解码内容不能为空";
                return jsonMessage;
            }
            try {
                jsonMessage.Flag = true;
                jsonMessage.Data = HtmlUtils.FromBase64String(text);
            }
            catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }
    }
}
