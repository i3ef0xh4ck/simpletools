using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.BLL
{
    public class Utf8Service
    {
        public static JsonMessage Utf8Encode(string text) {
            JsonMessage jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "编码内容不能为空";
                return jsonMessage;
            }
            try {
                jsonMessage.Flag = true;
                UTF8Encoding utf8 = new UTF8Encoding(); 
                Byte[] encodedBytes = utf8.GetBytes(text);
                jsonMessage.Data = utf8.GetString(encodedBytes);
            } catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }

        public static JsonMessage Utf8Decode(string text) {
            JsonMessage jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "解码内容不能为空";
                return jsonMessage;
            }
            try {
                jsonMessage.Flag = true;
                jsonMessage.Data = "";
            } catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }
    }
}
