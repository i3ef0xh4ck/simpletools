using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sample.BLL
{
    public class AESService
    {
        static string s_key = "12345678901234567890123456789012";

        public static JsonMessage AESEncrypt(string text, string key) {
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
                jsonMessage.Data = Encrypt(text, key);
            }
            catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }

        public static JsonMessage AESDecrypt(string text,string key) {
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
                jsonMessage.Data = Decrypt(text, key);
            }
            catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }

        public static string Encrypt(string toEncrypt, string key) {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt,string key) {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
