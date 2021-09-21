using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Configuration;

namespace DanielProject
{
    public partial class Signup : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
        string connectionString = ConfigurationManager.ConnectionStrings["danielOracleConnectionString"].ToString();

        OracleConnection oracleConn = new OracleConnection(@"USER ID = DANIEL; PASSWORD = danjho; DATA SOURCE = XE; PERSIST SECURITY INFO = True");

        protected void Page_Load(object sender, EventArgs e)
        {
            PasswordsDoNotMatch.Visible = false;
            InvalidEmail.Visible = false;
            EmailUsed.Visible = false;
            UsernameTaken.Visible = false;
            Master.HideLoginSignupButtons();
            if (Master.LoggedIn == true)
            {
                Master.HideLoginSignupButtons();
            }
        }
        protected void CancelEventMethod(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
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
        protected void SignUpEventMethod(object sender, EventArgs e)
        {

            bool ErrorFound = false;
            if (CheckBox1.Checked && TextBoxSignUpPassword.Text.Length > 0 && TextBoxSignUpConfirmPassword.Text.Length > 0 && TextBoxSignUpConfirmPassword.Text.Length > 0 && TextBoxSignUpEmail.Text.Length > 0 && TextBoxSignUpUsername.Text.Length > 0 && TextBoxSignUpName.Text.Length > 0)
            {
                if (IsValidEmail(TextBoxSignUpEmail.Text) == false)
                {
                    InvalidEmail.Visible = true;
                    ErrorFound = true;
                }
                if (TextBoxSignUpPassword.Text == TextBoxSignUpConfirmPassword.Text)
                {
                    string oracleFindEmailQuery = "SELECT * FROM signup WHERE email = '"+ TextBoxSignUpEmail.Text +"'";
                    oracleConn.Open();
                    OracleCommand oracleCmd = new OracleCommand(oracleFindEmailQuery,oracleConn);
                    OracleDataReader oracleRes = oracleCmd.ExecuteReader();


                    string findEmailQuery = "SELECT * FROM danielsschema.signup WHERE email = '" + TextBoxSignUpEmail.Text + "'";
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(findEmailQuery, conn);
                    SqlDataReader res = cmd.ExecuteReader();
                    if (/*oracleRes.HasRows */res.HasRows)
                    {
                        EmailUsed.Visible = true;
                        ErrorFound = true;
                    }
                    conn.Close();
                    string findUserQuery = "SELECT * FROM danielsschema.signup WHERE username = '" + TextBoxSignUpUsername.Text + "'";
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand(findUserQuery, conn);
                    SqlDataReader res1 = cmd1.ExecuteReader();
                    if (res1.HasRows)
                    {
                        UsernameTaken.Visible = true;
                        ErrorFound = true;
                        conn.Close();
                    }
                }
                if (TextBoxSignUpPassword.Text != TextBoxSignUpConfirmPassword.Text)
                {
                    PasswordsDoNotMatch.Visible = true;
                    ErrorFound = true;
                }
                if (ErrorFound == false)
                {
                    conn.Close();

                    conn.Open();
                    SqlCommand cmd2 = new SqlCommand("INSERTNEWUSER", conn);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.Add("@name", SqlDbType.NVarChar).Value = TextBoxSignUpName.Text;
                    cmd2.Parameters.Add("@username", SqlDbType.NVarChar).Value = TextBoxSignUpUsername.Text;
                    cmd2.Parameters.Add("@password", SqlDbType.NVarChar).Value = TextBoxSignUpPassword.Text;
                    cmd2.Parameters.Add("@email", SqlDbType.NVarChar).Value = TextBoxSignUpEmail.Text;
                    cmd2.Parameters.Add("@ssn", SqlDbType.Decimal).Value = TextBoxSignUpSSN.Text;
                    cmd2.ExecuteNonQuery();

                    /*conn.Open();
                    SqlCommand cmd2 = new SqlCommand("DYNAMICINSERTUSER", conn);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.Add("@tablename", SqlDbType.NVarChar).Value = "dbo.signupcopy";
                    cmd2.Parameters.Add("@name", SqlDbType.NVarChar).Value = TextBoxSignUpName.Text;
                    cmd2.Parameters.Add("@username", SqlDbType.NVarChar).Value = TextBoxSignUpUsername.Text;
                    cmd2.Parameters.Add("@password", SqlDbType.NVarChar).Value = TextBoxSignUpPassword.Text;
                    cmd2.Parameters.Add("@email", SqlDbType.NVarChar).Value = TextBoxSignUpEmail.Text;
                    cmd2.Parameters.Add("@ssn", SqlDbType.Decimal).Value = TextBoxSignUpSSN.Text;
                    cmd2.ExecuteNonQuery();*/

                    /* conn.Open();
                     string insertQuery = "INSERT INTO signup(name,username,password,email,ssn) VALUES ( '" + TextBoxSignUpName.Text + "','" + TextBoxSignUpUsername.Text + "', '" + TextBoxSignUpPassword.Text + "', '" + TextBoxSignUpEmail.Text + "', '" + TextBoxSignUpSSN.Text + "')";
                     SqlCommand cmd2 = new SqlCommand(insertQuery, conn);
                     cmd2.ExecuteNonQuery();*/

                    conn.Close();
                    conn.Open();
                    string insertNewAccountSummaryQuery = "INSERT INTO danielsschema.accountsummary(accountType,ssn) VALUES ( '" + DropdownSignUpAccountType.Text + "','" + TextBoxSignUpSSN.Text + "')";
                    SqlCommand cmd3 = new SqlCommand(insertNewAccountSummaryQuery, conn); 
                    cmd3.ExecuteNonQuery();
                    Response.Redirect("Home.aspx");
                }
            }
        }
    }
}