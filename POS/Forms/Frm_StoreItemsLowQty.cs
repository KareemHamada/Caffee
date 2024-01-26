using Microsoft.Reporting.WinForms;
using POS.Classes;
using POS.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Forms
{
    public partial class Frm_StoreItemsLowQty : Form
    {
        public Frm_StoreItemsLowQty()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        DataTable tblItem = new DataTable();
        private void AutoNumber()
        {
            tblItem.Clear();
            tblItem = db.readData("select DISTINCT ItemStoreID from ItemsStoreRelation", "");
            if(tblItem.Rows.Count > 0)
            {
                for(int i = 0; i < tblItem.Rows.Count; i++)
                {
                    tbl.Clear();
                    tbl = db.readData("SELECT [id] as 'رقم العنصر',[name] as 'العنصر',[Unit] as 'الوحدة',[Qty] as 'الكمية',[LowQty] as 'الحد الادني' FROM [dbo].[storeItems] where id = "+tblItem.Rows[i][0]+"", "");

                    DgvSearch.Rows.Add(1);
                    int indexrow = DgvSearch.Rows.Count - 1;
                    DgvSearch.Rows[indexrow].Cells[0].Value = tbl.Rows[0][0];
                    DgvSearch.Rows[indexrow].Cells[1].Value = tbl.Rows[0][1];
                    DgvSearch.Rows[indexrow].Cells[2].Value = tbl.Rows[0][2];
                    DgvSearch.Rows[indexrow].Cells[3].Value = tbl.Rows[0][3];
                    DgvSearch.Rows[indexrow].Cells[4].Value = tbl.Rows[0][4];
                }


                foreach (DataGridViewRow s in DgvSearch.Rows)
                {
                    if (Convert.ToInt32(s.Cells[4].Value) >= 1)
                    {
                        if (Convert.ToInt32(s.Cells[3].Value) <= Convert.ToInt32(s.Cells[4].Value))
                        {
                            s.DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }


            //DgvSearch.DataSource = tblItem;
            //foreach (DataGridViewRow s in DgvSearch.Rows)
            //{
            //    if (Convert.ToInt32(s.Cells[5].Value) >= 1)
            //    {
            //        if (Convert.ToInt32(s.Cells[4].Value) <= Convert.ToInt32(s.Cells[5].Value))
            //        {
            //            s.DefaultCellStyle.BackColor = Color.Red;
            //        }
            //    }
            //}

            //tbl.Clear();
            //tbl = db.readData("select max (id) from storeItems", "");

            //if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            //{

            //    txtID.Text = "1";
            //}
            //else
            //{

            //    txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            //}
        }
        private void Frm_StoreItemsLowQty_Load(object sender, EventArgs e)
        {
            try
            {
                AutoNumber();
            }
            catch (Exception ex)
            {
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count > 0)
            {
                dsShowStoreItems tbl = new dsShowStoreItems();
                for (int i = 0; i < DgvSearch.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowStoreItems"].NewRow();
                    dro["name"] = DgvSearch[1, i].Value;
                    dro["Unit"] = DgvSearch[2, i].Value;
                    dro["Qty"] = DgvSearch[3, i].Value;
                    dro["LowQty"] = DgvSearch[4, i].Value;
                    tbl.Tables["dtShowStoreItems"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShowStoreItems.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowStoreItems"]));


                if (Properties.Settings.Default.DirectPrint)
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShowStoreItems.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowStoreItems"]));
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
