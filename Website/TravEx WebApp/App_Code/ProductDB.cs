using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    [DataObject(true)]
    public static class ProductDB
    {

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>(); // make an empty list
            Product product;
            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create select command
            string selectString = "SELECT * FROM Products " +
                                  "ORDER BY ProductId";
            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            try
            {

                connection.Open();

                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    product = new Product();
                    product.ProductId = (int)reader["ProductId"];
                    product.ProdName = reader["ProdName"].ToString();
 
                    products.Add(product);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex; // throw it to the form to handle
            }
            finally
            {
                connection.Close();
            }
            return products;
        }
    }
}