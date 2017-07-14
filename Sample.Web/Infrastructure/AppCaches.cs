using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Web.Infrastructure
{
    public class AppCaches
    {
        /// <summary>
        /// 在HttpRuntime.Cache缓存中获取指定的键值。
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static string AppGet(string key) {
            string result = null;
            var dval = HttpRuntime.Cache[key];
            if (dval != null) {
                result = dval.ToString();
            }
            return result;
        }

        /// <summary>
        /// 在HttpRuntime.Cache缓存中获取指定的键值。
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static T AppGet<T>(string key) {
            T result = default(T);
            var val = HttpRuntime.Cache[key];
            if (val != null) {
                result = (T)val;
            }
            return result;
        }

        /// <summary>
        /// 在HttpRuntime.Cache缓存中存储键值。
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="val">值</param>
        public static void AppSet(string key, string val) {
            if (val != null) {
                HttpRuntime.Cache.Insert(key, val, null, DateTime.Now.AddMinutes(60),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default,
                    null);
            } else {
                AppRemove(key);
            }
        }

        /// <summary>
        /// 在HttpRuntime.Cache缓存中存储键值。
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="val">值</param>
        /// <param name="expire">超时时间</param>
        public static void AppSet(string key, string val, DateTime expire) {
            if (val != null) {
                var dexp = DateTime.Now.AddMinutes(60);
                HttpRuntime.Cache.Insert(key, val, null, expire < dexp ? expire : dexp,
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default,
                    null);
            } else {
                AppRemove(key);
            }
        }

        /// <summary>
        /// 在HttpRuntime.Cache缓存中存储键值。
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="val">值</param>
        public static void AppSet(string key, object val) {
            if (val != null) {
                HttpRuntime.Cache.Insert(key, val, null, DateTime.Now.AddMinutes(30),
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default,
                    null);
            } else {
                AppRemove(key);
            }
        }

        /// <summary>
        /// 在HttpRuntime.Cache缓存中存储键值。
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="val">值</param>
        /// <param name="expire">超时时间</param>
        public static void AppSet(string key, object val, DateTime expire) {
            if (val != null) {
                var dexp = DateTime.Now.AddMinutes(60);
                HttpRuntime.Cache.Insert(key, val, null, expire < dexp ? expire : dexp,
                    System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default,
                    null);
            } else {
                AppRemove(key);
            }
        }

        public static bool AppContains(string key) {
            return HttpRuntime.Cache[key] != null;
        }

        /// <summary>
        /// 从HttpRuntime.Cache缓存中移除一个缓存键值。
        /// </summary>
        /// <param name="key">键名称</param>
        public static void AppRemove(string key) {
            HttpRuntime.Cache.Remove(key);
        }
    }
}