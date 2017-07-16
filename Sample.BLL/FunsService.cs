using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.BLL
{
    public class FunsService : BaseService
    {
        public static PageList<Funs> GetList(int pageIndex, int pageSize, int cateID) {
            PageList<Funs> result = new PageList<Funs>();
            SearchItem item = new SearchItem();
            item.Fields = @"f.ID,f.CateID,c.Name as CateName,f.Name,f.Url,f.Icon,f.VisitCount,f.GroupIndex,f.Intime";
            item.Orderby = " f.CateID asc, f.GroupIndex asc";
            item.PageIndex = pageIndex;
            item.PageSize = pageSize;
            item.Tables = @"funs f left join funscate c on f.CateID = c.ID";
            if (cateID > 0) {
                item.Filter = " f.CateID=" + cateID;
            }
            return SearchByPage<Funs>(item);
        }

        public static List<Funs> GetList() {
            string sql = @"select * from funs order by GroupIndex asc";
            var result = Conn.Get().Query<Funs>(sql);
            return result != null ? result.ToList() : null;
        }

        public static List<Funs> GetListByCateID(int cateID) {
            string sql = @"select f.ID,f.CateID,c.Name as CateName,f.Name,f.Url,f.Icon,f.VisitCount,f.GroupIndex,f.Intime 
                           from funs f left join funscate c on f.CateID = c.ID order by f.GroupIndex asc";
            var result = Conn.Get().Query<Funs>(sql, new { cateID = cateID });
            return result != null ? result.ToList() : null;
        }
        
        public static JsonMessage Add(Funs model) {
            JsonMessage slt = new JsonMessage() { Flag = false };
            slt.Message = Validate(model);
            if (string.IsNullOrEmpty(slt.Message)) {
                string sql = "insert into Funs(CateID,Name,Url,Icon,VisitCount,GroupIndex,Intime)values(@CateID,@Name,@Url,@Icon,@VisitCount,@GroupIndex,@Intime);";
                var result = Conn.Get().Execute(sql, new {
                    CateID = model.CateID,
                    Name = model.Name,
                    Url = model.Url,
                    Icon = model.Icon,
                    VisitCount = 0,
                    GroupIndex = model.GroupIndex,
                    Intime = DateTime.Now                   
                });
                slt.Flag = result > 0;
            }
            return slt;
        }

        public static JsonMessage Delete(int id) {
            JsonMessage slt = new JsonMessage() { Flag = false };
            if (string.IsNullOrEmpty(slt.Message)) {
                string sql = "delete from Funs where id=@id";
                var result = Conn.Get().Execute(sql, new { id = id });
                slt.Flag = result > 0;
            }
            return slt;
        }

        public static JsonMessage VisitPlus(int id) {
            JsonMessage slt = new JsonMessage() { Flag = false };
            if (string.IsNullOrEmpty(slt.Message)) {
                string sql = "update Funs set VisitCount = isnull(VisitCount,0) + 1 where id=@id";
                var result = Conn.Get().Execute(sql, new { id = id });
                slt.Flag = result > 0;
            }
            return slt;
        }

        public static string Validate(Funs model) {
            string slt = string.Empty;
            if (model == null) {
                slt = "实体不能为空";
            } else if (string.IsNullOrEmpty(model.Name)) {
                slt = "名称不能为空";
            } else if (string.IsNullOrEmpty(model.Url)) {
                slt = "链接不能为空";
            } 
            return slt;
        }
    }
}
