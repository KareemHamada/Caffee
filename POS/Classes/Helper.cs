using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Tulpep.NotificationWindow;

namespace POS.Classes
{
    public class Helper
    {
        static private SqlCommand cmd;
        static private SqlDataAdapter da;
        static DataTable dt = new DataTable();

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Byte[] ImageTOByte(Image img)
        {
            img = ResizeImage(img, 64, 64);
            Byte[] bResult = null;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                bResult = ms.ToArray();
            }

            return bResult;
        }


        public static Image ByteToImage(object bObj)
        {
            Image image = null;
            try
            {
                Byte[] myImg = (Byte[])bObj;

                using (MemoryStream ms = new MemoryStream(myImg, 0, myImg.Length))
                {
                    ms.Write(myImg, 0, myImg.Length);
                    image = Image.FromStream(ms, true);
                }

                return image;
            }
            catch
            {
                return image;
            }

        }


        public static void fillComboBox(ComboBox name, string statement, string displayMember, string valueMember)
        {
            SqlCommand cmd = new SqlCommand(statement, adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable table1 = new DataTable();
            da.Fill(table1);
            DataRow itemRow = table1.NewRow();
            itemRow[1] = "";
            table1.Rows.InsertAt(itemRow, 0);
            
            name.DataSource = table1;
            name.DisplayMember = displayMember;
            name.ValueMember = valueMember;

        }
    
        
        public static void updateShiftValues(string shiftId,int expenses)
        {
            if(expenses == 0)
            {
                double totalWared = 0; // total wared 
                double totalExpenses = 0; // total expenses
                double total = 0; // total

                // calculate wared
                dt = new DataTable();
                cmd = new SqlCommand("Select total from Orders where shiftId = '" + shiftId + "'", adoClass.sqlcn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        totalWared += double.Parse(row["total"].ToString());
                    }
                }

                //// calculate Expenses

                //dt = new DataTable();
                //cmd = new SqlCommand("Select price from Expenses where shiftId = '" + shiftId + "'", adoClass.sqlcn);
                //da = new SqlDataAdapter(cmd);
                //da.Fill(dt);

                //if (dt.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        totalExpenses += double.Parse(row["price"].ToString());
                //    }
                //}

                total = totalWared - totalExpenses;
                
                
                cmd = new SqlCommand("Update Shifts set expenses = @expenses,wared = @wared,total=@total Where id = '" + shiftId + "'", adoClass.sqlcn);
                cmd.Parameters.AddWithValue("@expenses", totalExpenses);
                cmd.Parameters.AddWithValue("@wared", totalWared);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.ExecuteNonQuery();
                
                
            }
            else
            {
                double totalWared = 0; // total wared 
                double totalExpenses = 0; // total expenses
                double total = 0; // total

                // calculate wared
                dt = new DataTable();
                cmd = new SqlCommand("Select total from Orders where shiftId = '" + shiftId + "'", adoClass.sqlcn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        totalWared += double.Parse(row["total"].ToString());
                    }
                }

                // calculate Expenses

                dt = new DataTable();
                cmd = new SqlCommand("Select price from Expenses where shiftId = '" + shiftId + "'", adoClass.sqlcn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        totalExpenses += double.Parse(row["price"].ToString());
                    }
                }

                total = totalWared - totalExpenses;

