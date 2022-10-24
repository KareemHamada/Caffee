﻿using System;
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
    public partial class FormReportSupplierCashWithdraw : Form
    {
        public FormReportSupplierCashWithdraw()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private void FormReportSupplierCashWithdraw_Load(object sender, EventArgs e)
        {
            loadTable("select SupplierCashWithdraw.id,Suppliers.name,SupplierCashWithdraw.operationType,SupplierCashWithdraw.money,SupplierCashWithdraw.dateTime from SupplierCashWithdraw LEFT JOIN Suppliers on SupplierCashWithdraw.supplierId = Suppliers.id");
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
                dsSupplierCashWithdraw cw = new dsSupplierCashWithdraw();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = cw.Tables["dtSupplierCashWithdraw"].NewRow();
                    dro["supplier"] = dgvLoading[3, i].Value;
                    dro["operationType"] = dgvLoading[2, i].Value;
                    dro["money"] = dgvLoading[1, i].Value;
                    dro["dateTime"] = dgvLoading[0, i].Value;

                    cw.Tables["dtSupplierCashWithdraw"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportSupplierCashWithdraw.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", cw.Tables["dtSupplierCashWithdraw"]));

                ReportParameter[] reportParameters = new ReportParameter[2];
                reportParameters[0] = new ReportParameter("From", dgvLoading[0, 0].Value.ToString());
                reportParameters[1] = new ReportParameter("To", dgvLoading[0, dgvLoading.Rows.Count - 1].Value.ToString());


                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportSupplierCashWithdraw.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", cw.Tables["dtSupplierCashWithdraw"]));
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
            loadTable("select SupplierCashWithdraw.id,Suppliers.name,SupplierCashWithdraw.operationType,SupplierCashWithdraw.money,SupplierCashWithdraw.dateTime from SupplierCashWithdraw LEFT JOIN Suppliers on SupplierCashWithdraw.supplierId = Suppliers.id where dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'");
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select SupplierCashWithdraw.id,Suppliers.name,SupplierCashWithdraw.operationType,SupplierCashWithdraw.money,SupplierCashWithdraw.dateTime from SupplierCashWithdraw LEFT JOIN Suppliers on SupplierCashWithdraw.supplierId = Suppliers.id");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select SupplierCashWithdraw.id,Suppliers.name,SupplierCashWithdraw.operationType,SupplierCashWithdraw.money,SupplierCashWithdraw.dateTime from SupplierCashWithdraw LEFT JOIN Suppliers on SupplierCashWithdraw.supplierId = Suppliers.id");
            }
            else
            {
                loadTable("select SupplierCashWithdraw.id,Suppliers.name,SupplierCashWithdraw.operationType,SupplierCashWithdraw.money,SupplierCashWithdraw.dateTime from SupplierCashWithdraw LEFT JOIN Suppliers on SupplierCashWithdraw.supplierId = Suppliers.id where Suppliers.name like '%" + text + "%' or SupplierCashWithdraw.operationType like '%" + text + "%' ");
            }
        }
    }
}
