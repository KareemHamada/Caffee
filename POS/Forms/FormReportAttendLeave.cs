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
    public partial class FormReportAttendLeave : Form
    {
        public FormReportAttendLeave()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private void FormReportAttendLeave_Load(object sender, EventArgs e)
        {
            loadTable("select EmpAttendLeave.id,employee.name,EmpAttendLeave.dateTimeAttend,EmpAttendLeave.dateTimeLeave,EmpAttendLeave.notes from EmpAttendLeave LEFT JOIN Employee on EmpAttendLeave.EmpId = Employee.id");
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

            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                            row["notes"],
                            row["dateTimeLeave"],
                            row["dateTimeAttend"],
                            row["name"],
                            row["id"]
                            }
                        ); ;
                }
            }

        }

       

       

       

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsAttendLeave expenses = new dsAttendLeave();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = expenses.Tables["dtAttendLeave"].NewRow();
                    dro["name"] = dgvLoading[3, i].Value;
                    dro["dateAttend"] = dgvLoading[2, i].Value;
                    dro["dateLeave"] = dgvLoading[1, i].Value;
                    dro["Notes"] = dgvLoading[0, i].Value;

                    expenses.Tables["dtAttendLeave"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportAttendLeave.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", expenses.Tables["dtAttendLeave"]));

                ReportParameter[] reportParameters = new ReportParameter[2];
                reportParameters[0] = new ReportParameter("From", dgvLoading[2, 0].Value.ToString());
                reportParameters[1] = new ReportParameter("To", dgvLoading[2, dgvLoading.Rows.Count - 1].Value.ToString());


                if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportAttendLeave.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", expenses.Tables["dtAttendLeave"]));
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

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select EmpAttendLeave.id,employee.name,EmpAttendLeave.dateTimeAttend,EmpAttendLeave.dateTimeLeave,EmpAttendLeave.notes from EmpAttendLeave LEFT JOIN Employee on EmpAttendLeave.EmpId = Employee.id");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadTable("select EmpAttendLeave.id,employee.name,EmpAttendLeave.dateTimeAttend,EmpAttendLeave.dateTimeLeave,EmpAttendLeave.notes from EmpAttendLeave LEFT JOIN Employee on EmpAttendLeave.EmpId = Employee.id where dateTimeAttend between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select EmpAttendLeave.id,employee.name,EmpAttendLeave.dateTimeAttend,EmpAttendLeave.dateTimeLeave,EmpAttendLeave.notes from EmpAttendLeave LEFT JOIN Employee on EmpAttendLeave.EmpId = Employee.id");
            }
            else
            {
                loadTable("select EmpAttendLeave.id,employee.name,EmpAttendLeave.dateTimeAttend,EmpAttendLeave.dateTimeLeave,EmpAttendLeave.notes from EmpAttendLeave LEFT JOIN Employee on EmpAttendLeave.EmpId = Employee.id where employee.name like '%" + text + "%' or EmpAttendLeave.notes like '%" + text + "%' ");

                
            }
        }
    }
}
