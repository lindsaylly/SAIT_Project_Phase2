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
    public static class BookingDetailDB
    {
        // retrieves customer with given ID
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static BookingDetail GetBookingDetailByBookingId(int BookingId)
        {
            BookingDetail bookingDetail = null; // found booking
            // define connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "select * " +
                                 "from BookingDetails " +
                                 "where BookingId = @BookingId";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@BookingId", BookingId);
            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                // process the result if any
                if (reader.Read()) // if there is customer
                {
                    bookingDetail = new BookingDetail();
                    bookingDetail.BookingDetailId = (int)reader["BookingDetailId"];
                    bookingDetail.ItineraryNo = reader["ItineraryNo"] as float?;
                    bookingDetail.TripStart = reader["TripStart"] as DateTime?;
                    bookingDetail.TripEnd = reader["TripEnd"] as DateTime?;
                    bookingDetail.Description = reader["Description"].ToString();
                    bookingDetail.Destination = reader["Destination"].ToString();
                    bookingDetail.BasePrice = (decimal)reader["BasePrice"];
                    bookingDetail.AgencyCommission = (decimal)reader["AgencyCommission"];                   
                    bookingDetail.BookingId = reader["BookingId"] as int?;
                    bookingDetail.RegionId = reader["RegionId"].ToString();
                    bookingDetail.ClassId = reader["ClassId"].ToString();
                    bookingDetail.FeeId = reader["FeeId"].ToString();
                    bookingDetail.ProductSupplierId = reader["ProductSupplierId"] as int?;
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

            return bookingDetail;
        }

    }
}