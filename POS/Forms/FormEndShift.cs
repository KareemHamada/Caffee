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
    public partial class FormEndShift : Form
    {
        public FormEndShift()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private SqlDataAdapter da;

        private void FormEndShift_Load(object sender, EventArgs e)
        {
            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                double totalWared = 0; // total wared 
                double totalExpenses = 0; // total expenses
                double total = 0; // total

                // calculate wared
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select total from Orders where shiftId = '" + declarations.shiftId + "'", adoClass.sqlcn);
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
                cmd = new SqlCommand("Select price from Expenses where shiftId = '" + declarations.shiftId + "'", adoClass.sqlcn);
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

                txtShiftId.Text = declarations.shiftId.ToString();
                txtWared.Text = totalWared.ToString();
                txtExpenses.Text = totalExpenses.ToString();
                txtTotal.Text = total.ToString();
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

        private void btnPrintEndShift_Click(object sender, EventArgs e)
        {
            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                cmd = new SqlCommand("Update Shifts set dateTimeEnd = @dateTimeEnd,wared = @wared,expenses = @expenses,total = @total Where id = '" + declarations.shiftId + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@dateTimeEnd", DateTime.Now);
                cmd.Parameters.AddWithValue("@wared", double.Parse(txtWared.Text));
                cmd.Parameters.AddWithValue("@expenses", double.Parse(txtExpenses.Text));
                cmd.Parameters.AddWithValue("@total", double.Parse(txtTotal.Text));
                cmd.ExecuteNonQuery();
                //report
                printChecks checks = new printChecks();
                checks.printEndShift(declarations.shiftId);
                checks.endShiftItems();
                checks.endShiftExpenses(declarations.shiftId);


                MessageBox.Show("تم تقفيل الوردية بنجاح");


                //cmd = new SqlCommand("delete from ItemQuantityEndShift", adoClass.sqlcn);
                //cmd.ExecuteNonQuery();
                Application.Exit();

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
