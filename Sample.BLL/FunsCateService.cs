using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.BLL
{
    public class FunsCateService : BaseService
    {
        public static PageList<FunsCate> GetList(int pageIndex, int pageSize) {
            PageList<FunsCate> result = new PageList<FunsCate>();
            SearchItem item = new SearchItem();
            item.Fields = @"*";
            item.Orderby = " GroupIndex asc";
            item.PageIndex = pageIndex;
            item.PageSize = pageSize;
            item.Tables = @"FunsCate";
            return SearchByPage<FunsCate>(item);
        }

        public static List<FunsCate> GetList(bool getFuns = true) {
            var slt = Conn.Get().Query<FunsCate>("select top 100 * from funscate order by groupindex asc");
            if (slt != null) {
                foreach (var item in slt) {
                    item.Funs = FunsService.GetListByCateID(item.ID);
                }
            }
            return slt != null ? slt.ToList() : null;
        }

        public static JsonMessage Add(FunsCate model) {
            JsonMessage slt = new JsonMessage() { Flag = false };
            slt.Message = Validate(model);
            if (string.IsNullOrEmpty(slt.Message)) {
                string sql = "insert into FunsCate(Name,Code,ParentID,Intime,GroupIndex)values(@name,@code,@parentid,@intime,@groupindex);";
                var result = Conn.Get().Execute(sql, new {
                    name = model.Name,
                    code = model.Code,
                    parentid = model.ParentID,
                    intime = DateTime.Now,
                    groupindex = model.GroupIndex
                });
                slt.Flag = result > 0;
            }
            return slt;
        }

        public static JsonMessage Delete(int id) {
            JsonMessage slt = new JsonMessage() { Flag = false };
            if (string.IsNullOrEmpty(slt.Message)) {
                string sql = "delete from FunsCate where id=@id";
                var result = Conn.Get().Execute(sql, new { id = id });
                slt.Flag = result > 0;
            }
            return slt;
        }

        public static string Validate(FunsCate model) {
            string slt = string.Empty;
            if (model == null) {
                slt = "实体不能为空";
            } else if (string.IsNullOrEmpty(model.Name)) {
                slt = "名称不能为空";
            } 
            return slt;
        }
    }
}
