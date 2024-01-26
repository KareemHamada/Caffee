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
    public partial class Frm_Serial : Form
    {
        public Frm_Serial()
        {
            InitializeComponent();
        }
         
        private string identifier(string wmiClass,string wmiProperty)
            // return a hardware identifier
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach(System.Management.ManagementObject mo in moc)
            {
                // only get the first one
                if(result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {

                    }
                }
            }

            return result;
        }
        string x = "0";
        private void Frm_Serial_Load(object sender, EventArgs e)
        {
            string serial = identifier("Win32_DiskDrive", "SerialNumber");
            string signature = identifier("Win32_DiskDrive", "Signature");
            lbl2.Text = signature;
            lbl1.Text = serial;
            x = (((Convert.ToDecimal(signature) * 12345 - 3) * 21 - 9) * 2000).ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtBox.Text == "")
            {
                MessageBox.Show("من فضلك ادخل كود التفعيل", "تاكيد");
                return;
            }

            if(txtBox.Text == x)
            {
                Properties.Settings.Default.Product_Key = x;
                Properties.Settings.Default.Save();

                MessageBox.Show("تم تفعيل البرنامج بنجاح", "تاكيد");
                Close();
            }
            else
            {
                MessageBox.Show("كود التفعيل خطا", "تاكيد");
                return;
            }
        }
    }
}
