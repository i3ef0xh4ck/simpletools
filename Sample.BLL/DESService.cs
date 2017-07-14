using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.BLL
{
    public class DESService
    {
        static string s_key = "12345678901234567890123456789012";

        public static JsonMessage DESEncrypt(string text, string key) {
            JsonMessage jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "加密内容不能为空";
                return jsonMessage;
            }
            try {
                jsonMessage.Flag = true;
                if (string.IsNullOrEmpty(key)) {
                    key = s_key;
                }
                jsonMessage.Data = DES.Encrypt(text, key);
            }
            catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }

        public static JsonMessage DESDecrypt(string text, string key) {
            JsonMessage jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "解密内容不能为空";
                return jsonMessage;
            }
            try {
                jsonMessage.Flag = true;
                if (string.IsNullOrEmpty(key)) {
                    key = s_key;
                }
                jsonMessage.Data = DES.Decrypt(text, key);
            }
            catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }
    }
}
