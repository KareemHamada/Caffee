using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using POS.Classes;
using System.Drawing.Printing;

namespace POS.Forms
{
    public partial class FormOptions : Form
    {
        public FormOptions()
        {
            InitializeComponent();
        }

        private SqlDataAdapter adapter;
        private DataTable dataTable;
        private DataRow Row;

        string printerName = "";
        //call to show printers name in combo
        private void showPrinters()
        {

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                printerName = PrinterSettings.InstalledPrinters[i];
                cbxPrinter.Items.Add(printerName);
            }

            if (Properties.Settings.Default.PrinterName == "")
                cbxPrinter.SelectedIndex = 0;
            else
                cbxPrinter.Text = Properties.Settings.Default.PrinterName;
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            try
            {
                showPrinters();
            }
            catch (Exception) { }

            adapter = new SqlDataAdapter("Select Top 1 * From Options", adoClass.sqlcn);
            dataTable = new DataTable();

            try
            {
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    Row = dataTable.Rows[0];
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        txtPlace.Text = dataTable.Rows[i]["name"].ToString();
                        txtAddress.Text = dataTable.Rows[i]["address"].ToString();
                        //txtRestAddress2.Text = dataTable.Rows[i]["RestAddress2"].ToString();
                        txtPhone.Text = dataTable.Rows[i]["phone"].ToString();
                        txtLine1.Text = dataTable.Rows[i]["line1"].ToString();
                        txtLine2.Text = dataTable.Rows[i]["line2"].ToString();
                        txtPass.Text = dataTable.Rows[i]["pass"].ToString();
                        //if (dataTable.Rows[i]["image"] != DBNull.Value)
                        //{
                        //    picBox.BackgroundImage = Helper.ByteToImage(dataTable.Rows[i]["image"]);
                        //}

                        bool directPrintValue = false;
                        bool.TryParse(dataTable.Rows[i]["directPrint"].ToString(), out directPrintValue);
                        rdoDirectPrint.Checked = directPrintValue;


                        bool showBeforePrintValue = false;
                        bool.TryParse(dataTable.Rows[i]["showBeforePrint"].ToString(), out showBeforePrintValue);
                        rdoShowBeforePrint.Checked = showBeforePrintValue;


                        bool dontShowValue = false;
                        bool.TryParse(dataTable.Rows[i]["dontShow"].ToString(), out dontShowValue);
                        rdoDontShow.Checked = dontShowValue;


                        bool printTables = false;
                        bool.TryParse(dataTable.Rows[i]["printTables"].ToString(), out printTables);
                        chBoxTable.Checked = printTables;
                    }
                }
                else
                {
                    Row = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveData()
        {
            
            if (txtPass.Text == "")
            {
                MessageBox.Show("ادخل باسورد تقفيل الوردية");
                txtPhone.Focus();
                return;
            }
            if (Row == null)
            {
                Row = dataTable.NewRow();
                dataFillRow();
                dataTable.Rows.Add(Row);
            }
            else
            {
                Row.BeginEdit();
                dataFillRow();
                Row.EndEdit();
            }

            try
            {
                adoClass.builder = new SqlCommandBuilder(adapter);
                adapter.Update(dataTable);
                MessageBox.Show("تم حفظ البيانات بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataFillRow()
        {
            
            Row["name"] = txtPlace.Text;
            Row["address"] = txtAddress.Text;
            Row["phone"] = txtPhone.Text;
            Row["line1"] = txtLine1.Text;
            Row["line2"] = txtLine2.Text;
            Row["pass"] = txtPass.Text;
            //if (picBox.BackgroundImage != null)
            //{
            //    Row["image"] = Helper.ImageTOByte(picBox.BackgroundImage);
            //}
            Row["directPrint"] = rdoDirectPrint.Checked.ToString();
            Row["showBeforePrint"] = rdoShowBeforePrint.Checked.ToString();
            Row["dontShow"] = rdoDontShow.Checked.ToString();
            Row["printTables"] = chBoxTable.Checked.ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("حفظ البيانات", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (cbxPrinter.Text == "")
                {
                    MessageBox.Show("من فضلك تاكد من بيانات الطابعة", "تاكيد");
                    return;
                }


                Properties.Settings.Default.PrinterName = cbxPrinter.Text;
                Properties.Settings.Default.Save();

                saveData();
                ClassLoading loading = new ClassLoading();
                loading.loadSystemOptions();
            }
        }


        //private void btnChoose_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog fileDialog = new OpenFileDialog();
        //    //fileDialog.Filter = "Images|*.png";
        //    if (fileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        txtImage.Text = fileDialog.FileName;
        //        picBox.BackgroundImage = new Bitmap(txtImage.Text);
        //    }
        //}

        
    }
}
