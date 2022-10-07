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

        private void FormStartUp_Load(object sender, EventArgs e)
        {

        }
    }

}
