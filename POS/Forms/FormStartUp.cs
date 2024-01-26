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
using System.Management;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace POS.Forms
{
    public partial class FormStartUp : Form
    {
        public FormStartUp()
        {
            InitializeComponent();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (progressBar.Value == 10)
            //{
            //    try
            //    {
            //        ClassSerialNumber sn = new ClassSerialNumber();
            //        string serialNumber = Regex.Replace(sn.GetSerialNumber(@"C:"), @"\s+", ""); // remove empty space

            //        if (File.Exists(Application.StartupPath + "\\serialNumber\\serial.txt") == false)
            //        {
            //            FormEnterSerialNumber frm = new FormEnterSerialNumber();
            //            frm.Show();
            //        }
            //        else
            //        {
            //            StreamReader sr = new StreamReader(Application.StartupPath + "\\serialNumber\\serial.txt");
            //            string txt = sr.ReadToEnd();
            //            sr.Close();
            //            txt = Regex.Replace(txt, @"\s+", "");


            //            if (txt == serialNumber)
            //            {
            //            }
            //            else
            //            {
            //                MessageBox.Show("خطا في تشغيل البرنامج الرجاء الاتصال علي الشركة");
            //                Application.Exit();
            //            }

            //        }


            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        MessageBox.Show("خطا في تشغيل البرنامج الرجاء الاتصال علي الشركة");
            //        Application.Exit();
            //    }
            //}
            if (progressBar.Value == 5)
            {
                try
                {
                    createDatabase();
                }
                catch (Exception)
                {

                }
            }

            if (progressBar.Value == 10)
            {
                ClassLoading loading = new ClassLoading();
                loading.loadSystemOptions();
            }

            if (progressBar.Value == 20)
            {
                Close();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                progressBar.Value++;
                progressBar.Refresh();
            }
        }
        private bool checkDatabase()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True");

            //SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-KE662S4;Integrated Security=True");
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

                    //var con = new SqlConnection(@"Data Source=DESKTOP-KE662S4;Integrated Security=True");

                    var cmd = new SqlCommand("query", con);
                    con.Open();

                    foreach (var query in sqlqueries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();

                        //Console.WriteLine(query + "GO");
                    }

                    con.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine("-------------------");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("-------------------");
                }

                //try
                //{
                //    string conn = @"Data Source=.\SQLEXPRESS;Integrated Security=True";

                //    string script = File.ReadAllText(Application.StartupPath + @"\sqlScript.sql");

                //    SqlConnection co = new SqlConnection(conn);
                //    Server server = new Server(new ServerConnection(co));

                //    server.ConnectionContext.ExecuteNonQuery(script);
                //}
                //catch(Exception e)
                //{
                //    Console.WriteLine("-------------------");
                //    Console.WriteLine(e.Message);
                //    Console.WriteLine("-------------------");

                //}
            }
        }
        private void FormStartUp_Load(object sender, EventArgs e)
        {

        }
    }

}
