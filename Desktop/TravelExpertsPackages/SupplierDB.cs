using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public static class SupplierDB
    {
        //the method of getting all the suppliers data from the database
        public static List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            Supplier sup = null;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string SelectSmt = "SELECT SupplierId, SupName " +
                                "FROM Suppliers " +
                                "ORDER BY SupName";
            SqlCommand cmd = new SqlCommand(SelectSmt, con);
            
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) //while there are suppliers
                {
                    sup = new Supplier();
                    sup.SupplierId = (int)dr["SupplierId"];
                    sup.SupName = dr["SupName"].ToString();
                    suppliers.Add(sup);
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
            return suppliers;
        }

        //the method of getting the supplier by supplierId
        public static Supplier GetSupplier(int supplierId)
        {
            Supplier sup = null;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string selectSmt = "SELECT SupplierId, SupName " +
                                "FROM Suppliers " +
                                "WHERE SupplierId = @SupplierId";
            SqlCommand cmd = new SqlCommand(selectSmt, con);
            cmd.Parameters.AddWithValue("@SupplierId", supplierId); // value comes from the method's argument
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) //found a supplier
                {
                    sup = new Supplier();
                    sup.SupplierId = (int)dr["SupplierId"];
                    sup.SupName = dr["SupName"].ToString();
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
            return sup;
        }

        //the method of getting new unique supplierId for adding new supplier
        public static int GetNewSupplierId()
        {
            Supplier sup = null;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string selectSmt = "SELECT TOP 1 SupplierId " +
                                 "FROM Suppliers " +
                                 "ORDER BY SupplierId DESC";
            SqlCommand cmd = new SqlCommand(selectSmt, con);

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()) //found a supplier
                {
                    sup = new Supplier();
                    sup.SupplierId = 1 + (int)dr["SupplierId"];
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
            return sup.SupplierId;

        }

        /// <summary>
        /// Adds a new supplier to the Suppliers table in Travel Experts database
        /// </summary>
        /// <param name="sup"> Supplier object that containg data for the new record</param>
        /// <returns>generated SupplierID</returns>
        public static void AddSupplier(Supplier sup)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();            
            string insertSmt = "INSERT INTO Suppliers (Supplierid, SupName) " +
                                "VALUES(@SupplierId, @SupName)";
            SqlCommand cmd = new SqlCommand(insertSmt, con);
            cmd.Parameters.AddWithValue("@SupplierId", sup.SupplierId);
            cmd.Parameters.AddWithValue("@SupName", sup.SupName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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

        /// <summary>
        /// Delete a upplier from the Suppliers table in Travel Experts database
        /// </summary>
        /// <param name="sup"> Supplier object that containg data for the supplier </param>
        /// <returns>generated SupplierID</returns>
        public static bool DeleteSupplier(Supplier sup)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            String deleteSmt = "DELETE FROM Suppliers " +
                                "WHERE SupplierId = @SupplierId " +
                                "And SupName = @SupName";
            SqlCommand cmd = new SqlCommand(deleteSmt, con);
            cmd.Parameters.AddWithValue("@SupplierId", sup.SupplierId);
            cmd.Parameters.AddWithValue("@SupName", sup.SupName);
            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0) return true;
                else return false;
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Updates existing supplier record
        /// </summary>
        /// <param name="oldSup">data before update</param>
        /// <param name="newSup">new data for the update</param>
        /// <returns>indicator of success</returns>
        public static bool UpdateSupplier(Supplier oldSup, Supplier newSup)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string UpdateSmt = "UPDATE Suppliers " +
                               "SET SupName = @NewSupName " +
                               "Where SupplierId = @OldSupplierId " +
                               "And SupName = @OldSupName";            
            SqlCommand cmd = new SqlCommand(UpdateSmt, con);
            cmd.Parameters.AddWithValue("@NewSupName", newSup.SupName);
            cmd.Parameters.AddWithValue("@OldSupplierId", oldSup.SupplierId);
            cmd.Parameters.AddWithValue("@OldSupName", oldSup.SupName);

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

        //getting all the products supplieried by the supplier
        public static List<Product> GetProductsBySupplier(int supplierId)
        {
            List<Product> SuppliedProds = new List<Product>();
            Product suppliedProd;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string SelectSmt = "SELECT SupplierId, p.ProductId, p.ProdName " +
                                "FROM Products_Suppliers ps JOIN Products p " +
                                "ON ps.ProductId = p.ProductId " +
                                "WHERE SupplierId = @SupplierId " +
                                "ORDER BY p.ProductId";
            SqlCommand selectCmd = new SqlCommand(SelectSmt, con);
            selectCmd.Parameters.AddWithValue("@SupplierId", supplierId);

            try
            {
                con.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read())//while there is a product
                {
                    suppliedProd = new Product();
                    suppliedProd.ProductId = (int)dr["ProductId"];
                    suppliedProd.ProdName = dr["prodName"].ToString();
                    SuppliedProds.Add(suppliedProd);
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
            return SuppliedProds;
        }


        //getting all the products not supplieried by the supplier
        public static List<Product> GetProdsUnsuppliedBySup(int supplierId)
        {
            List<Product> unsuppliedProds = new List<Product>();
            Product unsupplied;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string SelectSmt = "SELECT ProductId, ProdName FROM Products " +
                                "WHERE ProductId NOT IN " +
                                "(SELECT ProductId FROM Products_Suppliers " +
                                "WHERE SupplierId = @SupplierId) " +
                                "ORDER BY ProductId";
            SqlCommand selectCmd = new SqlCommand(SelectSmt, con);
            selectCmd.Parameters.AddWithValue("@SupplierId", supplierId);

            try
            {
                con.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read())
                {
                    unsupplied = new Product();
                    unsupplied.ProductId = (int)dr["ProductId"];
                    unsupplied.ProdName = dr["prodName"].ToString();
                    unsuppliedProds.Add(unsupplied);
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
            return unsuppliedProds;
        }
    }
}
