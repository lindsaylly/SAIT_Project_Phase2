﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    public class Package
    {
        public Package() { }

        public int PackageId { get; set; }

        public string PkgName { get; set; }

        public DateTime? PkgStartDate { get; set; }

        public DateTime? PkgEndDate { get; set; }

        public string PkgDesc { get; set; }

        public decimal PkgBasePrice { get; set; }

        public decimal PkgAgencyCommission { get; set; }
    }
}