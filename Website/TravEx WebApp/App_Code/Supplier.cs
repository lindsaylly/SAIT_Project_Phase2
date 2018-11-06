using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    [DataObject(true)]
    public class Supplier
    {
        public Supplier() { }

        public int SupplierId { get; set; }
        public string SupName { get; set; }
    }
}