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
    public partial class FormTayar : Form
    {
        public FormTayar()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtHidden;


        private void loadTable(string query)
        {
            dgvTayar.Rows.Clear();
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

                    dgvTayar.Rows.Add
                        (new object[]
                            {
                            row["address"],
                            row["phone"],
                            row["name"],
                            row["id"],
                            }
                        ); ;
                }
            }

        }


        private void FormTayar_Load(object sender, EventArgs e)
        {
        

            loadTable("Select * from Tayar");
            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم الطيار");
                return;
            }

            try
            {
                cmd = new SqlCommand("Insert into Tayar (name,phone,address) values (@name,@phone,@address)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);


                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة الطيار بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select * from Tayar");

            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            //MessageBox.Show("Text box hidden :  " + txtHidden.Text);
            if (id == "")
            {
                MessageBox.Show("حدد الطيار المراد تعديلة");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم الطيار");
                return;
            }


            try
            {

                cmd = new SqlCommand("Update Tayar set name = @name,phone = @phone,address = @address Where id = '" + id + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);

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

            loadTable("Select * from Tayar");

            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTayar.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvTayar.CurrentRow.Cells[3].Value.ToString();
                    if (txtHidden.Text == "")
                    {
                        MessageBox.Show("حدد الطيار المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Tayar Where id = '" + txtHidden.Text + "'", adoClass.sqlcn);

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

                    loadTable("Select * from Tayar");

                    txtName.Text = "";
                    txtPhone.Text = "";
                    txtAddress.Text = "";
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("Select * from Tayar");
            }
            else
            {
                loadTable("Select * from Tayar where name like '%" + text + "%' or " +
                    "phone like '%" + text + "%' " +
                    "or address like '%" + text + "%' ");

                

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvTayar.Rows.Count > 0)
            {
                dsShowTayar tbl = new dsShowTayar();
                for (int i = 0; i < dgvTayar.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowTayar"].NewRow();
                    dro["address"] = dgvTayar[0, i].Value;
                    dro["phone"] = dgvTayar[1, i].Value;
                    dro["name"] = dgvTayar[2, i].Value;

                    tbl.Tables["dtShowTayar"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShowTayar.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowTayar"]));

                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShowTayar.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowTayar"]));
                    PrintersClass.PrintToPrinter(report);
                }
                else if (bool.Parse(declarations.systemOptions["showBeforePrint"].ToString()))
                {
                    rptForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("لا يوجد عناصر لعرضها");
            }
        }

        private void dgvTayar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvTayar.CurrentRow.Cells[3].Value.ToString();
            txtName.Text = dgvTayar.CurrentRow.Cells[2].Value.ToString();
            txtPhone.Text = dgvTayar.CurrentRow.Cells[1].Value.ToString();
            txtAddress.Text = dgvTayar.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
