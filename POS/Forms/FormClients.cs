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
using POS.Tools;
using Microsoft.Reporting.WinForms;

namespace POS.Forms
{
    public partial class FormClients : Form
    {
        public FormClients()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private TextBox txtHidden;

        private void FormClients_Load(object sender, EventArgs e)
        {
            Helper.fillComboBox(comboRegions, "Select id,name from Regions", "name", "id");
            
            loadTable("Select Clients.address,Regions.name as region,Clients.phone,Clients.name,Clients.id from Clients LEFT JOIN Regions on Clients.regionId = Regions.id");



            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

  
        private void loadTable(string query)
        {
            dgvClients.Rows.Clear();
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

                    dgvClients.Rows.Add
                        (new object[]
                            {
                            row["address"],
                            row["region"],
                            row["phone"],
                            row["name"],
                            row["id"],
                            }
                        ); ;
                }
            }

        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم العميل");
                return;
            }

            try
            {
                cmd = new SqlCommand("Insert into Clients (name,phone,regionId,address) values (@name,@phone,@regionId,@address)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@regionId", comboRegions.SelectedValue);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة العميل بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select Clients.address,Regions.name as region,Clients.phone,Clients.name,Clients.id from Clients LEFT JOIN Regions on Clients.regionId = Regions.id");

            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            comboRegions.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد العميل المراد تعديلها");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم العميل");
                return;
            }


            try
            {
                // Clients (name,phone,regionId,address)
                cmd = new SqlCommand("Update Clients set name = @name, phone=@phone, regionId=@regionId, address=@address Where id = '" + id + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@regionId", comboRegions.SelectedValue);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);


                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم التعديل بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select Clients.address,Regions.name as region,Clients.phone,Clients.name,Clients.id from Clients LEFT JOIN Regions on Clients.regionId = Regions.id");

            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            comboRegions.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvClients.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvClients.CurrentRow.Cells[4].Value.ToString();
                    if (txtHidden.Text == "")
                    {
                        MessageBox.Show("حدد العميل المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Clients Where id = '" + id + "'", adoClass.sqlcn);

                        if (adoClass.sqlcn.State != ConnectionState.Open)
                        {
                            adoClass.sqlcn.Open();
                        }

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("تم الحذف بنجاح");

                    }
                    catch
                    {
                        MessageBox.Show("خطا في الحذف");
                    }
                    finally
                    {
                        adoClass.sqlcn.Close();
                    }

                    loadTable("Select Clients.address,Regions.name as region,Clients.phone,Clients.name,Clients.id from Clients LEFT JOIN Regions on Clients.regionId = Regions.id");

                    txtName.Text = "";
                    txtAddress.Text = "";
                    txtPhone.Text = "";
                    comboRegions.Text = "";
                    txtHidden.Text = "";
                }
            }

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }





        private void dgvClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvClients.CurrentRow.Cells[4].Value.ToString();
            txtName.Text = dgvClients.CurrentRow.Cells[3].Value.ToString();
            txtPhone.Text = dgvClients.CurrentRow.Cells[2].Value.ToString();
            comboRegions.Text = dgvClients.CurrentRow.Cells[1].Value.ToString();
            txtAddress.Text = dgvClients.CurrentRow.Cells[0].Value.ToString();
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }


        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("Select Clients.address,Regions.name as region,Clients.phone,Clients.name,Clients.id from Clients LEFT JOIN Regions on Clients.regionId = Regions.id");
            }
            else
            {
                loadTable("Select Clients.address,Regions.name as region,Clients.phone,Clients.name,Clients.id from Clients LEFT JOIN Regions on Clients.regionId = Regions.id where Clients.name like '%" + text + "%' or Clients.phone like '%" + text + "%' " +
                    "or Regions.name like '%" + text + "%' " +
                    "or Clients.address like '%" + text + "%'");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvClients.Rows.Count > 0)
            {
                dsShowClients tbl = new dsShowClients();
                for (int i = 0; i < dgvClients.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowClients"].NewRow();
                    dro["address"] = dgvClients[0, i].Value;
                    dro["region"] = dgvClients[1, i].Value;
                    dro["phone"] = dgvClients[2, i].Value;
                    dro["client"] = dgvClients[3, i].Value;

                    tbl.Tables["dtShowClients"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShowClients.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowClients"]));

        
                if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShowClients.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowClients"]));
                    PrintersClass.PrintToPrinter(report);
                }
                else
                {
                    rptForm.ShowDialog();
                }

            }
            else
            {
                MessageBox.Show("لا يوجد عناصر لعرضها");
            }
        }
    }
}
