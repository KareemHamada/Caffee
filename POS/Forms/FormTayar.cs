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
    public partial class FormTayar : Form
    {
        public FormTayar()
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
            SqlCommand cmd = new SqlCommand("Select * from Tayar", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
        }

        private void FormTayar_Load(object sender, EventArgs e)
        {
            try
            {
                dgvTayar.DataSource = loadTable();
                dgvTayar.Columns[0].HeaderText = "العنوان";
                dgvTayar.Columns[1].HeaderText = "التليفون";
                dgvTayar.Columns[2].HeaderText = "الاسم";
                dgvTayar.Columns[3].HeaderText = "#";

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

            dgvTayar.DataSource = loadTable();

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

            dgvTayar.DataSource = loadTable();

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
                MessageBox.Show("حدد الطيار المراد حذفة");
                return;
            }
            try
            {

                cmd = new SqlCommand("delete from Tayar Where id = '" + id + "'", adoClass.sqlcn);

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

            dgvTayar.DataSource = loadTable();

            txtName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtHidden.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvTayar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvTayar.CurrentRow.Cells[3].Value.ToString();
            txtName.Text = dgvTayar.CurrentRow.Cells[2].Value.ToString();
            txtPhone.Text = dgvTayar.CurrentRow.Cells[1].Value.ToString();
            txtAddress.Text = dgvTayar.CurrentRow.Cells[0].Value.ToString();
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }


    }
}
