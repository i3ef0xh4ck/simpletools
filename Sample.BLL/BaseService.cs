using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sample.BLL
{
    public class BaseService
    {
        public static PageList<T> SearchByPage<T>(SearchItem filter) {
            PageList<T> result = new PageList<T>();
            var p = new DynamicParameters();
            p.Add("@TableName", filter.Tables);
            p.Add("@ReFieldsStr", filter.Fields);
            p.Add("@OrderString", filter.Orderby);
            p.Add("@WhereString", string.IsNullOrEmpty(filter.Filter) ? "" : filter.Filter);
            p.Add("@PageSize", filter.PageSize);
            p.Add("@PageIndex", filter.PageIndex);
            p.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            result.Data = Conn.Get().Query<T>("UP_PageData2005", p, commandType: CommandType.StoredProcedure).ToList();
            result.TotalItem = p.Get<int>("@TotalRecord");
            result.PageIndex = filter.PageIndex;
            result.PageSize = filter.PageSize;
            return result;
        }
    }
}
