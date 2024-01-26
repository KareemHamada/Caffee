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
    public partial class FormStoreItems : Form
    {
        public FormStoreItems()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;

        Database db = new Database();
        DataTable tbl = new DataTable();
        DataTable tblItem = new DataTable();
        private void AutoNumber()
        {
            tblItem.Clear();
            tblItem = db.readData("SELECT [id] as 'رقم العنصر',[name] as 'العنصر',[image] as 'الصورة',[Unit] as 'الوحدة',[Qty] as 'الكمية',[LowQty] as 'الحد الادني' FROM [dbo].[storeItems]", "");
            DgvSearch.DataSource = tblItem;
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

            tbl.Clear();
            tbl = db.readData("select max (id) from storeItems", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            picBox.BackgroundImage = null;
            txtImage.Text = "";
            txtName.Clear();
            txtLowQty.Clear();
            txtQty.Clear();
            txtUnit.Clear();
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnUpdate.Enabled = false;
            btnNew.Enabled = true;

        }

        private void FormStoreItems_Load(object sender, EventArgs e)
        {

            try
            {
                AutoNumber();
            }
            catch (Exception)
            {

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم العنصر");
                return;
            }
            if (txtUnit.Text == "")
            {
                MessageBox.Show("من فضلك ادخل وحدة");
                return;
            }
            try
            {
                if (picBox.BackgroundImage != null)
                {
                    cmd = new SqlCommand("Insert into storeItems (name,image,id,Unit,Qty,LowQty) values (@name,@image,@id,@Unit,@Qty,@LowQty)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Unit", txtUnit.Text);
                    cmd.Parameters.AddWithValue("@Qty", txtQty.Text);
                    cmd.Parameters.AddWithValue("@LowQty", txtLowQty.Text);
                    cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                }
                else
                {
                    cmd = new SqlCommand("Insert into storeItems (name,id,Unit,Qty,LowQty) values (@name,@id,@Unit,@Qty,@LowQty)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Unit", txtUnit.Text);
                    cmd.Parameters.AddWithValue("@Qty", txtQty.Text);
                    cmd.Parameters.AddWithValue("@LowQty", txtLowQty.Text);
                }
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();
                MessageBox.Show("تم اضافة العنصر بنجاح");
            }
            catch (Exception ex)
            {
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            AutoNumber();

            
        } 

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم العنصر");
                return;
            }

            try
            {
                if (picBox.BackgroundImage != null)
                {
                    cmd = new SqlCommand("Update storeItems set name = @name,image=@image,Unit=@Unit,Qty=@Qty,LowQty=@LowQty Where id = '" + txtID.Text + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Unit", txtUnit.Text);
                    cmd.Parameters.AddWithValue("@Qty", txtQty.Text);
                    cmd.Parameters.AddWithValue("@LowQty", txtLowQty.Text);
                    cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                }
                else
                {
                    cmd = new SqlCommand("Update storeItems set name = @name,image=@image,Unit=@Unit,Qty=@Qty,LowQty=@LowQty Where id = '" + txtID.Text + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Unit", txtUnit.Text);
                    cmd.Parameters.AddWithValue("@Qty", txtQty.Text);
                    cmd.Parameters.AddWithValue("@LowQty", txtLowQty.Text);
                    cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = DBNull.Value;
                }


                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم التعديل بنجاح");

            }
            catch (Exception ex)
            {
            }
            finally
            {
                adoClass.sqlcn.Close();
            }
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DgvSearch.Rows.Count > 0)
            {
                if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.executeData("delete from storeItems where id=" + txtID.Text + "", "تم مسح البيانات بنجاح", "لا يمكن حذف هذا العنصر قد يكون هذا العنصر متعلق بعمليات اخري عند حذفها يتم حذف هذا العنصر");
                    AutoNumber();
                }

                
            }
        }

       

        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Filter = "Images|*.png";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtImage.Text = fileDialog.FileName;
                picBox.BackgroundImage = new Bitmap(txtImage.Text);
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            picBox.BackgroundImage = null;
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
                    dro["Unit"] = DgvSearch[3, i].Value;
                    dro["Qty"] = DgvSearch[4, i].Value;
                    dro["LowQty"] = DgvSearch[5, i].Value;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                AutoNumber();
            }
            else
            {
                //loadTable("Select * from storeItems where name like '%" + text + "%'");
                tblItem.Clear();
                tblItem = db.readData("SELECT [id] as 'رقم العنصر',[name] as 'العنصر',[image] as 'الصورة',[Unit] as 'الوحدة',[Qty] as 'الكمية',[LowQty] as 'الحد الادني' FROM [dbo].[storeItems] where name like '%" + text + "%'", "");
                DgvSearch.DataSource = tblItem;
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            
            if (DgvSearch.Rows.Count > 0)
            {
                if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.executeData("delete from storeItems ", "تم مسح البيانات بنجاح", "لا يمكن حذف جميع العناصر قد يكون هناك عنصر متعلق بعمليات اخري عند حذفها يتم حذف هذا العنصر");
                    AutoNumber();
                }
            }
        }

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    DataTable tblShow = new DataTable();
                    tblShow.Clear();
                    tblShow = db.readData("select * from storeItems where id=" + DgvSearch.CurrentRow.Cells[0].Value + "", "");

                    txtID.Text = tblShow.Rows[0][0].ToString();
                    txtName.Text = tblShow.Rows[0][1].ToString();
                    picBox.BackgroundImage = Helper.ByteToImage(tblShow.Rows[0][2]);
                    txtUnit.Text = tblShow.Rows[0][3].ToString();
                    txtQty.Text = tblShow.Rows[0][4].ToString();
                    txtLowQty.Text = tblShow.Rows[0][5].ToString();

                    btnAdd.Enabled = false;
                    btnDelete.Enabled = true;
                    btnDeleteAll.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnNew.Enabled = true;

                }

            }
            catch (Exception) { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtLowQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
