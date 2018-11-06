using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    public class OrderSummary
    {
        public OrderSummary() { }

        public int BookingDetailId { get; set; }
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public string ProdName{ get; set; }
        public string SupName { get; set; }
        public decimal BasePrice { get; set; }
        public decimal FeeAmt { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal TotalPrice { get; set; }

    }
}