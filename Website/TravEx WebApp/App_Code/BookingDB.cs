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
    public static class BookingDB
    {
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Booking> GetAllBookingId()
        {
            List<Booking> bookings = new List<Booking>(); // make an empty list
            Booking bk; // reference to new state object
            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create select command
            string selectString = "select BookingId from Bookings " +
                                  "order by BookingId";
            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            try
            {
                // open connection
                connection.Open();
                // run the select command and process the results adding states to the list
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())// process next row
                {
                    bk = new Booking();
                    bk.BookingId = (int)reader["BookingId"];                  
                    bookings.Add(bk);
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
            return bookings;
        }


        // retrieves customer with given ID
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Booking GetBookingByBookingId(int BookingId)
        {
            
            Booking booking = null; // found booking
            // define connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "select * " +
                                 "from Bookings " +
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
                    booking = new Booking();
                    booking.BookingId = (int)reader["BookingId"];
                    booking.BookingDate = reader["BookingDate"] as DateTime?;
                    booking.BookingNo = reader["BookingNo"].ToString();
                    booking.TravelerCount = reader["TravelerCount"] as float?;
                    booking.CustomerId = reader["CustomerId"] as int?;
                    booking.TripTypeId = reader["TripTypeId"].ToString();
                    booking.PackageId = reader["PackageId"] as int?;
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

            return booking;
        }

        // retrieves customer with given ID
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Booking> GetBookingsByCustomerId(int CustomerId)
        {
            List<Booking> bookings = new List<Booking>();
            Booking booking = null; // found booking
            // define connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "select * " +
                                 "from Bookings " +
                                 "where CustomerId = @CustomerId";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@CustomerId", CustomerId);
            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader();

                // process the result if any
                while (reader.Read()) // if there is customer
                {
                    booking = new Booking();
                    booking.BookingId = (int)reader["BookingId"];
                    booking.BookingDate = reader["BookingDate"] as DateTime?;
                    booking.BookingNo = reader["BookingNo"].ToString();
                    booking.TravelerCount = reader["TravelerCount"] as float?;
                    booking.CustomerId = reader["CustomerId"] as int?;
                    booking.TripTypeId = reader["TripTypeId"].ToString();
                    booking.PackageId = reader["PackageId"] as int?;
                    bookings.Add(booking);
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

            return bookings;
        }

    }
}