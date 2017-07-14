using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class FormatController : Controller
    {
        //
        // GET: /Format/

        public ActionResult Js()
        {
            return View();
        }

        public ActionResult Html() {
            return View();
        }

        public ActionResult Css() {
            return View();
        }

        public ActionResult Json() {
            return View();
        }

        public ActionResult Xml() {
            return View();
        }

        public ActionResult Sql() {
            return View();
        }
    }
}
