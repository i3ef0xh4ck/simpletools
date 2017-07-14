using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Simple
{
    public class AppEnum
    {
        /// <summary>
        /// 日志操作类型
        /// </summary>
        public enum OperType
        {
            [Description("添加")]
            Insert = 1,
            [Description("修改")]
            Update = 2,
            [Description("删除")]
            Delete = 3,
            [Description("置顶")]
            Top = 4,
            [Description("推荐")]
            Recommend = 5,
            [Description("回复")]
            Reply = 6,
            [Description("显示")]
            Show = 7,
            [Description("上架")]
            ProductUp = 8,
            [Description("下架")]
            ProductDown = 9
        }
    }
}
