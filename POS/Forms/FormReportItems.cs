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
    public partial class FormReportItems : Form
    {
        public FormReportItems()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private SqlDataReader dr;
        public bool shiftItems = false;
        public int shiftID;
        private void FormReportItems_Load(object sender, EventArgs e)
        {
            if (shiftItems)
            {
                dgvLoading.Columns[0].Visible = true;
            }
            else
            {
                dgvLoading.Columns[0].Visible = false;
            }
            loadTable("select id,name,quantity from Items");
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
                            "",
                            row["quantity"],
                            row["name"],
                            row["id"],
                            }
                        ); ;
                }
            }

        }

      

        public void showShiftItems(int shiftId)
        {
            dgvLoading.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            string query = "select ItemQuantityEndShift.itemId,ItemQuantityEndShift.quan,Items.name,ItemQuantityEndShift.total from ItemQuantityEndShift LEFT JOIN Items on ItemQuantityEndShift.itemId = Items.id where shiftId = '" + shiftId + "'";
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
                            row["total"],
                            row["quan"],
                            row["name"],
                            row["itemId"],
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
            if (shiftItems == false)
            {
                if (dgvLoading.Rows.Count > 0)
                {
                    dsItems items = new dsItems();
                    for (int i = 0; i < dgvLoading.Rows.Count; i++)
                    {
                        DataRow dro = items.Tables["dtItems"].NewRow();
                        dro["item"] = dgvLoading[2, i].Value;
                        dro["count"] = dgvLoading[1, i].Value;

                        items.Tables["dtItems"].Rows.Add(dro);
                    }

                    FormReports rptForm = new FormReports();
                    rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportItems.rdlc";
                    rptForm.mainReport.LocalReport.DataSources.Clear();
                    rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", items.Tables["dtItems"]));


                    if (Properties.Settings.Default.DirectPrint)
                    {
                        LocalReport report = new LocalReport();
                        string path = Application.StartupPath + @"\Reports\ReportItems.rdlc";
                        report.ReportPath = path;
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource("DataSet1", items.Tables["dtItems"]));
                        PrintersClass pC = new PrintersClass(Properties.Settings.Default.PrinterName);
                        pC.PrintToPrinter(report);
                    }
                    else if (Properties.Settings.Default.ShowBeforePrint)
                    {
                        rptForm.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("لا يوجد عناصر لعرضها");
                }
            }
            else
            {
                printChecks checks = new printChecks();
                checks.endShiftItems(shiftID);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            loadTable("select id,name,quantity from Items");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select id,name,quantity from Items");
            }
            else
            {
                loadTable("select id,name,quantity from Items where name like '%" + text + "%'");
            }
        }
    }
}
