using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.BLL
{
    public class ASCIIService
    {
        public static JsonMessage ASCIIEncode(string text) {
            JsonMessage jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "编码内容不能为空";
                return jsonMessage;
            }
            try {
                jsonMessage.Flag = true;
                jsonMessage.Data = ToASCII(text);
            } catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }

        public static JsonMessage ASCIIDecode(string text) {
            JsonMessage jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "解码内容不能为空";
                return jsonMessage;
            }
            try {
                jsonMessage.Flag = true;
                jsonMessage.Data = FromASCII(text);               
            } catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }

        public static string ToASCII(string text) {
            string slt = string.Empty;
            byte[] array = System.Text.Encoding.ASCII.GetBytes(text);   
            for (int i = 0; i < array.Length; i++) {
                int asciicode = (int)(array[i]);
                slt += Convert.ToString(asciicode);
            }
            return slt;
        }

        public static string FromASCII(string code) {
            string slt = string.Empty;
            for (int i = 0; i < code.Length; i++) {
                int j = int.Parse(code.Substring(i, 2));
                if (j < 64) {
                    j = int.Parse(code.Substring(i, 3));
                    i += 2;
                } else {
                    i += 1;
                }
                slt += Encoding.ASCII.GetString(new byte[] { (byte)j });
            }
            return slt;
        }
    }
}
