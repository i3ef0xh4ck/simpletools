using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.BLL
{
    public class HelpService : BaseService
    {
        public static PageList<Help> GetList(int pageIndex, int pageSize) {
            PageList<Help> result = new PageList<Help>();
            SearchItem item = new SearchItem();
            item.Fields = @"*";
            item.Orderby = " Intime desc";
            item.PageIndex = pageIndex;
            item.PageSize = pageSize;
            item.Tables = @"Help";
            return SearchByPage<Help>(item);
        }
    }
}
