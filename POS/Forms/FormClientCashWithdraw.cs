using POS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Forms
{
    public partial class FormClientCashWithdraw : Form
    {
        public FormClientCashWithdraw()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private void FormClientCashWithdraw_Load(object sender, EventArgs e)
        {
            Helper.fillComboBox(comboClient, "Select id,name from Clients", "name", "id");

            // combo box type of operation
            DataTable tblPrivilege = new DataTable();
            tblPrivilege.Columns.Add("id", typeof(int));
            tblPrivilege.Columns.Add("type");

            tblPrivilege.Rows.Add(new object[] { 0, "" });
            tblPrivilege.Rows.Add(new object[] { 1, "اجل" });
            tblPrivilege.Rows.Add(new object[] { 2, "سداد" });

            comboType.DataSource = tblPrivilege;
            comboType.DisplayMember = "type";
            comboType.ValueMember = "id";
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            Decimal moneyDecimal = Decimal.Parse(txtMoney.Text);

            if (comboClient.Text == "")
            {
                MessageBox.Show("اختار اسم العميل");
                return;
            }
            if (comboType.Text == "")
            {
                MessageBox.Show("اختار نوع العملية");
                return;
            }
            if (txtMoney.Text == "" || moneyDecimal <= 0)
            {
                MessageBox.Show("ادخل المبلغ");
                return;
            }

            try
            {
                cmd = new SqlCommand("Insert into ClientCashWithdraw (ClientId,OperationType,money,dateTime) values (@ClientId,@OperationType,@money,@dateTime)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@ClientId", comboClient.SelectedValue);
                cmd.Parameters.AddWithValue("@OperationType", comboType.Text);
                cmd.Parameters.AddWithValue("@money", moneyDecimal);
                cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تمت العملية بنجاح");
                adoClass.sqlcn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            comboClient.Text = "";
            comboType.Text = "";
            txtMoney.Text = "";
            lblInfo.Text = "";
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            decimal Withdrawal = 0; // سحب
            decimal cash = 0;
            decimal total; // االي عليه
            DataTable dt = new DataTable();
            if (comboClient.Text == "")
            {
                MessageBox.Show("حدد العميل");
                return;
            }

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }

            cmd = new SqlCommand("Select * from ClientCashWithdraw where ClientId = '" + comboClient.SelectedValue + "'", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                //TextBox1.Text = row["ImagePath"].ToString();
                if (row["OperationType"].ToString() == "اجل")
                {
                    Withdrawal += decimal.Parse(row["money"].ToString());
                }
                else
                {
                    cash += decimal.Parse(row["money"].ToString());

                }
            }

            total = Withdrawal - cash;

            lblInfo.Text = total.ToString();

            adoClass.sqlcn.Close();
        }

        private void txtMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
