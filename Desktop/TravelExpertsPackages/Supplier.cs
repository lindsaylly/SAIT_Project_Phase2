using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: ASP.NET Workshop 5
 * Author: Lindsay
 * Date:July, 2018 
 */

namespace TravelExpertsPackages
{
    public class Supplier
    {
        public Supplier() { }
        public int SupplierId { get; set; }
        public string SupName { get; set; }
        public bool SupEnable { get; set; }

        public Supplier CopySupplier()
        {
            Supplier copy = new Supplier();
            copy.SupplierId = SupplierId;
            copy.SupName = SupName;
            copy.SupEnable = SupEnable;
            return copy;
        }

    }
}
