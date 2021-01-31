using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace KID.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base(@"Data Source=DESKTOP-LNTMEKQ\SQLEXPRESS;Initial Catalog=KID2;Integrated Security=True;multipleactiveresultsets=True;") { }
        //public ApplicationContext() : base("Clost") { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}