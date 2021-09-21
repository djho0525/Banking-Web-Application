using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;


namespace DanielProject
{
    public partial class HomeLI : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
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
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.FindControl("addmoneybutton").Visible = false;

            string findNameQuery = "SELECT name FROM danielsschema.signup WHERE (email = '" + Session["User"] + "') OR (username = '" + Session["User"] + "')";
            conn.Open();
            SqlCommand cmd = new SqlCommand(findNameQuery, conn);
            if (cmd.ExecuteScalar() != null)
            {
                string res = cmd.ExecuteScalar().ToString();
                WelcomeBack.InnerText = "Welcome Back, " + res + "!";
                conn.Close();
                string findUserSSNQuery = "SELECT ssn FROM danielsschema.signup WHERE (email = '" + Session["User"] + "') OR (username = '" + Session["User"] + "')";
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(findUserSSNQuery, conn);
                string res1 = cmd1.ExecuteScalar().ToString();
                conn.Close();
                string findUserCheckingsAccountSummaryQuery = "SELECT balance FROM danielsschema.accountsummary WHERE (ssn = '" + res1 + "') AND (accountType = 'Checkings')";
                conn.Open();
                SqlCommand cmd5 = new SqlCommand(findUserCheckingsAccountSummaryQuery, conn);

                CultureInfo curCulture = new CultureInfo("en-US");
                curCulture.NumberFormat.CurrencyNegativePattern = 1;
                Thread.CurrentThread.CurrentCulture = curCulture;
                if (cmd5.ExecuteScalar() != null) 
                {
                    string res5 = cmd5.ExecuteScalar().ToString();
                    string currencyFormatted = Convert.ToDouble(res5).ToString("C", CultureInfo.CurrentCulture);
                    conn.Close();

                    CurrentBalanceCheckings.InnerText = currencyFormatted;
                }
                else
                {
                    CheckingsBalance.Visible = false;
                }
                conn.Close();

                string findUserSavingsAccountSummaryQuery = "SELECT balance FROM danielsschema.accountsummary WHERE (ssn = '" + res1 + "') AND (accountType = 'Savings')";
                conn.Open();
                SqlCommand cmd6 = new SqlCommand(findUserSavingsAccountSummaryQuery, conn);
                if (cmd6.ExecuteScalar() != null)
                {
                    string res6 = cmd6.ExecuteScalar().ToString();
                    string currencyFormatted = Convert.ToDouble(res6).ToString("C", CultureInfo.CurrentCulture);
                    CurrentBalanceSavings.InnerText = currencyFormatted;
                }
                else
                {
                    SavingsBalance.Visible = false;
                }
                conn.Close();
                string findRequestsForUsernameQuery = "SELECT username FROM danielsschema.signup WHERE (email = '" + Session["User"] + "') OR (username = '" + Session["User"] + "')";
                conn.Open();
                SqlCommand cmd3 = new SqlCommand(findRequestsForUsernameQuery, conn);
                string username = cmd3.ExecuteScalar().ToString();
                conn.Close();
                string findRequestsForEmailQuery = "SELECT email FROM danielsschema.signup WHERE (email = '" + Session["User"] + "') OR (username = '" + Session["User"] + "')";
                conn.Open();
                SqlCommand cmd4 = new SqlCommand(findRequestsForEmailQuery, conn);
                string email = cmd4.ExecuteScalar().ToString();
                conn.Close();
                string findRequestsQuery = "SELECT * FROM danielsschema.requests WHERE (usernameoremail = '" + username + "') OR (usernameoremail = '" + email + "')";
                conn.Open();
                SqlCommand cmd2 = new SqlCommand(findRequestsQuery, conn);
                SqlDataReader res2 = cmd2.ExecuteReader();
                if (res2.HasRows)
                {
                    CurrentBalanceGrid.DataSource = res2;
                    CurrentBalanceGrid.DataBind();
                }

                conn.Close();
                string findCheckingsAccountExistQuery = "SELECT accountNumber FROM danielsschema.accountsummary WHERE (ssn = '" + res1 + "') AND (accountType = 'Checkings')";
                conn.Open();
                SqlCommand cmd7 = new SqlCommand(findCheckingsAccountExistQuery, conn);
                conn.Close();

                string findSavingsAccountExistQuery = "SELECT accountNumber FROM danielsschema.accountsummary WHERE (ssn = '" + res1 + "') AND (accountType = 'Savings')";
                conn.Open();
                SqlCommand cmd8 = new SqlCommand(findSavingsAccountExistQuery, conn);
                if (cmd7.ExecuteScalar() != null && cmd8.ExecuteScalar() != null)
                {
                    AddNewAccountTypeButton.Visible = false;
                }
                conn.Close();
            }
            
        }
        public static string CheckingsORSavings;

        protected void LoadCheckingsAccountMethod(object sender, EventArgs e)
        {
            CheckingsORSavings = "Checkings";
            Response.Redirect("Transactions.aspx");
        }
        protected void LoadSavingsAccountMethod(object sender, EventArgs e)
        {
            CheckingsORSavings = "Savings";
            Response.Redirect("Transactions.aspx");
        }

        protected void LoadAddNewAccountTypeMethod(object sender, EventArgs e)
        {
            Response.Redirect("NewAccountType.aspx");
        }
        protected void LoadAddMoneyMethod(object sender, EventArgs e)
        {
            Response.Redirect("Deposit.aspx");
        }
    }
}