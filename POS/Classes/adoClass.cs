using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using POS.Forms;

namespace POS.Classes
{
    class adoClass
    {

        public static SqlConnection sqlcn;
        public static SqlCommandBuilder builder;
        static SqlCommand cmd = new SqlCommand();

        public static void setConnection()
        {
            try
            {
                sqlcn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=POS;Integrated Security=True");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // read data from database
        public static DataTable readData(string stmt, string message)
        {
            DataTable tbl = new DataTable();
            try
            {
                cmd.Connection = sqlcn;
                cmd.CommandText = stmt;
                sqlcn.Open();
                tbl.Load(cmd.ExecuteReader()); // get data and load to table
                sqlcn.Close();
                if (message != "")
                {
                    MessageBox.Show(message, "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcn.Close();
            }

            return tbl;
        }

        // insert update delete
        public static bool executeData(string stmt, string message)
        {
            try
            {
                cmd.Connection = sqlcn;
                cmd.CommandText = stmt;
                sqlcn.Open();
                cmd.ExecuteNonQuery();
                sqlcn.Close();
                if (message != "")
                {
                    MessageBox.Show(message, "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا");
                return false;
            }
            finally
            {
                sqlcn.Close();
            }

        }
    }
}



