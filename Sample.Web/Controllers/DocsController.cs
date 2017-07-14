using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class DocsController : Controller
    {
        //
        // GET: /Docs/

        public ActionResult Http()
        {
            return View();
        }

        public ActionResult ContentType() {
            return View();
        }

        public ActionResult Html() {
            return View();
        }

        public ActionResult Ascii() {
            return View();
        }
    }
}
