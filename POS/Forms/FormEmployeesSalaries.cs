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
        private DataTable loadTable()
        {
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("Select EmployeesSalaries.dateTime,EmployeesSalaries.salary,Employee.name,EmployeesSalaries.id from EmployeesSalaries LEFT JOIN Employee on EmployeesSalaries.employeeId = Employee.id", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
        }


        private void FormEmployeesSalaries_Load(object sender, EventArgs e)
        {
            Helper.fillComboBox(comboEmployees, "Select id,name from Employee", "name", "id");
            try
            {
                dgvEmployeesSalareis.DataSource = loadTable();
                dgvEmployeesSalareis.Columns[0].HeaderText = "التاريخ";
                dgvEmployeesSalareis.Columns[1].HeaderText = "المرتب";
                dgvEmployeesSalareis.Columns[2].HeaderText = "الموظف";
                dgvEmployeesSalareis.Columns[3].HeaderText = "#";
            }
            catch
            {

            }
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

            dgvEmployeesSalareis.DataSource = loadTable();

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

            dgvEmployeesSalareis.DataSource = loadTable();

            comboEmployees.Text = "";
            txtSalary.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد المرتب المراد حذفة");
                return;
            }
            try
            {

                cmd = new SqlCommand("delete from EmployeesSalaries Where id = '" + id + "'", adoClass.sqlcn);

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

            dgvEmployeesSalareis.DataSource = loadTable();

            comboEmployees.Text = "";
            txtSalary.Text = "";
            txtHidden.Text = "";
        }

     


        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void dgvEmployeesSalareis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvEmployeesSalareis.CurrentRow.Cells[3].Value.ToString();
            comboEmployees.Text = dgvEmployeesSalareis.CurrentRow.Cells[2].Value.ToString();
            txtSalary.Text = dgvEmployeesSalareis.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
