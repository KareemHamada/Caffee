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
    public partial class FormCreateNewCopy : Form
    {
        public FormCreateNewCopy()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (txtBackup.Text == "")
            {
                MessageBox.Show("اختار المسار");
            }
            else
            {
                SqlCommand cmd;

                string fileName = txtBackup.Text + "\\POS" + DateTime.Now.ToShortDateString().Replace("/", "-") + " - " + DateTime.Now.ToLongTimeString().Replace(":", "-");
                string strQuery = "Backup Database POS to Disk='" + fileName + ".bak'";
                cmd = new SqlCommand(strQuery, adoClass.sqlcn);
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
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

        private void FormCreateNewCopy_Load(object sender, EventArgs e)
        {

        }
    }
}
