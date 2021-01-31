using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KID.Models;

namespace KID.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public static ApplicationContext Database = new ApplicationContext();
        public ActionResult LoggIn(string login, string password)
        {
            if (DBService.CheckUser(login))
            {
                if (DBService.GetUser(login, password) != null)
                {
                    HttpCookie cookie = new HttpCookie("KIDTest");
                    // Установить значения в нем
                    cookie["UserID"] = DBService.GetUser(login, password).UserID.ToString();
                    cookie["role"] = DBService.GetUser(login, password).role;                  
                    cookie["login"] = DBService.GetUser(login, password).login;
                    // Добавить куки в ответ
                    Response.Cookies.Add(cookie);
                    return RedirectToActionPermanent("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Неправильный пароль";
                    return View("Loggin");
                }
            }
            else
            {
                ViewBag.error = "Такого пользователя не существует";
                return View("Loggin");
            }
        }
        public ActionResult LogginOut()
        {
            HttpCookie cookie = new HttpCookie("KIDTest");
            User user = new User();
            user.UserID = 0; user.role = "guest";
            cookie["UserID"] = user.UserID.ToString();
            cookie["role"] = user.role;
            Response.Cookies.Add(cookie);
            return RedirectToActionPermanent("Index", "Home");
        }
        public ActionResult LogginPage()
        {
            return View("Loggin");
        }
        public ActionResult LK()
        {
            HttpCookie cookieReq = Request.Cookies["KIDTest"];
            string role = "";
            if (cookieReq != null)
            {
                role = cookieReq["role"];
            }
            ViewBag.role = role;
            return View("LK");
        }
        public ActionResult RegisterPage()
        {
            return View("Register");
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Registration(string login, string password, string email, string post)
        {
            if (DBService.CheckUser(login))
            {
                ViewBag.error = "Пользователь с таким логином уже существует!";
                return View("Register");
            }
            else
            {
                User user = new Models.User();
                user.login = login;
                user.password = password.GetHashCode().ToString();
                user.email = email;
                user.role = "user";
                user.post = post;
                DBService.PasteUser(user);
                ViewBag.error = "Пользователь успешно создан!";
                return View("Register");
            }
        }
        public ActionResult OrdersPage()
        {
            HttpCookie cookieReq = Request.Cookies["KIDTest"];
            string id = "";
            if (cookieReq != null)
            {
                id = cookieReq["UserID"];
            }
            IEnumerable<Order> orders = DBService.GetOrders(id);

            List<OR> or = new List<OR>();

            int buff_n = 0, buff_summ = 0; bool flag = false;
            string buff_status = "status", buff_adress = "Адрес", buff_pay = ""; int buff_id = 30; DateTime buff_date = DateTime.Now;

            foreach (Order order in orders)
            {
                if (buff_n == order.OrderNumber)
                {
                    buff_id = int.Parse(order.ClientID);
                    buff_n = order.OrderNumber;
                    buff_status = order.Status;
                    buff_adress = order.Adress;
                    buff_date = order.Date;
                    buff_pay = order.Pay_Method;
                    order.Product = DBService.GetProduct(order.ProductID);
                    buff_summ += order.Product.price * order.Count;
                }
                else
                {
                    if (flag)
                    {
                        OR o = new OR();
                        o.id = buff_id;
                        o.status = buff_status;
                        o.or_numb = buff_n;
                        o.summ = buff_summ;
                        o.adress = buff_adress;
                        o.date = buff_date;
                        o.pay = buff_pay;
                        o.on = buff_n.ToString();
                        o.ssumm = buff_summ.ToString();
                        or.Add(o);
                    }
                    flag = true;
                    buff_n = order.OrderNumber;
                    order.Product = DBService.GetProduct(order.ProductID);
                    buff_summ = order.Product.price * order.Count;
                    buff_status = order.Status; buff_id = int.Parse(order.ClientID); buff_adress = order.Adress; buff_date = order.Date; buff_pay = order.Pay_Method;
                }
            }
            if (flag)
            {
                OR last = new OR();
                last.id = buff_id;
                last.status = buff_status;
                last.or_numb = buff_n;
                last.summ = buff_summ;
                last.adress = buff_adress;
                last.date = buff_date;
                last.pay = buff_pay;
                last.on = buff_n.ToString();
                last.ssumm = buff_summ.ToString();
                or.Add(last);
            }
            ViewBag.count = or.Count;
            ViewBag.order = or;
            return View();
        }


        public ActionResult AllOrdersPage()
        {
            IEnumerable<Order> orders = DBService.GetOrders();

            List<OR> or = new List<OR>();

            int buff_n = 0, buff_summ = 0; bool flag = false;
            string buff_status = "status", buff_adress = "Адрес", buff_pay = ""; int buff_id = 30; DateTime buff_date = DateTime.Now;

            foreach (Order order in orders)
            {
                if (buff_n == order.OrderNumber)
                {
                    buff_id = int.Parse(order.ClientID);
                    buff_n = order.OrderNumber;
                    buff_status = order.Status;
                    buff_adress = order.Adress;
                    buff_date = order.Date;
                    buff_pay = order.Pay_Method;
                    order.Product = DBService.GetProduct(order.ProductID);
                    buff_summ += order.Product.price * order.Count;
                }
                else
                {
                    if (flag)
                    {
                        OR o = new OR();
                        o.id = buff_id;
                        o.status = buff_status;
                        o.or_numb = buff_n;
                        o.summ = buff_summ;
                        o.adress = buff_adress;
                        o.date = buff_date;
                        o.pay = buff_pay;
                        o.on = buff_n.ToString();
                        o.ssumm = buff_summ.ToString();
                        or.Add(o);
                    }
                    flag = true;
                    buff_n = order.OrderNumber;
                    order.Product = DBService.GetProduct(order.ProductID);
                    buff_summ = order.Product.price * order.Count;
                    buff_status = order.Status; buff_id = int.Parse(order.ClientID); buff_adress = order.Adress; buff_date = order.Date; buff_pay = order.Pay_Method;
                }
            }
            if (flag)
            {
                OR last = new OR();
                last.id = buff_id;
                last.status = buff_status;
                last.or_numb = buff_n;
                last.summ = buff_summ;
                last.adress = buff_adress;
                last.date = buff_date;
                last.pay = buff_pay;
                last.on = buff_n.ToString();
                last.ssumm = buff_summ.ToString();
                or.Add(last);
            }
            ViewBag.count = or.Count;
            ViewBag.order = or;
            return View();
        }
    }
    public class OR
    {
        public int id;
        public string status;
        public string adress;
        public int or_numb;
        public DateTime date;
        public int summ;
        public string on;
        public string ssumm;
        public string pay;
    }
}