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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private SqlDataReader dr;

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
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


            cmd = new SqlCommand("Select * from Users where userName = @username and password = @password", adoClass.sqlcn);
            dr = null;
            cmd.Parameters.AddWithValue("@username", txtUserName.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);

            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        declarations.userid = int.Parse(dr["id"].ToString());
                        declarations.userFullName = dr["fullName"].ToString();
                        declarations.privilege = dr["privilege"].ToString();
                    }

                    this.DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("فشل تسجيل الدخول");
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    return;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }
        }
    }
}
