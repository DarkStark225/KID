using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KID.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string ClientID { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
        public int OrderNumber { get; set; }
        public string Status { get; set; }
        public string Adress { get; set; }
        public string Pay_Method { get; set; }
        public DateTime Date { get; set; }
    }
}