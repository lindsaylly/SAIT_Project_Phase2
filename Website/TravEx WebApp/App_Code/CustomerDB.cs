using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/*
 * Purpose: ASP.NET Workshop 5
 * Author: Carol, Lindsay, Joel
 * Date:July, 2018 
 */

namespace TravEx_WebApp.App_Code
{
    [DataObject(true)]
    public static class CustomerDB
    {
        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// By Carol
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>(); // make an empty list
            Customer cust;
            // create connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // create select command
            string selectString = "SELECT * FROM Customers " +
                                  "ORDER BY CustomerId";
            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            try
            {

                connection.Open();

                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    cust = new Customer();
                    cust.CustomerId = (int)reader["CustomerId"];
                    cust.CustFirstName = reader["CustFirstName"].ToString();
                    cust.CustLastName = reader["CustLastName"].ToString();
                    cust.CustAddress = reader["CustAddress"].ToString();
                    cust.CustCity = reader["CustCity"].ToString();
                    cust.CustProv = reader["CustProv"].ToString();
                    cust.CustPostal = reader["CustPostal"].ToString();
                    cust.CustCountry = reader["CustCountry"].ToString();
                    cust.CustHomePhone = reader["CustHomePhone"].ToString();
                    cust.CustBusPhone = reader["CustBusPhone"].ToString();
                    cust.CustEmail = reader["CustEmail"].ToString();
                    cust.AgentId = (int)reader["AgentId"];
                    customers.Add(cust);
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
            return customers;
        }


