using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


namespace DanielProject
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master.LoggedIn == true)
            {
                Master.HideLoginSignupButtons();
            }
            NoSuchUser.Visible = false;
            NoSuchEmail.Visible = false;
            WrongPassword.Visible = false;
            Master.HideLoginSignupButtons();
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
        protected void LoginEventMethod(object sender, EventArgs e)
        {
            if (TextBoxEmailUsername.Text.Length > 0 && TextBoxPassword.Text.Length > 0)
            {
                if (IsValidEmail(TextBoxEmailUsername.Text) == true)
                {
                    string findUserQuery = "SELECT * FROM danielsschema.signup WHERE email = '" + TextBoxEmailUsername.Text + "'";
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand(findUserQuery, conn);
                    SqlDataReader res1 = cmd1.ExecuteReader();

                    if (res1.HasRows)
                    {
                        conn.Close();
                        string findPasswordQuery = "SELECT * FROM danielsschema.signup WHERE email = '" + TextBoxEmailUsername.Text + "' AND password='" + TextBoxPassword.Text + "'";
                        conn.Open();
                        SqlCommand cmd2 = new SqlCommand(findPasswordQuery, conn);
                        SqlDataReader res2 = cmd2.ExecuteReader();
                        if (res2.HasRows)
                        {
                            WrongPassword.Visible = false;
                            Master.HideLoginSignupButtons();
                            Master.LoggedIn = true;
                            Session["User"] = TextBoxEmailUsername.Text;
                            Response.Redirect("HomeLI.aspx");
                        }
                        else
                        {
                            WrongPassword.Visible = true;
                        }
                    }
                    else
                    {
                        NoSuchEmail.Visible = true;
                    }
                }
                else
                {
                    string findUserQuery = "SELECT * FROM danielsschema.signup WHERE username = '" + TextBoxEmailUsername.Text + "'";
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand(findUserQuery, conn);
                    SqlDataReader res1 = cmd1.ExecuteReader();

                    if (res1.HasRows)
                    {
                        conn.Close();
                        string findPasswordQuery = "SELECT * FROM danielsschema.signup WHERE username = '" + TextBoxEmailUsername.Text + "' AND password='" + TextBoxPassword.Text + "'";
                        conn.Open();
                        SqlCommand cmd2 = new SqlCommand(findPasswordQuery, conn);
                        SqlDataReader res2 = cmd2.ExecuteReader();
                        if (res2.HasRows)
                        {
                            WrongPassword.Visible = false;
                            Master.HideLoginSignupButtons();
                            Master.LoggedIn = true;
                            Session["User"] = TextBoxEmailUsername.Text;
                            Response.Redirect("HomeLI.aspx");
                        }
                        else
                        {
                            WrongPassword.Visible = true;
                        }
                    }
                    else
                    {
                        NoSuchUser.Visible = true;
                    }
                    conn.Close();
                }
            }
        }
        protected void LoadSignupMethod(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }
    }
}