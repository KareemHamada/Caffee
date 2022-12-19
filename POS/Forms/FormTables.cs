using POS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Forms
{
    public partial class FormTables : Form
    {
        public FormTables()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private TextBox txtHidden;

        private void loadTable(string query)
        {
            //DataTable dt = new DataTable();

            //if (adoClass.sqlcn.State != ConnectionState.Open)
            //{
            //    adoClass.sqlcn.Open();
            //}
            //cmd = new SqlCommand("Select * from TablesNames", adoClass.sqlcn);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt);
            //adoClass.sqlcn.Close();
            //return dt;


            //////////////////////
            dgvTable.Rows.Clear();
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand(query, adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            //double total = 0; // اجمالي الورديات
            //double FinalTotal = 0;
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow row in dt.Rows)
                {
                    //double.TryParse(row["total"].ToString(), out total);
                    //FinalTotal += total;
                    dgvTable.Rows.Add
                        (new object[]
                            {
                            row["name"],
                            row["id"]
                            }
                        ); ;
                }
            }
        }


        private void FormTables_Load(object sender, EventArgs e)
        {
            try
            {
                loadTable("Select * from TablesNames");
                //dgvTable.Columns[0].HeaderText = "الاسم";

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
                MessageBox.Show("ادخل اسم الترابيزة");
                return;
            }

            try
            {
                cmd = new SqlCommand("Insert into TablesNames (name) values (@name)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة الترابيزة بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select * from TablesNames");

            txtName.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد الترابيزة المراد تعديلها");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم الترابيزة");
                return;
            }


            try
            {

                cmd = new SqlCommand("Update TablesNames set name = @name Where id = '" + id + "'", adoClass.sqlcn);

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

            loadTable("Select * from TablesNames");

            txtName.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد الترابيزة المراد حذفها");
                return;
            }
            try
            {

                cmd = new SqlCommand("delete from TablesNames Where id = '" + id + "'", adoClass.sqlcn);

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

            loadTable("Select * from TablesNames");

            txtName.Text = "";
            txtHidden.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvTable.CurrentRow.Cells[1].Value.ToString();
            txtName.Text = dgvTable.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (dgvTable.Rows.Count > 0)
            {
                if (MessageBox.Show("هل متاكد من حذف الكل", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    try
                    {

                        cmd = new SqlCommand("delete from TablesNames DBCC CHECKIDENT (TablesNames,RESEED,0)", adoClass.sqlcn);

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

                    loadTable("Select * from TablesNames");

                    txtName.Text = "";
                    txtHidden.Text = "";
                }
            }
        }
    }
}
