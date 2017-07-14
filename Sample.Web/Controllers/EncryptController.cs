using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class EncryptController : Controller
    {
        //
        // GET: /Encrypt/

        public ActionResult Md5()
        {
            return View();
        }

        public ActionResult Base64() {
            return View();
        }

        public ActionResult FileMd5() {
            return View();
        }

        public ActionResult Image64() {
            return View();
        }

    }
}
