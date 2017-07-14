using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class TransferController : Controller
    {
        //
        // GET: /Transfer/

        public ActionResult Dbc()
        {
            return View();
        }

        public ActionResult Upper() {
            return View();
        }

        public ActionResult Unix() {
            return View();
        }

        public ActionResult Len() {
            return View();
        }

        public ActionResult Guid() {
            return View();
        }

        public ActionResult Url() {
            return View();
        }

    }
}
