using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/***********************************
 * This page written by Carol He
 ***********************************/

namespace TravEx_WebApp
{
    public partial class OrderSummary : System.Web.UI.Page
    {
        int loggedInCustomerID;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CustomerId"] = 143;
            loggedInCustomerID = (int)Session["CustomerId"];
        }

    }
}