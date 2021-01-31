using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using KID.Models;

namespace KID.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminPanel()
        {
            return View();
        }

        public ActionResult EditPanel()
        {
            return View();
        }
        public ActionResult AddProduct()
        {
            return View();
        }
        public ActionResult EditProduct(int id)
        {
            ViewBag.product = DBService.GetProduct(id);
            ViewBag.id = id;
            return View();
        }
        public ActionResult DeleteProduct(int id)
        {
            DBService.DeleteProduct(id);
            return RedirectToActionPermanent("Index", "Home");
        }
        public RedirectToRouteResult AddProductToDB(HttpPostedFileBase image, string name, string description, string category,string brand, double rating, string sex, string age,   int price, int discount, HttpPostedFileBase shots1, HttpPostedFileBase shots2, HttpPostedFileBase shots3)
        {
            string fileName, sava = "", shot1 = "", shot2 = "", shot3 = "";
            Directory.CreateDirectory(@"C:\Users\я\Desktop\KID\KID\Content\images\" + name);
            if (image != null)
            {
                fileName = System.IO.Path.GetFileName(image.FileName);
                image.SaveAs(Server.MapPath("/Content/images/" + name + "/" + fileName));
                sava = name + "/" + fileName;
            }
            if (shots1 != null)
            {
                fileName = System.IO.Path.GetFileName(shots1.FileName);
                shots1.SaveAs(Server.MapPath("/Content/images/" + name + "/" + fileName));
                shot1 = name + "/" + fileName;
            }
            if (shots2 != null)
            {
                fileName = System.IO.Path.GetFileName(shots2.FileName);
                shots2.SaveAs(Server.MapPath("/Content/images/" + name + "/" + fileName));
                shot2 = name + "/" + fileName;
            }
            if (shots3 != null)
            {
                fileName = System.IO.Path.GetFileName(shots3.FileName);
                shots3.SaveAs(Server.MapPath("/Content/images/" + name + "/" + fileName));
                shot3 = name + "/" + fileName;
            }

            Product product = new Product();
            product.image = sava; product.name = name; product.description = description; product.category = category;
            product.price = price; product.shots1 = shot1; product.shots2 = shot2; product.shots3 = shot3;
            product.sex = sex; product.age = age; product.brand = brand; product.rating = rating; product.discount =discount;
            return RedirectToActionPermanent("Index", "Product", new { id = DBService.PasteProduct(product) });

        }

        public RedirectToRouteResult EditProductToDB(int id, string name, HttpPostedFileBase image, string description, string category,string brand, double rating, string sex, string age, int price,int discount, HttpPostedFileBase shots1, HttpPostedFileBase shots2, HttpPostedFileBase shots3)
        {
            string fileName, sava = "", shot1 = "", shot2 = "", shot3 = "";
            Product product = DBService.GetProduct(id);
            product.description = description; product.category = category; product.price = price;
            product.sex = sex; product.age = age; product.brand = brand; product.rating = rating; product.discount = discount; product.name = name;
            Directory.CreateDirectory(@"C:\Users\я\Desktop\KID\KID\Content\images\" + name);
            if (image != null)
            {
                fileName = System.IO.Path.GetFileName(image.FileName);
                image.SaveAs(Server.MapPath("/Content/images/" + product.name + "/" + fileName));
                sava = product.name + "/" + fileName; product.image = sava;
            }
            if (shots1 != null)
            {
                fileName = System.IO.Path.GetFileName(shots1.FileName);
                shots1.SaveAs(Server.MapPath("/Content/images/" + product.name + "/" + fileName));
                shot1 = product.name + "/" + fileName; product.shots1 = shot1;
            }
            if (shots2 != null)
            {
                fileName = System.IO.Path.GetFileName(shots2.FileName);
                shots2.SaveAs(Server.MapPath("/Content/images/" + product.name + "/" + fileName));
                shot2 = product.name + "/" + fileName; product.shots2 = shot2;
            }
            if (shots3 != null)
            {
                fileName = System.IO.Path.GetFileName(shots3.FileName);
                shots3.SaveAs(Server.MapPath("/Content/images/" + product.name + "/" + fileName));
                shot3 = product.name + "/" + fileName; product.shots3 = shot3;
            }

            DBService.PasteProduct(product);
            return RedirectToActionPermanent("Index", "Product", new { id = id });
        }


        public ActionResult ChangeOrder(int ordernumber, string status)
        {
            IEnumerable<Order> orders = DBService.GetOrderByNumber(ordernumber);
            foreach (Order order in orders)
            {
                order.Status = status;
            }
            foreach (Order order in orders.ToList<Order>())
            {
                DBService.PasteToOrder(order);
            }
            return RedirectToActionPermanent("AllOrdersPage", "User");
        }

        public ActionResult ChangeRole(int userid, string role)
        {
            User user = DBService.GetUser(userid.ToString());
            user.role = role;
            DBService.PasteUser(user);
            return RedirectToActionPermanent("RolesAdmin", "Admin");
        }

        public ActionResult RolesAdmin()
        {
            IEnumerable<User> users = DBService.GetUsers();
            ViewBag.users = users;
            return View();
        }

    }
}