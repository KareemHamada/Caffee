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
    public partial class FormAttendingLeaving : Form
    {
        public FormAttendingLeaving()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;


        private void FormAttendingLeaving_Load(object sender, EventArgs e)
        {
            Helper.fillComboBox(comboName, "Select id,name from Employee", "name", "id");
        }


        private void btnLeave_Click(object sender, EventArgs e)
        {
            if (comboName.Text == "")
            {
                MessageBox.Show("اختار اسم الموظف");
                return;
            }

            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select TOP 1 * from EmpAttendLeave where EmpId = '" + comboName.SelectedValue + "' ORDER BY id DESC", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    object id = row["id"];
                    object valueDateTimeLeave = row["dateTimeLeave"];
                    object valueDateTimemAttend = row["dateTimeAttend"];
                    if (valueDateTimeLeave == DBNull.Value && valueDateTimemAttend != DBNull.Value)
                    {
                        cmd = new SqlCommand("Update EmpAttendLeave set dateTimeLeave = @dateTimeLeave,Notes=@Notes where id = '" + id + "'", adoClass.sqlcn);

                        cmd.Parameters.AddWithValue("@dateTimeLeave", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                        if (adoClass.sqlcn.State != ConnectionState.Open)
                        {
                            adoClass.sqlcn.Open();
                        }

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("تمت تسجيل الانصراف بنجاح");
                        adoClass.sqlcn.Close();
                    }
                    else
                    {
                        MessageBox.Show("يجب تسجيل الحضور اولا");
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

            comboName.Text = "";
            txtNotes.Text = "";
        }

        private void btnAttend_Click(object sender, EventArgs e)
        {
            if (comboName.Text == "")
            {
                MessageBox.Show("اختار اسم الموظف");
                return;
            }

            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                DataTable dt = new DataTable();
                cmd = new SqlCommand("Select TOP 1 * from EmpAttendLeave where EmpId = '" + comboName.SelectedValue + "' ORDER BY id DESC", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    object valueDateTimeLeave = row["dateTimeLeave"];
                    object valueDateTimemAttend = row["dateTimeAttend"];
                    if (valueDateTimeLeave != DBNull.Value && valueDateTimemAttend != DBNull.Value)
                    {
                        cmd = new SqlCommand("Insert into EmpAttendLeave (EmpId,dateTimeAttend,Notes) values (@EmpId,@dateTimeAttend,@Notes)", adoClass.sqlcn);

                        cmd.Parameters.AddWithValue("@EmpId", comboName.SelectedValue);
                        cmd.Parameters.AddWithValue("@dateTimeAttend", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);


                        if (adoClass.sqlcn.State != ConnectionState.Open)
                        {
                            adoClass.sqlcn.Open();
                        }

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("تمت تسجيل الحضور بنجاح");
                    }
                    else
                    {
                        MessageBox.Show("يجب تسجيل الانصراف اولا");
                    }
                }
                else
                {
                    cmd = new SqlCommand("Insert into EmpAttendLeave (EmpId,dateTimeAttend,Notes) values (@EmpId,@dateTimeAttend,@Notes)", adoClass.sqlcn);

                    cmd.Parameters.AddWithValue("@EmpId", comboName.SelectedValue);
                    cmd.Parameters.AddWithValue("@dateTimeAttend", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);


                    if (adoClass.sqlcn.State != ConnectionState.Open)
                    {
                        adoClass.sqlcn.Open();
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("تمت تسجيل الحضور بنجاح");
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


            comboName.Text = "";
            txtNotes.Text = "";
        }
    }
}
