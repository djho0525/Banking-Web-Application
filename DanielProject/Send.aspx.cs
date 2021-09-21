using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace DanielProject
{
    public partial class Send : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            InvalidUser.Visible = false;
            InvalidAmount.Visible = false;
            PayMoneySuccess.Visible = false;
            NotEnoughMoney.Visible = false;
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
        protected void PayMoneyMethod(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxPayEmailUsername.Text))
            {
                InvalidUser.Visible = true;
                return;
            }
            else if (string.IsNullOrWhiteSpace(TextBoxSendMoney.Text) || Convert.ToDouble(TextBoxSendMoney.Text.ToString()) == 0.00)
            {
                InvalidAmount.Visible = true;
                return;
            }
            else if (TextBoxPayEmailUsername.Text == Session["User"].ToString())
            {
                InvalidUser.Visible = true;
                return;
            }
            else
            {
                conn.Open();
                SqlCommand cmd6 = new SqlCommand("RETRIEVESSN", conn);
                cmd6.CommandType = CommandType.StoredProcedure;

                cmd6.Parameters.Add("@email", SqlDbType.NVarChar).Value = Session["User"];//TextBoxPayEmailUsername.Text;
                cmd6.Parameters.Add("@username", SqlDbType.NVarChar).Value = Session["User"];//TextBoxPayEmailUsername.Text;
                string ssn = cmd6.ExecuteScalar().ToString();
                string ssnReceiver = "";
                if (ssn != null /*res.HasRows*/)
                {
                    conn.Close();
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("RETRIEVEBALANCE", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssn;//res
                    cmd1.Parameters.Add("@accountNumber", SqlDbType.Int).Value = senderAccountNumber.Text;
                    double res1 = Convert.ToDouble(cmd1.ExecuteScalar().ToString());
                    double transactionAmount = Convert.ToDouble(TextBoxSendMoney.Text);

                    if (res1 - transactionAmount >= 0)
                    {
                        double newBalance = res1 - transactionAmount;
                        conn.Close();
                        conn.Open();
                        SqlCommand cmd7 = new SqlCommand("UPDATEBALANCE", conn);
                        cmd7.CommandType = CommandType.StoredProcedure;
                        cmd7.Parameters.Add("@balance", SqlDbType.Decimal).Value = newBalance;
                        cmd7.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssn;//res
                        cmd7.Parameters.Add("@accountNumber", SqlDbType.Int).Value = senderAccountNumber.Text;
                        cmd7.ExecuteNonQuery();
                        conn.Close();


                        conn.Open();
                        SqlCommand cmd3 = new SqlCommand("RETRIEVESSN", conn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add("@email", SqlDbType.NVarChar).Value = TextBoxPayEmailUsername.Text;
                        cmd3.Parameters.Add("@username", SqlDbType.NVarChar).Value = TextBoxPayEmailUsername.Text;
                        ssnReceiver = cmd3.ExecuteScalar().ToString();
                        conn.Close();

                        conn.Open();
                        SqlCommand cmd4 = new SqlCommand("RETRIEVEBALANCE", conn);
                        cmd4.CommandType = CommandType.StoredProcedure;
                        cmd4.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssnReceiver;//res
                        cmd4.Parameters.Add("@accountNumber", SqlDbType.Int).Value = recipientAccountNumber.Text;
                        double res4 = Convert.ToDouble(cmd4.ExecuteScalar().ToString());

                        if (cmd3.ExecuteScalar() != null)
                        {
                            double newBalanceReceiver = res4 + transactionAmount;
                            conn.Close();

                            conn.Open();
                            SqlCommand cmd8 = new SqlCommand("UPDATEBALANCE", conn);
                            cmd8.CommandType = CommandType.StoredProcedure;
                            cmd8.Parameters.Add("@balance", SqlDbType.Decimal).Value = newBalanceReceiver;
                            cmd8.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssnReceiver;//res
                            cmd8.Parameters.Add("@accountNumber", SqlDbType.Int).Value = recipientAccountNumber.Text;
                            cmd8.ExecuteNonQuery();

                            conn.Close();
                            PayMoneySuccess.Visible = true;
                        }
                        else
                        {
                            Debug.WriteLine("Invalid User!");
                            InvalidUser.Visible = true;
                        }
                    }
                    else
                    {
                        NotEnoughMoney.Visible = true;
                    }
                }
                /*else
                {
                    InvalidUser.Visible = true;
                }*/
                DateTime transactionTime = DateTime.Now;

                conn.Open();
                SqlCommand cmd9 = new SqlCommand("INSERTTRANSACTION", conn);
                cmd9.CommandType = CommandType.StoredProcedure;
                cmd9.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssn;
                cmd9.Parameters.Add("@accountNumber", SqlDbType.Int).Value = senderAccountNumber.Text;
                cmd9.Parameters.Add("@transactionName", SqlDbType.NVarChar).Value = TextBoxTransactionName.Text;
                cmd9.Parameters.Add("@amount", SqlDbType.Decimal).Value = Convert.ToDouble(TextBoxSendMoney.Text) * -1;
                cmd9.Parameters.Add("@transactionTime", SqlDbType.DateTime).Value = transactionTime;

                CultureInfo curCulture = new CultureInfo("en-US");
                curCulture.NumberFormat.CurrencyNegativePattern = 1;
                Thread.CurrentThread.CurrentCulture = curCulture;

                string currencyFormattedSender = (Convert.ToDouble(TextBoxSendMoney.Text)*-1).ToString("C", CultureInfo.CurrentCulture);
                cmd9.Parameters.Add("@displayAmount", SqlDbType.NVarChar).Value = currencyFormattedSender;
                cmd9.ExecuteNonQuery();
                conn.Close();

                conn.Open();
                cmd9 = new SqlCommand("INSERTTRANSACTION", conn);
                cmd9.CommandType = CommandType.StoredProcedure;
                cmd9.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssnReceiver;
                cmd9.Parameters.Add("@accountNumber", SqlDbType.Int).Value = recipientAccountNumber.Text;
                cmd9.Parameters.Add("@transactionName", SqlDbType.NVarChar).Value = TextBoxTransactionName.Text;
                cmd9.Parameters.Add("@amount", SqlDbType.Decimal).Value = TextBoxSendMoney.Text;
                cmd9.Parameters.Add("@transactionTime", SqlDbType.DateTime).Value = transactionTime;

                string currencyFormattedRecipient = Convert.ToDouble(TextBoxSendMoney.Text).ToString("C", CultureInfo.CurrentCulture);
                cmd9.Parameters.Add("@displayAmount", SqlDbType.NVarChar).Value = currencyFormattedRecipient;
                cmd9.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}