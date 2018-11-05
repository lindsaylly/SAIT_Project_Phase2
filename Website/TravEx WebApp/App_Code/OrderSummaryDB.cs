using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TravEx_WebApp.App_Code
{
    [DataObject(true)]
    public static class OrderSummaryDB
    {
        const decimal TAX_RATE = 0.05m;
        // retrieves customer with given ID
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<OrderSummary> GetOrderSummaryByBookingId(int BookingId)
        {
            List<OrderSummary> orderSummaries = new List<OrderSummary>(); // make an empty list
            OrderSummary orderSummary;
            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create select command
            string selectString = "SELECT BookingDetailId, TripStart, TripEnd, Description, Destination, p.ProdName, s.SupName, bd.BasePrice, f.FeeAmt " +
                                   "FROM BookingDetails bd INNER JOIN Products_Suppliers ps  ON bd.ProductSupplierId = ps.ProductSupplierId " +
                                   "INNER JOIN Products p ON ps.ProductId = p.ProductId " +
                                   "INNER JOIN Suppliers s ON ps.SupplierId = s.SupplierId " +
                                   "INNER JOIN Fees f ON bd.FeeId = f.FeeId " +
                                   "WHERE bd.BookingId = @BookingId " +
                                   "ORDER BY BookingDetailId";
            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            selectCommand.Parameters.AddWithValue("@BookingId", BookingId);
            try
            {

                connection.Open();

                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    orderSummary = new OrderSummary();
                    orderSummary.BookingDetailId = (int)reader["BookingDetailId"];
                    orderSummary.TripStart = (DateTime)reader["TripStart"];
                    orderSummary.TripEnd = (DateTime)reader["TripEnd"];
                    orderSummary.Description = reader["Description"].ToString();
                    orderSummary.Destination = reader["Destination"].ToString();
                    orderSummary.ProdName = reader["ProdName"].ToString();
                    orderSummary.SupName = reader["SupName"].ToString();
                    orderSummary.BasePrice = (decimal)reader["BasePrice"];
                    orderSummary.FeeAmt = (decimal)reader["FeeAmt"];
                    orderSummary.TaxAmt = (orderSummary.BasePrice + orderSummary.FeeAmt) * TAX_RATE;
                    orderSummary.TotalPrice = orderSummary.BasePrice + orderSummary.FeeAmt + orderSummary.TaxAmt;        
                    orderSummaries.Add(orderSummary);
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
            return orderSummaries;
        }
    }
}