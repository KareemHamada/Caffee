using POS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POS.Forms
{
    public partial class FormOrderItems : Form
    {
        public FormOrderItems()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private void FormOrderItems_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadTable(string query)
        {
            dgvLoading.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand(query, adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            //double total = 0; // اجمالي الورديات
            //double FinalTotal = 0;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    //double.TryParse(row["total"].ToString(), out total);
                    //FinalTotal += total;
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                            row["totalItem"],
                            row["quantity"],
                            row["price"],
                            row["name"],
                            row["id"],
                            }
                        ); ;
                }
            }

            //lblTotal.Text = FinalTotal.ToString();
        }

        public void showOrderItems(string orderId)
        {
            loadTable("select OrderItems.id,OrderItems.quantity,OrderItems.totalItem,OrderItems.price,Items.name from OrderItems LEFT JOIN Items on OrderItems.itemId = Items.id where orderId = '" + orderId + "'");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printChecks checks = new printChecks();
            checks.runPrintCheck(int.Parse(lblOrderId.Text));
        }
    }
}
