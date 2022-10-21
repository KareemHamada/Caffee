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
using POS.Tools;
using Microsoft.Reporting.WinForms;

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
        //private DataTable loadTable()
        //{
        //    DataTable dt = new DataTable();

        //    if (adoClass.sqlcn.State != ConnectionState.Open)
        //    {
        //        adoClass.sqlcn.Open();
        //    }
        //    cmd = new SqlCommand("Select Expenses.shiftId,Users.fullName,Expenses.dateTime,Expenses.price,Expenses.name,Expenses.id from Expenses LEFT JOIN Users on Expenses.userId = Users.id where shiftId IS NULL", adoClass.sqlcn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(dt);
        //    adoClass.sqlcn.Close();
        //    return dt;
        //}


        private void loadTable(string query)
        {
            dgvExpenses.Rows.Clear();
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

                    dgvExpenses.Rows.Add
                        (new object[]
                            {
                            row["shiftId"],
                            row["userName"],
                            row["dateTime"],
                            row["price"],
                            row["name"],
                            row["id"],
                            }
                        ); ;
                }
            }

        }



        private void FormAdminExpenses_Load(object sender, EventArgs e)
        {

            loadTable("Select Expenses.shiftId,Users.fullName as userName,Expenses.dateTime,Expenses.price,Expenses.name,Expenses.id from Expenses LEFT JOIN Users on Expenses.userId = Users.id where shiftId IS NULL");

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

            loadTable("Select Expenses.shiftId,Users.fullName as userName,Expenses.dateTime,Expenses.price,Expenses.name,Expenses.id from Expenses LEFT JOIN Users on Expenses.userId = Users.id where shiftId IS NULL");

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

            loadTable("Select Expenses.shiftId,Users.fullName as userName,Expenses.dateTime,Expenses.price,Expenses.name,Expenses.id from Expenses LEFT JOIN Users on Expenses.userId = Users.id where shiftId IS NULL");

            txtName.Text = "";
            txtPrice.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvExpenses.CurrentRow.Cells[5].Value.ToString();
                    if (txtHidden.Text == "")
                    {
                        MessageBox.Show("حدد المصروف المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Expenses Where id = '" + txtHidden.Text + "'", adoClass.sqlcn);

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

                    loadTable("Select Expenses.shiftId,Users.fullName as userName,Expenses.dateTime,Expenses.price,Expenses.name,Expenses.id from Expenses LEFT JOIN Users on Expenses.userId = Users.id where shiftId IS NULL");

                    txtName.Text = "";
                    txtPrice.Text = "";
                    txtHidden.Text = "";
                }
            }
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

       

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("Select Expenses.shiftId,Users.fullName as userName,Expenses.dateTime,Expenses.price,Expenses.name,Expenses.id from Expenses LEFT JOIN Users on Expenses.userId = Users.id where shiftId IS NULL");
            }
            else
            {
                loadTable("Select Expenses.shiftId,Users.fullName as userName,Expenses.dateTime,Expenses.price,Expenses.name,Expenses.id from Expenses LEFT JOIN Users on Expenses.userId = Users.id where shiftId IS NULL and( Expenses.name like '%" + text + "%' or Users.fullName like '%" + text + "%' )");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.Rows.Count > 0)
            {
                dsShowAdminExpenses tbl = new dsShowAdminExpenses();
                for (int i = 0; i < dgvExpenses.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowAdminExpenses"].NewRow();
                    dro["user"] = dgvExpenses[1, i].Value;
                    dro["dateTime"] = dgvExpenses[2, i].Value;
                    dro["price"] = dgvExpenses[3, i].Value;
                    dro["name"] = dgvExpenses[4, i].Value;

                    tbl.Tables["dtShowAdminExpenses"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShowAdminExpenses.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowAdminExpenses"]));

                if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShowAdminExpenses.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowAdminExpenses"]));
                    PrintersClass.PrintToPrinter(report);
                }
                else
                {
                    rptForm.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show("لا يوجد عناصر لعرضها");
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
