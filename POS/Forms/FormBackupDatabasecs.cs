using POS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Forms
{
    public partial class FormBackupDatabasecs : Form
    {
        
        public FormBackupDatabasecs()
        {
            InitializeComponent();
        }

        private void FormBackupDatabasecs_Load(object sender, EventArgs e)
        {

        }


        private void btnBackup_Click(object sender, EventArgs e)
        {
            //string database = adoClass.sqlcn.Database.ToString();
            StreamReader sr = new StreamReader(Application.StartupPath + "\\Serial\\serial.txt");
            string txt = sr.ReadLine();
            string ds = sr.ReadLine();

            sr.Close();
            txt = Regex.Replace(txt, @"\s+", "");
            ds = Regex.Replace(ds, @"\s+", "");

            if (txtBackup.Text == "")
            {
                MessageBox.Show("اختار المسار");
            }
            else
            {


                SqlConnection con = new SqlConnection(@"Server=" + ds + "; Database=POS;Integrated Security=True");
                SqlCommand cmd;

                string fileName = txtBackup.Text + "\\POS" + DateTime.Now.ToShortDateString().Replace("/", "-") + " - " + DateTime.Now.ToLongTimeString().Replace(":", "-");
                string strQuery = "Backup Database POS to Disk='" + fileName + ".bak'";
                cmd = new SqlCommand(strQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                adoClass.sqlcn.Close();
                MessageBox.Show("تم الحفظ بنجاح", "انشاء نسخة احتياطبة", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBackup.Text = dlg.SelectedPath;
                //btnBackup.Enabled = true;
            }
        }
    }
}
