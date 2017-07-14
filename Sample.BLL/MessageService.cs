using Sample.Model;
using Simple;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Sample.BLL
{
    public class MessageService : BaseService
    {
        public static JsonMessage Insert(Message m) {
            JsonMessage jm = new JsonMessage() { Flag = false };
            if (string.IsNullOrEmpty(m.Content)) {
                jm.Message = "留言内容不能为空";
            } else {
                if (string.IsNullOrEmpty(m.Name)) {
                    m.Name = "匿名";
                }
                if (m.Content.Length > 500) {
                    m.Content = m.Content.Substring(0, 500);
                }
                m.Content = Regex.Replace(m.Content, "delete|update|exec|master|truncate|declare|create|xp_", "", RegexOptions.IgnoreCase);
                string sql = "insert into Message(Name,Content,Status,Intime) values (@Name,@Content,@Status,@Intime);";
                jm.Flag = Conn.Get().Execute(sql, new {
                    Name = m.Name,
                    Content = m.Content,
                    Status = 1,
                    Intime = DateTime.Now
                }) > 0;
            }
            return jm;
        }

        public static int GetMessageCount() {
            string sql = "select count(1) from message where status=1;";
            return Conn.Get().ExecuteScalar<int>(sql);
        }

        public static PageList<Message> GetList(int pageIndex, int pageSize) {
            PageList<Message> result = new PageList<Message>();
            SearchItem item = new SearchItem();
            item.Fields = @"*";
            item.Filter = " Status = 1";
            item.Orderby = " Intime desc";
            item.PageIndex = pageIndex;
            item.PageSize = pageSize;
            item.Tables = @"Message";
            var list = SearchByPage<Message>(item);
            if (list != null && list.Data != null) {
                foreach (var data in list.Data) {
                    data.IntimeStr = DateStringFromNow(data.Intime);
                }
            }
            return list;
        }

        public static string DateStringFromNow(DateTime dt) {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 7) {
                return string.Format("{0:yyyy-MM-dd HH:mm:ss}",dt);
            } else if (span.TotalDays > 1) {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            } else if (span.TotalHours > 1) {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            } else if (span.TotalMinutes > 1) {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            } else if (span.TotalSeconds >= 1) {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            } else {
                return "1秒前";
            }
        }
    }
}
