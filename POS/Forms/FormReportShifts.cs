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
    public partial class FormReportShifts : Form
    {
        public FormReportShifts()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        
        private void FormReportShifts_Load(object sender, EventArgs e)
        {
            loadTable("select Shifts.total,Shifts.expenses,Shifts.wared,Shifts.dateTimeEnd,Shifts.dateTimeStart,Users.fullName,Shifts.id from Shifts LEFT JOIN Users on Shifts.userId = Users.id where Shifts.dateTimeEnd != ''");
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
            if(dt.Rows.Count > 0)
            {
                
                foreach (DataRow row in dt.Rows)
                {
                    double.TryParse(row["total"].ToString(),out total);
                    FinalTotal += total;
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                            Properties.Resources.dgv,
                            Properties.Resources.expenses,
                            Properties.Resources.dgv,
                            row["total"].ToString(),
                            row["expenses"],
                            row["wared"],
                            row["dateTimeEnd"],
                            row["dateTimeStart"],
                            row["fullName"],
                            row["id"]
                            }
                        ); ;
                }
            }

            lblTotal.Text = FinalTotal.ToString();
        }

       

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadTable("select Shifts.total,Shifts.expenses,Shifts.wared,Shifts.dateTimeEnd,Shifts.dateTimeStart,Users.fullName,Shifts.id from Shifts LEFT JOIN Users on Shifts.userId = Users.id where dateTimeStart between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'");
        }

        private void dgvLoading_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLoading.CurrentCell.ColumnIndex.Equals(2) && e.RowIndex != -1)
            {
                if (dgvLoading.CurrentCell != null && dgvLoading.CurrentCell.Value != null)
                {
                    string shiftId = dgvLoading.CurrentRow.Cells[9].Value.ToString();
                    FormReportOrders frm = new FormReportOrders();
                    frm.Show();
                    frm.showShiftOrders(shiftId);

                }
            }


            if (dgvLoading.CurrentCell.ColumnIndex.Equals(1) && e.RowIndex != -1)
            {
                if (dgvLoading.CurrentCell != null && dgvLoading.CurrentCell.Value != null)
                {
                    string shiftId = dgvLoading.CurrentRow.Cells[9].Value.ToString();
                    FormReportExpenses frm = new FormReportExpenses();
                    frm.Show();
                    frm.showShiftExpenses(shiftId);
                }
            }

            if (dgvLoading.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
            {
                if (dgvLoading.CurrentCell != null && dgvLoading.CurrentCell.Value != null)
                {
                    string shiftId = dgvLoading.CurrentRow.Cells[9].Value.ToString();
                    FormReportItems frm = new FormReportItems();
                    frm.btnReload.Visible = false;
                    frm.txtSearch.Visible = false;
                    frm.Show();
                    frm.showShiftItems(shiftId);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select Shifts.total,Shifts.expenses,Shifts.wared,Shifts.dateTimeEnd,Shifts.dateTimeStart,Users.fullName,Shifts.id from Shifts LEFT JOIN Users on Shifts.userId = Users.id where Shifts.dateTimeEnd != ''");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsShifts shifts = new dsShifts();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = shifts.Tables["dtShifts"].NewRow();
                    dro["user"] = dgvLoading[8, i].Value;
                    dro["dateTimeStart"] = dgvLoading[7, i].Value;
                    dro["dateTimeEnd"] = dgvLoading[6, i].Value;
                    dro["wared"] = dgvLoading[5, i].Value;
                    dro["expenses"] = dgvLoading[4, i].Value;
                    dro["total"] = dgvLoading[3, i].Value;


                    shifts.Tables["dtShifts"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShifts.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", shifts.Tables["dtShifts"]));

                ReportParameter[] reportParameters = new ReportParameter[2];
                reportParameters[0] = new ReportParameter("From", dgvLoading[7, 0].Value.ToString());
                reportParameters[1] = new ReportParameter("To", dgvLoading[7, dgvLoading.Rows.Count - 1].Value.ToString());


                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShifts.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", shifts.Tables["dtShifts"]));
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select Shifts.total,Shifts.expenses,Shifts.wared,Shifts.dateTimeEnd,Shifts.dateTimeStart,Users.fullName,Shifts.id from Shifts LEFT JOIN Users on Shifts.userId = Users.id where Shifts.dateTimeEnd != ''");
            }
            else
            {
                loadTable("select Shifts.total,Shifts.expenses,Shifts.wared,Shifts.dateTimeEnd,Shifts.dateTimeStart,Users.fullName,Shifts.id from Shifts LEFT JOIN Users on Shifts.userId = Users.id where Shifts.dateTimeEnd != '' and Users.fullName like '%" + text + "%'");
            }
        }
    }
}
