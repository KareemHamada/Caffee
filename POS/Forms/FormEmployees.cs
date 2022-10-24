using POS.Classes;
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
    public partial class FormEmployees : Form
    {
        public FormEmployees()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtHidden;

        private void loadTable(string query)
        {
            dgvEmployees.Rows.Clear();
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

                    dgvEmployees.Rows.Add
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

        private void FormEmployees_Load(object sender, EventArgs e)
        {
            loadTable("Select * from Employee");
            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم الموظف");
                return;
            }

            try
            {
                cmd = new SqlCommand("Insert into Employee (name,phone,address) values (@name,@phone,@address)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);


                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة الموظف بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select * from Employee");

            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد الموظف المراد تعديلة");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم الموظف");
                return;
            }


            try
            {

                cmd = new SqlCommand("Update Employee set name = @name,phone = @phone,address = @address Where id = '" + id + "'", adoClass.sqlcn);

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

            loadTable("Select * from Employee");

            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvEmployees.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvEmployees.CurrentRow.Cells[3].Value.ToString();
                    if (txtHidden.Text == "")
                    {
                        MessageBox.Show("حدد الموظف المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Employee Where id = '" + txtHidden.Text + "'", adoClass.sqlcn);

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

                    loadTable("Select * from Employee");

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
                loadTable("Select * from Employee");

            }
            else
            {
                loadTable("Select * from Employee where name like '%" + text + "%' or phone like '%" + text + "%' " +
                    "or address like '%" + text + "%'");
            }
        }

        private void dgvEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvEmployees.CurrentRow.Cells[3].Value.ToString();
            txtName.Text = dgvEmployees.CurrentRow.Cells[2].Value.ToString();
            txtPhone.Text = dgvEmployees.CurrentRow.Cells[1].Value.ToString();
            txtAddress.Text = dgvEmployees.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.Rows.Count > 0)
            {
                dsShowEmployee tbl = new dsShowEmployee();
                for (int i = 0; i < dgvEmployees.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowEmployee"].NewRow();
                    dro["address"] = dgvEmployees[0, i].Value;
                    dro["phone"] = dgvEmployees[1, i].Value;
                    dro["name"] = dgvEmployees[2, i].Value;

                    tbl.Tables["dtShowEmployee"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShowEmployee.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowEmployee"]));


                if (bool.Parse(declarations.systemOptions["directPrint"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShowEmployee.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowEmployee"]));
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
    }
}
