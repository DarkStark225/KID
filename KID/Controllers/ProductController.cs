using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KID.Models;

namespace KID.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public static ApplicationContext Database = new ApplicationContext();
        public ActionResult Index(int id)
        {
            Product product = DBService.GetProduct(id);
            ViewBag.product = product;
            ViewBag.id = product.ProductID;
            HttpCookie cookieReq = Request.Cookies["KIDTest"];
            string role = "";
            if (cookieReq != null)
            {
                role = cookieReq["role"];
            }
            ViewBag.role = role;
            return View();
        }
       
    }
}