using Newtonsoft.Json;
using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.BLL
{
    public class LogService:BaseService
    {
        public static PageList<Logs> GetList(int pageIndex, int pageSize) {
            PageList<Logs> result = new PageList<Logs>();
            SearchItem item = new SearchItem();
            item.Fields = @"*";
            item.Orderby = " Intime desc";
            item.PageIndex = pageIndex;
            item.PageSize = pageSize;
            item.Tables = @"Logs";
            return SearchByPage<Logs>(item);
        }
    }
}
