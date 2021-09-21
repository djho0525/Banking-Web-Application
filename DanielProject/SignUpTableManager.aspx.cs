using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DanielProject
{
    public partial class SignUpTableManager : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
        OracleConnection oracleConn = new OracleConnection(@"USER ID = DANIEL; PASSWORD = danjho; DATA SOURCE = XE; PERSIST SECURITY INFO = True");

        protected void Page_Load(object sender, EventArgs e)
        {
            PasswordsDoNotMatch.Visible = false;
            InvalidEmail.Visible = false;
            EmailUsed.Visible = false;
            UsernameTaken.Visible = false;
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
        protected void SignUpManagerApplyEventMethod(object sender, EventArgs e)
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
                    string oracleFindEmailQuery = "SELECT * FROM signup WHERE email = '" + TextBoxSignUpEmail.Text + "'";
                    oracleConn.Open();
                    OracleCommand oracleCmd = new OracleCommand(oracleFindEmailQuery, oracleConn);
                    OracleDataReader oracleRes = oracleCmd.ExecuteReader();
                    oracleConn.Close();

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
                    }
                    conn.Close();
                }
                if (TextBoxSignUpPassword.Text != TextBoxSignUpConfirmPassword.Text)
                {
                    PasswordsDoNotMatch.Visible = true;
                    ErrorFound = true;
                }
                if (ErrorFound == false)
                {
                    oracleConn.Open();
                    OracleCommand orclcmd = new OracleCommand("SIGNUPANDLOGIN.INSERTUPDATEDELETESIGNUP", oracleConn);
                    orclcmd.CommandType = CommandType.StoredProcedure;
                    orclcmd.Parameters.Add("RETSTR", OracleDbType.Varchar2,100).Direction = ParameterDirection.ReturnValue; //RETURN VALUE HAS TO BE FIRST PARAMETER
                    orclcmd.Parameters.Add("OPERATION", OracleDbType.Varchar2).Value = TextBoxSignUpManagerOperation.Text;
                    orclcmd.Parameters.Add("NAMEUSERPW", OracleDbType.Varchar2).Value = TextBoxSignUpManagerUpdateValue.Text;
                    orclcmd.Parameters.Add("INPUTNAME", OracleDbType.Varchar2).Value = TextBoxSignUpName.Text;
                    orclcmd.Parameters.Add("INPUTUSERNAME", OracleDbType.Varchar2).Value = TextBoxSignUpUsername.Text;
                    orclcmd.Parameters.Add("INPUTPASSWORD", OracleDbType.Varchar2).Value = TextBoxSignUpPassword.Text;
                    orclcmd.Parameters.Add("INPUTEMAIL", OracleDbType.Varchar2).Value = TextBoxSignUpEmail.Text;
                    orclcmd.Parameters.Add("INPUTSSN", OracleDbType.Int32).Value = TextBoxSignUpSSN.Text;
                    orclcmd.ExecuteNonQuery();
                    string retstr = orclcmd.Parameters["RETSTR"].Value.ToString();
                    oracleConn.Close();

                    if(TextBoxSignUpManagerOperation.Text == "I")
                    {
                        oracleConn.Open();
                        string insertNewAccountSummaryQuery = "INSERT INTO accountsummary(accountType,ssn) VALUES ( '" + DropdownSignUpAccountType.Text + "','" + TextBoxSignUpSSN.Text + "')";
                        OracleCommand cmd3 = new OracleCommand(insertNewAccountSummaryQuery, oracleConn);
                        cmd3.ExecuteNonQuery();
                        oracleConn.Close();
                    }


                    Response.Redirect("SignUpTableManager.aspx");
                }
            }
        }

    }
}