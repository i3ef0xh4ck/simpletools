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

        public static JsonMessage Add(Logs model) {
            JsonMessage slt = new JsonMessage() { Flag = false };
            slt.Message = Validate(model);
            if (string.IsNullOrEmpty(slt.Message)) {
                string sql = "insert into Logs(Content,Intime)values(@content,@intime);";
                var result = Conn.Get().Execute(sql, new {
                    content = model.Content,
                    intime = model.Intime.Value
                });
                slt.Flag = result > 0;
            }
            return slt;
        }

        public static JsonMessage Delete(int id) {
            JsonMessage slt = new JsonMessage() { Flag = false };
            if (string.IsNullOrEmpty(slt.Message)) {
                string sql = "delete from Logs where id=@id";
                var result = Conn.Get().Execute(sql, new { id = id });
                slt.Flag = result > 0;
            }
            return slt;
        }

        public static string Validate(Logs model) {
            string slt = string.Empty;
            if (model == null) {
                slt = "实体不能为空";
            } else if (model.Intime == null) {
                slt = "时间不能为空";
            } else if (string.IsNullOrEmpty(model.Content)) {
                slt = "内容不能为空";
            }
            return slt;
        }
    }
}
