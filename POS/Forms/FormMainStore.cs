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
    public partial class FormMainStore : Form
    {
        public FormMainStore()
        {
            InitializeComponent();
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            FormSuppliers frm = new FormSuppliers();
            frm.Show();
        }

        private void btnStoreItems_Click(object sender, EventArgs e)
        {
            FormStoreItems frm = new FormStoreItems();
            frm.Show();
        }

        private void btnAddingToStore_Click(object sender, EventArgs e)
        {
            FormAddStores frm = new FormAddStores();
            frm.Show();
        }

        private void btnCashWithdraw_Click(object sender, EventArgs e)
        {
            FormSupplierCashWithdraw frm = new FormSupplierCashWithdraw();
            frm.Show();
        }

        
    }
}
