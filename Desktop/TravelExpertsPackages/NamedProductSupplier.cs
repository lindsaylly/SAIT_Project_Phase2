using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: ASP.NET Workshop 5
 * Author: Joel
 * Date:July, 2018 
 */

namespace TravelExpertsPackages
{
    public class NamedProductSupplier : ProductSupplier
    {
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
    }
}
