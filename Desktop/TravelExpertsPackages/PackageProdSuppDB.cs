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
    public static class PackageProdSuppDB
    {
        public static List<NamedPackageProductSupplier> GetPackageProductSuppliersByPackage(int pkgID)
        {
            List<NamedPackageProductSupplier> productSuppliers = new List<NamedPackageProductSupplier>();
            SqlConnection conn = TravelExpertsDB.GetConnection();
            string sqlQuery = "SELECT pkgps.PackageId as pkgID, ps.ProductSupplierId as psID, ProdName, SupName " +
                                "FROM Products p " +
                                "INNER JOIN Products_Suppliers ps ON p.ProductId = ps.ProductId " +
                                "INNER JOIN Suppliers s ON s.SupplierId = ps.SupplierId " +
                                "INNER JOIN Packages_Products_Suppliers pkgps ON pkgps.ProductSupplierId = ps.ProductSupplierId " +
                                "WHERE pkgps.PackageID = @PackageID "  +
                                "ORDER BY pkgps.ProductSupplierId";
            //string sqlQuery = "SELECT PackageID, ProductSupplierID FROM Packages_Products_Suppliers "+
            //                    "WHERE PackageID = @PackageID ORDER BY ProductSupplierID";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@PackageId", pkgID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    NamedPackageProductSupplier prodSup = new NamedPackageProductSupplier
                    {
                        PackageID = (int)reader["pkgID"],
                        ProdSuppID = (int)reader["psID"],
                        ProductName = reader["ProdName"].ToString(),
                        SupplierName = reader["SupName"].ToString()
                    };

                    productSuppliers.Add(prodSup);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return productSuppliers;
        }

        public static List<NamedPackageProductSupplier> GetPackageProductSuppliersByPackage(TravelPackage pkg)
        {
            return GetPackageProductSuppliersByPackage(pkg.ID);
        }

        public static bool Insert(PackageProdSupplier pkgProdSup)
        {
            bool successfulInsert = false;

            SqlConnection con = TravelExpertsDB.GetConnection();
            string insertSmt = "INSERT INTO Packages_Products_Suppliers (PackageId, ProductSupplierId) " +
                                "VALUES(@pkgID, @psID)";
            SqlCommand cmd = new SqlCommand(insertSmt, con);
            cmd.Parameters.AddWithValue("@pkgID", pkgProdSup.PackageID);
            cmd.Parameters.AddWithValue("@psID", pkgProdSup.ProdSuppID);
            try
            {
                con.Open();
                successfulInsert = cmd.ExecuteNonQuery() >= 1; // run the insert command
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return successfulInsert;
        }

        public static int Delete(PackageProdSupplier packagePS)
        {
            int deletedRows = 0;
            SqlConnection conn = TravelExpertsDB.GetConnection();
            string sqlQuery = "DELETE FROM Packages_Products_Suppliers "+
                                "WHERE PackageId = @pkgID AND ProductSupplierId = @psID";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.Parameters.AddWithValue("@pkgID", packagePS.PackageID);
            cmd.Parameters.AddWithValue("@psID", packagePS.ProdSuppID);
            try
            {
                conn.Open();
                deletedRows += cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return deletedRows;
        }
    }
}
