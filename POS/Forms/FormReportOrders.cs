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
using POS.Tools;
using System.Drawing.Printing;
using System.IO;
using POS.Forms;
using Microsoft.Reporting.WinForms;

namespace POS.Forms
{
    public partial class FormReportOrders : Form
    {
        public FormReportOrders()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

       

        private void FormReportOrders_Load(object sender, EventArgs e)
        {
            
            loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id");
            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now;
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
            double total = 0; // اجمالي الورديات
            double FinalTotal = 0;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    double.TryParse(row["total"].ToString(), out total);
                    FinalTotal += total;
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                            Properties.Resources.dgv,
                            row["tayar"],
                            row["name"],
                            row["total"],
                            row["discount"],
                            row["tax"],
                            row["delivery"],
                            row["orderType"],
                            row["dateTime"],
                            row["fullName"],
                            row["shiftId"],
                            row["id"]
                            }
                        ); ;
                }
            }

            lblTotal.Text = FinalTotal.ToString();
        }
        
        public void showShiftOrders(string shiftId)
        {
            loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id where Orders.shiftId = '" + shiftId +"'");
        }



     

       
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id where Orders.dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'");
        }

        private void btnSala_Click(object sender, EventArgs e)
        {
            loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id where Orders.orderType = 'صالة'");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id where Orders.orderType = 'دليفري'");
        }

        private void btnTeckaway_Click(object sender, EventArgs e)
        {
            loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id where Orders.orderType = 'تيك اوي'");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsOrders orders = new dsOrders();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = orders.Tables["dtOrders"].NewRow();
                    dro["id"] = dgvLoading[11, i].Value;
                    dro["shiftId"] = dgvLoading[10, i].Value;
                    dro["dateTime"] = dgvLoading[8, i].Value;
                    dro["userName"] = dgvLoading[9, i].Value;
                    dro["orderType"] = dgvLoading[7, i].Value;
                    dro["delivery"] = dgvLoading[6, i].Value;
                    dro["total"] = dgvLoading[3, i].Value;
                    dro["tax"] = dgvLoading[5, i].Value;
                    dro["discount"] = dgvLoading[4, i].Value;
                    dro["tayar"] = dgvLoading[1, i].Value;
                    orders.Tables["dtOrders"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportOrders.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", orders.Tables["dtOrders"]));

                ReportParameter[] reportParameters = new ReportParameter[2];
                reportParameters[0] = new ReportParameter("From", dgvLoading[8, 0].Value.ToString());
                reportParameters[1] = new ReportParameter("To", dgvLoading[8, dgvLoading.Rows.Count - 1].Value.ToString());


                if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportOrders.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", orders.Tables["dtOrders"]));
                    report.SetParameters(reportParameters);
                    PrintersClass.PrintToPrinter(report);
                }
                else
                {
                    rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                    rptForm.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show("لا يوجد عناصر لعرضها");
            }
        }

    

        private void dgvLoading_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLoading.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
            {
                if (dgvLoading.CurrentCell != null && dgvLoading.CurrentCell.Value != null)
                {
                    string orderId = dgvLoading.CurrentRow.Cells[11].Value.ToString();
                    FormOrderItems frm = new FormOrderItems();
                    frm.Show();
                    frm.showOrderItems(orderId);
                    frm.lblDelivery.Text = dgvLoading.CurrentRow.Cells[6].Value.ToString();
                    frm.lblDiscount.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                    frm.lblOrderId.Text = orderId;
                    frm.lblOrderType.Text = dgvLoading.CurrentRow.Cells[7].Value.ToString();
                    frm.lblTax.Text = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                    frm.lblTotal.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();

                }

            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id");
            }
            else
            {
                loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id where Orders.shiftId like '%" + text + "%' " +
                    "or Orders.orderType like '%" + text + "%' " +
                    "or Users.fullName like '%" + text + "%' " +
                    "or Clients.name like '%" + text + "%' " +
                    "or Tayar.name like '%" + text + "%' ");
            }
        }
    }
}
