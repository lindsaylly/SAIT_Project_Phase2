using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TravEx_WebApp.App_Code;

/***********************************
 * This page written by Joel Barr
 ***********************************/
namespace TravEx_WebApp
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Handles the Click event of the btnRegister control.
        /// Tries to insert a new customer to the database
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="Exception">Cannot retrieve new customer ID</exception>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //Create customer from form controls
            Customer customer = new Customer()
            {
                CustFirstName = txtFirstName.Text,
                CustLastName = txtLastName.Text,
                CustAddress = txtAddress.Text,
                CustCity = txtCity.Text,
                CustPostal = txtPostal.Text,
                CustProv = drpProvince.Text,
                CustCountry = "Canada",
                CustHomePhone = txtHomePhone.Text,
                CustBusPhone = txtWorkPhone.Text,
                CustEmail = txtEmail.Text
            };

            //Try to insert to the database
            int? customerID = CustomerDB.InsertCustomer(customer);

            if (customerID == null) //Throw exception if insertion failed
                throw new Exception("Cannot retrieve new customer ID");

            //Create Login
            CustomerLogin login = new CustomerLogin()
            {
                CustomerId = customerID.Value,
                UserName = txtRegUserName.Text,
                Password = txtPassword1.Text
            };

            CustomerDB.InsertLogin(login);

            // Log in
            Login(customerID.Value);
        }

        /// <summary>
        /// Sets a session variable to track the user and redirects to home page
        /// </summary>
        /// <param name="customerID">The customer identifier.</param>
        private void Login(int customerID)
        {
            Session["CustomerId"] = customerID;
            Server.Transfer("Default.aspx", true);
        }

        /// <summary>
        /// Checks that the username used for logging in exists
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void loginEmailvalidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            
            int? customerId = GetLoginID();

            if (customerId != null && CustomerDB.LoginExists(customerId.Value))
                args.IsValid = true;
        }

        /// <summary>
        /// Retrieve a customerID related to the entered username
        /// </summary>
        /// <returns></returns>
        private int? GetLoginID()
        {
            if (txtLoginEmail.Text == String.Empty) return null;

            CustomerLogin login = CustomerDB.GetLoginByUserName(txtLoginEmail.Text);
            if (login == null) return null;

            return login.CustomerId;
        }

        /// <summary>
        /// Handles the Click event of the btnLogin control.
        /// Tries to log the user in if the password is correct
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int? customerID = GetLoginID();
            if (customerID == null || !CustomerDB.CheckPassword(customerID.Value, txtLoginPassword.Text)) return;

            Login(customerID.Value);
        }

        /// <summary>
        /// Handles the ServerValidate event of the loginPasswordValidator control.
        /// Checks that the given login password is a match for the username
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void loginPasswordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            string pwInput;

            if (txtLoginPassword.Text == String.Empty)
                return;
            else
                pwInput = txtLoginPassword.Text;

            int? customerID = GetLoginID();
            
            if (customerID != null && CustomerDB.CheckPassword(customerID.Value, pwInput))
                args.IsValid = true;
        }

        /// <summary>
        /// Handles the ServerValidate event of the emailExistsValidator control.
        /// Checks whether an email already exists in the Customer database
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="args">The <see cref="ServerValidateEventArgs"/> instance containing the event data.</param>
        protected void emailExistsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            string emailInput = txtEmail.Text;

            Customer existingCust = CustomerDB.GetCustomerByEmail(emailInput);
            if (existingCust == null) //Email does not exist in database
                args.IsValid = true;
        }
    }
}