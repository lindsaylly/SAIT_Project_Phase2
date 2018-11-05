using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Purpose: ASP.NET Workshop 5
 * Author: Carol
 * Date:July, 2018 
 */

namespace TravelExpertsPackages
{
    public static class ProductDB
    {
       
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            Product product;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT ProductId, ProdName " +
                                     "FROM Products ORDER BY ProductId";

            SqlCommand selectCmd = new SqlCommand(selectStatement, con);

            try
            {
                con.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();
                while (dr.Read())
                {
                    product = new Product();
                    product.ProductId = (int)dr["ProductId"];
                    product.ProdName = dr["ProdName"].ToString();

                    products.Add(product);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return products;
        }

        public static Product GetProduct(int productId)
        {
            Product prod = null;
            SqlConnection con = TravelExpertsDB.GetConnection();
            string selectStatement = "SELECT ProductId, ProdName " +
                                     "FROM Products " +
                                     "WHERE ProductId = @ProductId";
            SqlCommand cmd = new SqlCommand(selectStatement, con);
            cmd.Parameters.AddWithValue("@ProductId", productId); // value comes from the method's argument
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read()) // found a customer
                {
                    prod = new Product();
                    prod.ProductId = (int)reader["ProductId"];
                    prod.ProdName = reader["ProdName"].ToString();
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
            return prod;
        }

        /// <summary>
        /// Adds a new product to the Products table in TrevalExperts database
        /// </summary>
        /// <param name="prod"> Product object that contain data for the new record</param>
        /// <returns>generated ProductId</returns>
        public static int AddProduct(Product prod)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string insertStatement = "INSERT INTO Products (ProdName) " +
                                     "VALUES (@ProdName) ";
            SqlCommand cmd = new SqlCommand(insertStatement, con);
            cmd.Parameters.AddWithValue("@ProdName", prod.ProdName);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery(); // run the insert command
                // get the generated ID - current identity value for  Products table
                string selectQuery = "SELECT IDENT_CURRENT('Products') FROM Products";
                SqlCommand selectCmd = new SqlCommand(selectQuery, con);
                int productID = Convert.ToInt32(selectCmd.ExecuteScalar()); // single value
                                                                             // typecase (int) does NOT work!
                return productID;
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
        /// Updates existing product record
        /// </summary>
        /// <param name="oldProd">data before update</param>
        /// <param name="newProd">new data for the update</param>
        /// <returns>indicator of success</returns>
        public static bool UpdateProduct(Product oldProd, Product newProd)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string updateStatement = "UPDATE Products " +
                                     "SET ProdName = @NewProdName " +                                
                                     "WHERE ProductId = @OldProductId " +
                                     "AND ProdName = @OldProdName";
            SqlCommand cmd = new SqlCommand(updateStatement, con);
            cmd.Parameters.AddWithValue("@NewProdName", newProd.ProdName);
            cmd.Parameters.AddWithValue("@OldProductId", oldProd.ProductId);
            cmd.Parameters.AddWithValue("@OldProdName", oldProd.ProdName);

            try
            {
                con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("Product Name updated successfully !");
                    return true;
                }
                else
                {
                    MessageBox.Show("Product Name is not updated successfully !");
                    return false;
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
        }

        public static bool DeleteProduct(Product prod)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string deleteStatement = "DELETE FROM Products " +
                                     "WHERE ProductId = @ProductId " + // to identify the prduct to be  deleted
                                     "AND ProdName = @ProdName"; // remaining conditions - to ensure optimistic concurrency

            SqlCommand cmd = new SqlCommand(deleteStatement, con);
            cmd.Parameters.AddWithValue("@ProductId", prod.ProductId);
            cmd.Parameters.AddWithValue("@ProdName", prod.ProdName);
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
