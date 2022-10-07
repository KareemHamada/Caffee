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
    public partial class FormAdminExpenses : Form
    {
        public FormAdminExpenses()
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
            cmd = new SqlCommand("Select Expenses.shiftId,Users.fullName,Expenses.dateTime,Expenses.price,Expenses.name,Expenses.id from Expenses LEFT JOIN Users on Expenses.userId = Users.id where shiftId IS NULL", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
        }
        
       

        private void FormAdminExpenses_Load(object sender, EventArgs e)
        {
            try
            {
                dgvExpenses.DataSource = loadTable();
                dgvExpenses.Columns[0].HeaderText = "رقم الشيفت";
                dgvExpenses.Columns[1].HeaderText = "المستخدم";
                dgvExpenses.Columns[2].HeaderText = "التاريخ";
                dgvExpenses.Columns[3].HeaderText = "السعر";
                dgvExpenses.Columns[4].HeaderText = "المصروف";
                dgvExpenses.Columns[5].HeaderText = "#";
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
                MessageBox.Show("ادخل اسم العنصر");
                return;
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("ادخل سعر العنصر");
                return;
            }

            try
            {
                cmd = new SqlCommand("Insert into Expenses (name,price,dateTime,userId) values (@name,@price,@dateTime,@userId)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@userId", declarations.userid);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة المصروف بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            dgvExpenses.DataSource = loadTable();

            txtName.Text = "";
            txtPrice.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد العنصر المراد تعديله");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم العنصر");
                return;
            }
            if (txtPrice.Text == "")
            {
                MessageBox.Show("ادخل سعر العنصر");
                return;
            }


            try
            {

                cmd = new SqlCommand("Update Expenses set name = @name,price = @price Where id = '" + id + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);

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

            dgvExpenses.DataSource = loadTable();

            txtName.Text = "";
            txtPrice.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد المصروف المراد حذفة");
                return;
            }
            try
            {

                cmd = new SqlCommand("delete from Expenses Where id = '" + id + "'", adoClass.sqlcn);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم الحذف بنجاح");

            }
            catch
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("خطا في الحذف");
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            dgvExpenses.DataSource = loadTable();

            txtName.Text = "";
            txtPrice.Text = "";
            txtHidden.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }




        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void dgvExpenses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvExpenses.CurrentRow.Cells[5].Value.ToString();
            txtName.Text = dgvExpenses.CurrentRow.Cells[4].Value.ToString();
            txtPrice.Text = dgvExpenses.CurrentRow.Cells[3].Value.ToString();
        }

    }
}
