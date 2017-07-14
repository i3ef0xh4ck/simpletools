using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class FrontController : Controller
    {
        //
        // GET: /Front/

        public ActionResult Color()
        {
            return View();
        }

        public ActionResult Hex() {
            return View();
        }

        public ActionResult Rem() {
            return View();
        }

        public ActionResult MarkDown() {
            return View();
        }
    }
}
