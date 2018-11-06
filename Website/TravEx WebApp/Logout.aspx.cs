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
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["CustomerId"] = null;
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}