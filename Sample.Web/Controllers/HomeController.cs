﻿using Sample.BLL;
using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.FunsCates = FunsCateService.GetList(true);
            return View();
        }
    }
}