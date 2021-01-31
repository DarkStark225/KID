using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KID.Models;

namespace KID.Controllers
{
    public class HomeController : Controller
    {
        public static ApplicationContext Database = new ApplicationContext();
        public ActionResult Index()
        {
            IEnumerable<Product> products = DBService.GetProducts();
            ViewBag.products = products;
            return View();
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