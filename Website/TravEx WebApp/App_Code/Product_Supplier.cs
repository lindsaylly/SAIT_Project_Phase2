using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    public class Product_Supplier
    {
        public Product_Supplier() { }

        public int ProductSupplierId { get; set; }
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
    }
}