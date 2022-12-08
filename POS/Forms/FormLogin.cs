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
using System.IO;

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
        

        private bool Trail()
        {
            int num = Properties.Settings.Default.Trial;
            int thisNum = num + 1;
            Properties.Settings.Default.Trial = thisNum;
            int Times_of_trail = 100;
            if(thisNum >= Times_of_trail)
            {
                MessageBox.Show("هذه النسخة التجريبية منتهية لطلب النسخة المدفوعة تواصل معنا علي 01090802802", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                Properties.Settings.Default.Save();

                int baky = Times_of_trail - Properties.Settings.Default.Trial;
                MessageBox.Show("هذه نسخة تجريبية و متبقي لك عدد مرات "+baky+" مرة", "تاكيد",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            

            return true;
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

            if(Properties.Settings.Default.Product_Key == "NO")
            {
                Frm_Serial frm = new Frm_Serial();
                frm.ShowDialog();
            }
            else
            {
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
                        //// code for free trail 
                        //bool chech = Trail();
                        //if (chech == false)
                        //{
                        //    return;
                        //}
                        //// end of code for free trail

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


        private void FormLogin_Load(object sender, EventArgs e)
        {
            
        }
    }
}
