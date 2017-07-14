using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class UnitController : Controller
    {
        //
        // GET: /Unit/

        public ActionResult Binary()
        {
            return View();
        }

        public ActionResult Time() {
            return View();
        }
    }
}
