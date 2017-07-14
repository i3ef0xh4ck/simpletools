using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Simple
{
    public class ValidateUtils
    {
        static readonly Regex RE_EMAIL = new Regex(@"^[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)*@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$");
        public static bool IsEmail(string email)
        {
            if (email == null) return false;
            return RE_EMAIL.IsMatch(email);
        }

        static readonly Regex RE_CELLPHONE = new Regex(@"^[1][3-8]\d{9}$");
        public static bool IsCellPhone(string cellphone)
        {
            if (cellphone == null) return false;
            return RE_CELLPHONE.IsMatch(cellphone);
        }

        public static bool IsJsonRequest()
        {
            var context = HttpContext.Current;
            return context != null && context.Request.Headers["Content-Type"] != null && context.Request.Headers["Content-Type"].IndexOf("application/json",
                StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        private class ExtArray
        {
            public static string[] ImageExt = { "255216", "7173", "13780", "6677" };
            public static string[] MP3Ext = { "255251", "255250", "7368" };
        }

        /// <summary>
        /// 根据文件头判断上传的文件类型
        /// </summary>
        /// <modifyLog>
        /// @2015-1-22 @yaowq @新增
        /// </modifyLog>
        /// <param name="fu">fu是上传的文件 </param>
        /// <returns>返回true或false</returns>
        public static bool IsPicture(HttpPostedFileBase fu)
        {
            int fileLen = fu.ContentLength;
            byte[] imgArray = new byte[fileLen];
            fu.InputStream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            finally
            {
                br.Close();
                ms.Close();
                fu.InputStream.Position = 0;
            }
            return Array.IndexOf(ExtArray.ImageExt, fileclass) > -1;
            if (fileclass == "255216" || fileclass == "7173" || fileclass == "13780" || fileclass == "6677")//255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
                return true;
            return false;
        }

        public static bool IsMP3(HttpPostedFileBase fu)
        {
            int fileLen = fu.ContentLength;
            byte[] imgArray = new byte[fileLen];
            fu.InputStream.Read(imgArray, 0, fileLen);
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            finally
            {
                br.Close();
                ms.Close();
                fu.InputStream.Position = 0;
            }
            return Array.IndexOf(ExtArray.MP3Ext, fileclass) > -1;
            if (fileclass == "255216" || fileclass == "7173" || fileclass == "13780" || fileclass == "6677")//255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
                return true;
            return false;
        }

        /// <summary>
        /// 根据文件头判断上传的文件类型
        /// </summary>
        /// <modifyLog>
        /// @2015-1-22 @yaowq @新增
        /// </modifyLog>
        /// <param name="imgArray">字节数组</param>
        /// <returns>返回true或false</returns>
        public static bool IsPicture(byte[] imgArray)
        {
            MemoryStream ms = new MemoryStream(imgArray);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer;
            try
            {
                buffer = br.ReadByte();
                fileclass = buffer.ToString();
                buffer = br.ReadByte();
                fileclass += buffer.ToString();
            }
            catch
            {
            }
            finally
            {
                br.Close();
                ms.Close();
            }
            return Array.IndexOf(ExtArray.ImageExt, fileclass) > -1;
            if (fileclass == "255216" || fileclass == "7173" || fileclass == "13780" || fileclass == "6677")//255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar 
                return true;
            return false;
        }

    }
}
