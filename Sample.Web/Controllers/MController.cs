using Sample.BLL;
using Sample.Model;
using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class MController : Controller
    {
        //
        // GET: /M/

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Help(int page = 1) {
            int pageSize = 100;
            var list = HelpService.GetList(page, pageSize);
            ViewBag.Total = list.TotalItem;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.TotalItem * 1.0 / pageSize);
            return View(list.Data);
        }

        public ActionResult Talk(int page = 1) {
            int pageSize = 20;
            var list = MessageService.GetList(page, pageSize);
            ViewBag.Total = list.TotalItem;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.TotalItem * 1.0 / pageSize);
            return View(list.Data);
        }

        [HttpPost]
        public JsonResult TalkList(int page = 1) {
            int pageSize = 20;
            var list = MessageService.GetList(page, pageSize);
            ViewBag.Total = list.TotalItem;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.TotalItem * 1.0 / pageSize);            
            return Json(list.Data);
        }

        [HttpPost]
        public JsonResult LeaveMessage(Message m) {
            JsonMessage jm = new JsonMessage() { Flag = false };
            if (string.IsNullOrEmpty(m.Code)) {
                jm.Message = "请输入验证码";
            } else if (Session["authcode"] == null) {
                jm.Message = "验证码过期";
            } else if (m.Code.ToLower() != Session["authcode"].ToString().ToLower()) {
                jm.Message = "验证码不正确";
            } else {
                jm = MessageService.Insert(m);
                Session["authcode"] = null;
            }
            return Json(jm);
        }

        public ActionResult Logs(int page = 1) {
            int pageSize = 100;
            var list = LogService.GetList(page, pageSize);
            ViewBag.Total = list.TotalItem;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.TotalItem * 1.0 / pageSize);
            return View(list.Data);
        }

        public ActionResult Funs() {
            return View();
        }

        public ActionResult Friends() {
            return View();
        }

        public ActionResult Ad()
        {
            return View();        
        }

    }
}
