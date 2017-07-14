using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Model
{
    public class PageList<T>
    {
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 当前页码（第一页：1）
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页展示条目数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总条目数
        /// </summary>
        public int TotalItem { get; set; }
    }
}
