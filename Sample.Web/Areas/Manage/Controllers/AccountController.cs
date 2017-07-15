using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Areas.Manage.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string name) {
            JsonMessage jm = new JsonMessage();
            if (name == "zhengfei") {
                jm.Flag = true;
                Session["admin"] = "zhengfei";
            } else {
                jm.Flag = false;
                jm.Message = "凭证错误";
            }
            return Json(jm);
        }

        [HttpPost]
        public void Quit() {
            Session["admin"] = null;
        }
    }
}
