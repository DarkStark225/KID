using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KID.Models;

namespace KID.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public static ApplicationContext Database = new ApplicationContext();
        public ActionResult Main()
        {
            IEnumerable<Product> products = DBService.GetProducts();
            ViewBag.products = products;
            return View();
        }

        public ActionResult Categories(string category, string sex)
        {
            IEnumerable<Product> buff_products = DBService.GetProducts();
            List<Product> products = new List<Product>();
            foreach (Product product in buff_products)
            {
                if (product.category.Contains(category) && product.sex.Contains(sex)) products.Add(product);
            }
            ViewBag.products = products;
            ViewBag.category = category;
            if (sex == "Для мальчиков") ViewBag.sex = "(Для мальчиков)";
            else if (sex == "Для девочек") ViewBag.sex = "(Для девочек)";
            else ViewBag.sex = "(для всех)";
            return View();
        }
        public ActionResult Age(string age)
        {
            IEnumerable<Product> buff_products = DBService.GetProducts();
            List<Product> products = new List<Product>();
            foreach (Product product in buff_products)
            {
                if (product.age.Contains(age)) products.Add(product);
            }
            ViewBag.products = products;
            ViewBag.category = age;
            return View("Categories");
        }
        public ActionResult Search(string search)
        {
            IEnumerable<Product> buff_products = DBService.GetProducts();
            List<Product> products = new List<Product>();
            foreach (Product product in buff_products)
            {
                if (product!=null)
                if (product.name.ToLower().Contains(search.ToLower())  || product.age.ToLower().Contains(search.ToLower()) || product.description.ToLower().Contains(search.ToLower()) || product.brand.ToLower().Contains(search.ToLower())) products.Add(product);
            }
            ViewBag.products = products;
            ViewBag.category = search;
            return View("Categories");
        }
    }
}