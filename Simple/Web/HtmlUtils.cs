using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Simple
{
    public class HtmlUtils
    {
        #region Html编码
        /// <summary>
        /// Html编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HtmlEncode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return HttpUtility.HtmlEncode(input);
        }
        #endregion

        #region Html解码
        /// <summary>
        /// Html解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HtmlDecode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return HttpUtility.HtmlDecode(input);
        }
        #endregion

        #region Url编码
        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UrlEncode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return HttpUtility.UrlEncode(input);
        }

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string UrlEncode(string input, Encoding encode)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return HttpUtility.UrlEncode(input, encode);
        }
        #endregion

        #region Url解码
        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UrlDecode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return HttpUtility.UrlDecode(input);
        }

        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string UrlDecode(string input, Encoding encode)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            return HttpUtility.UrlDecode(input, encode);
        }
        #endregion

        #region Base64编码
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBase64String(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            byte[] bytes = Encoding.Default.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string ToBase64String(string input, Encoding encode)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            byte[] bytes = encode.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }
        #endregion

        #region Base64解码
        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FromBase64String(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            byte[] outputb = Convert.FromBase64String(input);
            return Encoding.Default.GetString(outputb);
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string FromBase64String(string input, Encoding encode)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            byte[] outputb = Convert.FromBase64String(input);
            return encode.GetString(outputb);
        }
        #endregion

        #region Base64图片编码
        /// <summary>
        /// Base64图片编码
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static string ImageToBase64(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return string.Empty;
            MemoryStream m = new MemoryStream();
            Bitmap bp = new Bitmap(imagePath);
            bp.Save(m, System.Drawing.Imaging.ImageFormat.Gif);
            byte[] b = m.GetBuffer();
            return Convert.ToBase64String(b);
        }
        #endregion

        #region Base64图片解码
        /// <summary>
        /// Base64图片解码
        /// </summary>
        /// <param name="base64string"></param>
        /// <returns></returns>
        public static Image ImageFromBase64(string base64string)
        {
            if (string.IsNullOrEmpty(base64string))
                return null;
            MemoryStream m = new MemoryStream();
            byte[] bt = Convert.FromBase64String(base64string);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(bt);
            Bitmap bitmap = new Bitmap(stream);
            return bitmap;
        }
        #endregion

        #region 清除HTML标记
        ///<summary>   
        ///清除HTML标记   
        ///</summary>   
        ///<param name="NoHTML">包括HTML的源码</param>   
        ///<returns>已经去除后的文字</returns>   
        public static string RemoveHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);

            //删除HTML   
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            Htmlstring = regex.Replace(Htmlstring, "");
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");

            return Htmlstring;
        }
        #endregion
    }
}
