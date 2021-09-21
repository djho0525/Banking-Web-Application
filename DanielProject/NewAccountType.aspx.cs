using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;

namespace DanielProject
{
    public partial class NewAccountType : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            WrongSSN.Visible = false;
        }
        protected void NewAccountTypeEventMethod(object sender, EventArgs e)
        {
            string findUserSSNQuery = "SELECT ssn FROM danielsschema.signup WHERE (email = '" + Session["User"] + "') OR (username = '" + Session["User"] + "')";
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(findUserSSNQuery, conn);
            string res1 = cmd1.ExecuteScalar().ToString();
            conn.Close();

            conn.Close();
            conn.Open();
            if(TextBoxSignUpSSN.Text == res1) {
                string insertNewAccountSummaryQuery = "INSERT INTO danielsschema.accountsummary(accountType,ssn) VALUES ( '" + DropdownSignUpAccountType.Text + "','" + TextBoxSignUpSSN.Text + "')";
                SqlCommand cmd3 = new SqlCommand(insertNewAccountSummaryQuery, conn);
                cmd3.ExecuteNonQuery();
                Response.Redirect("HomeLI.aspx");
            }
            else
            {
                WrongSSN.Visible = true;
            }
        }
    }
}