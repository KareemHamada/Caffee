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
        private SqlCommand cmd;
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (tableOrder == true)
            {
                Close();
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
            else
            {
                Close();
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

        private void FormFatora_Load(object sender, EventArgs e)
        {
            txtFatoraPaid.Focus();
            
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

        private void btnPrint_Click(object sender, EventArgs e)
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


                    /////


                    string query = "Insert into Orders (userId,clientId,dateTime,total,tax,discount,delivery,shiftId,orderType,tayarId) values (@userId,@clientId,@dateTime,@total,@tax,@discount,@delivery,@shiftId,@orderType,@tayarId); ";
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

                    //order id
                    cmd.Parameters["@orderId"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();

                    orderId = cmd.Parameters["@orderId"].Value.ToString();
                    saveItems(orderId);

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

                    MessageBox.Show("تم");
                    this.Close();

                    printChecks checks = new printChecks();
                    checks.runPrintCheck(int.Parse(orderId));
                }
                else
                {
                    // 
                    orderId = txtFatoraOrderId.Text;
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

                    MessageBox.Show("تم");

                    this.Close();

                    printChecks checks = new printChecks();
                    
                    checks.runPrintCheck(int.Parse(orderId));
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

                    cmd = new SqlCommand("Insert into OrderItems (orderId,itemId,quantity,price,totalItem,dateTime) values (@orderId,@itemId,@quantity,@price,@totalItem,@dateTime)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.Parameters.AddWithValue("@itemId", FormPOSResponsive.instance.dgvItems[0, i].Value);
                    cmd.Parameters.AddWithValue("@quantity", FormPOSResponsive.instance.dgvItems[3, i].Value);
                    cmd.Parameters.AddWithValue("@price", FormPOSResponsive.instance.dgvItems[4, i].Value);
                    cmd.Parameters.AddWithValue("@totalItem", FormPOSResponsive.instance.dgvItems[2, i].Value);
                    cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    // update final report items at end shift
                    itemsEndShift(int.Parse(FormPOSResponsive.instance.dgvItems[0, i].Value.ToString()),int.Parse(FormPOSResponsive.instance.dgvItems[3, i].Value.ToString()));

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


       private void itemsEndShift(int itemId,int quantity)
        {
            try
            {
               
                SqlCommand cmd;
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select quan from ItemQuantityEndShift where itemId = '" + itemId + "' and shiftId = '" + declarations.shiftId + "'", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    object tableQuantity = row["quan"];

                    if (tableQuantity != DBNull.Value)
                    {
                        int totalQuantity = 0;
                        int oldQuan = 0;
                        int.TryParse(tableQuantity.ToString(), out oldQuan);
                        
                        totalQuantity = oldQuan + quantity;

                        cmd = new SqlCommand("update ItemQuantityEndShift set quan = '" + totalQuantity + "' where itemId = '" + itemId + "' and shiftId = '" + declarations.shiftId + "'", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        
                    }
                    
                }

                else
                {
                    int totalQuantity = 0;
                    int oldQuan = 0;
                    
                    totalQuantity = oldQuan + quantity;

                    cmd = new SqlCommand("insert into ItemQuantityEndShift (itemId,quan,shiftId) values(@itemId,@quan,@shiftId)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@itemId", itemId);
                    cmd.Parameters.AddWithValue("@quan", quantity);
                    cmd.Parameters.AddWithValue("@shiftId", declarations.shiftId);

                    cmd.ExecuteNonQuery();

                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

    }
}