        // retrieves customer with given ID
        // By Carol
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Customer GetCustomerByCustomerId(int CustomerId)
        {
            Customer cust = null; // found customer
            // define connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "select * " +
                                 "from Customers " +
                                 "where CustomerId = @CustomerId";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@CustomerId", CustomerId);
            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                // process the result if any
                if (reader.Read()) // if there is customer
                {
                    cust = new Customer();
                    cust.CustomerId = (int)reader["CustomerId"];
                    cust.CustFirstName = reader["CustFirstName"].ToString();
                    cust.CustLastName = reader["CustLastName"].ToString();
                    cust.CustAddress = reader["CustAddress"].ToString();
                    cust.CustCity = reader["CustCity"].ToString();
                    cust.CustProv = reader["CustProv"].ToString();
                    cust.CustPostal = reader["CustPostal"].ToString();
                    cust.CustCountry = reader["CustCountry"].ToString();
                    cust.CustHomePhone = reader["CustHomePhone"].ToString();
                    cust.CustBusPhone = reader["CustBusPhone"].ToString();
                    cust.CustEmail = reader["CustEmail"].ToString();
                    if (reader["AgentId"] == DBNull.Value)
                        cust.AgentId = null;
                    else
                        cust.AgentId = (int)reader["AgentId"];
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

            return cust;
        }

        // By Joel
        internal static CustomerLogin GetLoginByUserName(string text)
        {
            CustomerLogin login = null; // found customer
            // define connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "select * " +
                                 "from Logins " +
                                 "where UserName = @uname";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@uname", text);
            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleResult);

                // process the result if any
                if (reader.Read()) // if there is customer
                {
                    login = new CustomerLogin();
                    login.CustomerId = (int)reader["CustomerId"];
                    login.UserName = reader["UserName"].ToString();
                    login.Password = reader["Password"].ToString();
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

            return login;
        }

        // BY Joel
        // OBSOLETE
        internal static Customer GetCustomerByEmail(string email)
        {
            Customer cust = null; // found customer
            // define connection
            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "select * " +
                                 "from Customers " +
                                 "where CustEmail = @email";
            SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@email", email);
            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleResult);

                // process the result if any
                if (reader.Read()) // if there is customer
                {
                    cust = new Customer();
                    cust.CustomerId = (int)reader["CustomerId"];
                    cust.CustFirstName = reader["CustFirstName"].ToString();
                    cust.CustLastName = reader["CustLastName"].ToString();
                    cust.CustAddress = reader["CustAddress"].ToString();
                    cust.CustCity = reader["CustCity"].ToString();
                    cust.CustProv = reader["CustProv"].ToString();
                    cust.CustPostal = reader["CustPostal"].ToString();
                    cust.CustCountry = reader["CustCountry"].ToString();
                    cust.CustHomePhone = reader["CustHomePhone"].ToString();
                    cust.CustBusPhone = reader["CustBusPhone"].ToString();
                    cust.CustEmail = reader["CustEmail"].ToString();
                    if (reader["AgentId"] == DBNull.Value)
                        cust.AgentId = null;
                    else
                        cust.AgentId = (int)reader["AgentId"];
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

            return cust;
        }

        // By Joel
        internal static bool CheckPassword(int customerId, string password)
        {
            SqlConnection conn = TravelExpertsDB.GetConnection();
            string stmt = "SELECT CASE WHEN Password = @pw THEN 1 ELSE 0 END FROM Logins WHERE CustomerID = @cID";
            SqlCommand cmd = new SqlCommand(stmt, conn);

            cmd.Parameters.AddWithValue("@cID", customerId);
            cmd.Parameters.AddWithValue("@pw", password);

            try
            {
                conn.Open();
                bool matches = Convert.ToBoolean(cmd.ExecuteScalar());

                return matches;
            }
            finally
            {
                conn.Close();
            }
        }

        // By Joel
        internal static bool LoginExists(int customerId)
        {
            SqlConnection conn = TravelExpertsDB.GetConnection();
            string stmt = "SELECT CASE WHEN CustomerID = @cID THEN 1 ELSE 0 END FROM Logins";
            SqlCommand cmd = new SqlCommand(stmt, conn);

            cmd.Parameters.AddWithValue("@cID", customerId);

            try
            {
                conn.Open();
                bool exists = Convert.ToBoolean(cmd.ExecuteScalar());

                return exists;
            }
            finally
            {
                conn.Close();
            }
        }

        // By Joel
        internal static void InsertLogin(CustomerLogin login)
        {
            SqlConnection conn = TravelExpertsDB.GetConnection();
            
            string insStmt = "INSERT INTO Logins (CustomerId, UserName, Password) VALUES (@cID, @uname, @pw)";
            SqlCommand cmd = new SqlCommand(insStmt, conn);

            cmd.Parameters.AddWithValue("@cID", login.CustomerId);
            cmd.Parameters.AddWithValue("@uname", login.UserName);
            cmd.Parameters.AddWithValue("@pw", login.Password);
            
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery(); //Insert
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        // By Carol
        // retrieves CustomerId with given BookingId
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static int GetCustomerIdByBookingId(int BookingId)
        {
            int customerId = 0;

            SqlConnection connection = TravelExpertsDB.GetConnection();

            // define the select query command
            string selectQuery = "SELECT CustomerId " +
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
                    customerId = (int)reader["CustomerId"];
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

            return customerId;
        }

        // retrieves customer with given ID
        // By Carol
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Customer GetCustomerByBookingId(int BookingId)
        {         
            return GetCustomerByCustomerId(GetCustomerIdByBookingId(BookingId));
        }

        //Author: Lindsay-----------------------------------------------------------
        //updates customer's information by customerId
        [DataObjectMethod(DataObjectMethodType.Update)]
        public static bool UpdateCustomerByCustomerId(int custId, Customer cust)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string UpdateSmt = "UPDATE Customers " +
                               "SET CustFirstName = @CustFirstName, " +
                                    "CustLastName = @CustLastName, " +
                                    "CustAddress = @CustAddress, " +
                                    "CustCity = @CustCity, " +
                                    "CustProv = @CustProv, " +
                                    "CustPostal = @CustPostal, " +
                                    "CustHomePhone = @CustHomePhone, " +
                                    "CustBusPhone = @CustBusphone, " +
                                    "CustEmail = @CustEmail " +
                                "WHERE CustomerId = @CustomerId";
            SqlCommand cmd = new SqlCommand(UpdateSmt, con);
            cmd.Parameters.AddWithValue("@CustomerId", custId);
            cmd.Parameters.AddWithValue("@CustFirstName", cust.CustFirstName);
            cmd.Parameters.AddWithValue("@CustLastName", cust.CustLastName);
            cmd.Parameters.AddWithValue("@CustAddress", cust.CustAddress);
            cmd.Parameters.AddWithValue("@CustCity", cust.CustCity);
            cmd.Parameters.AddWithValue("@CustProv", cust.CustProv);
            cmd.Parameters.AddWithValue("@CustPostal", cust.CustPostal);
            cmd.Parameters.AddWithValue("@CustHomePhone", cust.CustHomePhone);
            cmd.Parameters.AddWithValue("@CustBusPhone", cust.CustBusPhone);
            cmd.Parameters.AddWithValue("@CustEmail", cust.CustEmail);

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

        //Author: Lindsay---------------------------------------------------------------------------
        //reset customer's password by customerId
        [DataObjectMethod(DataObjectMethodType.Update)]
        public static bool ResetCustomerPassword(CustomerLogin reset)
        {
            SqlConnection con = TravelExpertsDB.GetConnection();
            string UpdateSmt = "Update Logins " +
                                "SET Password = @Password " +
                                "WHERE CustomerId = @CustomerId";
            SqlCommand cmd = new SqlCommand(UpdateSmt, con);
            cmd.Parameters.AddWithValue("@CustomerId", reset.CustomerId);
            cmd.Parameters.AddWithValue("@Password", reset.Password);
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



        /// <summary>
        /// Inserts the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns>The new Customer's Customer ID</returns>
        /// By Joel
        public static int? InsertCustomer(Customer customer)
        {
            int? custID = null;

            SqlConnection conn = TravelExpertsDB.GetConnection();
            string columns = " CustFirstName, CustLastName, CustAddress, CustCity, CustProv, " +
                                "CustPostal, CustCountry, CustHomePhone, CustBusPhone, CustEmail";
            string values = "@fname, @lname, @addr, @city, @prov, @post, @country, @phome, @pbus, @email";
            string insStmt = "INSERT INTO Customers (" + columns + ") VALUES (" + values + ")";
            SqlCommand cmd = new SqlCommand(insStmt, conn);

            cmd.Parameters.AddWithValue("@fname", customer.CustFirstName);
            cmd.Parameters.AddWithValue("@lname", customer.CustLastName);
            cmd.Parameters.AddWithValue("@addr", customer.CustAddress);
            cmd.Parameters.AddWithValue("@city", customer.CustCity);
            cmd.Parameters.AddWithValue("@prov", customer.CustProv);
            cmd.Parameters.AddWithValue("@post", customer.CustPostal);
            cmd.Parameters.AddWithValue("@country", customer.CustCountry ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@phome", customer.CustHomePhone);
            cmd.Parameters.AddWithValue("@pbus", customer.CustBusPhone ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@email", customer.CustEmail);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery(); //Insert

                //Retreive Customer ID
                SqlCommand selCmd = new SqlCommand("SELECT @@IDENTITY", conn);
                custID = Convert.ToInt32(selCmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return custID;
        }

    }
}