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
    public partial class FormRegions : Form
    {
        public FormRegions()
        {
            InitializeComponent();
        }
        private SqlCommand cmd;
        private TextBox txtHidden;

        private void loadTable(string query)
        {
            dgvRegions.Rows.Clear();
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

                    dgvRegions.Rows.Add
                        (new object[]
                            {
                            row["name"],
                            row["id"],
                            }
                        ); ;
                }
            }

        }


        private void FormRegions_Load(object sender, EventArgs e)
        {
        
            loadTable("Select * from Regions");
            // hidden text box
            txtHidden = new TextBox();
            txtHidden.Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم المنطقة");
                return;
            }

            try
            {
                cmd = new SqlCommand("Insert into Regions (name) values (@name)", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }

                cmd.ExecuteNonQuery();

                MessageBox.Show("تم اضافة المنطقة بنجاح");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                adoClass.sqlcn.Close();
            }

            loadTable("Select * from Regions");

            txtName.Text = "";
            txtHidden.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtHidden.Text;
            if (id == "")
            {
                MessageBox.Show("حدد المنطقة المراد تعديلها");
                return;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("ادخل اسم المنطقة");
                return;
            }


            try
            {

                cmd = new SqlCommand("Update Regions set name = @name Where id = '" + id + "'", adoClass.sqlcn);

                cmd.Parameters.AddWithValue("@name", txtName.Text);


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

            loadTable("Select * from Regions");

            txtName.Text = "";
            txtHidden.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRegions.Rows.Count > 0)
            {
                if (MessageBox.Show("هل تريد الحذف", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtHidden.Text = dgvRegions.CurrentRow.Cells[1].Value.ToString();
                    if (txtHidden.Text == "")
                    {
                        MessageBox.Show("حدد المنطقة المراد حذفه");
                        return;
                    }
                    try
                    {

                        cmd = new SqlCommand("delete from Regions Where id = '" + txtHidden.Text + "'", adoClass.sqlcn);

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

                    loadTable("Select * from Regions");
                    dgvRegions.Text = "";

                    txtHidden.Text = "";
                }
            } 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

       

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search(txtSearch.Text);
        }

        void search(string text = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                loadTable("Select * from Regions");
            }
            else
            {
                loadTable("Select * from Regions where name like '%" + text + "%'");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvRegions.Rows.Count > 0)
            {
                dsShowRegions tbl = new dsShowRegions();
                for (int i = 0; i < dgvRegions.Rows.Count; i++)
                {
                    DataRow dro = tbl.Tables["dtShowRegions"].NewRow();
                    dro["name"] = dgvRegions[0, i].Value;
                    

                    tbl.Tables["dtShowRegions"].Rows.Add(dro);
                }

                FormReports rptForm = new FormReports();
                rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShowRegions.rdlc";
                rptForm.mainReport.LocalReport.DataSources.Clear();
                rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowRegions"]));


                if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
                {
                    LocalReport report = new LocalReport();
                    string path = Application.StartupPath + @"\Reports\ReportShowRegions.rdlc";
                    report.ReportPath = path;
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource("DataSet1", tbl.Tables["dtShowRegions"]));
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

        private void dgvRegions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtHidden.Text = dgvRegions.CurrentRow.Cells[1].Value.ToString();
            txtName.Text = dgvRegions.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
