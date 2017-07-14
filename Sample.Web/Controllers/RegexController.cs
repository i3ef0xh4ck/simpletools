using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class RegexController : Controller
    {
        //
        // GET: /Regex/

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Docs() {
            return View();
        }
    }
}
