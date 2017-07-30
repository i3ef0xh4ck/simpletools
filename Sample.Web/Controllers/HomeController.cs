using Sample.BLL;
using Sample.Model;
using Sample.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string keyword = Request.Form["name"];
            var cates = FunsCateService.GetList(true);
            if (!string.IsNullOrEmpty(keyword)) {
                foreach (var item in cates) {
                    item.Funs = Filter(source: item.Funs, keyword: keyword);
                }
            }
            ViewBag.FunsCates = cates;
            ViewBag.KeyWord = keyword;
            return View();
        }

        public ActionResult Statistics() {
            return View();
        }

        [NonAction]
        private List<Funs> Filter(List<Funs> source, string keyword) {
            List<Funs> slt = new List<Funs>();
            keyword = keyword.ToLower().Trim();
            slt = source.AsParallel().Where(n => n.Name.ToLower().Contains(keyword)).ToList();
            return slt;
        }
    }
}