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
    public partial class FormReportExpenses : Form
    {
        public FormReportExpenses()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormReportExpenses_Load(object sender, EventArgs e)
        {
            loadTable("select Expenses.id,Expenses.name,Expenses.price,Expenses.dateTime,Users.fullName,Expenses.shiftId from Expenses LEFT JOIN Users on Expenses.userId = Users.Id");
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
            double total = 0; // اجمالي المصروفات
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
                            row["shiftId"],
                            row["fullName"],
                            row["dateTime"],
                            row["price"],
                            row["name"],
                            row["id"]
                            }
                        ); ;
                }
            }

            lblTotal.Text = FinalTotal.ToString();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select Expenses.id,Expenses.name,Expenses.price,Expenses.dateTime,Users.fullName,Expenses.shiftId from Expenses LEFT JOIN Users on Expenses.userId = Users.Id");
        }


        public void showShiftExpenses(string shiftId)
        {
            loadTable("select Expenses.id,Expenses.name,Expenses.price,Expenses.dateTime,Users.fullName,Expenses.shiftId from Expenses LEFT JOIN Users on Expenses.userId = Users.Id where shiftId = '" + shiftId + "'");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadTable("select Expenses.id,Expenses.name,Expenses.price,Expenses.dateTime,Users.fullName,Expenses.shiftId from Expenses LEFT JOIN Users on Expenses.userId = Users.Id where dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsExpenses expenses = new dsExpenses();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = expenses.Tables["dtExpenses"].NewRow();
                    dro["name"] = dgvLoading[4, i].Value;
                    dro["price"] = dgvLoading[3, i].Value;
                    dro["dateTime"] = dgvLoading[2, i].Value;
                    dro["userName"] = dgvLoading[1, i].Value;
                    dro["shiftId"] = dgvLoading[0, i].Value;

                    expenses.Tables["dtExpenses"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportExpenses.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", expenses.Tables["dtExpenses"]));

                ReportParameter[] reportParameters = new ReportParameter[2];
                reportParameters[0] = new ReportParameter("From", dgvLoading[2, 0].Value.ToString());
                reportParameters[1] = new ReportParameter("To", dgvLoading[2, dgvLoading.Rows.Count - 1].Value.ToString());


                if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportExpenses.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", expenses.Tables["dtExpenses"]));
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
    }
}
