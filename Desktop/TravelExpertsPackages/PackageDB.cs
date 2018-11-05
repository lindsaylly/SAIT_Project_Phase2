using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Purpose: ASP.NET Workshop 5
 * Author: Joel
 * Date:July, 2018 
 */

namespace TravelExpertsPackages
{
    public abstract class PackageDB
    {
        public static List<TravelPackage> GetTravelPackages()
        {
            List<TravelPackage> packages = new List<TravelPackage>();
            SqlConnection conn = TravelExpertsDB.GetConnection();
            string selStmt = "SELECT * FROM Packages";
            SqlCommand cmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    packages.Add(new TravelPackage(reader));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while connecting to database: " + ex.Message);
                throw ex;
            }
            finally
            {
                conn.Close();  
            }

            return packages;
        }

        public static void Update(TravelPackage oldPkg, TravelPackage newPkg)
        {
            if (oldPkg.ID != newPkg.ID)
                throw new ArgumentException("ID mismatch between old package and new package");

            SqlConnection conn = TravelExpertsDB.GetConnection();
            string updStmt = "UPDATE Packages " +
                                "SET PkgName = @name, " +
                                "PkgStartDate = @start, " +
                                "PkgEndDate = @end, " +
                                "PkgDesc = @desc, " +
                                "PkgBasePrice = @price, " +
                                "PkgAgencyCommission = @commiss " +
                                "WHERE PackageId = @id";
            SqlCommand cmd = new SqlCommand(updStmt, conn);

            cmd.Parameters.AddWithValue("@name", newPkg.Name);
            cmd.Parameters.AddWithValue("@desc", newPkg.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@start", newPkg.StartDate);
            cmd.Parameters.AddWithValue("@end", newPkg.EndDate);
            cmd.Parameters.AddWithValue("@price", newPkg.BasePrice);
            cmd.Parameters.AddWithValue("@commiss", newPkg.Commission ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@id", oldPkg.ID);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while updating database: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static TravelPackage Insert(TravelPackage newPkg)
        {
            SqlConnection conn = TravelExpertsDB.GetConnection();
            string columns = "PkgName, PkgStartDate, PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission";
            string values = "@name, @start, @end, @desc, @price, @commiss";
            string insStmt = "INSERT INTO Packages (" + columns + ") VALUES (" + values + ")";
            SqlCommand cmd = new SqlCommand(insStmt, conn);

            cmd.Parameters.AddWithValue("@name", newPkg.Name);
            cmd.Parameters.AddWithValue("@start", newPkg.StartDate);
            cmd.Parameters.AddWithValue("@end", newPkg.EndDate);
            cmd.Parameters.AddWithValue("@desc", newPkg.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@price", newPkg.BasePrice);
            cmd.Parameters.AddWithValue("@commiss", newPkg.Commission ?? (object)DBNull.Value);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                //Retreive package ID
                string selStmt = "SELECT IDENT_CURRENT('Packages') FROM Packages";
                SqlCommand selCmd = new SqlCommand(selStmt, conn);
                newPkg.ID = Convert.ToInt32(selCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while inserting into database: " + ex.Message);
                newPkg.ID = -1;
            }
            finally
            {
                conn.Close();
            }

            return newPkg;
        }

        public static int Delete(TravelPackage package)
        {
            //Delete all related Package_Product_Suppliers
            foreach (PackageProdSupplier pps in package.ProductsAndSuppliers)
            {
                if (PackageProdSuppDB.Delete(pps) < 1) throw new Exception("Error deleting Package Product/Supplier");
            }

            int deletedPkgs = 0;
            SqlConnection conn = TravelExpertsDB.GetConnection();
            string sqlQuery = "DELETE FROM Packages " +
                                "WHERE PackageId = @pkgID AND PkgName = @Name " +
                                "AND PkgStartDate = @start AND PkgEndDate = @end " +
                                "AND PkgDesc = @desc AND PkgBasePrice = @price " +
                                "AND PkgAgencyCommission = @commission";
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);

            //TODO: Account for null values
            cmd.Parameters.AddWithValue("@pkgID", package.ID);
            cmd.Parameters.AddWithValue("@name", package.Name);
            // cmd.Parameters.AddWithValue("@start", package.StartDate); //Nullable
            cmd.Parameters.AddWithValue("@start", package.StartDate == null ? (object)DBNull.Value : package.StartDate);
            cmd.Parameters.AddWithValue("@end", package.EndDate); //Nullable
            cmd.Parameters.AddWithValue("@desc", package.Description ?? (object)DBNull.Value); //Nullable
            cmd.Parameters.AddWithValue("@price", package.BasePrice);
            cmd.Parameters.AddWithValue("@commission", package.Commission ?? (object)DBNull.Value); //Nullable
            try
            {
                conn.Open();
                deletedPkgs += cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return deletedPkgs;
        }
    }
}
