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
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerId"] != null)
            {
                HyperLogin1.Visible = false;
                HyperLogin2.Visible = false;
                HyperLink1.Visible = true;
                HyperLink2.Visible = true;
                HyperLink3.Visible = true;
                HyperLink5.Visible = true;
                HyperLink6.Visible = true;
                HyperLink7.Visible = true;
                HyperLogin1.Style.Add("display", "none");
                HyperLogin2.Style.Add("display", "none");


            }
            else
            {
                HyperLogin1.Visible = true;
                HyperLogin2.Visible = true;
                HyperLink1.Visible = false;
                HyperLink2.Visible = false;
                HyperLink3.Visible = false;
                HyperLink5.Visible = false;
                HyperLink6.Visible = false;
                HyperLink7.Visible = false;
                HyperLink1.Style.Add("display", "none");
                HyperLink2.Style.Add("display", "none");
                HyperLink3.Style.Add("display", "none");
                HyperLink5.Style.Add("display", "none");
                HyperLink6.Style.Add("display", "none");
                HyperLink7.Style.Add("display", "none");
            }
        }
    }
}