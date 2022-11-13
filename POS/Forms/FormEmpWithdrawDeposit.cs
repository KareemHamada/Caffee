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
    public partial class FormEmpWithdrawDeposit : Form
    {
        public FormEmpWithdrawDeposit()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private void FormEmpWithdrawDeposit_Load(object sender, EventArgs e)
        {
            loadTable("select EmpWithdrawDeposit.id,employee.name,EmpWithdrawDeposit.OperationType,EmpWithdrawDeposit.money,EmpWithdrawDeposit.dateTime from EmpWithdrawDeposit LEFT JOIN Employee on EmpWithdrawDeposit.EmpId = Employee.id");
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
                            row["dateTime"],
                            row["money"],
                            row["OperationType"],
                            row["name"],
                            row["id"],
                            Properties.Resources.delete
                            }
                        ); ;
                }
            }

        }

 

        


        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadTable("select EmpWithdrawDeposit.id,employee.name,EmpWithdrawDeposit.OperationType,EmpWithdrawDeposit.money,EmpWithdrawDeposit.dateTime from EmpWithdrawDeposit LEFT JOIN Employee on EmpWithdrawDeposit.EmpId = Employee.id where dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsDepositWithdrawEmployees dw = new dsDepositWithdrawEmployees();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = dw.Tables["dtDepositWithdrawEmployees"].NewRow();
                    dro["employee"] = dgvLoading[3, i].Value;
                    dro["operationType"] = dgvLoading[2, i].Value;
                    dro["money"] = dgvLoading[1, i].Value;
                    dro["dateTime"] = dgvLoading[0, i].Value;

                    dw.Tables["dtDepositWithdrawEmployees"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportDepositWithdrawEmployees.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dw.Tables["dtDepositWithdrawEmployees"]));

                ReportParameter[] reportParameters = new ReportParameter[2];
                reportParameters[0] = new ReportParameter("From", dgvLoading[0, 0].Value.ToString());
                reportParameters[1] = new ReportParameter("To", dgvLoading[0, dgvLoading.Rows.Count - 1].Value.ToString());


                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportDepositWithdrawEmployees.rdlc";
                    //MessageBox.Show("path before : " + path);
                    report.ReportPath = path;
                    //MessageBox.Show("path after : " + path);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", dw.Tables["dtDepositWithdrawEmployees"]));
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select EmpWithdrawDeposit.id,employee.name,EmpWithdrawDeposit.OperationType,EmpWithdrawDeposit.money,EmpWithdrawDeposit.dateTime from EmpWithdrawDeposit LEFT JOIN Employee on EmpWithdrawDeposit.EmpId = Employee.id");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select EmpWithdrawDeposit.id,employee.name,EmpWithdrawDeposit.OperationType,EmpWithdrawDeposit.money,EmpWithdrawDeposit.dateTime from EmpWithdrawDeposit LEFT JOIN Employee on EmpWithdrawDeposit.EmpId = Employee.id");
            }
            else
            {
                loadTable("select EmpWithdrawDeposit.id,employee.name,EmpWithdrawDeposit.OperationType,EmpWithdrawDeposit.money,EmpWithdrawDeposit.dateTime from EmpWithdrawDeposit LEFT JOIN Employee on EmpWithdrawDeposit.EmpId = Employee.id where employee.name like '%" + text + "%' " +
                    "or EmpWithdrawDeposit.OperationType like '%" + text + "%'");               
            }
        }

        private void dgvLoading_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLoading.CurrentCell.ColumnIndex.Equals(5) && e.RowIndex != -1)
            {
                string deletedId = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (adoClass.sqlcn.State != ConnectionState.Open)
                        {
                            adoClass.sqlcn.Open();
                        }

                        cmd = new SqlCommand("delete from EmpWithdrawDeposit where id = '" + deletedId + "'", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("تم الحذف بنجاح");

                        loadTable("select EmpWithdrawDeposit.id,employee.name,EmpWithdrawDeposit.OperationType,EmpWithdrawDeposit.money,EmpWithdrawDeposit.dateTime from EmpWithdrawDeposit LEFT JOIN Employee on EmpWithdrawDeposit.EmpId = Employee.id");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        adoClass.sqlcn.Close();
                    }

                }


            }
        }
    }
}
