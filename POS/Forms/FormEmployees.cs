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
    public partial class FormEmployees : Form
    {
        public FormEmployees()
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
            cmd = new SqlCommand("Select * from Employee", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
        }

        

        private void FormEmployees_Load(object sender, EventArgs e)
        {
            try
            {
                dgvEmployees.DataSource = loadTable();
                dgvEmployees.Columns[0].HeaderText = "العنوان";
                dgvEmployees.Columns[1].HeaderText = "التليفون";
                dgvEmployees.Columns[2].HeaderText = "الاسم";
                dgvEmployees.Columns[3].HeaderText = "#";

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

            dgvEmployees.DataSource = loadTable();

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

            dgvEmployees.DataSource = loadTable();

            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد الموظف المراد حذفة");
                return;
            }
            try
            {

                cmd = new SqlCommand("delete from Employee Where id = '" + id + "'", adoClass.sqlcn);

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

            dgvEmployees.DataSource = loadTable();

            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtHidden.Text = "";
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

        private void dgvEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvEmployees.CurrentRow.Cells[3].Value.ToString();
            txtName.Text = dgvEmployees.CurrentRow.Cells[2].Value.ToString();
            txtPhone.Text = dgvEmployees.CurrentRow.Cells[1].Value.ToString();
            txtAddress.Text = dgvEmployees.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
