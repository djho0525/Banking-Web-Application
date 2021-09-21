using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

namespace DanielProject
{
    public partial class SiteMaster : MasterPage
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
        public Boolean LoggedIn = false;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoggedIn == true)
            {
                HideLoginSignupButtons();
            }
        }
        public void HideLoginSignupButtons()
        {
            loginsignupbuttons.Visible = false;
        }
        protected void LoadLoginMethod(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
        protected void LoadSignupMethod(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }
    }
}