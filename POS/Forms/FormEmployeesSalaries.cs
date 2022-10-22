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
    public partial class FormEmployeesSalaries : Form
    {
        public FormEmployeesSalaries()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private TextBox txtHidden;


        private void loadTable(string query)
        {
            dgvEmployeesSalareis.Rows.Clear();
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

                    dgvEmployeesSalareis.Rows.Add
                        (new object[]
                            {
                            row["dateTime"],
                            row["salary"],
                            row["name"],
                            row["id"],
                            }
                        ); ;
                }
            }

        }


        private void FormEmployeesSalaries_Load(object sender, EventArgs e)
        {
            Helper.fillComboBox(comboEmployees, "Select id,name from Employee", "name", "id");
            

            loadTable("Select EmployeesSalaries.dateTime,EmployeesSalaries.salary,Employee.name,EmployeesSalaries.id from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id");
            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (comboEmployees.Text == "")
            {
                MessageBox.Show("اختار اسم الموظف");
                return;
            }
            if (txtSalary.Text == "")
            {
                MessageBox.Show("ادخل المرتب");
                return;
            }

            try
            {
                cmd = new SqlCommand("Insert into EmployeesSalaries (dateTime,salary,employeeId) values (@dateTime,@salary,@employeeId)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@salary", txtSalary.Text);
                cmd.Parameters.AddWithValue("@employeeId", comboEmployees.SelectedValue);


                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة مرتب الموظف بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select EmployeesSalaries.dateTime,EmployeesSalaries.salary,Employee.name,EmployeesSalaries.id from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id");

            comboEmployees.Text = "";
            txtSalary.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد المرتب المراد تعديله");
                return;
            }
            if (comboEmployees.Text == "")
            {
                MessageBox.Show("اختار اسم الموظف");
                return;
            }
            if (txtSalary.Text == "")
            {
                MessageBox.Show("ادخل المرتب");
                return;
            }


            try
            {

                cmd = new SqlCommand("Update EmployeesSalaries set employeeId = @employeeId,salary = @salary Where id = '" + id + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@employeeId", comboEmployees.SelectedValue);
                cmd.Parameters.AddWithValue("@salary", txtSalary.Text);

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

            loadTable("Select EmployeesSalaries.dateTime,EmployeesSalaries.salary,Employee.name,EmployeesSalaries.id from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id");

            comboEmployees.Text = "";
            txtSalary.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployeesSalareis.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvEmployeesSalareis.CurrentRow.Cells[3].Value.ToString();
                    if (txtHidden.Text == "")
                    {
                        MessageBox.Show("حدد المرتب المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from EmployeesSalaries Where id = '" + txtHidden.Text + "'", adoClass.sqlcn);

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

                    loadTable("Select EmployeesSalaries.dateTime,EmployeesSalaries.salary,Employee.name,EmployeesSalaries.id from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id");

                    comboEmployees.Text = "";
                    txtSalary.Text = "";
                    txtHidden.Text = "";
                }
            }
        }

     


        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

    

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvEmployeesSalareis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvEmployeesSalareis.CurrentRow.Cells[3].Value.ToString();
            comboEmployees.Text = dgvEmployeesSalareis.CurrentRow.Cells[2].Value.ToString();
            txtSalary.Text = dgvEmployeesSalareis.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvEmployeesSalareis.Rows.Count > 0)
            {
                dsShowEmployeeSalaries tbl = new dsShowEmployeeSalaries();
                for (int i = 0; i < dgvEmployeesSalareis.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowEmployeeSalaries"].NewRow();
                    dro["dateTime"] = dgvEmployeesSalareis[0, i].Value;
                    dro["salary"] = dgvEmployeesSalareis[1, i].Value;
                    dro["name"] = dgvEmployeesSalareis[2, i].Value;

                    tbl.Tables["dtShowEmployeeSalaries"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShowEmployeeSalaries.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowEmployeeSalaries"]));


                if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShowEmployeeSalaries.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowEmployeeSalaries"]));
                    PrintersClass.PrintToPrinter(report);
                }
                else
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
                loadTable("Select EmployeesSalaries.dateTime,EmployeesSalaries.salary,Employee.name,EmployeesSalaries.id from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id");

            }
            else
            {
                loadTable("Select EmployeesSalaries.dateTime,EmployeesSalaries.salary,Employee.name,EmployeesSalaries.id from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id  where Employee.name like '%" + text + "%'");
            }
        }
    }
}
