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
    public partial class FormRegions : Form
    {
        public FormRegions()
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
            cmd = new SqlCommand("Select * from Regions", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
        }

        

        private void FormRegions_Load(object sender, EventArgs e)
        {
            try
            {
                dgvRegions.DataSource = loadTable();
                dgvRegions.Columns[0].HeaderText = "اسم المنطقة";
                dgvRegions.Columns[1].HeaderText = "#";

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
                MessageBox.Show("ادخل اسم المنطقة");
                return;
            }

            try
            {
                cmd = new SqlCommand("Insert into Regions (name) values (@name)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة المنطقة بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            dgvRegions.DataSource = loadTable();

            txtName.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد المنطقة المراد تعديلها");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم المنطقة");
                return;
            }


            try
            {

                cmd = new SqlCommand("Update Regions set name = @name Where id = '" + id + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);


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

            dgvRegions.DataSource = loadTable();

            txtName.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد المنطقة المراد حذفها");
                return;
            }
            try
            {

                cmd = new SqlCommand("delete from Regions Where id = '" + id + "'", adoClass.sqlcn);

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

            dgvRegions.DataSource = loadTable();

            dgvRegions.Text = "";

            txtHidden.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvRegions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvRegions.CurrentRow.Cells[1].Value.ToString();
            txtName.Text = dgvRegions.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
