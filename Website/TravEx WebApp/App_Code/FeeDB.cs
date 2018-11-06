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
    public static class FeeDB
    {
        // retrieves customer with given ID
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Fee GetFeeByFeeId(string FeeId)
        {
            Fee fee = null; // found booking
            // define connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "select * " +
                                 "from Fees " +
                                 "where FeeId = @FeeId";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@FeeId", FeeId);
            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                // process the result if any
                if (reader.Read()) // if there is fee
                {
                    fee = new Fee();
                    fee.FeeId = reader["FeeId"].ToString();
                    fee.FeeName = reader["FeeName"].ToString();
                    fee.FeeAmt = (decimal)reader["FeeAmt"];
                    fee.FeeDesc = reader["FeeDesc"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex; // let the form handle it
            }
            finally
            {
                connection.Close(); // close connecto no matter what
            }

            return fee;
        }

        // retrieves PackageId with given BookingId
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static string GetFeeIdByBookingId(int BookingId)
        {
            string feeId = null;

            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "SELECT FeeId " +
                                 "FROM BookingDetails " +
                                 "WHERE BookingId = @BookingId";

            //$sql_pkg_id = "SELECT PackageId FROM bookings where bookings.bookingId = '$bookingId';";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@BookingId", BookingId);
            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    feeId = reader["FeeId"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex; // let the form handle it
            }
            finally
            {
                connection.Close(); // close connection no matter what
            }

            return feeId;
        }

        // retrieves fees with given BookingId
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Fee GetFeeByBookingId(int BookingId)
        {
            return GetFeeByFeeId(GetFeeIdByBookingId(BookingId));
        }
    }
}