using POS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Forms
{
    public partial class frm_Settings : Form
    {
        public frm_Settings()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        string printerName = "";
        //call to show printers name in combo
        private void showPrinters()
        {

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                printerName = PrinterSettings.InstalledPrinters[i];
                cbxMainPrinter.Items.Add(printerName);
                cbxKitchenPrinter.Items.Add(printerName);
            }

            if (Properties.Settings.Default.PrinterName == "")
                cbxMainPrinter.SelectedIndex = 0;
            else
                cbxMainPrinter.Text = Properties.Settings.Default.PrinterName;

            if (Properties.Settings.Default.KitchenPrinter == "")
                cbxKitchenPrinter.SelectedIndex = 0;
            else
                cbxKitchenPrinter.Text = Properties.Settings.Default.KitchenPrinter;
        }


        private void showPassword()
        {
            if (Properties.Settings.Default.PasswordLockWardia == "")
                txtPassword.Text = "";
            else
                txtPassword.Text = Properties.Settings.Default.PasswordLockWardia;
        }
        private void btnSavePrintingOptions_Click(object sender, EventArgs e)
        {
            if (cbxMainPrinter.Text == "")
            {
                MessageBox.Show("من فضلك تاكد من بيانات الطابعة الرئيسية", "تاكيد");
                return;
            }
            if (cbxKitchenPrinter.Text == "")
            {
                MessageBox.Show("من فضلك تاكد من بيانات طابعة المطبخ", "تاكيد");
                return;
            }

            if (nudFatoraNumber.Value < 1 || nudKitchenNumber.Value < 1)
            {
                MessageBox.Show("من فضلك لا يمكن ان يقل عدد الفواتير المطبوعه عن 1", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Properties.Settings.Default.FatoraNumber = Convert.ToInt32(nudFatoraNumber.Value);
            Properties.Settings.Default.KitchenNumber = Convert.ToInt32(nudKitchenNumber.Value);


            if (rdoDirectPrint.Checked)
                Properties.Settings.Default.DirectPrint = true;
            else
                Properties.Settings.Default.DirectPrint = false;

            if (rdoShowBeforePrint.Checked)
                Properties.Settings.Default.ShowBeforePrint = true;
            else
                Properties.Settings.Default.ShowBeforePrint = false;

            if (rdoDontShow.Checked)
                Properties.Settings.Default.NotPrint = true;
            else
                Properties.Settings.Default.NotPrint = false;

            if (cbxprintKitchen.Checked)
                Properties.Settings.Default.PrintKitchen = true;
            else
                Properties.Settings.Default.PrintKitchen = false;


            if (cbxPrintFatoraToKitchen.Checked)
                Properties.Settings.Default.PrintFatoraToKitchen = true;
            else
                Properties.Settings.Default.PrintFatoraToKitchen = false;



            Properties.Settings.Default.PrinterName = cbxMainPrinter.Text;
            Properties.Settings.Default.KitchenPrinter = cbxKitchenPrinter.Text;

            Properties.Settings.Default.Save();

            MessageBox.Show("تم الحفظ بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        private void showGeneralSetting()
        {
            nudFatoraNumber.Value = Properties.Settings.Default.FatoraNumber;
            nudKitchenNumber.Value = Properties.Settings.Default.KitchenNumber;

            if (Properties.Settings.Default.ShowBeforePrint)
                rdoShowBeforePrint.Checked = true;
            else
                rdoShowBeforePrint.Checked = false;

            if (Properties.Settings.Default.DirectPrint)
                rdoDirectPrint.Checked = true;
            else
                rdoDirectPrint.Checked = false;

            if (Properties.Settings.Default.NotPrint)
                rdoDontShow.Checked = true;
            else
                rdoDontShow.Checked = false;

            if (Properties.Settings.Default.PrintKitchen)
                cbxprintKitchen.Checked = true;
            else
                cbxprintKitchen.Checked = false;


            if (Properties.Settings.Default.PrintFatoraToKitchen)
                cbxPrintFatoraToKitchen.Checked = true;
            else
                cbxPrintFatoraToKitchen.Checked = false;

        }
        private void frm_Settings_Load(object sender, EventArgs e)
        {
            try
            {
                showPrinters();
            }
            catch (Exception) { }
            try
            {
                showFatoraData();
            }
            catch (Exception) { }
            try
            {
                showGeneralSetting();
            }
            catch (Exception) { }
            try
            {
                showPassword();
            }
            catch (Exception)
            {

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData("select * from Options", "");
            if (tbl.Rows.Count >= 1)
            {
                //call function to save image
                db.executeData("update Options set name = N'" + txtName.Text + "' ,phone = N'" + txtPhone.Text + "' ,address = N'" + txtAddress.Text + "' ,line1 = N'" + txtLine1.Text + "',line2=N'" + txtLine2.Text + "'", "تم الحفظ بنجاح", "");
            }
            else
            {
                db.executeData("insert into Options values (N'" + txtName.Text + "' ,N'" + txtPhone.Text + "' ,N'" + txtAddress.Text + "' ,N'" + txtLine1.Text + "',N'" + txtLine2.Text + "')", "تم الحفظ بنجاح", "");
            }
        }

        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                MessageBox.Show("من فضلك ادخل باسورد تقفيل الوردية", "تاكيد");
                return;
            }

            Properties.Settings.Default.PasswordLockWardia = txtPassword.Text;
            Properties.Settings.Default.Save();

            MessageBox.Show("تم الحفظ بنجاح", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void showFatoraData()
        {
            tbl.Clear();
            tbl = db.readData("select * from Options", "");

            if (tbl.Rows.Count >= 1)
            {
                txtName.Text = tbl.Rows[0][1].ToString();
                txtPhone.Text = tbl.Rows[0][2].ToString();
                txtAddress.Text = tbl.Rows[0][3].ToString();
                txtLine1.Text = tbl.Rows[0][4].ToString();
                txtLine2.Text = tbl.Rows[0][5].ToString();
            }
        }
    }
}
