using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    [DataObject(true)]
    public static class Product_SupplierDB
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Product_Supplier> GetAllProductsSuppliers()
        {
            List<Product_Supplier> products_suppliers = new List<Product_Supplier>(); // make an empty list
            Product_Supplier product_supplier;
            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create select command
            string selectString = "SELECT * FROM Products_Suppliers " +
                                  "ORDER BY ProductSupplierId";
            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            try
            {

                connection.Open();

                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    product_supplier = new Product_Supplier();
                    product_supplier.ProductSupplierId = (int)reader["ProductSupplierId"];
                    product_supplier.ProductId = (int)reader["ProductId"];
                    product_supplier.SupplierId = (int)reader["SupplierId"];

                    products_suppliers.Add(product_supplier);
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
            return products_suppliers;
        }

    }
}