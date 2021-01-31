using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KID.Models;
using System.Net;
using System.Net.Mail;

namespace KID.Controllers
{
    public class BasketController : Controller
    {
        public static ApplicationContext Database = new ApplicationContext();
        public ActionResult Basket()
        {
            HttpCookie cookieReq = Request.Cookies["KIDTest"];
            string id = "";
            if (cookieReq != null)
            {
                id = cookieReq["UserID"];
            }

            IEnumerable<Basket> buff_basket = DBService.GetBasket(id);
            List<Basket> basket = new List<Basket>();
            int summ = 0;
            foreach (var b in buff_basket)
            {
                basket.Add(b);
                basket.Last().Product = DBService.GetProduct(b.ProductID);
                summ += DBService.GetProduct(b.ProductID).price * b.Count;
            }

            ViewBag.Id = id;

            if (id != "") ViewBag.basket = basket;
            else ViewBag.basket = null;
            ViewBag.summ = summ;
            return View();
        }

        public ActionResult PageOrders(int number, int summ)
        {
            HttpCookie cookieReq = Request.Cookies["KIDTest"];
            string id = "";
            if (cookieReq != null)
            {
                id = cookieReq["UserID"];
            }

            IEnumerable<Order> order = DBService.GetOrderByNumber(number);

            foreach (var o in order)
            {
                o.Product = DBService.GetProduct(o.ProductID);
            }

            ViewBag.Id = id;
            ViewBag.basket = order;
            ViewBag.number = number;
            ViewBag.summ = summ;
            return View();
        }

        public ActionResult AddToBasket(int productID, int count)
        {
            HttpCookie cookieReq = Request.Cookies["KIDTest"];
            string id = "";
            if (cookieReq != null)
            {
                id = cookieReq["UserID"];
            }
            Basket basket = DBService.GetBasket(id).Where(b => b.Product == DBService.GetProduct(productID)).FirstOrDefault();

            if (basket != null)
            {
                basket.Count += count;
                DBService.PasteBasket(basket);
            }
            else
            {
                Basket b = new Basket();
                b.ClientID = id;
                b.Product = DBService.GetProduct(productID);
                b.ProductID = productID;
                b.Count = count;
                DBService.PasteBasket(b);
            }

            return RedirectToActionPermanent("Index", "Product", new { id = productID });
        }

        public ActionResult DeleteFromBasket(int productID)
        {
            HttpCookie cookieReq = Request.Cookies["KIDTest"];
            string id = "";
            if (cookieReq != null)
            {
                id = cookieReq["UserID"];
            }
            Basket basket = DBService.GetBasket(id).Where(b => b.ProductID == productID).FirstOrDefault();
            DBService.DeleteFromBasket(basket);
            return RedirectToActionPermanent("Basket", "Basket");
        }
        public ActionResult ToOrder()
        {
            HttpCookie cookieReq = Request.Cookies["KIDTest"];
            string id = "";
            if (cookieReq != null)
            {
                id = cookieReq["UserID"];
            }
            IEnumerable<Basket> basket = DBService.GetBasket(id);
            List<Order> or = new List<Order>();
            int summ = 0; //, or_number = 0;
            foreach (var b in basket)
            {
                summ += b.Product.price * b.Count;
            }
            ViewBag.client = DBService.GetUser(id);
            ViewBag.summ = summ;
            return View();
        }

        public ActionResult MakeOrder(string email, string adress, string pay)
        {
            HttpCookie cookieReq = Request.Cookies["KIDTest"];
            string id = "";
            if (cookieReq != null)
            {
                id = cookieReq["UserID"];
            }
            IEnumerable<Basket> basket = DBService.GetBasket(id);
            List<Order> or = new List<Order>();
            int summ = 0, ornumber = 0;
            string buff = "<table><tr><th>Модель</th><th>Цена</th><th>Количество</th><th>Итого</th></tr>";
            foreach (var b in basket)
            {
                Order order = new Order();
                order.ClientID = b.ClientID;
                order.Count = b.Count;
                order.OrderNumber = DBService.GetOrdersCount() + 1;
                order.Product = b.Product;
                order.ProductID = b.ProductID;
                order.Adress = adress;
                order.Pay_Method = pay;
                order.Date = DateTime.Now;
                order.Status = "Заказ забронирован. Ожидается оплата.";
                or.Add(order);
                buff += "<tr><td>" + order.Product.name + "</td><td>" + order.Product.price + " рублей </td><td>" + order.Count + " шт.</td><td>" + order.Count * order.Product.price + " рублей</td></tr>";
                summ += b.Product.price * b.Count;
                ornumber = order.OrderNumber;
            }
            foreach (Order o in or)
            {
                DBService.PasteToOrder(o);
            }
            DBService.ClearBasket(id);

            ViewBag.or_number = ornumber;
            ViewBag.summ = summ;

            buff += "<tr><th colspan=\"3\">Сумма</th><th>" + summ + " рублей</th></tr></td><tr></table>";
            string message = "<style type=\"text / css\">        table {            border-spacing: 0 10px;            font-family: 'Open Sans', sans-serif;            font-weight: bold;            width: 120%;            font-size: 18px;        }                th {            padding: 10px 20px;            background: #AAA;            color: black;            border: 2px solid;            font-size: 0.9em;            text-align: center;        }                   th:first-child {                text-align: left;            }                    tr:last-child {            border-top: 2px black;        }        td {            vertical-align: middle;            padding: 10px;            font-size: 16px;            text-align: center;            border-top: 2px solid #ddd;            border-bottom: 2px solid #ddd;            border-right: 2px solid #ddd;        }                    td:first-child {                border-left: 2px solid #ddd;            }                        td:nth-child(2) {                text-align: left;            }                    table a {            font-size: 20px;            color: black;        }</style> <h2> Ваш заказ №" + ornumber + " на сумму " + summ + "р.готов к оплате.</h2>  " + buff;

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("kid.rushop@yandex.ru", "Интернет-магазин игрушек KID");
            // кому отправляем
            MailAddress to = new MailAddress(email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Заказ";
            // текст письма

            m.Body = message;
            //m.Body = "<h2>Ваш заказ №"+ornumber+" на сумму "+summ+"р. готов к оплате.</h2>";

            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("kid.rushop@yandex.ru", "projectmvc01");
            smtp.EnableSsl = true;
            smtp.Send(m);

            return View();
        }
    }
}