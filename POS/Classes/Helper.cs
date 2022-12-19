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
                cmd = new SqlCommand("Update Shifts set expenses = @expenses,wared = @wared,total=@total Where id = '" + shiftId + "'", adoClass.sqlcn);
                cmd.Parameters.AddWithValue("@expenses", totalExpenses);
                cmd.Parameters.AddWithValue("@wared", totalWared);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.ExecuteNonQuery();
            }
            
        }
    
    }
}
