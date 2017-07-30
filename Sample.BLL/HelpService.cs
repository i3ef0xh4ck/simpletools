using Sample.Model;
using Simple;
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

        public static JsonMessage Add(Help model) {
            JsonMessage slt = new JsonMessage() { Flag = false };
            slt.Message = Validate(model);
            if (string.IsNullOrEmpty(slt.Message)) {
                string sql = "insert into Help(Name,Source,[Money],Intime)values(@name,@source,@money,@intime);";
                var result = Conn.Get().Execute(sql, new { 
                    name = model.Name,
                    source = model.Source,
                    money = model.Money,
                    intime = model.Intime.Value
                });
                slt.Flag = result > 0;
            }
            return slt;
        }

        public static JsonMessage Delete(int id) {
            JsonMessage slt = new JsonMessage() { Flag = false };
            if (string.IsNullOrEmpty(slt.Message)) {
                string sql = "delete from Help where id=@id";
                var result = Conn.Get().Execute(sql, new {id = id});
                slt.Flag = result > 0;
            }
            return slt;
        }

        public static string Validate(Help model) {
            string slt = string.Empty;
            if (model == null) {
                slt = "实体不能为空";
            } else if (model.Intime == null) {
                slt = "时间不能为空";
            } else if (string.IsNullOrEmpty(model.Source)) {
                slt = "来源不能为空";
            } else if (string.IsNullOrEmpty(model.Name)) {
                slt = "赞助人不能为空";
            } else if (model.Money <= 0) {
                slt = "金额必须大于0";
            }
            return slt;
        }

        public static decimal Sum() {
            return Conn.Get().ExecuteScalar<decimal>("select sum(money) from Help");
        }
    }
}
