using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class EncodeController : Controller
    {
        //
        // GET: /Encode/

        public ActionResult Url()
        {
            return View();
        }

        public ActionResult Html() {
            return View();
        }

        public ActionResult Ascii() {
            return View();
        }

        public ActionResult Utf8() {
            return View();
        }

        public ActionResult Unicode() {
            return View();
        }

    }
}
