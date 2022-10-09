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
    public partial class FormStoreItems : Form
    {
        public FormStoreItems()
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
            cmd = new SqlCommand("Select * from storeItems", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
        }
        private void FormStoreItems_Load(object sender, EventArgs e)
        {
            try
            {
                dgvItems.DataSource = loadTable();
                dgvItems.Columns[0].HeaderText = "الصورة";
                dgvItems.Columns[1].HeaderText = "العنصر";
                dgvItems.Columns[2].HeaderText = "#";

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

            try
            {
                if (picBox.BackgroundImage != null)
                {
                    cmd = new SqlCommand("Insert into storeItems (name,image) values (@name,@image)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                }
                else
                {
                    cmd = new SqlCommand("Insert into storeItems (name) values (@name)", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
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

            try
            {
                if (picBox.BackgroundImage != null)
                {
                    cmd = new SqlCommand("Update storeItems set name = @name,image=@image Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@image", Helper.ImageTOByte(picBox.BackgroundImage));
                }
                else
                {
                    cmd = new SqlCommand("Update storeItems set name = @name,image=@image Where id = '" + id + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = DBNull.Value;
                }


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

                cmd = new SqlCommand("delete from storeItems Where id = '" + id + "'", adoClass.sqlcn);

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
            txtHidden.Text = "";
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

        private void dgvItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            picBox.BackgroundImage = Helper.ByteToImage(dgvItems.CurrentRow.Cells[0].Value);
            txtName.Text = dgvItems.CurrentRow.Cells[1].Value.ToString();
            txtHidden.Text = dgvItems.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            picBox.BackgroundImage = null;
        }
    }
}
