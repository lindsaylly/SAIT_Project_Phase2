using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    [DataObject(true)]
    public static class SupplierDB
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>(); // make an empty list
            Supplier supplier;
            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create select command
            string selectString = "SELECT * FROM Suppliers " +
                                  "ORDER BY SupplierId";
            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            try
            {

                connection.Open();

                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    supplier = new Supplier();
                    supplier.SupplierId = (int)reader["SupplierId"];
                    supplier.SupName = reader["SupName"].ToString();

                    suppliers.Add(supplier);
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
            return suppliers;
        }
    }
}