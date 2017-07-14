using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class ErcodeController : Controller
    {
        //
        // GET: /Ercode/

        public ActionResult Create()
        {
            return View();
        }
    }
}
