using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Simple
{
    public static class Expansion
    {
        #region Object Ext
        /// <summary>
        /// 返回当前对象的字符串表示，若为空则返回指定默认字符串。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="def">当对象为空时要返回的值。</param>
        /// <returns></returns>
        public static string ToStringOrDefault(this object obj, string def)
        {
            return obj == null ? def : obj.ToString();
        }
        /// <summary>
        /// 返回当前对象的字符串表示，若为空则返回指定默认字符串。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="def">当对象为空时要返回的值。</param>
        /// <returns></returns>
        public static string ToStringOrDefault(this object obj)
        {
            return ToStringOrDefault(obj, string.Empty);
        }

        /// <summary>
        /// 将对象转换为Int32类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static Int32 ToInt32(this object obj, int defValue = 0)
        {
            int result = 0;
            if (obj is int || obj is Enum)
                result = (int)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (!int.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将Boolean转换为Int32类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static Int32 ToInt32(this bool b, int defValue = 0)
        {
            return b ? 1 : 0;
        }

        public static Int32 ToInt32(this decimal dec)
        {
            return decimal.ToInt32(dec);
        }

        public static Int32 ToInt32(this double d)
        {
            return (int)d;
        }

        public static Int32 ToInt32(this float f)
        {
            return (int)f;
        }

        public static Int32 ToInt32(this char c)
        {
            return (int)c - 48;
        }

        /// <summary>
        /// 将对象转换为Int64类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static Int64 ToInt64(this object obj, int defValue = 0)
        {
            long result = 0;
            if (obj is long || obj is Enum)
                result = (long)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (!long.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将Boolean转换为Int64类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static Int64 ToInt64(this bool b, int defValue = 0)
        {
            return b ? 1L : 0L;
        }

        public static Int64 ToInt64(this decimal dec)
        {
            return decimal.ToInt64(dec);
        }

        public static Int64 ToInt64(this double d)
        {
            return (long)d;
        }

        public static Int64 ToInt64(this float f)
        {
            return (long)f;
        }

        public static Int64 ToInt64(this char c)
        {
            return (long)c - 48L;
        }

        /// <summary>
        /// 将对象转换为单精度浮点型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static float ToFloat(this object obj, float defValue = 0f)
        {
            float result = 0f;
            if (obj is float)
                result = (float)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                float.TryParse(obj.ToString(), out result);
                if (result.Equals(0f) && !defValue.Equals(0f))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将对象转换为Decimal类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj, decimal defValue = 0m)
        {
            decimal result = 0m;
            if (obj is decimal)
                result = (decimal)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (!decimal.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将对象转换为双精度浮点型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double defValue = 0d)
        {
            double result = 0d;
            if (obj is double)
                result = (double)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (!double.TryParse(obj.ToString(), out result))
                {
                    result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 将对象转换为日期时间类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            DateTime result = DateTime.MinValue;
            if (obj != null)
                DateTime.TryParse(obj.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将对象转换为日期时间类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">转换无效时的默认值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj, DateTime defValue)
        {
            DateTime result = defValue;
            if (obj is DateTime)
                result = (DateTime)obj;
            else if (obj == null)
                result = defValue;
            else
            {
                if (obj != null)
                {
                    if (!DateTime.TryParse(obj.ToString(), out result))
                        result = defValue;
                }
            }
            return result;
        }

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateFormat(this DateTime obj, string format = "yyyy-MM-dd")
        {
            return string.Format("{0:" + format + "}", obj);
        }

        /// <summary>
        /// 将对象转换为布尔类型。
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="defValue">当转换无效时的默认值</param>
        /// <returns></returns>
        public static bool ToBool(this object obj, bool defValue = false)
        {
            bool result = false;
            if (obj is bool)
                result = (bool)obj;
            else if (obj == null)
                result = defValue;
            else if (obj is int)
                result = !((int)obj).Equals(0);
            else
            {
                if (obj != null)
                {
                    var s = obj.ToString();
                    if (!(result = s.Equals("1")) && (result = !s.Equals("0")))
                    {
                        if (!bool.TryParse(s, out result))
                            result = defValue;
                    }
                }
            }
            return result;
        }

        public static T ToEnum<T>(this object obj, T defValue)
        {
            T result = defValue;
            if (obj is T)
                result = (T)obj;
            else if (obj == null)
                result = defValue;
            else if (obj is int)
                result = (T)obj;
            else
            {
                if (obj != null)
                    try
                    {
                        result = (T)Enum.Parse(typeof(T), obj.ToString());
                    }
                    catch
                    {
                        result = defValue;
                    }
            }
            return result;
        }
        #endregion

        #region string expansion
        /// <summary>
        /// 去除图片路径后的时间戳 ?t=5877
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveDateSpan(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return Regex.Replace(str, "|\\?t=.+", "", RegexOptions.IgnoreCase);
        }
        #endregion
    }
}
