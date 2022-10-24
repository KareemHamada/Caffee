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
using POS.Classes;
using POS.Tools;
using Microsoft.Reporting.WinForms;

namespace POS.Forms
{
    public partial class FormReportStoreItems : Form
    {
        public FormReportStoreItems()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;


      

        private void FormReportStoreItems_Load(object sender, EventArgs e)
        {
            loadTable("select storeOrderItems.id,storeOrderItems.price,storeOrderItems.quantity,storeId,storeItems.name,Stores.dateTime from storeOrderItems LEFT JOIN Stores on storeOrderItems.storeId = Stores.id LEFT JOIN storeItems on storeOrderItems.storeItemId = storeItems.id");
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
            double total = 0; // اجمالي الفواتير
            double FinalTotal = 0;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    double.TryParse(row["price"].ToString(), out total);
                    FinalTotal += total;
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                            row["dateTime"],
                            row["price"],
                            row["quantity"],
                            row["name"],
                            row["storeId"],
                            row["id"],
                            }
                        ); ;
                }
            }

            lblTotal.Text = FinalTotal.ToString();
        }

        

      




        public void showStoreItems(string storeId)
        {
            loadTable("select storeOrderItems.id,storeOrderItems.price,storeOrderItems.quantity,storeId,storeItems.name,Stores.dateTime from storeOrderItems LEFT JOIN Stores on storeOrderItems.storeId = Stores.id LEFT JOIN storeItems on storeOrderItems.storeItemId = storeItems.id where storeId = '" + storeId + "'");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsStoreOrdersItems storeItems = new dsStoreOrdersItems();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = storeItems.Tables["dtStoreOrdersItems"].NewRow();
                    dro["storeId"] = dgvLoading[4, i].Value;
                    dro["item"] = dgvLoading[3, i].Value;
                    dro["quantity"] = dgvLoading[2, i].Value;
                    dro["price"] = dgvLoading[1, i].Value;
                    dro["dateTime"] = dgvLoading[0, i].Value;

                    storeItems.Tables["dtStoreOrdersItems"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportStoreOrdersItems.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", storeItems.Tables["dtStoreOrdersItems"]));

                ReportParameter[] reportParameters = new ReportParameter[2];
                reportParameters[0] = new ReportParameter("From", dgvLoading[0, 0].Value.ToString());
                reportParameters[1] = new ReportParameter("To", dgvLoading[0, dgvLoading.Rows.Count - 1].Value.ToString());


                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportStoreOrdersItems.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", storeItems.Tables["dtStoreOrdersItems"]));
                    report.SetParameters(reportParameters);
                    PrintersClass.PrintToPrinter(report);
                }
                else if (bool.Parse(declarations.systemOptions["showBeforePrint"].ToString()))
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadTable("select storeOrderItems.id,storeOrderItems.price,storeOrderItems.quantity,storeId,storeItems.name,Stores.dateTime from storeOrderItems LEFT JOIN Stores on storeOrderItems.storeId = Stores.id LEFT JOIN storeItems on storeOrderItems.storeItemId = storeItems.id where dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'");
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select storeOrderItems.id,storeOrderItems.price,storeOrderItems.quantity,storeId,storeItems.name,Stores.dateTime from storeOrderItems LEFT JOIN Stores on storeOrderItems.storeId = Stores.id LEFT JOIN storeItems on storeOrderItems.storeItemId = storeItems.id");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select storeOrderItems.id,storeOrderItems.price,storeOrderItems.quantity,storeId,storeItems.name,Stores.dateTime from storeOrderItems LEFT JOIN Stores on storeOrderItems.storeId = Stores.id LEFT JOIN storeItems on storeOrderItems.storeItemId = storeItems.id");
            }
            else
            {
                loadTable("select storeOrderItems.id,storeOrderItems.price,storeOrderItems.quantity,storeId,storeItems.name,Stores.dateTime from storeOrderItems LEFT JOIN Stores on storeOrderItems.storeId = Stores.id LEFT JOIN storeItems on storeOrderItems.storeItemId = storeItems.id where storeId like '%" + text + "%' " +
                    "or storeItems.name like '%" + text + "%'");

            }
        }
    }
}
