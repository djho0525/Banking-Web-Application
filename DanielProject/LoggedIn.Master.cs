using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DanielProject
{
    public partial class LoggedIn : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LogoutMethod(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Home.aspx");
        }
        protected void LoadRequestMethod(object sender, EventArgs e)
        {
            Response.Redirect("Request.aspx");
        }
        protected void LoadPayMethod(object sender, EventArgs e)
        {
            Response.Redirect("Send.aspx");
        }
        protected void LoadAddMoneyMethod(object sender, EventArgs e)
        {
            Response.Redirect("Deposit.aspx");
        }
    }
}