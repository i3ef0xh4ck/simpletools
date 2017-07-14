using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.BLL
{
    public class DES3Service
    {
        static string s_key = "1234567890123456";

        public static JsonMessage DES3Encrypt(string text, string key) {
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
                jsonMessage.Data = DES3.Encrypt(text, key);
            }
            catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }

        public static JsonMessage DES3Decrypt(string text, string key) {
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
                jsonMessage.Data = DES3.Decrypt(text, key);
            }
            catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }
    }
}
