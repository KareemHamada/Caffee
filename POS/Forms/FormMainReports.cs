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
    public partial class FormMainReports : Form
    {
        public FormMainReports()
        {
            InitializeComponent();
        }

        private void btnShifts_Click(object sender, EventArgs e)
        {
            FormReportShifts frm = new FormReportShifts();
            frm.Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            FormReportOrders frm = new FormReportOrders();
            frm.Show();
        }

        private void btnExpensesReport_Click(object sender, EventArgs e)
        {
            FormReportExpenses frm = new FormReportExpenses();
            frm.Show();
        }

        private void btnReportStore_Click(object sender, EventArgs e)
        {
            FormReportStore frm = new FormReportStore();
            frm.Show();
        }

        private void btnReportSalaries_Click(object sender, EventArgs e)
        {
            FormReportSalaries frm = new FormReportSalaries();
            frm.Show();
        }

        private void btnReportAttendLeave_Click(object sender, EventArgs e)
        {
            FormReportAttendLeave frm = new FormReportAttendLeave();
            frm.Show();
        }

        private void btnReportWithdrawDeposit_Click(object sender, EventArgs e)
        {
            FormEmpWithdrawDeposit frm = new FormEmpWithdrawDeposit();
            frm.Show();
        }

        private void btnStoreOrderItems_Click(object sender, EventArgs e)
        {
            FormReportStoreItems frm = new FormReportStoreItems();
            frm.Show();
        }

        private void btnReportItems_Click(object sender, EventArgs e)
        {
            FormReportItems frm = new FormReportItems();
            frm.Show();
        }

        private void btnSupplierCashWithdraw_Click(object sender, EventArgs e)
        {
            FormReportSupplierCashWithdraw frm = new FormReportSupplierCashWithdraw();
            frm.Show();
        }

        private void btnReportOverAll_Click(object sender, EventArgs e)
        {
            FormReportOverAll frm = new FormReportOverAll();
            frm.Show();
        }

        private void btnClientsCashWithdraw_Click(object sender, EventArgs e)
        {
            FormReportClientsCashWithdraw frm = new FormReportClientsCashWithdraw();
            frm.Show();
        }
    }
}
