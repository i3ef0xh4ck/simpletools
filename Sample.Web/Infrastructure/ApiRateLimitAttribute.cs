using Newtonsoft.Json;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Infrastructure
{
    public class ApiRateLimitAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            var ckey = "APIRATE_" + AppUtils.GetClientIP(filterContext.HttpContext.Request);
            var counter = AppCaches.AppGet<RateCounter>(ckey);
            if (counter == null) {
                counter = new RateCounter();
                AppCaches.AppSet(ckey, counter);
            }
            if (!counter.IsOverstepSeted) {
                this.Increase(counter);
            }
            if (counter.IsOverstepSeted) {
                if (AutoResult) {
                    filterContext.Result = new ContentResult() { Content = "频率太快了，不要刷了" };
                }
                else {
                    filterContext.Controller.TempData["OUT_COUNT"] = "1";
                }
            }
        }

        /// <summary>
        /// API简单限制访问频率。
        /// </summary>
        /// <param name="duration">单个周期时长（秒）</param>
        /// <param name="cycleNumber">单周期最大计数</param>
        /// <param name="overstep">允许超频次数</param>
        public ApiRateLimitAttribute(int duration, int cycleNumber, int overstep) {
            this.LimitDuration = duration;
            this.LimitCycleNumber = cycleNumber;
            this.LimitOverstep = overstep;
            this.AutoResult = true;
        }

        /// <summary>
        /// API简单限制访问频率（超频上限3次）。
        /// </summary>
        /// <param name="duration">单个周期时长（秒）</param>
        /// <param name="cycleNumber">单周期最大计数</param>
        public ApiRateLimitAttribute(int duration, int cycleNumber) : this(duration, cycleNumber, 3) {

        }

        /// <summary>
        /// API简单限制一分钟内访问频率（超频上限3次）。
        /// </summary>
        /// <param name="duration">单个周期时长（秒）</param>
        /// <param name="cycleNumber">单周期最大计数</param>
        /// <param name="overstep">允许超频次数</param>
        public ApiRateLimitAttribute(int cycleNumber) : this(60, cycleNumber, 3) {

        }

        /// <summary>
        /// API简单限制一分钟内不能超过45次访问（超频上限3次）。
        /// </summary>
        public ApiRateLimitAttribute() : this(60, 45, 3) {

        }

        public bool AutoResult { get; set; }

        /// <summary>
        /// 获取或设置允许周期超频次数。
        /// </summary>
        public int LimitOverstep { get; private set; }
        /// <summary>
        /// 获取或设置计算周期时长（秒）。
        /// </summary>
        public int LimitDuration { get; private set; }
        /// <summary>
        /// 获取或设置单个周期内的最大计数。
        /// </summary>
        public int LimitCycleNumber { get; private set; }

        /// <summary>
        /// 在当前频率计数域内为指定计数器递增。
        /// </summary>
        /// <param name="counter">计数器对象</param>
        /// <returns>返回计数递增后，计数器是否超频。</returns>
        private bool Increase(RateCounter counter) {
            var result = counter.IsOverstepSeted;
            var now = DateTime.Now;
            var span = now - counter.TimePoint;
            if (span.TotalSeconds >= LimitDuration) {
                counter.Reset(now);
                counter.Count = 1;
                counter.Overstep = 0;
                counter.IsOverstepSeted = false;
            }
            else {
                ++counter.Count;
                if (!result && counter.Count > LimitCycleNumber) {
                    ++counter.Overstep;
                    counter.Reset(now);
                    if (counter.Overstep >= LimitOverstep) {
                        result = counter.IsOverstepSeted = true;
                    }
                }
            }
            return result;
        }
    }

    [Serializable]
    public class RateCounter
    {
        /// <summary>
        /// 获取计数器最后周期开始时间。
        /// </summary>
        public DateTime TimePoint { get; internal set; }

        /// <summary>
        /// 获取当前时间周期内计数。
        /// </summary>
        public int Count {
            get;
            internal set;
        }
        /// <summary>
        /// 获取当前已超频次数。
        /// </summary>
        public int Overstep { get; internal set; }
        /// <summary>
        /// 获取计数是否以超出累积超频次数。
        /// </summary>
        public bool IsOverstepSeted { get; internal set; }

        public void Reset(DateTime now) {
            this.TimePoint = now;
            Count = 0;
        }

        public void Reset() {
            Reset(DateTime.Now);
        }
    }
}