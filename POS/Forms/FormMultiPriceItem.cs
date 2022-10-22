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
    public partial class FormMultiPriceItem : Form
    {
        public FormMultiPriceItem()
        {
            InitializeComponent();
        }

        public string _price { get; set; }
        public bool isQuantity = false;

        private void FormMultiPriceItem_Load(object sender, EventArgs e)
        {

        }

        private void txtMultiPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isQuantity)
            {
                char ch = e.KeyChar;
                if (!Char.IsDigit(ch) && ch != 8)
                {
                    e.Handled = true;
                }
            }
            else
            {
                char ch = e.KeyChar;
                if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
                {
                    e.Handled = true;
                }
            }
            
        }

        private void btnPrice_Click(object sender, EventArgs e)
        {
            if(txtMultiPrice.Text == "")
            {
                MessageBox.Show("ادخل السعر");
                return;
            }
            _price = txtMultiPrice.Text;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
