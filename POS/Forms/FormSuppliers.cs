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
    public partial class FormSuppliers : Form
    {
        public FormSuppliers()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtHidden;

        private void FormSuppliers_Load(object sender, EventArgs e)
        {
            loadTable("Select * from Suppliers");
            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

        private void loadTable(string query)
        {
            dgvSuppliers.Rows.Clear();
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

                    dgvSuppliers.Rows.Add
                        (new object[]
                            {
                            row["phone"],
                            row["name"],
                            row["id"],
                            }
                        ); ;
                }
            }

        }

       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم المورد");
                return;
            }

            try
            {

                cmd = new SqlCommand("Insert into Suppliers (name,phone) values (@name,@phone)", adoClass.sqlcn);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();


                MessageBox.Show("تم اضافة المورد بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select * from Suppliers");

            txtName.Text = "";
            txtPhone.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد المورد المراد تعديله");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم المورد");
                return;
            }


            try
            {
                // Clients (name,phone,regionId,address)
                cmd = new SqlCommand("Update Suppliers set name = @name, phone=@phone Where id = '" + id + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);

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

            loadTable("Select * from Suppliers");

            txtName.Text = "";
            txtPhone.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvSuppliers.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvSuppliers.CurrentRow.Cells[2].Value.ToString();
                    if (txtHidden.Text == "")
                    {
                        MessageBox.Show("حدد المورد المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Suppliers Where id = '" + txtHidden.Text + "'", adoClass.sqlcn);

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

                    loadTable("Select * from Suppliers");

                    txtName.Text = "";
                    txtPhone.Text = "";
                    txtHidden.Text = "";
                }
            }

           
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void dgvSuppliers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvSuppliers.CurrentRow.Cells[2].Value.ToString();
            txtName.Text = dgvSuppliers.CurrentRow.Cells[1].Value.ToString();
            txtPhone.Text = dgvSuppliers.CurrentRow.Cells[0].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("Select * from Suppliers");
            }
            else
            {
                loadTable("Select * from Suppliers where name like '%" + text + "%' or phone like '%" + text + "%'");


           
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.Rows.Count > 0)
            {
                dsShowSuppliers tbl = new dsShowSuppliers();
                for (int i = 0; i < dgvSuppliers.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowSuppliers"].NewRow();
                    dro["phone"] = dgvSuppliers[0, i].Value;
                    dro["name"] = dgvSuppliers[1, i].Value;
                   

                    tbl.Tables["dtShowSuppliers"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShowSuppliers.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowSuppliers"]));


                if (Properties.Settings.Default.DirectPrint)
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShowSuppliers.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowSuppliers"]));
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

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.Rows.Count > 0)
            {
                if (MessageBox.Show("هل متاكد من حذف الكل", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    try
                    {

                        cmd = new SqlCommand("delete from Suppliers DBCC CHECKIDENT (Suppliers,RESEED,0)", adoClass.sqlcn);

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

                    loadTable("Select * from Suppliers");

                    txtName.Text = "";
                    txtPhone.Text = "";
                    txtHidden.Text = "";
                }
            }
        }
    }
}
