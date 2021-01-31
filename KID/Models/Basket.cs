using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KID.Models;
using System.ComponentModel.DataAnnotations;

namespace KID.Models
{
    public class Basket
    {
        public int BasketID { get; set; }
        public string ClientID { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }
        public void AddProduct(int count)
        {
            Count += count;
        }

        //public void RemoveProduct(Product product)
        //{
        //    basket.RemoveAll(p => p.Product.ProductID == product.ProductID);
        //}

        //public decimal TotalValue()
        //{
        //    return basket.Sum(p_c => p_c.Product.price * p_c.Count);

        //}
        //public void Clear()
        //{
        //    basket.Clear();
        //}
        //public IEnumerable<Product_Count> GetProducts
        //{
        //    get { return basket; }
        //}
    }
}