using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class TravelPackage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get;  set; }
        public string Description { get;  set; }
        public double BasePrice { get;  set; }
        public double? Commission { get;  set; }

        public List<NamedPackageProductSupplier> ProductsAndSuppliers
            { get => PackageProdSuppDB.GetPackageProductSuppliersByPackage(this.ID); }

        public TravelPackage(SqlDataReader reader)
        {
            ID = Convert.ToInt32(reader["PackageId"]);
            Name = reader["PkgName"].ToString();
            Description = reader["PkgDesc"] == DBNull.Value ? null : reader["PkgDesc"].ToString();
            StartDate = Convert.ToDateTime(reader["PkgStartDate"]);
            EndDate = Convert.ToDateTime(reader["PkgEndDate"]);
            BasePrice = Convert.ToDouble(reader["PkgBasePrice"]);
            if (reader["PkgAgencyCommission"] == DBNull.Value)
                Commission = null;
            else
                Commission = Convert.ToDouble(reader["PkgAgencyCommission"]);
        }

        public TravelPackage(TravelPackage source)
        {
            ID = source.ID;
            Name = source.Name;
            StartDate = source.StartDate;
            EndDate = source.EndDate;
            Description = source.Description;
            BasePrice = source.BasePrice;
            Commission = source.Commission;
        }

        public TravelPackage(int id)
        {
            ID = id;
        }

        public TravelPackage() { }
    }
}
