using Sample.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Sample.Model;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Sample.Web.Infrastructure;

namespace Sample.Web.Controllers
{
    public class FunController : Controller
    {
        [HttpPost]
        [ValidateInput(false)]
        [ApiRateLimit]
        public JsonResult XmlToObject(string text,string type) {
            return Json(XmlService.ToObject(text, type));
        }

        [HttpPost]
        [ApiRateLimit]
        public JsonResult Md5(string text, bool half) {
            return Json(Md5Service.Md5(text, half));
        }

        [HttpPost]
        [ApiRateLimit]
        public JsonResult Guid() {
            JsonMessage jm = new JsonMessage() { Flag = true};
            jm.Data = System.Guid.NewGuid();
            return Json(jm);
        }

        public FileResult Barcode(string type, string code, int width = 250, int height = 100, bool isTrans = false) {
            Image image = BarcodeService.GenerateStream(code, type, width, height, isTrans);
            if (image != null) {
                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Png);
                return File(ms.ToArray(), "image/png");
            } else {
                return File("/images/barcode_error.jpg", "image/jpeg");
            }
        }

        public FileResult Ercode(string text, int scale = 200, int margin = 4, string level = "L", bool isTrans = false) {
            Image image = ErcodeService.GenerateStream(text, scale, margin, level, isTrans);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return File(ms.ToArray(), "image/png");
        }

        public ActionResult AuthCode() {
            string code = string.Empty;
            byte[] bytes = ValidateCode.CreateValidateGraphic(out code, 4, 100, 32, 20);
            Session["authcode"] = code;
            return File(bytes, @"image/jpeg");
        }

        [HttpPost]
        [ApiRateLimit]
        public JsonResult IPQuery(string word) {
            return Json(QueryService.GetIPInfo(word));
        }

        [HttpPost]
        [ApiRateLimit]
        public JsonResult MobileQuery(string word) {
            return Json(QueryService.GetMobile(word));
        }

        [HttpPost]
        [ApiRateLimit]
        public JsonResult Httpreq(HttpRequestModel model) {
            return Json(HttpRequestService.Process(model));
        }
    }
}