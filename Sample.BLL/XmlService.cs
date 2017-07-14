using Sample.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Sample.BLL
{
    public class XmlService
    {
        public static JsonMessage Format(string text) {
            var jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "Xml内容不能为空";
                return jsonMessage;
            }
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            XmlTextWriter xtw = null;
            try {
                XmlDocument xd = new XmlDocument();
                xd.LoadXml(text);
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = 1;
                xtw.IndentChar = '\t';
                xd.WriteTo(xtw);
                jsonMessage.Flag = true;
                jsonMessage.Data = sb.ToString();
            } catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            } finally {
                if (xtw != null)
                    xtw.Close();
            }
            return jsonMessage;
        }

        public static JsonMessage ToObject(string text, string type) {
            var jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "Xml内容不能为空";
                return jsonMessage;
            }
            try {
                if (string.IsNullOrEmpty(type)) {
                    type = "c#";
                }
                type = type.ToLower();
                XmlTransfer transfer = new XmlTransfer(type);
                jsonMessage.Flag = true;
                jsonMessage.Data = transfer.XmlToObject(text);
            } catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }
    }
}