                if (total == 0 || totalWared == 0)
                {
                    // delete expenses under shifts
                    cmd = new SqlCommand("select * from Expenses where shiftId = '" + shiftId + "'", adoClass.sqlcn);

                    DataTable dtt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtt);
                    if (dtt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtt.Rows.Count; i++)
                        {
                            cmd = new SqlCommand("delete from Expenses where shiftId =" + dtt.Rows[i][0] + "", adoClass.sqlcn);
                            cmd.ExecuteNonQuery();

                        }
                    }


                    cmd = new SqlCommand("delete from ItemQuantityEndShift where shiftId ='" + shiftId + "'", adoClass.sqlcn);
                    cmd.ExecuteNonQuery();


                    cmd = new SqlCommand("delete from Shifts where id ='" + shiftId + "'", adoClass.sqlcn);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new SqlCommand("Update Shifts set expenses = @expenses,wared = @wared,total=@total Where id = '" + shiftId + "'", adoClass.sqlcn);
                    cmd.Parameters.AddWithValue("@expenses", totalExpenses);
                    cmd.Parameters.AddWithValue("@wared", totalWared);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.ExecuteNonQuery();
                }
                
            }
            
        }


        public static void DeleteOrders(string orderId,string shiftId,bool oneOrder)
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
                    deleteRelation(itemId, oldQuantity);
                }

            }

            //deleteItems(itemId, oldQuantity);
            cmd = new SqlCommand("delete from OrderItems Where orderId = '" + orderId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();


            cmd = new SqlCommand("delete from Orders Where id = '" + orderId + "'", adoClass.sqlcn);
            cmd.ExecuteNonQuery();


            if (oneOrder)
            {
                // update shift values
                updateShiftValues(shiftId, 1);
            }
            
        }
        public static void deleteItems(int itemId, int deletedQuantity)
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




        public static void DeleteditemsEndShift(int itemId, int quantity, string shftId)
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

        static Database db = new Database();
        static DataTable tblCheck = new DataTable();
        static DataTable tblItemStore = new DataTable();

        // check if saled item has relation with any store items
        public static void checkRelation(int itemID,int itemQty)
        {
            tblCheck.Clear();
            tblCheck = db.readData("select * from ItemsStoreRelation where ItemSaleID =" + itemID+"", "");
            if(tblCheck.Rows.Count > 0)
            {
                for(int i = 0; i < tblCheck.Rows.Count; i++)
                {
                    db.executeData("update storeItems set Qty=Qty-"+ Convert.ToDecimal(tblCheck.Rows[i][2]) * itemQty + " where id ="+tblCheck.Rows[i][1]+"", "", "");
                    // check if qty at storeItems table < 0
                    if (Convert.ToDecimal(db.readData("select Qty from storeItems where id =" + tblCheck.Rows[i][1] + "", "").Rows[0][0]) < 0)
                    {
                        db.executeData("update storeItems set Qty=" + 0 + " where id =" + tblCheck.Rows[i][1] + "", "", "");
                    }
                    if(Convert.ToDecimal(db.readData("select LowQty from storeItems where id =" + tblCheck.Rows[i][1] + "", "").Rows[0][0]) > 0)
                    {
                        if (Convert.ToDecimal(db.readData("select Qty from storeItems where id =" + tblCheck.Rows[i][1] + "", "").Rows[0][0]) <= Convert.ToDecimal(db.readData("select LowQty from storeItems where id =" + tblCheck.Rows[i][1] + "", "").Rows[0][0]))
                        {
                            PopupNotifier popup = new PopupNotifier();
                            popup.Image = Properties.Resources.war;
                            popup.TitleText = "تنبيه مخازن";
                            string pro = db.readData("select name from Items where id =" + itemID + "", "").Rows[0][0].ToString();

                            popup.ContentText = pro + " قل عن الحد الادني";
                            popup.Popup();
                        }
                    }
                    
                    
                }
            }
        }

        // when delete add to store 
        public static void deleteRelation(int itemID, int itemQty)
        {
            tblCheck.Clear();
            tblCheck = db.readData("select * from ItemsStoreRelation where ItemSaleID =" + itemID + "", "");
            if (tblCheck.Rows.Count > 0)
            {
                for (int i = 0; i < tblCheck.Rows.Count; i++)
                {
                    db.executeData("update storeItems set Qty=Qty+" + Convert.ToDecimal(tblCheck.Rows[i][2]) * itemQty + " where id =" + tblCheck.Rows[i][1] + "", "", "");

                }
            }
        }
    }
}
