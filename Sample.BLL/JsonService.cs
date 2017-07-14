using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.BLL
{
    public class JsonService
    {
        public static JsonMessage JsonFormat(string text) {
            JsonMessage jsonMessage = new JsonMessage();
            if (string.IsNullOrEmpty(text)) {
                jsonMessage.Flag = false;
                jsonMessage.Message = "Json内容不能为空";
                return jsonMessage;
            }
            try {
                jsonMessage.Flag = true;
                jsonMessage.Data = JsonUtils.ConvertJsonString(text);
            }
            catch (Exception ex) {
                jsonMessage.Flag = false;
                jsonMessage.Message = ex.Message;
            }
            return jsonMessage;
        }
    }
}
