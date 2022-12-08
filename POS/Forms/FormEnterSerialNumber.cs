using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using POS.Classes;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace POS.Forms
{
    public partial class FormEnterSerialNumber : Form
    {
        public FormEnterSerialNumber()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtSerialNumber.Text == "")
            {
                MessageBox.Show("Enter SN");
                return;
            }

            if (txtDS.Text == "")
            {
                MessageBox.Show("Enter DS");
                return;
            }

            try
            {
                ClassSerialNumber sn = new ClassSerialNumber();
                string serialNumber = Regex.Replace(sn.GetSerialNumber(@"C:"), @"\s+", ""); // remove empty space

                if (txtSerialNumber.Text == serialNumber)
                {
                    MessageBox.Show("تم تفعيل البرنامج");
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("خطا في تشغيل البرنامج الرجاء الاتصال علي الشركة");
                    Application.Exit();
                    return;
                }

                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\Tools\\serial.txt");
                sw.WriteLine(txtSerialNumber.Text);
                sw.WriteLine(txtDS.Text);
                sw.Close();

                //StreamReader sr = new StreamReader(Application.StartupPath + "\\Tools\\serial.txt");
                //string txt = sr.ReadLine();
                ////string ds = sr.ReadToEnd();
                //sr.Close();

                //txt = Regex.Replace(txt, @"\s+", "");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("خطا في تشغيل البرنامج الرجاء الاتصال علي الشركة");
                Application.Exit();
            }
        }
    }
}
