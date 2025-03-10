﻿using System;
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
using POS.Tools;
using Microsoft.Reporting.WinForms;
using Tulpep.NotificationWindow;

namespace POS.Forms
{
    public partial class FormPOSResponsive : Form
    {
        public static FormPOSResponsive instance;
        public FormPOSResponsive()
        {
            InitializeComponent();
            instance = this;
            tableOrderId = "";
        }

        private string tableOrderId;

        private SqlCommand cmd;
        public TextBox txtHiddenClientId;
        public TextBox tableIdHidden;

        //public bool isNewShift = false;
        private void FormPOSResponsive_Load(object sender, EventArgs e)
        {
            
            btnUpdateTable.Visible = false;
            lblShift.Text = declarations.shiftId.ToString();
            fillCategories();
            Helper.fillComboBox(comboRegions, "Select id,name from Regions", "name", "id");
            Helper.fillComboBox(comboTayar, "Select id,name from Tayar", "name", "id");
            lblUserName.Text = declarations.userFullName;
            // combo box OrderType
            DataTable tblPrivilege = new DataTable();
            tblPrivilege.Columns.Add("id", typeof(int));
            tblPrivilege.Columns.Add("OrderType");

            tblPrivilege.Rows.Add(new object[] { 1, "تيك اوي" });
            tblPrivilege.Rows.Add(new object[] { 2, "دليفري" });
            tblPrivilege.Rows.Add(new object[] { 3, "صالة" });

            comboOrderType.DataSource = tblPrivilege;
            comboOrderType.DisplayMember = "OrderType";
            comboOrderType.ValueMember = "id";


            // hidden text box for client id
            txtHiddenClientId = new TextBox();
            txtHiddenClientId.Visible = false;

            // hidden table
            tableIdHidden = new TextBox();
            tableIdHidden.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }

