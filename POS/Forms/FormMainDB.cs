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
    public partial class FormMainDB : Form
    {
        public FormMainDB()
        {
            InitializeComponent();
        }

        private void btnCreateNewCopy_Click(object sender, EventArgs e)
        {
            FormCreateNewCopy frm = new FormCreateNewCopy();
            frm.Show();
        }

        private void btnRestoreCopy_Click(object sender, EventArgs e)
        {
            FormRestoreCopy frm = new FormRestoreCopy();
            frm.Show();
        }
    }
}
