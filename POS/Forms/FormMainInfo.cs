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
    public partial class FormMainInfo : Form
    {
        public FormMainInfo()
        {
            InitializeComponent();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            FormUsers frm = new FormUsers();
            frm.Show();
        }

        private void btnTayar_Click(object sender, EventArgs e)
        {
            FormTayar frm = new FormTayar();
            frm.Show();
        }

        private void btnRegions_Click(object sender, EventArgs e)
        {
            FormRegions frm = new FormRegions();
            frm.Show();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            FormCategories frm = new FormCategories();
            frm.Show();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            FormItems frm = new FormItems();
            frm.Show();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            FormAdminExpenses frm = new FormAdminExpenses();
            frm.Show();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            FormClients frm = new FormClients();
            frm.Show();
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            FormDeleteOrder frm = new FormDeleteOrder();
            frm.Show();
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            FormTables frm = new FormTables();
            frm.Show();
        }
    }
}
