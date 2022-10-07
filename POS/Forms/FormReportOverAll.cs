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
using Microsoft.Reporting.WinForms;
using POS.Tools;

namespace POS.Forms
{
    public partial class FormReportOverAll : Form
    {
        public FormReportOverAll()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;

        private void FormReportOverAll_Load(object sender, EventArgs e)
        {
            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now;

            try
            {
                double wared = 0;
                double FinalWared = 0;

                double expenses = 0;
                double FinalExpenses = 0;


                double salary = 0;
                double FinalSalaries = 0;

                double store = 0;
                double FinalStore = 0;

                DataTable dt = new DataTable();

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                cmd = new SqlCommand("SELECT total from Orders", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        // wared
                        double.TryParse(row["total"].ToString(), out wared);
                        FinalWared += wared;                       
                    }
                }


                // for table employee salaries
                dt.Rows.Clear();
                cmd = new SqlCommand("SELECT salary from EmployeesSalaries", adoClass.sqlcn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        // salary
                        double.TryParse(row["salary"].ToString(), out salary);
                        FinalSalaries += salary;
                    }
                }


                // for table store
                dt.Rows.Clear();
                cmd = new SqlCommand("SELECT total from Stores", adoClass.sqlcn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        // total
                        double.TryParse(row["total"].ToString(), out store);
                        FinalStore += store;
                    }
                }


                // for table expenses
                dt.Rows.Clear();
                cmd = new SqlCommand("SELECT price from Expenses", adoClass.sqlcn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        // expenses
                        double.TryParse(row["price"].ToString(), out expenses);
                        FinalExpenses += expenses;
                    }
                }

                // total
                double finaTotal = FinalWared - (FinalExpenses + FinalSalaries + FinalStore);

                lblExpenses.Text = FinalExpenses.ToString();
                lblWared.Text = FinalWared.ToString();
                lblSalaries.Text = FinalSalaries.ToString();
                lblStore.Text = FinalStore.ToString();
                lblTotal.Text = finaTotal.ToString();
            }
            catch(Exception ex)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                double wared = 0;
                double FinalWared = 0;

                double expenses = 0;
                double FinalExpenses = 0;


                double salary = 0;
                double FinalSalaries = 0;

                double store = 0;
                double FinalStore = 0;

                DataTable dt = new DataTable();

                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                cmd = new SqlCommand("SELECT total from Orders where dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'", adoClass.sqlcn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        // wared
                        double.TryParse(row["total"].ToString(), out wared);
                        FinalWared += wared;

                        //// expenses
                        //double.TryParse(row["expenses"].ToString(), out expenses);
                        //FinalExpenses += expenses;
                    }
                }

                // for table expenses
                dt.Rows.Clear();
                cmd = new SqlCommand("SELECT price from Expenses where dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'", adoClass.sqlcn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        // expenses
                        double.TryParse(row["price"].ToString(), out expenses);
                        FinalExpenses += expenses;
                    }
                }


                // for table employee salaries
                dt.Rows.Clear();
                cmd = new SqlCommand("SELECT salary from EmployeesSalaries where dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'", adoClass.sqlcn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        // salary
                        double.TryParse(row["salary"].ToString(), out salary);
                        FinalSalaries += salary;
                    }
                }


                // for table stores
                dt.Rows.Clear();
                cmd = new SqlCommand("SELECT total from Stores where dateTime between '" + dtpFrom.Value.ToString("yyyy-MM-dd") + "' and '" + dtpTo.Value.ToString("yyyy-MM-dd") + "'", adoClass.sqlcn);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        // salary
                        double.TryParse(row["total"].ToString(), out store);
                        FinalStore += store;
                    }
                }

                // total
                double finaTotal = FinalWared - (FinalExpenses + FinalSalaries + FinalStore);

                lblExpenses.Text = FinalExpenses.ToString();
                lblWared.Text = FinalWared.ToString();
                lblSalaries.Text = FinalSalaries.ToString();
                lblStore.Text = FinalStore.ToString();
                lblTotal.Text = finaTotal.ToString();

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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //if (dgvLoading.Rows.Count > 0)
            //{
            dsOverAll overAll = new dsOverAll();
            //for (int i = 0; i < dgvLoading.Rows.Count; i++)
            //{
            DataRow dro = overAll.Tables["dtOverAll"].NewRow();
            dro["Wared"] = double.Parse(lblWared.Text);
            dro["expenses"] = double.Parse(lblExpenses.Text);
            dro["salaries"] = double.Parse(lblSalaries.Text);
            dro["stores"] = double.Parse(lblStore.Text);
            dro["total"] = double.Parse(lblTotal.Text);

            overAll.Tables["dtOverAll"].Rows.Add(dro);
            //}

            FormReports rptForm = new FormReports();
            rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportOverAll.rdlc";
            rptForm.mainReport.LocalReport.DataSources.Clear();
            rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", overAll.Tables["dtOverAll"]));

            ReportParameter[] reportParameters = new ReportParameter[2];
            reportParameters[0] = new ReportParameter("From", dtpFrom.Value.ToString());
            reportParameters[1] = new ReportParameter("To", dtpTo.Value.ToString());
            //reportParameters[2] = new ReportParameter("Wared", lblWared.ToString());
            //reportParameters[3] = new ReportParameter("expenses", lblExpenses.ToString());
            //reportParameters[4] = new ReportParameter("salaries", lblSalaries.ToString());
            //reportParameters[5] = new ReportParameter("stores", lblStore.ToString());
            //reportParameters[6] = new ReportParameter("total", lblTotal.ToString());


            if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
            {
                LocalReport report = new LocalReport();
                string path = Application.StartupPath + @"\Reports\ReportOverAll.rdlc";
                report.ReportPath = path;
                report.DataSources.Clear();
                report.DataSources.Add(new ReportDataSource("DataSet1", overAll.Tables["dtOverAll"]));
                report.SetParameters(reportParameters);
                PrintersClass.PrintToPrinter(report);
            }
            else
            {
                rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                rptForm.ShowDialog();
            }

            //}
            //else
            //{
            //    MessageBox.Show("لا يوجد عناصر لعرضها");
            //}
        }

    }
}
