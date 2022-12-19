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

namespace POS.Forms
{
    public partial class FormDeleteOrder : Form
    {
        public FormDeleteOrder()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private SqlDataAdapter da;

        private void FormDeleteOrder_Load(object sender, EventArgs e)
        {
            loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar,Orders.orderShiftId from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id");
        }
        private void loadTable(string query)
        {
            dgvLoading.Rows.Clear();
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

                foreach (DataRow row in dt.Rows)
                {
                    dgvLoading.Rows.Add
                        (new object[]
                            {
                            Properties.Resources.dgv,
                            row["tayar"],
                            row["name"],
                            row["total"],
                            row["discount"],
                            row["tax"],
                            row["delivery"],
                            row["orderType"],
                            row["dateTime"],
                            row["fullName"],
                            row["shiftId"],
                            row["id"],
                            row["orderShiftId"],
                            }
                        ); ;
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvLoading_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLoading.CurrentCell.ColumnIndex.Equals(0) && e.RowIndex != -1)
            {
                if (dgvLoading.CurrentCell != null && dgvLoading.CurrentCell.Value != null)
                {
                    string orderId = dgvLoading.CurrentRow.Cells[11].Value.ToString();
                    FormOrderItems frm = new FormOrderItems();
                    frm.Show();
                    frm.showOrderItems(orderId);
                    frm.lblDelivery.Text = dgvLoading.CurrentRow.Cells[6].Value.ToString();
                    frm.lblDiscount.Text = dgvLoading.CurrentRow.Cells[4].Value.ToString();
                    frm.lblOrderId.Text = orderId;
                    frm.lblOrderType.Text = dgvLoading.CurrentRow.Cells[7].Value.ToString();
                    frm.lblTax.Text = dgvLoading.CurrentRow.Cells[5].Value.ToString();
                    frm.lblTotal.Text = dgvLoading.CurrentRow.Cells[3].Value.ToString();

                }

            }
            else if(dgvLoading.CurrentCell.ColumnIndex.Equals(13) && e.RowIndex != -1)
            {
                
                string orderId = dgvLoading.CurrentRow.Cells[11].Value.ToString();
                string shiftId = dgvLoading.CurrentRow.Cells[10].Value.ToString();
                if (MessageBox.Show("هل تريد حذف الاوردر", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        if (adoClass.sqlcn.State != ConnectionState.Open)
                        {
                            adoClass.sqlcn.Open();
                        }

                        DataTable dt = new DataTable();
                        cmd = new SqlCommand("Select quantity,itemId from OrderItems where orderId = '" + orderId + "'", adoClass.sqlcn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);

                        //int oldQuantity = int.Parse(dt.Rows[0][0].ToString());
                        //int itemId = int.Parse(dt.Rows[0][1].ToString());

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                int oldQuantity = int.Parse(dt.Rows[i][0].ToString());
                                int itemId = int.Parse(dt.Rows[i][1].ToString());

                                deleteItems(itemId, oldQuantity);
                                DeleteditemsEndShift(itemId, oldQuantity, shiftId);
                            }

                        }

                        //deleteItems(itemId, oldQuantity);


                        cmd = new SqlCommand("delete from OrderItems Where orderId = '" + orderId + "'", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();


                        cmd = new SqlCommand("delete from Orders Where id = '" + orderId + "'", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();



                        // update shift values
                        Helper.updateShiftValues(shiftId,1);

                        MessageBox.Show("تم الحذف بنجاح");


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

                loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar,Orders.orderShiftId from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id");
            }
        }


        private void deleteItems(int itemId,int deletedQuantity)
        {
            try
            {
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select quantity from Items where id = '" + itemId + "'", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                int oldQuantity = int.Parse(dt.Rows[0][0].ToString());

                int totalQuantity = oldQuantity - deletedQuantity;

                cmd = new SqlCommand("update Items set quantity = '" + totalQuantity + "' where id = '" + itemId + "'", adoClass.sqlcn);
                cmd.ExecuteNonQuery();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }




        private void DeleteditemsEndShift(int itemId, int quantity, string shftId)
        {
            try
            {

                SqlCommand cmd;
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select quan from ItemQuantityEndShift where itemId = '" + itemId + "' and shiftId = '" + shftId + "'", adoClass.sqlcn);
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

                        totalQuantity = oldQuan - quantity;

                        cmd = new SqlCommand("update ItemQuantityEndShift set quan = '" + totalQuantity + "' where itemId = '" + itemId + "' and shiftId = '" + shftId + "'", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();


                    }

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (dgvLoading.Rows.Count > 0)
            {
                if (MessageBox.Show("هل متاكد من حذف الكل سيترتب علي ذلك حذف جميع الورديات و مصروفات الورديات ان وجدت ", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    try
                    {
                        if (adoClass.sqlcn.State != ConnectionState.Open)
                        {
                            adoClass.sqlcn.Open();
                        }


                        cmd = new SqlCommand("delete from OrderItems DBCC CHECKIDENT (OrderItems,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("delete from Orders DBCC CHECKIDENT (Orders,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("delete from ItemQuantityEndShift DBCC CHECKIDENT (ItemQuantityEndShift,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        // delete expenses under shifts
                        cmd = new SqlCommand("select * from Expenses where shiftId IS NOT NULL", adoClass.sqlcn);

                        DataTable dtt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtt);
                        if (dtt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtt.Rows.Count; i++)
                            {
                                cmd = new SqlCommand("delete from  Expenses where shiftId =" + dtt.Rows[i][0] + "", adoClass.sqlcn);
                                cmd.ExecuteNonQuery();

                            }
                        }
                        cmd = new SqlCommand("delete from Shifts DBCC CHECKIDENT (Shifts,RESEED,0)", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();


                        cmd = new SqlCommand("update Items set quantity = 0", adoClass.sqlcn);
                        cmd.ExecuteNonQuery();

                        

                        MessageBox.Show("تم الحذف بنجاح");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("خطا في الحذف");
                    }
                    finally
                    {
                        adoClass.sqlcn.Close();
                    }

                    loadTable("select Orders.id,Orders.dateTime,Orders.total,Orders.tax,Orders.discount,Orders.delivery,Orders.shiftId,Orders.orderType,Users.fullName,Clients.name,Tayar.name as tayar,Orders.orderShiftId from Orders LEFT JOIN Users on Orders.userId = Users.id LEFT JOIN Clients on Orders.clientId = Clients.id LEFT JOIN Tayar on Orders.tayarId = Tayar.id");
                }
            }
        }
    }
}
