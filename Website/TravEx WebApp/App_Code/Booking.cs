using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    public class Booking
    {
        public Booking() { }

        public int BookingId { get; set; }

        public DateTime? BookingDate { get; set; }

        public string BookingNo { get; set; }

        public float? TravelerCount { get; set; }

        public int? CustomerId { get; set; }

        public string TripTypeId { get; set; }

        public int? PackageId { get; set; }


    }
}