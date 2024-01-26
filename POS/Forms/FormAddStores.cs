using Microsoft.Reporting.WinForms;
using POS.Classes;
using POS.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Forms
{
    public partial class FormAddStores : Form
    {
        public FormAddStores()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private TextBox txtHidden;
        private string storesId;
        private Database db = new Database();


        private void FormAddStores_Load(object sender, EventArgs e)
        {
            Helper.fillComboBox(comboSuppliers, "Select id,name from Suppliers", "name", "id");
            fillItems("Select * from storeItems");

            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }



        private void fillItems(string query)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(query, adoClass.sqlcn);

            da.Fill(dt);
            DataRow[] drs = dt.Select();
            pnlItems.Controls.Clear();
            Button catBtn;
            for (int i = 0; i < drs.Length; i++)
            {
                catBtn = new Button();
                catBtn.AccessibleName = "IT";
                catBtn.AccessibleDescription = drs[i]["id"].ToString();
                catBtn.Name = "btnCat" + drs[i]["id"].ToString();

                catBtn.Text = drs[i]["name"].ToString();
                if (Helper.ByteToImage(drs[i]["image"]) == null)
                {
                    catBtn.BackColor = Color.Cyan;
                }
                else
                {
                    catBtn.BackColor = Color.White;
                }

                catBtn.Image = Helper.ByteToImage(drs[i]["image"]);
                catBtn.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                catBtn.TextAlign = ContentAlignment.MiddleCenter;
                catBtn.ImageAlign = ContentAlignment.MiddleCenter;
                catBtn.RightToLeft = RightToLeft.Yes;
                catBtn.TextImageRelation = TextImageRelation.ImageAboveText;
                catBtn.Size = new Size(150, 150);
                catBtn.FlatStyle = FlatStyle.Flat;
                catBtn.FlatAppearance.BorderSize = 0;
                catBtn.Click += cBtn_Click;
                pnlItems.Controls.Add(catBtn);

            }

        }
        private void cBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            txtHidden.Text = button.AccessibleDescription;
            txtItem.Text = button.Text;
            
        }


        private void CalcCheck()
        {
            double x = 0;
            double result = 0;
            for (int i = 0; i < dgvItems.Rows.Count; i++)
            {
                double.TryParse(dgvItems[price.Index, i].Value.ToString(), out x);
                result += x;
            }

            txtTotal.Text = result.ToString();
        }

        private void addFunction()
        {
            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }

            string query = "Insert into Stores (supplierId,total,dateTime,userId) values (@supplierId,@total,@dateTime,@userId); ";
            query += "SELECT @storesId = SCOPE_IDENTITY(); ";

            SqlCommand cmd = new SqlCommand(query, adoClass.sqlcn);
            cmd.Parameters.Add("@storesId", SqlDbType.Int);

            cmd.Parameters.AddWithValue("@total", txtTotal.Text);
            cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@userId", declarations.userid);
            if (comboSuppliers.Text != "")
            {
                cmd.Parameters.AddWithValue("@supplierId", comboSuppliers.SelectedValue);
            }
            else
            {
                cmd.Parameters.AddWithValue("@supplierId", DBNull.Value);
            }


            cmd.Parameters["@storesId"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            storesId = cmd.Parameters["@storesId"].Value.ToString();
            saveItems(storesId);

            MessageBox.Show("تم الحفظ بنجاح");
            
            txtTotal.Text = "";
        }
        private void saveItems(string id)
        {
            for (int i = 0; i < dgvItems.Rows.Count; i++)
            {

                cmd = new SqlCommand("Insert into storeOrderItems (price,quantity,storeItemId,storeId) values (@price,@quantity,@storeItemId,@storeId)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@storeId", id);
                cmd.Parameters.AddWithValue("@storeItemId", dgvItems[0, i].Value);
                cmd.Parameters.AddWithValue("@price", dgvItems[1, i].Value);
                cmd.Parameters.AddWithValue("@quantity", dgvItems[2, i].Value);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();


                // check if it has relation with item sale
                DataTable tbl = new DataTable();
                tbl.Clear();
                tbl = db.readData("select * from ItemsStoreRelation where ItemStoreID=" + dgvItems[0, i].Value + "", "");
                if(tbl.Rows.Count > 0)
                {
                    // add to store item qty
                    db.readData("update storeItems set Qty+=" + dgvItems[2, i].Value + " where id = "+ dgvItems[0, i].Value + "", "");
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (txtItem.Text == "")
            {
                MessageBox.Show("اختار العنصر");
                return;
            }

            if (txtPrice.Text == "" || txtPrice.Text == "0")
            {
                MessageBox.Show("ادخل السعر");
                return;
            }
            if (txtQuantity.Text == "" || txtQuantity.Text == "0")
            {
                MessageBox.Show("من فضلك ادخل كمية ");
                return;
            }

            dgvItems.Rows.Add(new object[]{
                    txtHidden.Text,
                    txtPrice.Text,
                    txtQuantity.Text,
                    txtItem.Text,
                    Properties.Resources.delete,
                }
                );

            txtHidden.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            txtItem.Text = "";


            CalcCheck();
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                if (dgvItems.CurrentCell.ColumnIndex.Equals(4) && e.RowIndex != -1)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dgvItems.Rows.Remove(dgvItems.CurrentRow);
                        CalcCheck();
                    }
                }
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد طباعة فاتورة", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        addFunction();
                        dsAddStore storeItems = new dsAddStore();
                        for (int i = 0; i < dgvItems.Rows.Count; i++)
                        {
                            DataRow dro = storeItems.Tables["dtAddStore"].NewRow();
                            dro["item"] = dgvItems[3, i].Value;
                            dro["quantity"] = dgvItems[2, i].Value;
                            dro["price"] = dgvItems[1, i].Value;

                            storeItems.Tables["dtAddStore"].Rows.Add(dro);
                        }

                        FormReports rptForm = new FormReports();
                        rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportAddStore.rdlc";
                        rptForm.mainReport.LocalReport.DataSources.Clear();
                        rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", storeItems.Tables["dtAddStore"]));

                        ReportParameter[] reportParameters = new ReportParameter[2];
                        reportParameters[0] = new ReportParameter("dateTime", DateTime.Now.ToString());
                        reportParameters[1] = new ReportParameter("supplier", comboSuppliers.Text);

                        if (Properties.Settings.Default.DirectPrint)
                        {
                            LocalReport report = new LocalReport();
                            string path = Application.StartupPath + @"\Reports\ReportAddStore.rdlc";
                            report.ReportPath = path;
                            report.DataSources.Clear();
                            report.DataSources.Add(new ReportDataSource("DataSet1", storeItems.Tables["dtAddStore"]));
                            report.SetParameters(reportParameters);
                            PrintersClass pC = new PrintersClass(Properties.Settings.Default.PrinterName);
                            pC.PrintToPrinter(report);
                        }
                        else if (Properties.Settings.Default.ShowBeforePrint)
                        {
                            rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                            rptForm.ShowDialog();
                        }


                        dgvItems.Rows.Clear();

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
                else
                {
                    try
                    {
                        addFunction();
                        dgvItems.Rows.Clear();
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
            else
            {
                MessageBox.Show("لا يوجد عناصر لاضافتها");
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
                fillItems("Select * from storeItems");
            }
            else
            {
                fillItems("Select * from storeItems where name like '%" + text + "%'");

            }
        }
    }







}
