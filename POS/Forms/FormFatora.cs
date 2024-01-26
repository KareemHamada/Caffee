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
using System.Data.SqlClient;
using POS.Tools;
using Microsoft.Reporting.WinForms;

namespace POS.Forms
{
    public partial class FormFatora : Form
    {
        public FormFatora()
        {
            InitializeComponent();
            orderId = "";
        }
        private string orderId;
        public bool tableOrder = false;
        public string TableOrderIDToPrint = "";
        private SqlCommand cmd;


        private void FormFatora_Load(object sender, EventArgs e)
        {
            txtFatoraPaid.Focus();
            
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
                for (int i = 0; i < FormPOSResponsive.instance.dgvItems.Rows.Count; i++)
                {

                    cmd = new SqlCommand("Insert into OrderItems (orderId,itemId,quantity,price,totalItem,dateTime,notes) values (@orderId,@itemId,@quantity,@price,@totalItem,@dateTime,@notes)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.Parameters.AddWithValue("@itemId", FormPOSResponsive.instance.dgvItems[0, i].Value);
                    cmd.Parameters.AddWithValue("@quantity", FormPOSResponsive.instance.dgvItems[3, i].Value);
                    cmd.Parameters.AddWithValue("@price", FormPOSResponsive.instance.dgvItems[4, i].Value);
                    cmd.Parameters.AddWithValue("@totalItem", FormPOSResponsive.instance.dgvItems[2, i].Value);
                    cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@notes", FormPOSResponsive.instance.dgvItems[1, i].Value);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    // update final report items at end shift
                    itemsEndShift(int.Parse(FormPOSResponsive.instance.dgvItems[0, i].Value.ToString()),int.Parse(FormPOSResponsive.instance.dgvItems[3, i].Value.ToString()),Convert.ToDecimal(FormPOSResponsive.instance.dgvItems[2, i].Value));

                    // update total quantity of system
                    DataTable dt = new DataTable();
                    cmd = new SqlCommand("Select quantity from Items where id = '" + FormPOSResponsive.instance.dgvItems[0, i].Value + "'", adoClass.sqlcn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        object quantity = row["quantity"];

                        if (quantity != DBNull.Value)
                        {
                            double totalQuantity = 0;
                            double oldQuan = 0;
                            double.TryParse(quantity.ToString(), out oldQuan);
                            double plusQuan = 0;
                            double.TryParse(FormPOSResponsive.instance.dgvItems[3, i].Value.ToString(), out plusQuan);

                            totalQuantity = oldQuan + plusQuan;

                            cmd = new SqlCommand("update Items set quantity = '" + totalQuantity + "' where id = '" + FormPOSResponsive.instance.dgvItems[0, i].Value.ToString() + "'", adoClass.sqlcn);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            double totalQuantity = 0;
                            double oldQuan = 0;
                            double plusQuan = 0;
                            double.TryParse(FormPOSResponsive.instance.dgvItems[3, i].Value.ToString(), out plusQuan);

                            totalQuantity = oldQuan + plusQuan;

                            cmd = new SqlCommand("update Items set quantity = '" + totalQuantity + "' where id = '" + FormPOSResponsive.instance.dgvItems[0, i].Value.ToString() + "'", adoClass.sqlcn);
                            cmd.ExecuteNonQuery();
                        }
                    }



                    // update store and items relation
                    int qty = Convert.ToInt32(FormPOSResponsive.instance.dgvItems[3, i].Value);
                    int itemID = Convert.ToInt32(FormPOSResponsive.instance.dgvItems[0, i].Value);
                    Helper.checkRelation(itemID,qty);

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


       private void itemsEndShift(int itemId,int quantity,decimal total)
        {
            try
            {
               
                SqlCommand cmd;
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select quan,total from ItemQuantityEndShift where itemId = '" + itemId + "' and shiftId = '" + declarations.shiftId + "'", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    object tableQuantity = row["quan"];
                    object tableTotal = row["total"];

                    if (tableQuantity != DBNull.Value)
                    {
                        // get total quantity
                        int totalQuantity = 0;
                        int oldQuan = 0;
                        int.TryParse(tableQuantity.ToString(), out oldQuan);
                        totalQuantity = oldQuan + quantity;

                        // get total price
                        decimal totalPrice = 0;
                        decimal oldTotal = 0;
                        decimal.TryParse(tableTotal.ToString(), out oldTotal);
                        totalPrice = oldTotal + total;

                        cmd = new SqlCommand("update ItemQuantityEndShift set quan = '" + totalQuantity + "',total='"+ totalPrice + "' where itemId = '" + itemId + "' and shiftId = '" + declarations.shiftId + "'", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();
                    }
                    
                }

                else
                {
                    int totalQuantity = 0;
                    int oldQuan = 0;
                    totalQuantity = oldQuan + quantity;

                    // get total price
                    decimal totalPrice = 0;
                    decimal oldTotal = 0;
                    totalPrice = oldTotal + total;

                    cmd = new SqlCommand("insert into ItemQuantityEndShift (itemId,quan,shiftId,total) values(@itemId,@quan,@shiftId,@total)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@itemId", itemId);
                    cmd.Parameters.AddWithValue("@quan", quantity);
                    cmd.Parameters.AddWithValue("@shiftId", declarations.shiftId);
                    cmd.Parameters.AddWithValue("@total", totalPrice);

                    cmd.ExecuteNonQuery();

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }



        private void txtFatoraPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void txtFatoraPaid_KeyUp(object sender, KeyEventArgs e)
        {
            // total
            double total = 0;
            double.TryParse(txtFatoraTotal.Text, out total);

            // paid
            double paid = 0;
            double.TryParse(txtFatoraPaid.Text, out paid);

            // calc remain
            double remain = 0;

            remain = paid - total;
            txtFatoraRemain.Text = remain.ToString();
        }

        private string returnClientID()
        {
            string clientNewId = "";

            if (FormPOSResponsive.instance.txtHiddenClientId.Text == "" && (FormPOSResponsive.instance.txtName.Text != "" || FormPOSResponsive.instance.txtPhone.Text != ""))
            {
                string queryNewClient = "Insert into Clients (name,phone,regionId,address) values (@name,@phone,@regionId,@address); ";
                queryNewClient += "SELECT @clientNewId = SCOPE_IDENTITY(); ";

                SqlCommand command = new SqlCommand(queryNewClient, adoClass.sqlcn);
                command.Parameters.Add("@clientNewId", SqlDbType.Int);


                if (FormPOSResponsive.instance.txtName.Text != "")
                {
                    command.Parameters.AddWithValue("@name", FormPOSResponsive.instance.txtName.Text);
                }
                else
                {
                    command.Parameters.AddWithValue("@name", DBNull.Value);
                }

                if (FormPOSResponsive.instance.txtPhone.Text != "")
                {
                    command.Parameters.AddWithValue("@phone", FormPOSResponsive.instance.txtPhone.Text);
                }
                else
                {
                    command.Parameters.AddWithValue("@phone", DBNull.Value);
                }

                if (FormPOSResponsive.instance.comboRegions.Text != "")
                {
                    command.Parameters.AddWithValue("@regionId", FormPOSResponsive.instance.comboRegions.SelectedValue);
                }
                else
                {
                    command.Parameters.AddWithValue("@regionId", DBNull.Value);
                }

                if (FormPOSResponsive.instance.comboRegions.Text != "")
                {
                    command.Parameters.AddWithValue("@address", FormPOSResponsive.instance.txtAddress.Text);
                }
                else
                {
                    command.Parameters.AddWithValue("@address", DBNull.Value);
                }

                //client id
                command.Parameters["@clientNewId"].Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();

                clientNewId = command.Parameters["@clientNewId"].Value.ToString();

            }

            return clientNewId;
        }

        // order ID
        private int returnOrderID()
        {
            DataTable tbl = new DataTable();
            tbl.Clear();
            cmd = new SqlCommand("Select TOP 1 * from Orders order by id DESC", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tbl);
            int nextOrderId = 0;
            if (tbl.Rows.Count > 0)
            {
                DataRow row = tbl.Rows[0];
                object orderId = row["id"];

                if (orderId != DBNull.Value)
                {
                    int oldOrderId = 0;
                    int.TryParse(orderId.ToString(), out oldOrderId);
                    nextOrderId = oldOrderId + 1;
                }
                else
                {
                    int oldOrderId = 0;
                    nextOrderId = oldOrderId + 1;
                }
            }
            else
            {
                nextOrderId = 1;
            }

            return nextOrderId;
        }

        // return order shift id
        private int returnOrderShiftID()
        {
            int orderShiftId;
            DataTable tbl2 = new DataTable();
            tbl2.Clear();
            cmd = new SqlCommand("Select MAX(orderShiftId) from Orders where shiftId = " + declarations.shiftId+"", adoClass.sqlcn);
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (FormPOSResponsive.instance.dgvItems.Rows.Count > 0)
            {
                try
                {
                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    double tax = 0;
                    double.TryParse(FormPOSResponsive.instance.txtTax.Text, out tax);

                    double discount = 0;
                    double.TryParse(FormPOSResponsive.instance.txtDiscount.Text, out discount);

                    double delivery = 0;
                    double.TryParse(FormPOSResponsive.instance.txtDelivery.Text, out delivery);

                    if (tableOrder == false)
                    {

                        // return id of client if enterd
                        string clientNewId = returnClientID();

                        string query = "Insert into Orders (userId,clientId,dateTime,total,tax,discount,delivery,shiftId,orderType,tayarId,orderShiftId) values (@userId,@clientId,@dateTime,@total,@tax,@discount,@delivery,@shiftId,@orderType,@tayarId,@orderShiftId); ";
                        query += "SELECT @orderId = SCOPE_IDENTITY(); ";

                        SqlCommand cmd = new SqlCommand(query, adoClass.sqlcn);
                        cmd.Parameters.Add("@orderId", SqlDbType.Int);



                        cmd.Parameters.AddWithValue("@userId", declarations.userid);
                        if (FormPOSResponsive.instance.txtHiddenClientId.Text != "")
                        {
                            cmd.Parameters.AddWithValue("@clientId", FormPOSResponsive.instance.txtHiddenClientId.Text);
                        }
                        else if (FormPOSResponsive.instance.txtHiddenClientId.Text == "" && (FormPOSResponsive.instance.txtName.Text != "" || FormPOSResponsive.instance.txtPhone.Text != ""))
                        {
                            cmd.Parameters.AddWithValue("@clientId", clientNewId);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@clientId", DBNull.Value);
                        }

                        cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@total", double.Parse(txtFatoraTotal.Text));
                        cmd.Parameters.AddWithValue("@tax", tax);
                        cmd.Parameters.AddWithValue("@discount", discount);
                        cmd.Parameters.AddWithValue("@delivery", delivery);
                        cmd.Parameters.AddWithValue("@shiftId", declarations.shiftId);
                        cmd.Parameters.AddWithValue("@orderType", FormPOSResponsive.instance.comboOrderType.Text);
                        cmd.Parameters.AddWithValue("@tayarId", FormPOSResponsive.instance.comboTayar.SelectedValue);
                        cmd.Parameters.AddWithValue("@orderShiftId", returnOrderShiftID());

                        //order id
                        cmd.Parameters["@orderId"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();

                        orderId = cmd.Parameters["@orderId"].Value.ToString();
                        saveItems(orderId);


                        printChecks checks = new printChecks();
                        for(int i = 0; i < Properties.Settings.Default.FatoraNumber; i++)
                        {
                            checks.runPrintCheck(int.Parse(orderId));
                        }

                        // print to kitchen
                        if (Properties.Settings.Default.PrintFatoraToKitchen)
                        {
                            for (int p = 0; p < Properties.Settings.Default.KitchenNumber; p++)
                            {
                                dsTables tables = new dsTables();
                                for (int i = 0; i < FormPOSResponsive.instance.dgvItems.Rows.Count; i++)
                                {
                                    DataRow dro = tables.Tables["dtTables"].NewRow();
                                    dro["itemName"] = FormPOSResponsive.instance.dgvItems[5, i].Value;
                                    dro["itemQuantity"] = FormPOSResponsive.instance.dgvItems[3, i].Value;
                                    dro["notes"] = FormPOSResponsive.instance.dgvItems[1, i].Value;
                                    tables.Tables["dtTables"].Rows.Add(dro);
                                }

                                FormReports rptForm = new FormReports();
                                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.Reportkitchen.rdlc";
                                rptForm.mainReport.LocalReport.DataSources.Clear();
                                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tables.Tables["dtTables"]));

                                ReportParameter[] reportParameters = new ReportParameter[2];
                                reportParameters[0] = new ReportParameter("orderId", txtFatoraOrderId.Text);
                                reportParameters[1] = new ReportParameter("dateTime", DateTime.Now.ToString());


                                if (Properties.Settings.Default.DirectPrint && Properties.Settings.Default.PrintKitchen)
                                {
                                    LocalReport report = new LocalReport();
                                    string path = Application.StartupPath + @"\Reports\Reportkitchen.rdlc";
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


                        FormPOSResponsive.instance.dgvItems.Rows.Clear();
                        FormPOSResponsive.instance.txtTax.Text = "0";
                        FormPOSResponsive.instance.txtDiscount.Text = "0";
                        FormPOSResponsive.instance.txtDelivery.Text = "0";
                        FormPOSResponsive.instance.txtPhone.Text = "";
                        FormPOSResponsive.instance.txtName.Text = "";
                        FormPOSResponsive.instance.comboRegions.Text = "";
                        FormPOSResponsive.instance.comboTayar.Text = "";
                        FormPOSResponsive.instance.txtAddress.Text = "";
                        FormPOSResponsive.instance.comboOrderType.Text = "تيك اوي";
                        FormPOSResponsive.instance.txtTotal.Text = "";

                        this.Close();

                    }
                    else
                    {
                        orderId = TableOrderIDToPrint;
                        try
                        {
                            // Clients (name,phone,regionId,address)
                            cmd = new SqlCommand("Update Orders set total = @total,tax=@tax,discount=@discount, status = @status,tableId=@tableId Where id = '" + orderId + "'", adoClass.sqlcn);

                            cmd.Parameters.AddWithValue("@status", "V");
                            cmd.Parameters.AddWithValue("@tableId", 0);
                            cmd.Parameters.AddWithValue("@total", double.Parse(txtFatoraTotal.Text));
                            cmd.Parameters.AddWithValue("@tax", tax);
                            cmd.Parameters.AddWithValue("@discount", discount);

                            if (adoClass.sqlcn.State != ConnectionState.Open)
                            {
                                adoClass.sqlcn.Open();
                            }

                            cmd.ExecuteNonQuery();


                            cmd = new SqlCommand("delete from OrderItems Where OrderId = '" + orderId + "'", adoClass.sqlcn);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            adoClass.sqlcn.Close();
                        }


                        saveItems(orderId);

                        FormPOSResponsive.instance.dgvItems.Rows.Clear();
                        FormPOSResponsive.instance.txtTax.Text = "0";
                        FormPOSResponsive.instance.txtDiscount.Text = "0";
                        FormPOSResponsive.instance.txtDelivery.Text = "0";
                        FormPOSResponsive.instance.txtPhone.Text = "";
                        FormPOSResponsive.instance.txtName.Text = "";
                        FormPOSResponsive.instance.comboRegions.Text = "";
                        FormPOSResponsive.instance.txtAddress.Text = "";
                        FormPOSResponsive.instance.comboOrderType.Text = "تيك اوي";
                        FormPOSResponsive.instance.tableIdHidden.Text = "";
                        FormPOSResponsive.instance.btnUpdateTable.Visible = false;
                        FormPOSResponsive.instance.txtTotal.Text = "";

                        tableOrder = false;

                        //MessageBox.Show("تم");

                        this.Close();

                        printChecks checks = new printChecks();
                        for (int i = 0; i < Properties.Settings.Default.FatoraNumber; i++)
                        {
                            checks.runPrintCheck(int.Parse(orderId));
                        }
                            
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
            else
            {
                MessageBox.Show("لا توجد عناصر");
                this.Close();
            }
        }

        private void FormFatora_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tableOrder == true)
            {
                //Close();
                FormPOSResponsive.instance.dgvItems.Rows.Clear();
                FormPOSResponsive.instance.txtTax.Text = "0";
                FormPOSResponsive.instance.txtDiscount.Text = "0";
                FormPOSResponsive.instance.txtDelivery.Text = "0";
                FormPOSResponsive.instance.txtPhone.Text = "";
                FormPOSResponsive.instance.txtName.Text = "";
                FormPOSResponsive.instance.comboRegions.Text = "";
                FormPOSResponsive.instance.txtAddress.Text = "";
                FormPOSResponsive.instance.comboOrderType.Text = "تيك اوي";
                FormPOSResponsive.instance.tableIdHidden.Text = "";
                FormPOSResponsive.instance.btnUpdateTable.Visible = false;
                FormPOSResponsive.instance.txtTotal.Text = "";

                tableOrder = false;

            }
            //else
            //{
            //    Close();
            //}
        }
    }
}
