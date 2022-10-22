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
    public partial class FormMainClients : Form
    {
        public FormMainClients()
        {
            InitializeComponent();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            FormClients frm = new FormClients();
            frm.Show();
        }

        private void btnClientsCashWithdraw_Click(object sender, EventArgs e)
        {
            FormClientCashWithdraw frm = new FormClientCashWithdraw();
            frm.Show();
        }
    }
}
