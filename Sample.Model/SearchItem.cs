using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Model
{
    public class SearchItem
    {
        /// <summary>
        /// 【必填】当前页码（第一页：1）
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 【必填】每页展示条目数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 【必填】需要排序的字段（示例：A.AddTime desc）
        /// </summary>
        public string Orderby { get; set; }
        /// <summary>
        /// 【必填】需要查询的表名
        /// </summary>
        public string Tables { get; set; }
        /// <summary>
        /// 【必填】需要查询的字段
        /// </summary>
        public string Fields { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string Filter { get; set; }
    }
}
