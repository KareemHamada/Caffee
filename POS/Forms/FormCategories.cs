﻿using POS.Classes;
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
using POS.Tools;
using Microsoft.Reporting.WinForms;

namespace POS.Forms
{
    public partial class FormCategories : Form
    {
        public FormCategories()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtHidden;

 
        private void loadTable(string query)
        {
            dgvCategories.Rows.Clear();
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

                    dgvCategories.Rows.Add
                        (new object[]
                            {
                            row["image"],
                            row["name"],
                            row["id"],
                            }
                        ); ;
                }
            }

        }


        private void FormCategories_Load(object sender, EventArgs e)
        {
           

            loadTable("Select * from Categories");

            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم الصنف");
                return;
            }

            try
            {
                if (picBox.BackgroundImage != null)
                {
                    cmd = new SqlCommand("Insert into Categories (name,image) values (@name,@image)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                }
                else
                {
                    cmd = new SqlCommand("Insert into Categories (name) values (@name)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                }
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();


                MessageBox.Show("تم اضافة الصنف بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select * from Categories");

            txtName.Text = "";
            picBox.BackgroundImage = null;
            txtImage.Text = "";
            txtHidden.Text = "";

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد الصنف المراد تعديلة");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم الصنف");
                return;
            }


            try
            {

                if (picBox.BackgroundImage != null)
                {
                    cmd = new SqlCommand("Update Categories set name = @name,image = @image Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                }
                else
                {
                    cmd = new SqlCommand("Update Categories set name = @name,image = @image Where id = '" + id + "'", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
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
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select * from Categories");

            txtName.Text = "";
            picBox.BackgroundImage = null;
            txtImage.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCategories.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvCategories.CurrentRow.Cells[2].Value.ToString();
                    if (txtHidden.Text == "")
                    {
                        MessageBox.Show("حدد الصنف المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Categories Where id = '" + txtHidden.Text + "'", adoClass.sqlcn);

                        if (adoClass.sqlcn.State != ConnectionState.Open)
                        {
                            adoClass.sqlcn.Open();
                        }

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("تم الحذف بنجاح");

                    }
                    catch
                    {
                        MessageBox.Show("خطا في الحذف");
                    }
                    finally
                    {
                        adoClass.sqlcn.Close();
                    }

                    loadTable("Select * from Categories");
                    txtName.Text = "";
                    picBox.BackgroundImage = null;
                    txtImage.Text = "";
                    txtHidden.Text = "";
                }
            }
 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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

       

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            picBox.BackgroundImage = null;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("Select * from Categories");
            }
            else
            {
                loadTable("Select * from Categories where name like '%" + text + "%'");

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvCategories.Rows.Count > 0)
            {
                dsShowCategories tbl = new dsShowCategories();
                for (int i = 0; i < dgvCategories.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowCategories"].NewRow();
                    dro["name"] = dgvCategories[1, i].Value;

                    tbl.Tables["dtShowCategories"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShowCategories.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowCategories"]));

                if (Properties.Settings.Default.DirectPrint)
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShowCategories.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowCategories"]));

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

        private void dgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvCategories.CurrentRow.Cells[2].Value.ToString();
            txtName.Text = dgvCategories.CurrentRow.Cells[1].Value.ToString();
            picBox.BackgroundImage = Helper.ByteToImage(dgvCategories.CurrentRow.Cells[0].Value);
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (dgvCategories.Rows.Count > 0)
            {
                if (MessageBox.Show("هل متاكد من حذف الكل سيترتب علي ذلك حذف جميع العناصر و الورديات و مصروفات الورديات و الاوردرات ان وجدت ", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    try
                    {
                        if (adoClass.sqlcn.State != ConnectionState.Open)
                        {
                            adoClass.sqlcn.Open();
                        }


                        cmd = new SqlCommand("delete from OrderItems DBCC CHECKIDENT (OrderItems,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("delete from Orders DBCC CHECKIDENT (Orders,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("delete from ItemQuantityEndShift DBCC CHECKIDENT (ItemQuantityEndShift,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        // delete expenses under shifts
                        cmd = new SqlCommand("select * from Expenses where shiftId IS NOT NULL", adoClass.sqlcn);

                        DataTable dtt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtt);
                        if(dtt.Rows.Count > 0)
                        {
                           for(int i = 0; i < dtt.Rows.Count; i++)
                            {
                                cmd = new SqlCommand("delete from  Expenses where shiftId ="+ dtt.Rows[i][0]+ "", adoClass.sqlcn);
                                cmd.ExecuteNonQuery();

                            }
                        }
                        cmd = new SqlCommand("delete from Shifts DBCC CHECKIDENT (Shifts,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("delete from Items DBCC CHECKIDENT (Items,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("delete from Categories DBCC CHECKIDENT (Categories,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();


                        MessageBox.Show("تم الحذف بنجاح");

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("خطا في الحذف");
                    }
                    finally
                    {
                        adoClass.sqlcn.Close();
                    }

                    loadTable("Select * from Categories");

                    txtName.Text = "";
                    picBox.BackgroundImage = null;
                    txtImage.Text = "";
                    txtHidden.Text = "";
                }
            }
        }
    }
}
