using Sample.BLL;
using Sample.Model;
using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple;

namespace Sample.Web.Areas.Manage.Controllers
{
    [AdminValidation]
    public class FunsController : Controller
    {
        public ActionResult List(int page = 1) {
            int cateID = Request.Form["cate"].ToInt32(0);
            int pageSize = 100;
            var list = FunsService.GetList(page, pageSize, cateID);
            ViewBag.Total = list.TotalItem;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.TotalItem * 1.0 / pageSize);
            ViewBag.Cates = FunsCateService.GetList(false);
            ViewBag.CurrentCateID = cateID;
            return View(list.Data);
        }

        [HttpPost]
        public JsonResult Add(Funs model) {
            return Json(FunsService.Add(model));
        }

        [HttpPost]
        public JsonResult Delete(int id) {
            return Json(FunsService.Delete(id));
        }
    }
}
