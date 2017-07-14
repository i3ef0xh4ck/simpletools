using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple
{
    public class JsonUtils
    {
        public static string Serialize(object item)
        {
            if (item == null) return string.Empty;
            return JsonConvert.SerializeObject(item, Formatting.Indented, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm" });
        }

        public static T Deserialize<T>(string value)
        {
            if (string.IsNullOrEmpty(value)) return default(T);
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static string ConvertJsonString(string str) {
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null) {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter) {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else {
                return str;
            }
        }
    }
}
