using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KID.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string brand { get; set; }
        public double rating { get; set; }
        public string sex { get; set; }
        public string age { get; set; }
        public int price { get; set; }
        public int discount { get; set; }
        public string shots1 { get; set; }
        public string shots2 { get; set; }
        public string shots3 { get; set; }


  }
}