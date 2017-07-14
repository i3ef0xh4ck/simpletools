using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class EntityController : Controller
    {
        //
        // GET: /Entity/

        public ActionResult Json()
        {
            return View();
        }

        public ActionResult Xml() {
            return View();
        }

        public ActionResult String() {
            return View();
        }
    }
}
