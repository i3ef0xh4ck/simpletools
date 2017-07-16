using Sample.BLL;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Web.Infrastructure
{
    public class FunsContainer : Dictionary<string, Funs>
    {
        public static FunsContainer GlobalFuns = new FunsContainer();

        public FunsContainer() {
            List<Funs> funs = null;
            string key = AppConfig.FunsContainerKey;
            funs = AppCaches.AppGet<List<Funs>>(key);
            if (funs == null) {
                funs = FunsService.GetList();
            }
            if (funs != null && funs.Any()) {
                foreach (Funs item in funs) {
                    this.Add(item.Url.ToLower(), item);
                }
                AppCaches.AppSet(key, funs);
            }
        }
    }
}