using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCShort.Models;
using System.Data.Entity;

namespace MVCShort.Controllers
{  
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string ShortUrl(string Url)
        {
            if (Url.Trim() != "")
            {
                string shortUrl = GetRandomUrl();
                using (UrlContext dbContext = new UrlContext())
                {
                    try
                    {
                        while (dbContext.Urls.Any(ur => ur.Surl == shortUrl))
                        {
                            shortUrl = GetRandomUrl();
                        }

                        UrlModel data = new UrlModel();
                        data.Surl = shortUrl;
                        data.Url = Url;
                        dbContext.Urls.Add(data);
                        dbContext.SaveChanges();
                    }
                    catch { }
                    return shortUrl;

                }
            }
            return "";
        }
        public void RedirectLink()
        {
            using (UrlContext dbContext = new UrlContext())
            {
                string url = Request["aspxerrorpath"]?.Replace("/", "");
                try
                {
                    string longUrl = dbContext.Urls.Where(u => u.Surl == url).Select(s => s.Url).FirstOrDefault().ToString();

                    Response.RedirectPermanent(longUrl, true);
                }
                catch { }
            }
        }
        public string GetRandomUrl()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}