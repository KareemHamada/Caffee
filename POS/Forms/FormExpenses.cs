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
    public partial class FormExpenses : Form
    {
        public FormExpenses()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private void FormExpenses_Load(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void txtOk_Click(object sender, EventArgs e)
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
                cmd = new SqlCommand("Insert into Expenses (name,price,dateTime,userId,shiftId) values (@name,@price,@dateTime,@userId,@shiftId)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@userId", declarations.userid);
                cmd.Parameters.AddWithValue("@shiftId", declarations.shiftId);


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

            txtName.Text = "";
            txtPrice.Text = "";
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
