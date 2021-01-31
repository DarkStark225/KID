using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace KID.Models
{
    public class DBService
    {
        public static ApplicationContext Database = new ApplicationContext();
        public static IEnumerable<Basket> GetBasket(string id)
        {
            IEnumerable<Basket> basket = Database.Baskets.Where(c => c.ClientID == id);
            return basket;
            //else return null;
        }
        public static IEnumerable<Order> GetOrderByNumber(int number)
        {
            IEnumerable<Order> or = Database.Orders.Where(o => o.OrderNumber == number);
            return or;
        }
        public static Product GetProduct(int id)
        {
            return Database.Products.Where(f => f.ProductID == id).FirstOrDefault();
        }
        public static bool CheckUser(string login)
        {
            if (Database.Users.Where(u => u.login == login).FirstOrDefault() != null) return true;
            else return false;
        }
        public static User GetUser(string login, string password)
        {
            password = password.GetHashCode().ToString();
            return Database.Users.Where(u => u.login == login).Where(u => u.password == password).FirstOrDefault();
        }
        public static User GetUser(string id)
        {
            return Database.Users.Where(u => u.UserID.ToString() == id).FirstOrDefault();
        }
        public static IEnumerable<Order> GetOrders(string id)
        {
            return Database.Orders.Where(o => o.ClientID.ToString() == id);
        }
        public static IEnumerable<Order> GetOrders()
        {
            return Database.Orders;
        }
        public static IEnumerable<User> GetUsers()
        {
            return Database.Users;
        }
        public static IEnumerable<Product> GetProducts()
        {
            return Database.Products;
        }
        public static int GetOrdersCount()
        {
            IEnumerable<Order> orders = Database.Orders;
            int number = 0;
            foreach (Order o in orders)
            {
                number = o.OrderNumber;
            }
            return number;
        }
        public static void PasteBasket(Basket basket)
        {
            Basket old_basket = Database.Baskets.Where(b => b.BasketID == basket.BasketID).FirstOrDefault();

            if (old_basket != null)
            {
                Database.Entry<Basket>(old_basket).State = EntityState.Modified;
                Database.SaveChanges();
            }
            else
            {
                Database.Baskets.Add(basket);
                Database.SaveChanges();
            }
        }
        public static void PasteToOrder(Order order)
        {
            Order old_order = Database.Orders.Where(o => o.OrderID == order.OrderID).FirstOrDefault();

            if (old_order != null)
            {
                Database.Entry<Order>(old_order).State = EntityState.Modified;
                Database.SaveChanges();
            }
            else
            {
                Database.Orders.Add(order);
                Database.SaveChanges();
            }
        }
        public static void PasteUser(User user)
        {
            User old_user = Database.Users.Where(u => u.UserID == user.UserID).FirstOrDefault();

            if (old_user != null)
            {
                Database.Entry<User>(old_user).State = EntityState.Modified;
                Database.SaveChanges();
            }
            else
            {
                Database.Users.Add(user);
                Database.SaveChanges();
            }
        }
        public static int PasteProduct(Product product)
        {
            Product old_product = Database.Products.Where(p => p.ProductID == product.ProductID).FirstOrDefault();

            if (old_product != null)
            {
                Database.Entry<Product>(old_product).State = EntityState.Modified;
                Database.SaveChanges();
            }
            else
            {
                Database.Products.Add(product);
                Database.SaveChanges();
            }

            return Database.Products.Where(p => p.ProductID == product.ProductID).FirstOrDefault().ProductID;
        }
        public static void DeleteFromBasket(Basket basket)
        {
            Basket bas = Database.Baskets.Where(b => b.BasketID == basket.BasketID).FirstOrDefault();
            Database.Entry<Basket>(bas).State = EntityState.Deleted;
            Database.SaveChanges();
        }
        public static void DeleteProduct(int id)
        {
            Product product = Database.Products.Where(p => p.ProductID == id).FirstOrDefault();
            Database.Entry<Product>(product).State = EntityState.Deleted;
            Database.SaveChanges();
        }
        public static void ClearBasket(string id)
        {
            foreach (Basket b in Database.Baskets.Where(b => b.ClientID == id))
            {
                Database.Entry<Basket>(b).State = EntityState.Deleted;
            }
            Database.SaveChanges();
        }
    }
}