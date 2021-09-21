using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace DanielProject
{
    public partial class Request : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            InvalidUser.Visible = false;
            InvalidAmount.Visible = false;
            RequestMoneySuccess.Visible = false;
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        protected void RequestMoneyMethod(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxRequestEmailUsername.Text))
            {
                InvalidUser.Visible = true;
                return;
            }
            else if (string.IsNullOrWhiteSpace(TextBoxAddMoney.Text) || Convert.ToDouble(TextBoxAddMoney.Text.ToString()) == 0.00)
            {
                InvalidAmount.Visible = true;
                return;
            }
            else if (TextBoxRequestEmailUsername.Text == Session["User"].ToString())
            {
                InvalidUser.Visible = true;
                return;
            }
            else
            {
                string findReceivingUserQuery = "SELECT * FROM danielsschema.signup WHERE email = (email = '" + TextBoxRequestEmailUsername.Text + "') OR (username = '" + TextBoxRequestEmailUsername.Text + "')";
                conn.Open();
                SqlCommand cmd = new SqlCommand(findReceivingUserQuery, conn);
                SqlDataReader res = cmd.ExecuteReader();
                if (res.HasRows)
                {
                    conn.Close();
                    string findSendingUserQuery = "SELECT name FROM danielsschema.signup WHERE (email = '" + Session["User"] + "') OR (username = '" + Session["User"] + "')";
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand(findSendingUserQuery, conn);
                    conn.Close();
                    conn.Open();
                    string insertQuery = "INSERT INTO requests(sender,usernameoremail,amount,note) VALUES ('" + cmd1.ExecuteScalar().ToString() + "','" + TextBoxRequestEmailUsername.Text + "','" + TextBoxAddMoney.Text + "', '" + TextBoxRequestNote.Text + "')";
                    SqlCommand cmd2 = new SqlCommand(insertQuery, conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    RequestMoneySuccess.Visible = true;
                }
                else
                {
                    InvalidUser.Visible = true;
                    return;
                }
            }
    }
}
}