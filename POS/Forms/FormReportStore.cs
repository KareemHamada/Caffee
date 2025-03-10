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
    public partial class FormReportStore : Form
    {
        public FormReportStore()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        Database db = new Database();
        private void FormReportStore_Load(object sender, EventArgs e)
        {
            loadTable("select Stores.id,Stores.dateTime,Stores.total,Suppliers.name,Users.fullName from Stores LEFT JOIN Users on Stores.userId = Users.id LEFT JOIN Suppliers on Stores.supplierId = Suppliers.id");
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
                    double.TryParse(row["total"].ToString(), out total);
                    FinalTotal += total;
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                            Properties.Resources.dgv,
                            row["fullName"].ToString(),
                            row["dateTime"],
                            row["total"],
                            row["name"],
                            row["id"],
                            Properties.Resources.delete,
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
            loadTable("select Stores.id,Stores.dateTime,Stores.total,Suppliers.name,Users.fullName from Stores LEFT JOIN Users on Stores.userId = Users.id LEFT JOIN Suppliers on Stores.supplierId = Suppliers.id");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                dsStoreOrders storeOrders = new dsStoreOrders();
                for (int i = 0; i < dgvLoading.Rows.Count; i++)
                {
                    DataRow dro = storeOrders.Tables["dtStoreOrders"].NewRow();
                    dro["supplier"] = dgvLoading[4, i].Value;
                    dro["total"] = dgvLoading[3, i].Value;
                    dro["dateTime"] = dgvLoading[2, i].Value;
                    dro["user"] = dgvLoading[1, i].Value;

                    storeOrders.Tables["dtStoreOrders"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportStoreOrders.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", storeOrders.Tables["dtStoreOrders"]));

                ReportParameter[] reportParameters = new ReportParameter[2];
                reportParameters[0] = new ReportParameter("From", dgvLoading[2, 0].Value.ToString());
                reportParameters[1] = new ReportParameter("To", dgvLoading[2, dgvLoading.Rows.Count - 1].Value.ToString());


                if (Properties.Settings.Default.DirectPrint)
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportStoreOrders.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", storeOrders.Tables["dtStoreOrders"]));
                    report.SetParameters(reportParameters);
                    PrintersClass pC = new PrintersClass(Properties.Settings.Default.PrinterName);
                    pC.PrintToPrinter(report);
                }
                else if (Properties.Settings.Default.ShowBeforePrint)
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
                    string storeId = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                    FormReportStoreItems frm = new FormReportStoreItems();
                    frm.Show();
                    frm.showStoreItems(storeId);

                }

            }
            else
            {

                if (dgvLoading.CurrentCell.ColumnIndex.Equals(6) && e.RowIndex != -1)
                {
                    string storeId = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                    if (MessageBox.Show("هل تريد حذف الفاتورة", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {


                        DataTable tblCheck = new DataTable();
                        tblCheck.Clear();
                        tblCheck = db.readData("select * from storeOrderItems where storeId = '" + storeId + "'", "");
                        if(tblCheck.Rows.Count > 0)
                        {
                            for(int x = 0; x < tblCheck.Rows.Count; x++)
                            {
                                DataTable tbl = new DataTable();
                                tbl.Clear();
                                tbl = db.readData("select * from ItemsStoreRelation where ItemStoreID=" + tblCheck.Rows[x][3] + "", "");
                                if (tbl.Rows.Count > 0)
                                {
                                    // add to store item qty
                                    db.readData("update storeItems set Qty-=" + tblCheck.Rows[x][2] + " where id = " + tblCheck.Rows[x][3] + "", "");
                                }
                            }
                            
                        }

                        db.readData("update storeItems set Qty=0 where Qty < 0", "");

                        db.executeData("delete from storeOrderItems where storeId = '" + storeId + "'", "", "");
                        db.executeData("delete from Stores where id = '" + storeId + "'", "تم الحذف بنجاح", "");


                        loadTable("select Stores.id,Stores.dateTime,Stores.total,Suppliers.name,Users.fullName from Stores LEFT JOIN Users on Stores.userId = Users.id LEFT JOIN Suppliers on Stores.supplierId = Suppliers.id");

                    }


                }

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadTable("select Stores.id,Stores.dateTime,Stores.total,Suppliers.name,Users.fullName from Stores LEFT JOIN Users on Stores.userId = Users.id LEFT JOIN Suppliers on Stores.supplierId = Suppliers.id where Stores.dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("select Stores.id,Stores.dateTime,Stores.total,Suppliers.name,Users.fullName from Stores LEFT JOIN Users on Stores.userId = Users.id LEFT JOIN Suppliers on Stores.supplierId = Suppliers.id");
            }
            else
            {
                loadTable("select Stores.id,Stores.dateTime,Stores.total,Suppliers.name,Users.fullName from Stores LEFT JOIN Users on Stores.userId = Users.id LEFT JOIN Suppliers on Stores.supplierId = Suppliers.id where Suppliers.name like '%" + text + "%' or Users.fullName like '%" + text + "%'");
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                if (MessageBox.Show("هل متاكد من حذف الكل", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    adoClass.executeData("delete from storeOrderItems DBCC CHECKIDENT (storeOrderItems,RESEED,0)", "");
                    
                    adoClass.executeData("delete from Stores DBCC CHECKIDENT (Stores,RESEED,0)", "تم الحذف بنجاح");
                    
                    
                    loadTable("select Stores.id,Stores.dateTime,Stores.total,Suppliers.name,Users.fullName from Stores LEFT JOIN Users on Stores.userId = Users.id LEFT JOIN Suppliers on Stores.supplierId = Suppliers.id");
                }
            }
        }
    }
}

