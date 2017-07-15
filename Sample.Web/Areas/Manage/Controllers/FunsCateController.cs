using Sample.BLL;
using Sample.Model;
using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Areas.Manage.Controllers
{
    [AdminValidation]
    public class FunsCateController : Controller
    {
        public ActionResult List(int page = 1) {
            int pageSize = 100;
            var list = FunsCateService.GetList(page, pageSize);
            ViewBag.Total = list.TotalItem;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.TotalItem * 1.0 / pageSize);
            return View(list.Data);
        }

        [HttpPost]
        public JsonResult Add(FunsCate model) {
            return Json(FunsCateService.Add(model));
        }

        [HttpPost]
        public JsonResult Delete(int id) {
            return Json(FunsCateService.Delete(id));
        }
    }
}
