using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace DanielProject
{
    public partial class Deposit : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
        OracleConnection oracleConn = new OracleConnection(@"USER ID = DANIEL; PASSWORD = danjho; DATA SOURCE = XE; PERSIST SECURITY INFO = True");

        protected void Page_Load(object sender, EventArgs e)
        {
            InvalidAmount.Visible = false;
            AddMoneySuccess.Visible = false;
        }
        protected void AddMoneyMethod(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxAddMoney.Text) || Convert.ToDouble(TextBoxAddMoney.Text.ToString()) == 0.00)
            {
                InvalidAmount.Visible = true;
                return;
            }
            else
            {
                oracleConn.Open();
                /*OracleCommand oraclcmd = new OracleCommand("SIGNUPANDLOGIN.RETRIEVESSNFUNC", oracleConn);
                oraclcmd.CommandType = CommandType.StoredProcedure;
                oraclcmd.Parameters.Add("RETSSN", OracleDbType.Int32).Direction = ParameterDirection.ReturnValue; //RETURN VALUE HAS TO BE FIRST PARAMETER
                oraclcmd.Parameters.Add("INPUTEMAIL", OracleDbType.Varchar2).Value = Session["User"];
                oraclcmd.Parameters.Add("INPUTUSERNAME", OracleDbType.Varchar2).Value = Session["User"];
                oraclcmd.ExecuteScalar();
                string ssnorcl = oraclcmd.Parameters["RETSSN"].Value.ToString();


                //string ssnorcl = oraclcmd.ExecuteScalar().ToString();
                */
                /*OracleCommand oraclcmd = new OracleCommand("SIGNUPANDLOGIN.RETRIEVESSN", oracleConn);
                oraclcmd.CommandType = CommandType.StoredProcedure;
                oraclcmd.Parameters.Add("RC", OracleDbType.RefCursor,ParameterDirection.Output);
                oraclcmd.Parameters.Add("INPUTEMAIL", OracleDbType.Varchar2).Value = Session["User"];
                oraclcmd.Parameters.Add("INPUTUSERNAME", OracleDbType.Varchar2).Value = Session["User"];
                string ssnorcl = oraclcmd.ExecuteScalar().ToString();
                */
                conn.Open();
                SqlCommand cmd6 = new SqlCommand("RETRIEVESSN", conn);
                cmd6.CommandType = CommandType.StoredProcedure;

                cmd6.Parameters.Add("@email", SqlDbType.NVarChar).Value = Session["User"];//TextBoxPayEmailUsername.Text;
                cmd6.Parameters.Add("@username", SqlDbType.NVarChar).Value = Session["User"];//TextBoxPayEmailUsername.Text;
                string ssn = cmd6.ExecuteScalar().ToString();

                if (/*ssnorcl != null*/ ssn != null)
                {
                    conn.Close();
                    string findAccountNumberQuery = "SELECT accountNumber FROM danielsschema.accountsummary WHERE (ssn = '" + ssn + "') AND (accountType ='" + HomeLI.CheckingsORSavings + "')";
                    conn.Open();
                    SqlCommand cmd2 = new SqlCommand(findAccountNumberQuery, conn);
                    int accNum = (int)cmd2.ExecuteScalar();
                    conn.Close();

                    conn.Close();
                    conn.Open();
                    SqlCommand cmd1 = new SqlCommand("RETRIEVEBALANCE", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssn;//res
                    cmd1.Parameters.Add("@accountNumber", SqlDbType.Int).Value = accNum;
                    double res1 = Convert.ToDouble(cmd1.ExecuteScalar().ToString());

                    double newBalance = res1 + Convert.ToDouble(TextBoxAddMoney.Text);
                    conn.Close();
                    conn.Open();
                    SqlCommand cmd7 = new SqlCommand("UPDATEBALANCE", conn);
                    cmd7.CommandType = CommandType.StoredProcedure;
                    cmd7.Parameters.Add("@balance", SqlDbType.Decimal).Value = newBalance;
                    cmd7.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssn;//res
                    cmd7.Parameters.Add("@accountNumber", SqlDbType.Int).Value = accNum;
                    cmd7.ExecuteNonQuery();
                    conn.Close();

                    conn.Open();
                    SqlCommand cmd9 = new SqlCommand("INSERTTRANSACTION", conn);
                    cmd9.CommandType = CommandType.StoredProcedure;
                    cmd9.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssn;
                    cmd9.Parameters.Add("@accountNumber", SqlDbType.Int).Value = accNum;
                    cmd9.Parameters.Add("@transactionName", SqlDbType.NVarChar).Value = "Deposit";
                    cmd9.Parameters.Add("@amount", SqlDbType.Decimal).Value = Convert.ToDouble(TextBoxAddMoney.Text);
                    cmd9.Parameters.Add("@transactionTime", SqlDbType.DateTime).Value = DateTime.Now;

                    string currencyFormattedRecipient = Convert.ToDouble(TextBoxAddMoney.Text).ToString("C", CultureInfo.CurrentCulture);
                    cmd9.Parameters.Add("@displayAmount", SqlDbType.NVarChar).Value = currencyFormattedRecipient;
                    cmd9.ExecuteNonQuery();
                    conn.Close();
                    AddMoneySuccess.Visible = true;
                }
            }
        }
    }
}