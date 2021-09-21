using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace DanielProject
{
    public partial class Transactions : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9MK9ES0;Initial Catalog=danielsProject1;Integrated Security=True");
        static int numberOfTransactionsDisplayed = 0;
        static string ssn = "";
        static string accountNumber = "";
        static int pageNum;
        static int rowsPerPage = 20;
        static double numberOfPagesNeededDec = 0;
        static double numberOfPagesNeededRounded = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageNum = 1;
                numberOfPagesNeededDec = 0;
                numberOfPagesNeededRounded = 0;
            }
            numberOfTransactionsDisplayed = 10;
            string findUserSSNQuery = "SELECT ssn FROM danielsschema.signup WHERE (email = '" + Session["User"] + "') OR (username = '" + Session["User"] + "')";
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(findUserSSNQuery, conn);
            string res1 = cmd1.ExecuteScalar().ToString();
            conn.Close();

            string findAccountNumberQuery = "SELECT accountNumber FROM danielsschema.accountsummary WHERE (ssn = '" + res1 + "') AND (accountType ='" + HomeLI.CheckingsORSavings + "')";
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(findAccountNumberQuery, conn);
            string res2 = cmd2.ExecuteScalar().ToString();
            conn.Close();

            conn.Open();
            string getDataListData = "SELECT TOP " + numberOfTransactionsDisplayed + " transactionName,amount,transactionTime,displayAmount FROM danielsschema.transactions WHERE (ssn = '" + res1 + "') AND (accountNumber = '" + res2 +"') ORDER BY transactionTime DESC";
            SqlCommand cmd3 = new SqlCommand(getDataListData, conn);
            SqlDataReader res3 = cmd3.ExecuteReader();
            if (res3.HasRows)
            {
                DataList1.DataSource = res3;
                DataList1.DataBind();
            }
            //conn.Close();

            /*DataTable dT = new DataTable();
            SqlDataAdapter dA = new SqlDataAdapter(cmd3);
            dA.Fill(dT);
            DataList3.DataSource = dT;
            DataList3.DataBind();*/

            ViewState["pageNum"] = pageNum;
            ViewState["rowsPerPage"] = rowsPerPage;

            if (Convert.ToInt32(ViewState["pageNum"]) == 1)
            {
                PrevPageButton.Enabled = false;
            }

            SqlCommand cmd4 = new SqlCommand("PAGINATION", conn);
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add("@pageNum", SqlDbType.Int).Value = pageNum;
            cmd4.Parameters.Add("@rowsPerPage", SqlDbType.Int).Value = rowsPerPage;
            cmd4.Parameters.Add("@ssn", SqlDbType.Decimal).Value = res1;
            cmd4.Parameters.Add("@accountNumber", SqlDbType.Int).Value = res2;
            //cmd4.ExecuteReader();

            conn.Close();
            DataTable dT = new DataTable();
            SqlDataAdapter dA = new SqlDataAdapter(cmd4);
            dA.Fill(dT);
            DataList3.DataSource = dT;
            DataList3.DataBind();

            DataTable dataTable = GetTable();

            ssn = res1;
            accountNumber = res2;

            conn.Open();
            SqlCommand cmd5 = new SqlCommand("TRANSACTIONSCOUNT", conn);
            cmd5.CommandType = CommandType.StoredProcedure;
            cmd5.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssn;
            cmd5.Parameters.Add("@accountNumber", SqlDbType.Int).Value = accountNumber;
            int transactionCount = Convert.ToInt32(cmd5.ExecuteScalar());
            conn.Close();
            numberOfPagesNeededDec = transactionCount / (double)rowsPerPage;
            numberOfPagesNeededRounded = Math.Ceiling(numberOfPagesNeededDec);

            if (pageNum == numberOfPagesNeededRounded)
            {
                NextPageButton.Enabled = false;
            }

            if (numberOfPagesNeededRounded == 0)
            {
                PrevPageButton.Enabled = false;
                NextPageButton.Enabled = false;
            }

            UpdatePageNumber();
        }

        protected void UpdatePageNumber()
        {
            pageNumLabel.Text = String.Format("Page {0}/{1}", Convert.ToInt32(ViewState["pageNum"]), numberOfPagesNeededRounded);
        }

        protected void LoadAddMoneyMethod(object sender, EventArgs e)
        {
            Response.Redirect("Deposit.aspx");
        }

        protected void ShowMoreResults(object sender, EventArgs e)
        {
            numberOfTransactionsDisplayed += 10;
            conn.Open();
            string getDataListData = "SELECT TOP " + numberOfTransactionsDisplayed + " transactionName,amount,transactionTime,displayAmount FROM danielsschema.transactions WHERE (ssn = '" + ssn + "') AND (accountNumber = '" + accountNumber + "') ORDER BY transactionTime DESC";
            SqlCommand cmd3 = new SqlCommand(getDataListData, conn);
            SqlDataReader res3 = cmd3.ExecuteReader();
            if (res3.HasRows)
            {
                DataList1.DataSource = res3;
                DataList1.DataBind();
            }
            conn.Close();

            DataTable dT = new DataTable();
            SqlDataAdapter dA = new SqlDataAdapter(cmd3);
            dA.Fill(dT);
            DataList3.DataSource = dT;
            DataList3.DataBind();
        }
        protected void PreviousPage(object sender, EventArgs e)
        {
            pageNum--;
            int newPageNum = Convert.ToInt32(ViewState["pageNum"]);
            newPageNum--;
            ViewState["pageNum"] = newPageNum;
            if (pageNum == 1)
            {
                PrevPageButton.Enabled = false;
            }
            NextPageButton.Enabled = true;

            SqlCommand cmd4 = new SqlCommand("PAGINATION", conn);
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add("@pageNum", SqlDbType.Int).Value = Convert.ToInt32(ViewState["pageNum"]);
            cmd4.Parameters.Add("@rowsPerPage", SqlDbType.Int).Value = rowsPerPage;
            cmd4.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssn;
            cmd4.Parameters.Add("@accountNumber", SqlDbType.Int).Value = accountNumber;
            //cmd4.ExecuteReader();

            conn.Close();
            DataTable dT = new DataTable();
            SqlDataAdapter dA = new SqlDataAdapter(cmd4);
            dA.Fill(dT);
            DataList3.DataSource = dT;
            DataList3.DataBind();
            UpdatePageNumber();

        }
        protected void NextPage(object sender, EventArgs e)
        {
            pageNum++;
            int newPageNum = Convert.ToInt32(ViewState["pageNum"]);
            newPageNum++;
            ViewState["pageNum"] = newPageNum;
            if (pageNum == numberOfPagesNeededRounded)
            {
                NextPageButton.Enabled = false;
            }
            PrevPageButton.Enabled = true;

            SqlCommand cmd4 = new SqlCommand("PAGINATION", conn);
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.Parameters.Add("@pageNum", SqlDbType.Int).Value = Convert.ToInt32(ViewState["pageNum"]);
            cmd4.Parameters.Add("@rowsPerPage", SqlDbType.Int).Value = rowsPerPage;
            cmd4.Parameters.Add("@ssn", SqlDbType.Decimal).Value = ssn;
            cmd4.Parameters.Add("@accountNumber", SqlDbType.Int).Value = accountNumber;
            //cmd4.ExecuteReader();

            conn.Close();
            DataTable dT = new DataTable();
            SqlDataAdapter dA = new SqlDataAdapter(cmd4);
            dA.Fill(dT);
            DataList3.DataSource = dT;
            DataList3.DataBind();
            UpdatePageNumber();

        }

        static DataTable GetTable()
        {
            DataTable table = new DataTable();
            /*table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Diagnosis", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            table.Rows.Add(25, "Drug A", "Disease A", DateTime.Now);
            table.Rows.Add(50, "Drug Z", "Problem Z", DateTime.Now);
            table.Rows.Add(10, "Drug Q", "Disorder Q", DateTime.Now);
            table.Rows.Add(21, "Medicine A", "Diagnosis A", DateTime.Now);*/
            return table;
        }
    }

}