        private void fillCategories()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Categories", adoClass.sqlcn);
            try
            {
                da.Fill(dt);
                DataRow[] drs = dt.Select();
                pnlItems.Controls.Clear();

                for (int i = 0; i < drs.Length; i++)
                {
                    Button catBtn = new Button();
                    catBtn.AccessibleName = "CAT";
                    catBtn.AccessibleDescription = drs[i]["id"].ToString();
                    catBtn.Name = "btnCat" + drs[i]["id"].ToString();
                    catBtn.Text = drs[i]["name"].ToString();
                    catBtn.Size = new Size(150, 150);
                    if(Helper.ByteToImage(drs[i]["image"]) == null)
                    {
                        catBtn.BackColor = Color.IndianRed;
                        catBtn.ForeColor = Color.White;
                    }
                    else
                    {
                        catBtn.BackColor = Color.White;
                    }
                    
                    catBtn.Image = Helper.ByteToImage(drs[i]["image"]);
                    catBtn.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular);
                    catBtn.TextAlign = ContentAlignment.MiddleCenter;
                    catBtn.ImageAlign = ContentAlignment.MiddleCenter;
                    catBtn.RightToLeft = RightToLeft.Yes;
                    catBtn.TextImageRelation = TextImageRelation.ImageAboveText;
                    
                    catBtn.FlatStyle = FlatStyle.Flat;
                    catBtn.FlatAppearance.BorderSize = 0;
                    catBtn.Click += cBtn_Click;
                    pnlItems.Controls.Add(catBtn);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void cBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.AccessibleName == "CAT")
            {
                string CatId = button.AccessibleDescription;
                fillItems(CatId);
            }
            else if (button.AccessibleName == "IT")
            {
                bool flagExist = false;
                bool multiPrice = loadTable("select * from Items where id = '" + button.AccessibleDescription + "'");
                int place = 0;
                if(dgvItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvItems.Rows.Count; i++)
                    {
                        if (button.AccessibleDescription == dgvItems[0, i].Value.ToString() && multiPrice == false)
                        {
                            flagExist = true;
                            place = i;
                        }
                    }
                }
                if (flagExist == false)
                {
                    if (multiPrice)
                    {
                        //string price;
                        FormMultiPriceItem frm = new FormMultiPriceItem();
                        //frm.Show();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {

                            //price = frm._price;

                            // set new price here
                            dgvItems.Rows.Add
                            (new object[]
                                {
                                    button.AccessibleDescription,
                                    "",
                                    frm._price,
                                    1,
                                    frm._price,
                                    button.Text,
                                 }
                            );
                        }
                    }
                    else
                    {
                        dgvItems.Rows.Add(new object[]{
                            button.AccessibleDescription,
                            "",
                            button.Tag,
                            1,
                            button.Tag,
                            button.Text,
                                }
                      );
                    }
                    
                }
                else
                {
                    // quantity
                    int y = 0;
                    int.TryParse(dgvItems[3, place].Value.ToString(), out y);
                    y += 1;
                    dgvItems[3, place].Value = y;
                    // price
                    double price = 0;
                    double.TryParse(dgvItems[4, place].Value.ToString(), out price);
                    // total price
                    double totalValue = y * price;
                    dgvItems[2, place].Value = totalValue;

                    CalcCheck();
                    flagExist = false;
                    place = 0;
                }

            }

            else
            {
                fillCategories();
            }
            CalcCheck();
        }

        ////// check if button item has multi price
        private bool loadTable(string query)
        {
            bool multiPrice = false;
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            cmd = new SqlCommand(query, adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                
                bool.TryParse(row["multiPrice"].ToString(), out multiPrice);
            }
            return multiPrice;

        }

        private void fillItems(string CatId)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("Select * from Items where categoryId = " + CatId, adoClass.sqlcn);

            da.Fill(dt);
            DataRow[] drs = dt.Select();
            pnlItems.Controls.Clear();
            Button catBtn;
            for (int i = 0; i < drs.Length; i++)
            {
                catBtn = new Button();
                catBtn.AccessibleName = "IT";
                catBtn.AccessibleDescription = drs[i]["id"].ToString();
                catBtn.Name = "btnCat" + drs[i]["id"].ToString();

                catBtn.Text = drs[i]["name"].ToString();
                if (Helper.ByteToImage(drs[i]["image"]) == null)
                {
                    catBtn.BackColor = Color.Cyan;
                }
                else
                {
                    catBtn.BackColor = Color.White;
                }

                catBtn.Image = Helper.ByteToImage(drs[i]["image"]);
                catBtn.Tag = drs[i]["price"].ToString();
                catBtn.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                catBtn.TextAlign = ContentAlignment.MiddleCenter;
                catBtn.ImageAlign = ContentAlignment.MiddleCenter;
                catBtn.RightToLeft = RightToLeft.Yes;
                catBtn.TextImageRelation = TextImageRelation.ImageAboveText;
                catBtn.Size = new Size(150, 150);
               
                catBtn.FlatStyle = FlatStyle.Flat;
                catBtn.FlatAppearance.BorderSize = 0;
                catBtn.Click += cBtn_Click;
                pnlItems.Controls.Add(catBtn);

            }

            catBtn = new Button();
            catBtn.AccessibleName = "C";
            catBtn.Name = "btnEnd" + CatId;
            catBtn.Text = "Cancel";
            catBtn.Size = new Size(150, 150);
            catBtn.BackColor = Color.Red;
            catBtn.ForeColor = Color.White;
            catBtn.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular);
            catBtn.Click += cBtn_Click;
            pnlItems.Controls.Add(catBtn);

        }

        private void CalcCheck()
        {
            double x = 0;
            double result = 0;
            for (int i = 0; i < dgvItems.Rows.Count; i++)
            {
                double.TryParse(dgvItems[ColTotal.Index, i].Value.ToString(), out x);
                result += x;
            }

            txtTotal.Text = result.ToString();
        }


        private double TotalCalculations()
        {
            // total under table
            double first_total = 0;
            double.TryParse(txtTotal.Text, out first_total);

            // discount
            double discount = 0;
            double.TryParse(txtDiscount.Text, out discount);

            // tax
            double tax = 0;
            double.TryParse(txtTax.Text, out tax);

            // delivery
            double delivery = 0;
            double.TryParse(txtDelivery.Text, out delivery);

            // final total
            double finalTotal = 0;
            //double.TryParse(txtFinalTotal.Text, out finalTotal);
            finalTotal = (first_total + tax + delivery) - discount;

            return finalTotal;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد تقفيل الوردية", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FormPasswordEndShift frm = new FormPasswordEndShift();
                frm.Show();

            }
            else
            {
                // exit
                Application.Exit();
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                // quantity
                int y = 0;
                int.TryParse(dgvItems.CurrentRow.Cells[3].Value.ToString(), out y);
                y += 1;
                dgvItems.CurrentRow.Cells[3].Value = y;
                // price
                double price = 0;
                double.TryParse(dgvItems.CurrentRow.Cells[4].Value.ToString(), out price);
                // total price
                double totalValue = y * price;
                dgvItems.CurrentRow.Cells[2].Value = totalValue;
            }
            CalcCheck();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                int y = 0;
                int.TryParse(dgvItems.CurrentRow.Cells[3].Value.ToString(), out y);
                if (y > 1)
                {
                    y -= 1;
                    dgvItems.CurrentRow.Cells[3].Value = y;
                }

                // price
                double price = 0;
                double.TryParse(dgvItems.CurrentRow.Cells[4].Value.ToString(), out price);
                // total price
                double totalValue = y * price;
                dgvItems.CurrentRow.Cells[2].Value = totalValue;
            }
            CalcCheck();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            dgvItems.Rows.Clear();
            CalcCheck();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                dgvItems.Rows.Remove(dgvItems.CurrentRow);
            }
            CalcCheck();
        }

        private void comboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboOrderType.Text == "دليفري")
            {
                pnlDelivery.Visible = true;
            }
            else if (comboOrderType.Text == "صالة")
            {
                pnlDelivery.Visible = false;
                TablesPOSForm frm = new TablesPOSForm();
                //frm.Show();
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    tableIdHidden.Text = frm._tableId;

                    DataTable table = new DataTable();
                    cmd = new SqlCommand("select * from vwTablesRoom where id = '" + tableIdHidden.Text + "'", adoClass.sqlcn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    //double nextOrderId = 0;
                    if (table.Rows.Count > 0)
                    {
                        DataRow row = table.Rows[0];
                        string status = row["status"].ToString();
                        string orderId = row["O_Id"].ToString();

                        if (status == "O")
                        {
                            btnUpdateTable.Visible = true;
                            // table is O
                            dgvItems.Rows.Clear();
                            DataTable tableOrderItems = new DataTable();

                            if (adoClass.sqlcn.State != ConnectionState.Open)
                            {
                                adoClass.sqlcn.Open();
                            }
                            cmd = new SqlCommand("select OrderItems.itemId,OrderItems.totalItem,OrderItems.notes,OrderItems.quantity,OrderItems.price,Items.name from OrderItems LEFT JOIN Items on OrderItems.itemId = Items.id  where orderId = '" + orderId + "'", adoClass.sqlcn);
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(tableOrderItems);
                            adoClass.sqlcn.Close();

                            if (tableOrderItems.Rows.Count > 0)
                            {

                                foreach (DataRow r in tableOrderItems.Rows)
                                {

                                    dgvItems.Rows.Add
                                        (new object[]
                                            {
                                            r["itemId"],
                                            r["notes"],
                                            r["totalItem"],
                                            r["quantity"],
                                            r["price"],
                                            r["name"]
                                            }
                                        ); ;
                                }

                                CalcCheck();
                            }


                            //
                            DataTable dd = new DataTable();
                            cmd = new SqlCommand("select tax,discount from Orders  where id = '" + orderId + "'", adoClass.sqlcn);
                            SqlDataAdapter adap = new SqlDataAdapter(cmd);
                            adap.Fill(dd);
                            adoClass.sqlcn.Close();

                            if (dd.Rows.Count > 0)
                            {

                                DataRow newRow = dd.Rows[0];
                                string tax = newRow["tax"].ToString();
                                string discount = newRow["discount"].ToString();
                                txtDiscount.Text = discount;
                                txtTax.Text = tax;
                            }
                        }

                    }
                }
            }
            else
            {
                pnlDelivery.Visible = false;
            }


        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtTax_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtPhone_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                cmd = new SqlCommand("Select Clients.name,Clients.address,Regions.name,Clients.phone,Clients.id from Clients LEFT JOIN Regions on Clients.regionId = Regions.id where phone = '" + txtPhone.Text + "'", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        txtName.Text = row[0].ToString();
                        txtAddress.Text = row[1].ToString();
                        comboRegions.Text = row[2].ToString();
                        txtHiddenClientId.Text = row[4].ToString();
                    }
                }
                else
                {
                    txtName.Text = "";
                    txtAddress.Text = "";
                    comboRegions.Text = "";
                    txtHiddenClientId.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }
        }

        private void txtDelivery_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
        // return order shift id
        private int returnOrderShiftID()
        {
            int orderShiftId;
            DataTable tbl2 = new DataTable();
            tbl2.Clear();
            cmd = new SqlCommand("Select MAX(orderShiftId) from Orders where shiftId = " + declarations.shiftId + "", adoClass.sqlcn);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            adap.Fill(tbl2);
            //int nextOrderId = 0;
            if (tbl2.Rows[0][0].ToString() == DBNull.Value.ToString())
            {
                orderShiftId = 1;
            }
            else
            {
                orderShiftId = Convert.ToInt32(tbl2.Rows[0][0]) + 1;
            }

            return orderShiftId;
        }
        Database db = new Database();
        private void btnDone_Click(object sender, EventArgs e)
        {
            DataTable checkPrinter = new DataTable();
            checkPrinter.Clear();
            checkPrinter = db.readData("select * from Options", "");
            if(checkPrinter.Rows.Count <= 0)
            {
                MessageBox.Show("من فضلك اكمل اعدادت الطباعة");
                return;
            }
            // لو مش مختار ترابيزات
            if (dgvItems.Rows.Count > 0 && tableIdHidden.Text == "")
            {
                FormFatora frm = new FormFatora();
                frm.txtFatoraOrderId.Text = returnOrderShiftID().ToString();
                frm.txtFatoraClientName.Text = txtName.Text;
                frm.txtFatoraTotal.Text = TotalCalculations().ToString();
                frm.Show();
            }
            // لو مختار ترابيزات
            else if (dgvItems.Rows.Count > 0 && tableIdHidden.Text != "")
            {
                DataTable table = new DataTable();
                cmd = new SqlCommand("select * from vwTablesRoom where id = '" + tableIdHidden.Text + "'", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);
                //double nextOrderId = 0;
                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    string status = row["status"].ToString();
                    string orderId = row["O_Id"].ToString();
                    string orderShiftId = row["orderShiftId"].ToString();
                    // لو محجوزة طلع الحساب
                    if (status == "O")
                    {

                        FormFatora frm = new FormFatora();
                        frm.txtFatoraOrderId.Text = orderShiftId;
                        frm.txtFatoraTotal.Text = TotalCalculations().ToString();
                        frm.tableOrder = true;
                        frm.TableOrderIDToPrint = orderId;
                        frm.Show();
                    }
                    // لو مش محجوزة احجزها
                    else
                    {
                        // table is V
                       
                        tableCalculations();
                           
                        comboOrderType.Text = "تيك اوي";

                    }
                }


            }
            else
            {
                MessageBox.Show("لا توجد عناصر");
            }
        }



        private void tableCalculations()
        {
            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                int insertOrderShiftId = returnOrderShiftID();
                string query = "Insert into Orders (userId,dateTime,total,tax,discount,delivery,shiftId,orderType,tableId,status,orderShiftId) values (@userId,@dateTime,@total,@tax,@discount,@delivery,@shiftId,@orderType,@tableId,@status,@orderShiftId); ";
                query += "SELECT @orderId = SCOPE_IDENTITY(); ";

                SqlCommand cmd = new SqlCommand(query, adoClass.sqlcn);
                cmd.Parameters.Add("@orderId", SqlDbType.Int);

                double tax = 0;
                double.TryParse(FormPOSResponsive.instance.txtTax.Text, out tax);

                double discount = 0;
                double.TryParse(FormPOSResponsive.instance.txtDiscount.Text, out discount);

                double delivery = 0;
                double.TryParse(FormPOSResponsive.instance.txtDelivery.Text, out delivery);
                
                cmd.Parameters.AddWithValue("@userId", declarations.userid);
                

                cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@total", double.Parse(TotalCalculations().ToString()));
                cmd.Parameters.AddWithValue("@tax", tax);
                cmd.Parameters.AddWithValue("@discount", discount);
                cmd.Parameters.AddWithValue("@delivery", delivery);
                cmd.Parameters.AddWithValue("@shiftId", declarations.shiftId);
                cmd.Parameters.AddWithValue("@orderType", comboOrderType.Text);
                cmd.Parameters.AddWithValue("@tableId", tableIdHidden.Text);
                cmd.Parameters.AddWithValue("@status", "O");
                cmd.Parameters.AddWithValue("@orderShiftId", insertOrderShiftId);

                //order id
                cmd.Parameters["@orderId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                tableOrderId = cmd.Parameters["@orderId"].Value.ToString();
                saveItems(tableOrderId);


               

                if (dgvItems.Rows.Count > 0)
                {
                    for (int p = 0; p < Properties.Settings.Default.KitchenNumber; p++)
                    {
                        dsTables tables = new dsTables();
                        for (int i = 0; i < dgvItems.Rows.Count; i++)
                        {
                            DataRow dro = tables.Tables["dtTables"].NewRow();
                            dro["itemName"] = dgvItems[5, i].Value;
                            dro["itemQuantity"] = dgvItems[3, i].Value;
                            dro["notes"] = dgvItems[1, i].Value;
                            tables.Tables["dtTables"].Rows.Add(dro);
                        }

                        FormReports rptForm = new FormReports();
                        rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportTable.rdlc";
                        rptForm.mainReport.LocalReport.DataSources.Clear();
                        rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tables.Tables["dtTables"]));

                        ReportParameter[] reportParameters = new ReportParameter[3];
                        reportParameters[0] = new ReportParameter("orderId", insertOrderShiftId.ToString());
                        reportParameters[1] = new ReportParameter("TableId", tableIdHidden.Text);
                        reportParameters[2] = new ReportParameter("dateTime", DateTime.Now.ToString());


                        if (Properties.Settings.Default.DirectPrint && Properties.Settings.Default.PrintKitchen)
                        {

                            LocalReport report = new LocalReport();
                            string path = Application.StartupPath + @"\Reports\ReportTable.rdlc";
                            report.ReportPath = path;
                            report.DataSources.Clear();
                            report.DataSources.Add(new ReportDataSource("DataSet1", tables.Tables["dtTables"]));
                            report.SetParameters(reportParameters);
                            PrintersClass pC = new PrintersClass(Properties.Settings.Default.KitchenPrinter);
                            pC.PrintToPrinter(report);
                        }
                        else if (Properties.Settings.Default.ShowBeforePrint && Properties.Settings.Default.PrintKitchen)
                        {
                            rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                            rptForm.ShowDialog();
                        }
                    }

                }
                else
                {
                    MessageBox.Show("لا يوجد عناصر لعرضها");
                }


                dgvItems.Rows.Clear();
                txtTax.Text = "0";
                txtDiscount.Text = "0";
                txtDelivery.Text = "0";
                txtPhone.Text = "";
                txtName.Text = "";
                comboRegions.Text = "";
                txtAddress.Text = "";
                txtTotal.Text = "";

                tableIdHidden.Text = "";
                btnUpdateTable.Visible = false;



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

        }

        private void saveItems(string orderId)
        {
            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                SqlCommand cmd;
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into OrderItems (orderId,itemId,quantity,price,totalItem,notes) values (@orderId,@itemId,@quantity,@price,@totalItem,@notes)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.Parameters.AddWithValue("@itemId", dgvItems[0, i].Value);
                    cmd.Parameters.AddWithValue("@quantity", dgvItems[3, i].Value);
                    cmd.Parameters.AddWithValue("@price", dgvItems[4, i].Value);
                    cmd.Parameters.AddWithValue("@totalItem", dgvItems[2, i].Value);
                    cmd.Parameters.AddWithValue("@notes", dgvItems[1, i].Value);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }
        }


        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                DataTable table = new DataTable();
                cmd = new SqlCommand("select * from vwTablesRoom where id = '" + tableIdHidden.Text + "'", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);

                if (table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    string orderId = row["O_Id"].ToString();
                    string orderShiftId = row["orderShiftId"].ToString();
                    // update order total-tax-discount
                    cmd = new SqlCommand("Update Orders set total = @total,tax=@tax,discount=@discount Where id = '" + orderId + "'", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@total", double.Parse(TotalCalculations().ToString()));
                    cmd.Parameters.AddWithValue("@tax", double.Parse(txtTax.Text));
                    cmd.Parameters.AddWithValue("@discount", double.Parse(txtDiscount.Text));
                    cmd.ExecuteNonQuery();

                    // old data in order 
                    DataTable oldDataTable = new DataTable();


                    cmd = new SqlCommand("select * from OrderItems where orderId = '" + orderId + "'", adoClass.sqlcn);
                    SqlDataAdapter oldDataAdapter = new SqlDataAdapter(cmd);
                    oldDataAdapter.Fill(oldDataTable);
                    // end of old data in order

                    //foreach (DataRow r in dgvItems.Rows)
                    //{
                    //    if(oldDataTable.Rows.Contains())
                    //}

                    DataTable newPrintingTable = new DataTable();
                    newPrintingTable.Columns.Add("Column1");
                    newPrintingTable.Columns.Add("Column2");
                    newPrintingTable.Columns.Add("Column3");

                    int quan = 0;
                    bool founded = false;
                    for (int i = 0; i < dgvItems.Rows.Count; i++)
                    {
                        founded = false;
                        foreach (DataRow rr in oldDataTable.Rows)
                        {
                            if (Convert.ToInt32(dgvItems[0, i].Value) == Convert.ToInt32(rr[2]))
                            {
                                if(Convert.ToInt32(dgvItems[3, i].Value) > Convert.ToInt32(rr[3]))
                                {
                                    quan = Convert.ToInt32(dgvItems[3, i].Value) - Convert.ToInt32(rr[3]);
                                    newPrintingTable.Rows.Add(dgvItems[5, i].Value, quan, dgvItems[1, i].Value);
                                }
                                founded = true;
                                break;
                            }
                        }
                        if (!founded)
                        {
                            newPrintingTable.Rows.Add(dgvItems[5, i].Value, dgvItems[3, i].Value, dgvItems[1, i].Value);
                        }
                    }

                    cmd = new SqlCommand("delete from OrderItems where orderId = '" + orderId + "'", adoClass.sqlcn);
                    cmd.ExecuteNonQuery();
                    for (int i = 0; i < dgvItems.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("Insert into OrderItems (orderId,itemId,quantity,price,totalItem,notes) values (@orderId,@itemId,@quantity,@price,@totalItem,@notes)", adoClass.sqlcn);
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.Parameters.AddWithValue("@itemId", dgvItems[0, i].Value);
                        cmd.Parameters.AddWithValue("@quantity", dgvItems[3, i].Value);
                        cmd.Parameters.AddWithValue("@price", dgvItems[4, i].Value);
                        cmd.Parameters.AddWithValue("@totalItem", dgvItems[2, i].Value);
                        cmd.Parameters.AddWithValue("@notes", dgvItems[1, i].Value);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    // print table order

                    for (int p = 0; p < Properties.Settings.Default.KitchenNumber; p++)
                    {
                        dsTables tables = new dsTables();
                        foreach (DataRow rrr in newPrintingTable.Rows)
                        {
                            DataRow dro = tables.Tables["dtTables"].NewRow();
                            dro["itemName"] = rrr[0];
                            dro["itemQuantity"] = rrr[1];
                            dro["notes"] = rrr[2];

                            //dro["itemName"] = dgvItems[5, i].Value;
                            //dro["itemQuantity"] = dgvItems[3, i].Value;
                            //dro["notes"] = dgvItems[1, i].Value;
                            tables.Tables["dtTables"].Rows.Add(dro);
                        }

                        //for (int i = 0; i < dgvItems.Rows.Count; i++)
                        //{
                        //    DataRow dro = tables.Tables["dtTables"].NewRow();
                        //    dro["itemName"] = dgvItems[5, i].Value;
                        //    dro["itemQuantity"] = dgvItems[3, i].Value;
                        //    dro["notes"] = dgvItems[1, i].Value;

                        //    //dro["itemName"] = dgvItems[5, i].Value;
                        //    //dro["itemQuantity"] = dgvItems[3, i].Value;
                        //    //dro["notes"] = dgvItems[1, i].Value;
                        //    tables.Tables["dtTables"].Rows.Add(dro);
                        //}

                        FormReports rptForm = new FormReports();
                        rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportTable.rdlc";
                        rptForm.mainReport.LocalReport.DataSources.Clear();
                        rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tables.Tables["dtTables"]));

                        ReportParameter[] reportParameters = new ReportParameter[3];
                        reportParameters[0] = new ReportParameter("orderId", orderShiftId);
                        reportParameters[1] = new ReportParameter("TableId", tableIdHidden.Text);
                        reportParameters[2] = new ReportParameter("dateTime", DateTime.Now.ToString());


                        if (Properties.Settings.Default.DirectPrint && Properties.Settings.Default.PrintKitchen)
                        {
                            LocalReport report = new LocalReport();
                            string path = Application.StartupPath + @"\Reports\ReportTable.rdlc";
                            report.ReportPath = path;
                            report.DataSources.Clear();
                            report.DataSources.Add(new ReportDataSource("DataSet1", tables.Tables["dtTables"]));
                            report.SetParameters(reportParameters);
                            PrintersClass pC = new PrintersClass(Properties.Settings.Default.KitchenPrinter);
                            pC.PrintToPrinter(report);
                        }
                        else if (Properties.Settings.Default.ShowBeforePrint && Properties.Settings.Default.PrintKitchen)
                        {
                            rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                            rptForm.ShowDialog();
                        }
                        
                    }
                    // end of print table order
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }
            
                

            dgvItems.Rows.Clear();
            txtTax.Text = "0";
            txtDiscount.Text = "0";
            txtDelivery.Text = "0";
            txtPhone.Text = "";
            txtName.Text = "";
            comboRegions.Text = "";
            txtAddress.Text = "";
            txtTotal.Text = "";
            comboOrderType.Text = "تيك اوي";
            tableIdHidden.Text = "";
            btnUpdateTable.Visible = false;
            btnUpdateTable.Visible = false;
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            dgvItems.Rows.Clear();
            CalcCheck();
        }

        private void btnEnterQuantity_Click(object sender, EventArgs e)
        {
            if (dgvItems.Rows.Count > 0)
            {
                FormMultiPriceItem frm = new FormMultiPriceItem();
                frm.lblTitle.Text = "ادخل عدد";
                frm.lblInfo.Text = "العدد";
                frm.isQuantity = true;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dgvItems.CurrentRow.Cells[3].Value = frm._price;
                }

                int y = 0;
                int.TryParse(dgvItems.CurrentRow.Cells[3].Value.ToString(), out y);
                

                // price
                double price = 0;
                double.TryParse(dgvItems.CurrentRow.Cells[4].Value.ToString(), out price);
                // total price
                double totalValue = y * price;
                dgvItems.CurrentRow.Cells[2].Value = totalValue;
            }
            CalcCheck();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        // fill items to search
        private void fillItemsSearch(string q)
        {
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(q, adoClass.sqlcn);

            da.Fill(dt);
            DataRow[] drs = dt.Select();
            pnlItems.Controls.Clear();
            Button catBtn;
            for (int i = 0; i < drs.Length; i++)
            {
                catBtn = new Button();
                catBtn.AccessibleName = "IT";
                catBtn.AccessibleDescription = drs[i]["id"].ToString();
                catBtn.Name = "btnCat" + drs[i]["id"].ToString();

                catBtn.Text = drs[i]["name"].ToString();
                if (Helper.ByteToImage(drs[i]["image"]) == null)
                {
                    catBtn.BackColor = Color.Cyan;
                }
                else
                {
                    catBtn.BackColor = Color.White;
                }

                catBtn.Image = Helper.ByteToImage(drs[i]["image"]);
                catBtn.Tag = drs[i]["price"].ToString();
                catBtn.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                catBtn.TextAlign = ContentAlignment.MiddleCenter;
                catBtn.ImageAlign = ContentAlignment.MiddleCenter;
                catBtn.RightToLeft = RightToLeft.Yes;
                catBtn.TextImageRelation = TextImageRelation.ImageAboveText;
                catBtn.Size = new Size(150, 150);

                catBtn.FlatStyle = FlatStyle.Flat;
                catBtn.FlatAppearance.BorderSize = 0;
                catBtn.Click += cBtn_Click;
                pnlItems.Controls.Add(catBtn);

            }

            catBtn = new Button();
            catBtn.AccessibleName = "C";
            catBtn.Name = "btnEndCancel";
            catBtn.Text = "Cancel";
            catBtn.Size = new Size(150, 150);
            catBtn.BackColor = Color.Red;
            catBtn.ForeColor = Color.White;
            catBtn.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular);
            catBtn.Click += cBtn_Click;
            pnlItems.Controls.Add(catBtn);

        }
        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                fillCategories();
            }
            else
            {
                fillItemsSearch("Select * from Items where name like '%" + text + "%'");
            }
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            FormExpenses frm = new FormExpenses();
            frm.Show();
        }

        private void btnTayar_Click(object sender, EventArgs e)
        {
            FormShowTayarPOS frm = new FormShowTayarPOS();
            frm.Show();
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            FormAttendingLeaving frm = new FormAttendingLeaving();
            frm.Show();
        }
    }
}
