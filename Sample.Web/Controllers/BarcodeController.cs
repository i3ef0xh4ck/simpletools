using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class BarcodeController : Controller
    {
        //
        // GET: /Barcode/

        public ActionResult Create()
        {
            return View();
        }

    }
}
