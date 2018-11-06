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
    public static class PackageDB
    {
        // retrieves package with given ID
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Package GetPackageByPackageId(int PackageId)
        {
            Package package = null; // found package
            // define connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "select * " +
                                 "from Packages " +
                                 "where PackageId = @PackageId";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@PackageId", PackageId);
            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                // process the result if any
                if (reader.Read()) // if there is customer
                {
                    package = new Package();
                    package.PackageId = (int)reader["PackageId"];
                    package.PkgName = reader["PkgName"].ToString();
                    package.PkgStartDate = reader["PkgStartDate"] as DateTime?;
                    package.PkgEndDate = reader["PkgEndDate"] as DateTime?;
                    package.PkgDesc = reader["PkgDesc"].ToString();
                    package.PkgBasePrice = (decimal) reader["PkgBasePrice"];
                    package.PkgAgencyCommission = (decimal)reader["PkgAgencyCommission"];
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

            return package;
        }
        
        // retrieves PackageId with given BookingId
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static int GetPackageIdByBookingId(int BookingId)
        {
            int packageId = 0;

            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "SELECT PackageId " +
                                 "FROM Bookings " +
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

                // process the result if any
                if (reader.Read()) // if there is booking
                {
                    //booking = new Booking();
                    //booking.PackageId = (int)reader["PackageId"];
                    packageId = (int)reader["PackageId"];
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
          
            return packageId;
        }

        // retrieves package with given ID
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Package GetPackageByBookingId(int BookingId)
        {
            return GetPackageByPackageId(GetPackageIdByBookingId(BookingId));
        }
    }
}