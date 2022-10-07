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
    public partial class FormItems : Form
    {
        public FormItems()
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
            cmd = new SqlCommand("Select Categories.name,Items.image,Items.price,Items.name,Items.id from Items LEFT JOIN Categories on Items.CategoryId = Categories.id", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
        }

        
        private void FormItems_Load(object sender, EventArgs e)
        {
            Helper.fillComboBox(comboCategory, "Select id,name from Categories", "name", "id");
            try
            {
                dgvItems.DataSource = loadTable();
                dgvItems.Columns[0].HeaderText = "الصنف";
                dgvItems.Columns[1].HeaderText = "الصورة";
                dgvItems.Columns[2].HeaderText = "السعر";
                dgvItems.Columns[3].HeaderText = "العنصر";
                dgvItems.Columns[4].HeaderText = "#";

            }
            catch
            {

            }
            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Filter = "Images|*.png";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtImage.Text = fileDialog.FileName;
                picBox.BackgroundImage = new Bitmap(txtImage.Text);
            }
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
            if (comboCategory.Text == "")
            {
                MessageBox.Show("اختر الصنف");
                return;
            }

            try
            {
                if (picBox.BackgroundImage != null)
                {
                    cmd = new SqlCommand("Insert into Items (name,price,image,categoryId,quantity) values (@name,@price,@image,@categoryId,@quantity)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@quantity", 0);
                    cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                    cmd.Parameters.AddWithValue("@categoryId", comboCategory.SelectedValue);
                }
                else
                {
                    cmd = new SqlCommand("Insert into Items (name,price,categoryId,quantity) values (@name,@price,@categoryId,@quantity)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@categoryId", comboCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@quantity", 0);
                }
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();


                MessageBox.Show("تم اضافة العنصر بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            dgvItems.DataSource = loadTable();

            txtName.Text = "";
            picBox.BackgroundImage = null;
            txtImage.Text = "";
            txtPrice.Text = "";
            comboCategory.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد العنصر المراد تعديلة");
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
            if (comboCategory.Text == "")
            {
                MessageBox.Show("اختر الصنف");
                return;
            }


            try
            {
                if (picBox.BackgroundImage != null)
                {
                    cmd = new SqlCommand("Update Items set name = @name,price=@price,image=@image,categoryId = @categoryId Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                    cmd.Parameters.AddWithValue("@categoryId", comboCategory.SelectedValue);
                }
                else
                {
                    cmd = new SqlCommand("Update Items set name = @name,price=@price,categoryId = @categoryId Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                    cmd.Parameters.AddWithValue("@categoryId", comboCategory.SelectedValue);
                }

                ////////////////////////////
                

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

            dgvItems.DataSource = loadTable();

            txtName.Text = "";
            picBox.BackgroundImage = null;
            txtImage.Text = "";
            txtPrice.Text = "";
            comboCategory.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد العنصر المراد حذفة");
                return;
            }
            try
            {

                cmd = new SqlCommand("delete from Items Where id = '" + id + "'", adoClass.sqlcn);

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
            dgvItems.DataSource = loadTable();
            txtName.Text = "";
            picBox.BackgroundImage = null;
            txtImage.Text = "";
            txtPrice.Text = "";
            comboCategory.Text = "";
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

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            comboCategory.Text = dgvItems.CurrentRow.Cells[0].Value.ToString();
            picBox.BackgroundImage = Helper.ByteToImage(dgvItems.CurrentRow.Cells[1].Value);
            txtPrice.Text = dgvItems.CurrentRow.Cells[2].Value.ToString();
            txtName.Text = dgvItems.CurrentRow.Cells[3].Value.ToString();
            txtHidden.Text = dgvItems.CurrentRow.Cells[4].Value.ToString();
        }



    }
}
