using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Classes;
using System.Data.SqlClient;

namespace POS.Forms
{
    public partial class FormUsers : Form
    {
        public FormUsers()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtHidden;

        //private DataTable loadTable()
        //{
        //    DataTable dt = new DataTable();

        //    if (adoClass.sqlcn.State != ConnectionState.Open)
        //    {
        //        adoClass.sqlcn.Open();
        //    }
        //    SqlCommand cmd = new SqlCommand("Select * from Users", adoClass.sqlcn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(dt);
        //    adoClass.sqlcn.Close();
        //    return dt;
        //}

        private void loadTable(string query)
        {
            dgvUsers.Rows.Clear();
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

                    dgvUsers.Rows.Add
                        (new object[]
                            {
                            row["privilege"],
                            row["address"],
                            row["phone"],
                            row["fullName"],
                            row["password"],
                            row["userName"],
                            row["id"],
                            }
                        ); ;
                }
            }

        }

        private void FormUsers_Load(object sender, EventArgs e)
        {
            
            // combo box user
            DataTable tblPrivilege = new DataTable();
            tblPrivilege.Columns.Add("id", typeof(int));
            tblPrivilege.Columns.Add("priviledge");

            tblPrivilege.Rows.Add(new object[] { 0, "" });
            tblPrivilege.Rows.Add(new object[] { 1, "Admin" });
            tblPrivilege.Rows.Add(new object[] { 2, "User" });

            comboPrivilege.DataSource = tblPrivilege;
            comboPrivilege.DisplayMember = "priviledge";
            comboPrivilege.ValueMember = "id";

            
            loadTable("Select * from Users");
            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("ادخل اسم المستخدم");
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("ادخل كلمة المرور");
                return;
            }
            if (comboPrivilege.Text == "")
            {
                MessageBox.Show("حدد الصلاحية");
                return;
            }
            try
            {
                cmd = new SqlCommand("Insert into Users (userName,password,fullName,privilege,phone,address) values (@userName,@password,@fullName,@privilege,@phone,@address)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@userName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@fullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@privilege", comboPrivilege.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة المستخدم بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select * from Users");

            txtUserName.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            comboPrivilege.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            //MessageBox.Show("Text box hidden :  " + txtHidden.Text);
            if (id == "")
            {
                MessageBox.Show("حدد الصف المراد تعديلة");
                return;
            }
            if (txtUserName.Text == "")
            {
                MessageBox.Show("ادخل اسم المستخدم");
                return;
            }
            if (txtPassword.Text == "")
            {
                MessageBox.Show("ادخل كلمة المرور");
                return;
            }
            if (comboPrivilege.Text == "")
            {
                MessageBox.Show("حدد الصلاحية");
                return;
            }

            try
            {

                cmd = new SqlCommand("Update Users set userName = @userName,password = @password,fullName = @fullName,privilege = @privilege,phone= @phone,address=@address Where id = '" + id + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@userName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@fullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@privilege", comboPrivilege.Text);
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

            loadTable("Select * from Users");

            txtUserName.Text = "";
            txtFullName.Text = "";
            txtPassword.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            comboPrivilege.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvUsers.CurrentRow.Cells[6].Value.ToString();
                    if (txtHidden.Text == "")
                    {
                        MessageBox.Show("حدد المستخدم المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Users Where id = '" + txtHidden.Text + "'", adoClass.sqlcn);

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

                    loadTable("Select * from Users");

                    txtUserName.Text = "";
                    txtFullName.Text = "";
                    txtPassword.Text = "";
                    txtPhone.Text = "";
                    txtAddress.Text = "";
                    comboPrivilege.Text = "";
                    txtHidden.Text = "";
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvUsers.CurrentRow.Cells[6].Value.ToString();

            txtUserName.Text = dgvUsers.CurrentRow.Cells[5].Value.ToString();
            txtPassword.Text = dgvUsers.CurrentRow.Cells[4].Value.ToString();
            txtFullName.Text = dgvUsers.CurrentRow.Cells[3].Value.ToString();
            txtPhone.Text = dgvUsers.CurrentRow.Cells[2].Value.ToString();
            txtAddress.Text = dgvUsers.CurrentRow.Cells[1].Value.ToString();
            comboPrivilege.Text = dgvUsers.CurrentRow.Cells[0].Value.ToString();
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
