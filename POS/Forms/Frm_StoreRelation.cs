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
    public partial class Frm_StoreRelation : Form
    {
        public Frm_StoreRelation()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        DataTable tblSearch = new DataTable();



        private void AutoNumber()
        {
            tblSearch.Clear();
            tblSearch = db.readData("SELECT ItemsStoreRelation.id as '#',  Items.name as 'العنصر',[minusQty]as 'يتكون من',storeItems.name as 'المخزن'FROM [dbo].[ItemsStoreRelation],Items,storeItems where Items.id = ItemsStoreRelation.ItemSaleID and storeItems.id = ItemsStoreRelation.ItemStoreID", "");
            DgvSearch.DataSource = tblSearch;
            tbl.Clear();
            tbl = db.readData("select max (id) from ItemsStoreRelation", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }
            txtQty.Text = "1";
            btnAdd.Enabled = true;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            btnNew.Enabled = true;

        }
        private void Frm_StoreRelation_Load(object sender, EventArgs e)
        {
            AutoNumber();
            db.FillComboBox(cbxItem, "select * from Items", "name", "id");
            db.FillComboBox(cbxStoreItem, "select * from storeItems", "name", "id");

        }
        private bool check()
        {
            if (cbxItem.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر العنصر");
                return false;
            }
            if (cbxStoreItem.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر عنصر المخزن");
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text) <= 0)
            {
                MessageBox.Show("من فضلك ادخل رقم اكبر من 0");
                return false;
            }
            DataTable check = new DataTable();
            check.Clear();
            check = db.readData("select * from ItemsStoreRelation where ItemSaleID=" + cbxItem.SelectedValue + " and ItemStoreID=" + cbxStoreItem.SelectedValue + "", "");
            if(check.Rows.Count > 0)
            {
                MessageBox.Show("تم الربط بينهما من قبل");

                return false;
            }

            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool checker = check();
            if(checker == false)
            {
                return;
            }
            db.executeData("insert into ItemsStoreRelation Values (" + cbxItem.SelectedValue + " ,"+cbxStoreItem.SelectedValue+","+ txtQty.Text + ","+txtID.Text+")", "تم الادخال بنجاح", "");

            AutoNumber();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DgvSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (DgvSearch.Rows.Count >= 1)
                {
                    txtID.Text = DgvSearch.CurrentRow.Cells[0].Value.ToString();
                    cbxItem.Text = DgvSearch.CurrentRow.Cells[1].Value.ToString();
                    txtQty.Text = DgvSearch.CurrentRow.Cells[2].Value.ToString();
                    cbxStoreItem.Text = DgvSearch.CurrentRow.Cells[3].Value.ToString();

                    btnAdd.Enabled = false;
                    btnNew.Enabled = true;
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;
                }

            }
            catch (Exception) { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bool checker = check();
            if (checker == false)
            {
                return;
            }

            db.executeData("Update ItemsStoreRelation set ItemSaleID = " + cbxItem.SelectedValue + ",  ItemStoreID ="+cbxStoreItem.SelectedValue+ ",minusQty=" +txtQty.Text + " where id="+txtID.Text+"", "تم تعديل البيانات بنجاح", "");
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.executeData("Delete from ItemsStoreRelation where id=" + txtID.Text + "", "تم مسح البيانات بنجاح", "");
                AutoNumber();
            }
        }
    }
}
