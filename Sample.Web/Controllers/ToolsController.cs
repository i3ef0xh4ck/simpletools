using Sample.BLL;
using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class ToolsController : Controller
    {

        public ActionResult Encode()
        {
            return View();
        }

        public ActionResult Encrypt() {
            return View();
        }

        public ActionResult Transfer() {
            return View();
        }

        public ActionResult Compress() {
            return View();
        }

        public ActionResult Entity() {
            return View();
        }

        public ActionResult Unit() {
            return View();
        }

        public ActionResult Ercode() {
            return View();
        }

        public ActionResult Barcode() {
            return View();
        }

        public ActionResult Docs() {
            return View();
        }

        public ActionResult Regex() {
            return View();
        }

        public ActionResult Http() {
            ViewBag.HttpModel = HttpService.GetHostInfo();
            return View();
        }

        public ActionResult Front() {
            return View();
        }

        public ActionResult Query() {
            ViewBag.Today = QueryService.GetToday();
            return View();
        }
    }
}