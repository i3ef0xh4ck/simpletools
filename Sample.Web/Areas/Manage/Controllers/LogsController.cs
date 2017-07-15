﻿using Sample.BLL;
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
    public class LogsController : Controller
    {
        public ActionResult List(int page = 1) {
            int pageSize = 10;
            var list = LogService.GetList(page, pageSize);
            ViewBag.Total = list.TotalItem;
            ViewBag.PageIndex = page;
            ViewBag.TotalPages = Math.Ceiling(list.TotalItem * 1.0 / pageSize);
            return View(list.Data);
        }

        [HttpPost]
        public JsonResult Add(Logs model) {
            return Json(LogService.Add(model));
        }

        [HttpPost]
        public JsonResult Delete(int id) {
            return Json(LogService.Delete(id));
        }
    }
}
