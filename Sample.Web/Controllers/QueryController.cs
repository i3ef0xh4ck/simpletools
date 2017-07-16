using Sample.BLL;
using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class QueryController : Controller
    {
        public ActionResult Local() {
            var temp = Request.Url.AbsolutePath;
            ViewBag.HttpModel = HttpService.GetHostInfo();
            return View();
        }

        public ActionResult Http() {
            return View();
        }

        public ActionResult IP()
        {
            return View();
        }

        public ActionResult Domain() {
            return View();
        }

        public ActionResult Phone() {
            return View();
        }

        public ActionResult Today() {
            ViewBag.Today = QueryService.GetToday();
            return View();
        }

    }
}
