using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Classes
{
    class Database
    {
        // connection to database == connection string
        SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=POS;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();


        // read data from database
        public DataTable readData(string stmt, string message)
        {
            DataTable tbl = new DataTable();
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = stmt;
                conn.Open();
                tbl.Load(cmd.ExecuteReader()); // get data and load to table
                conn.Close();
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
                conn.Close();
            }

            return tbl;
       }

        // insert update delete
        public bool executeData(string stmt, string message, string err_msg)
        {
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = stmt;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                if (message != "")
                {
                    MessageBox.Show(message, "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                if (err_msg != "")
                {
                    MessageBox.Show(err_msg, "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return false;
            }
            finally
            {
                conn.Close();
            }

        }

        public void FillComboBox(ComboBox cbx, string stmt, string dMember, string vMember)
        {
            cbx.DataSource = readData(stmt, "");
            cbx.DisplayMember = dMember;
            cbx.ValueMember = vMember;
        }
    }
}
