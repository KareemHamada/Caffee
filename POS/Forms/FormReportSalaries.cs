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
    public partial class FormReportSalaries : Form
    {
        public FormReportSalaries()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private void FormReportSalaries_Load(object sender, EventArgs e)
        {
            loadTable("select EmployeesSalaries.id,Employee.name,EmployeesSalaries.salary,EmployeesSalaries.dateTime from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id");
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
            double total = 0; // اجمالي المرتبات
            double FinalTotal = 0;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    double.TryParse(row["salary"].ToString(), out total);
                    FinalTotal += total;
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                            row["dateTime"],
                            row["salary"],
                            row["name"],
                            row["id"]
                            }
                        ); ;
                }
            }

            lblTotal.Text = FinalTotal.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select EmployeesSalaries.id,Employee.name,EmployeesSalaries.salary,EmployeesSalaries.dateTime from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadTable("select EmployeesSalaries.id,Employee.name,EmployeesSalaries.salary,EmployeesSalaries.dateTime from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id where dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsSalaries salaries = new dsSalaries();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = salaries.Tables["dtSalaries"].NewRow();
                    dro["name"] = dgvLoading[2, i].Value;
                    dro["salary"] = dgvLoading[1, i].Value;
                    dro["dateTime"] = dgvLoading[0, i].Value;

                    salaries.Tables["dtSalaries"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportSalaries.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", salaries.Tables["dtSalaries"]));

                ReportParameter[] reportParameters = new ReportParameter[2];
                reportParameters[0] = new ReportParameter("From", dgvLoading[0, 0].Value.ToString());
                reportParameters[1] = new ReportParameter("To", dgvLoading[0, dgvLoading.Rows.Count - 1].Value.ToString());


                if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportSalaries.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", salaries.Tables["dtSalaries"]));
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
