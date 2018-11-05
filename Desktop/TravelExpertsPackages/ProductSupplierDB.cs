using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Purpose: ASP.NET Workshop 5
 * Author: Lindsay, Joel
 * Date:July, 2018 
 */

namespace TravelExpertsPackages
{
    public static class ProductSupplierDB
    {
        //Author: Lindsay-----------------------------------------------------
        //the method of getting all the suppliers data from the database
        public static List<ProductSupplier> GetProductSuppliers()
        {
            List<ProductSupplier> prodSups = new List<ProductSupplier>();
            ProductSupplier prodSup;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string SelectSmt = "SELECT ProductSupplierId, ProductId, SupplierId " +
                                "FROM Products_Suppliers " +
                                "ORDER BY ProductSupplierId";
            SqlCommand selectCmd = new SqlCommand(SelectSmt, con);

            try
            {
                con.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read())
                {
                    prodSup = new ProductSupplier();
                    prodSup.ProductSupplierId = (int)dr["ProductSupplierId"];
                    prodSup.ProductId = (int)dr["ProductId"];
                    prodSup.SupplierId = (int)dr["SupplierId"];                  
                    prodSups.Add(prodSup);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return prodSups;
        }

        //Author: Lindsay---------------------------------------------------------------------
        /// <summary>
        /// Adds a record to the Products_Suppliers table in Travel Experts database
        /// </summary>
        /// <param name="supProd"> ProductSupplier object that containg data for the new record</param>
        /// <returns>generated productSupplierId</returns>
        public static int AddSupProd(ProductSupplier supProd)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string insertSmt = "INSERT INTO Products_Suppliers (SupplierId, ProductId) " +
                                "VALUES(@SupplierId, @ProductId)";
            SqlCommand cmd = new SqlCommand(insertSmt, con);
            cmd.Parameters.AddWithValue("@SupplierId", supProd.SupplierId);
            cmd.Parameters.AddWithValue("@ProductId", supProd.ProductId);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery(); // run the insert command
                //get the generated ID - current identity value of Products_Suppliers table
                string selectQuery = "SELECT IDENT_CURRENT('Products_Suppliers') From Products_Suppliers";
                SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                int productSupplierId = Convert.ToInt32(selectCmd.ExecuteScalar());
                return productSupplierId;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        //Author: Joel---------------------------------------------------------------------
        public static ProductSupplier GetProductSupplier(int productID, int supplierID)
        {
            ProductSupplier prodSup = null;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string SelectSmt = "SELECT ProductSupplierId, ProductId, SupplierId " +
                                "FROM Products_Suppliers " +
                                "WHERE ProductId = @pID AND SupplierId = @sID " +
                                "ORDER BY ProductSupplierId";
            SqlCommand selectCmd = new SqlCommand(SelectSmt, con);
            selectCmd.Parameters.AddWithValue("@pID", productID);
            selectCmd.Parameters.AddWithValue("@sID", supplierID);

            try
            {
                con.Open();
                SqlDataReader dr = selectCmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                if (dr.Read())
                {
                    prodSup = new ProductSupplier();
                    prodSup.ProductSupplierId = (int)dr["ProductSupplierId"];
                    prodSup.ProductId = (int)dr["ProductId"];
                    prodSup.SupplierId = (int)dr["SupplierId"];
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return prodSup;
        }

        //Author: Lindsay-----------------------------------------------------------------------------
        /// <summary>
        /// Delete a record to the Products_Suppliers table in Travel Experts database
        /// </summary>
        /// <param name="supProd">ProductSupplier object that containg data for the new record</param>
        /// <returns>generated productSupplierId</returns>
        public static bool DeleteSupProd(ProductSupplier supProd)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            String deleteSmt = "DELETE FROM Products_Suppliers " +
                                "WHERE SupplierId = @SupplierId " +
                                "And ProductId = @ProductId";
            SqlCommand cmd = new SqlCommand(deleteSmt, con);
            cmd.Parameters.AddWithValue("@SupplierId", supProd.SupplierId);
            cmd.Parameters.AddWithValue("@ProductId", supProd.ProductId);
            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0) return true;
                else return false;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

    }
}
