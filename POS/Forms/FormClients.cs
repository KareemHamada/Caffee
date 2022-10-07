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
    public partial class FormClients : Form
    {
        public FormClients()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private TextBox txtHidden;

        private void FormClients_Load(object sender, EventArgs e)
        {
            Helper.fillComboBox(comboRegions, "Select id,name from Regions", "name", "id");
            try
            {
                dgvClients.DataSource = loadTable();
                dgvClients.Columns[0].HeaderText = "العنوان";
                dgvClients.Columns[1].HeaderText = "المنطقة";
                dgvClients.Columns[2].HeaderText = "التليفون";
                dgvClients.Columns[3].HeaderText = "العميل";
                dgvClients.Columns[4].HeaderText = "#";

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
            cmd = new SqlCommand("Select Clients.address,Regions.name,Clients.phone,Clients.name,Clients.id from Clients LEFT JOIN Regions on Clients.regionId = Regions.id", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
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
                cmd = new SqlCommand("Insert into Clients (name,phone,regionId,address) values (@name,@phone,@regionId,@address)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@regionId", comboRegions.SelectedValue);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة العميل بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            dgvClients.DataSource = loadTable();

            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            comboRegions.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد العميل المراد تعديلها");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم العميل");
                return;
            }


            try
            {
                // Clients (name,phone,regionId,address)
                cmd = new SqlCommand("Update Clients set name = @name, phone=@phone, regionId=@regionId, address=@address Where id = '" + id + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@regionId", comboRegions.SelectedValue);
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

            dgvClients.DataSource = loadTable();

            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            comboRegions.Text = "";
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

                cmd = new SqlCommand("delete from Clients Where id = '" + id + "'", adoClass.sqlcn);

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

            dgvClients.DataSource = loadTable();

            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            comboRegions.Text = "";
            txtHidden.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }





        private void dgvClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvClients.CurrentRow.Cells[4].Value.ToString();
            txtName.Text = dgvClients.CurrentRow.Cells[3].Value.ToString();
            txtPhone.Text = dgvClients.CurrentRow.Cells[2].Value.ToString();
            comboRegions.Text = dgvClients.CurrentRow.Cells[1].Value.ToString();
            txtAddress.Text = dgvClients.CurrentRow.Cells[0].Value.ToString();
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
