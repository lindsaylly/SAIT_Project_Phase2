using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    public class Fee
    {
        public Fee() { }

        public string FeeId { get; set; }

        public string FeeName { get; set; }

        public decimal FeeAmt { get; set; }

        public string FeeDesc { get; set; }
    }
}