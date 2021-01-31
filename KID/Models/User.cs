using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KID.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string post { get; set; }
        public string role { get; set; }
    }
}