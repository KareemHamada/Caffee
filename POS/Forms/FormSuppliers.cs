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
            try
            {
                dgvSuppliers.DataSource = loadTable();
                dgvSuppliers.Columns[0].HeaderText = "التليفون";
                dgvSuppliers.Columns[1].HeaderText = "المورد";
                dgvSuppliers.Columns[2].HeaderText = "#";

            }
            catch
            {

            }
            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

       

        private DataTable loadTable()
        {
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand("Select * from Suppliers", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
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

            dgvSuppliers.DataSource = loadTable();

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

            dgvSuppliers.DataSource = loadTable();

            txtName.Text = "";
            txtPhone.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد المورد المراد حذفه");
                return;
            }
            try
            {

                cmd = new SqlCommand("delete from Suppliers Where id = '" + id + "'", adoClass.sqlcn);

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

            dgvSuppliers.DataSource = loadTable();

            txtName.Text = "";
            txtPhone.Text = "";
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

        private void dgvSuppliers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvSuppliers.CurrentRow.Cells[2].Value.ToString();
            txtName.Text = dgvSuppliers.CurrentRow.Cells[1].Value.ToString();
            txtPhone.Text = dgvSuppliers.CurrentRow.Cells[0].Value.ToString();
        }


        
    }
}
