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

namespace POS.Forms
{
    public partial class FormPasswordEndShift : Form
    {
        public FormPasswordEndShift()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(txtPass.Text == "")
            {
                MessageBox.Show("ادخل كلمة المرور");
                return;
            }
            if(txtPass.Text == declarations.systemOptions["pass"].ToString())
            {
                FormEndShift frm = new FormEndShift();
                frm.Show();
            }
            else
            {
                MessageBox.Show("كلمة المرور غير صحيحة");
            }
        }
    }
}
