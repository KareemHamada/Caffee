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

        private void createDatabase()
        {
            bool check = checkDatabase();
            if (check == false)
            {

                try
                {
                    var fileContent = File.ReadAllText(Application.StartupPath + @"\Lojy.sql");
                    var sqlqueries = fileContent.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                    var con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True");
                    var cmd = new SqlCommand("query", con);
                    con.Open();

                    foreach (var query in sqlqueries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
                catch (Exception) { }

            }
        }
        // to check if db exists or not
        private bool checkDatabase()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("", conn);
            SqlDataReader rdr;
            try
            {
                cmd.CommandText = "exec sys.sp_databases";
                conn.Open();
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (rdr.GetString(0) == "Lojy")
                    {
                        return true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            
            conn.Close();
            rdr.Dispose();
            cmd.Dispose();
            return false;
        }
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
        Database db = new Database();
        DataTable tbl = new DataTable();
        private string identifier(string wmiClass, string wmiProperty)
        // return a hardware identifier
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                // only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {

                    }
                }
            }

            return result;
        }
        string x = "0";
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

            string serial = identifier("Win32_DiskDrive", "SerialNumber");
            string signature = identifier("Win32_DiskDrive", "Signature"); // for hard drive
            //label2.Text = signature;
            //label1.Text = serial;
            x = (((Convert.ToDecimal(signature) * 12345 - 3) * 21 - 9) * 2000).ToString();

            if (Properties.Settings.Default.Product_Key != x)
            {
                Frm_Serial frm = new Frm_Serial();
                frm.ShowDialog();
            }
            else
            {
                tbl.Clear();
                tbl = db.readData("select * from Users", "");
                if(tbl.Rows.Count <= 0)
                {
                    db.executeData("Insert into Users (userName,password,fullName,privilege) values ('" + 123 + "','" + 123 + "','" + 123 + "','Admin')", "","");
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
        }


        private void FormLogin_Load(object sender, EventArgs e)
        {
            try
            {
                createDatabase();
            }
            catch (Exception) { }
            txtUserName.Clear();
            txtUserName.Focus();
        }
    }
}
