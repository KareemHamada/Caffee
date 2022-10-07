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
    public partial class FormMainEmployess : Form
    {
        public FormMainEmployess()
        {
            InitializeComponent();
        }

        private void btnEmp_Click(object sender, EventArgs e)
        {
            FormEmployees frm = new FormEmployees();
            frm.Show();
        }

        private void btnWD_Click(object sender, EventArgs e)
        {
            FormWithdrawDeposit frm = new FormWithdrawDeposit();
            frm.Show();
        }

        private void btnAttendingLeaving_Click(object sender, EventArgs e)
        {
            FormAttendingLeaving frm = new FormAttendingLeaving();
            frm.Show();
        }

        private void btnEmployeesSalaries_Click(object sender, EventArgs e)
        {
            FormEmployeesSalaries frm = new FormEmployeesSalaries();
            frm.Show();
        }

    }
}
