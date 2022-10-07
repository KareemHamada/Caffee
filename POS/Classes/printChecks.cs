using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using POS.Tools;
using System.Windows.Forms;
using POS.Forms;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using System.IO;

namespace POS.Classes
{
    public class printChecks
    {
        private SqlCommand cmd;
        private SqlDataReader dr;
        private SqlDataAdapter adaptor;
        public void runPrintCheck(int checkId)
        {
            cmd = new SqlCommand("SELECT * FROM vwChecks where id = '" + checkId + "'", adoClass.sqlcn);
            dr = null;
            dsChecks checks = new dsChecks();
            try
            {
                if (adoClass.sqlcn.State != ConnectionState.Open)
                {
                    adoClass.sqlcn.Open();
                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    DataRow dro = checks.Tables["dtCheck"].NewRow();
                    dro["id"] = dr["id"];
                    dro["checkDate"] = dr["dateTime"];
                    dro["checkTotal"] = dr["total"];
                    dro["itemId"] = dr["itemId"];
                    dro["itemName"] = dr["name"];
                    dro["itemQuantity"] = dr["quantity"];
                    dro["itemPrice"] = dr["price"];
                    dro["itemTotalPrice"] = dr["totalItem"];
                    dro["casherName"] = dr["fullName"];
                    dro["clientName"] = dr["clientName"];
                    dro["clientPhone"] = dr["phone"];
                    dro["tax"] = dr["tax"];
                    dro["discount"] = dr["discount"];
                    dro["delivery"] = dr["delivery"];
                    dro["orderType"] = dr["orderType"];
                    dro["address"] = dr["address"];
                    checks.Tables["dtCheck"].Rows.Add(dro);
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

            FormReports rptForm = new FormReports();
            rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.rptCheck.rdlc";
            rptForm.mainReport.LocalReport.DataSources.Clear();
            rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", checks.Tables["dtCheck"]));

            ReportParameter[] reportParameters = new ReportParameter[5];
            reportParameters[0] = new ReportParameter("Line1", declarations.systemOptions["line1"].ToString());
            reportParameters[1] = new ReportParameter("Line2", declarations.systemOptions["line2"].ToString());
            reportParameters[2] = new ReportParameter("RestaurantName", declarations.systemOptions["name"].ToString());

            //byte[] imageBytes = (byte[])declarations.systemOptions["image"];
            //reportParameters[3] = new ReportParameter("img", Convert.ToBase64String(imageBytes));
            reportParameters[3] = new ReportParameter("RestPhone", declarations.systemOptions["phone"].ToString());
            reportParameters[4] = new ReportParameter("RestAddress", declarations.systemOptions["address"].ToString());

            if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
            {
                //
                //String query = File.ReadAllText(Path.Combine(path, "files\\Test.txt"));

                //LocalReport report = new LocalReport();
                //string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                //string path = appPath + "\\Reports\\rptCheck.rdlc";

                LocalReport report = new LocalReport();
                string path = Application.StartupPath + @"\Reports\rptCheck.rdlc";
                report.ReportPath = path;
                report.DataSources.Clear();
                report.DataSources.Add(new ReportDataSource("DataSet1", checks.Tables["dtCheck"]));
                report.SetParameters(reportParameters);
                PrintersClass.PrintToPrinter(report);
                
                //LocalReport report = new LocalReport();
                //string path = Path.GetDirectoryName(Application.ExecutablePath);
                //string fullPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\Reports\rptCheck.rdlc";
                //report.ReportPath = fullPath;
                //report.DataSources.Add(new ReportDataSource("DataSet1", checks.Tables["dtCheck"]));
                //report.SetParameters(reportParameters);
                //PrintersClass.PrintToPrinter(report);
            }
            else
            {
                rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                rptForm.ShowDialog();
            }
            
            

        }


        public void runSaleReport(DateTime _from,DateTime _to)
        {
            string query = "select * from vwShifts where dateTimeStart between '" + _from.ToString("yyyy-MM-dd") + "' and '" + _to.ToString("yyyy-MM-dd") + "'";
            adaptor = new SqlDataAdapter(query, adoClass.sqlcn);
            dsReports reports = new dsReports();

            try
            {
                adaptor.Fill(reports.Tables["vwShifts"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            FormReports rptForm = new FormReports();
            rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportShifts.rdlc";
            rptForm.mainReport.LocalReport.DataSources.Clear();
            rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", reports.Tables["vwShifts"]));

            ReportParameter[] reportParameters = new ReportParameter[2];
            reportParameters[0] = new ReportParameter("From", _from.ToString("yyyy-MM-dd"));
            reportParameters[1] = new ReportParameter("To", _to.ToString("yyyy-MM-dd"));
            //reportParameters[2] = new ReportParameter("RestName", declarations.systemOptions["name"].ToString());

            //byte[] imageBytes = (byte[])declarations.systemOptions["image"];
           // reportParameters[3] = new ReportParameter("img", Convert.ToBase64String(imageBytes));

            // here
            rptForm.mainReport.LocalReport.SetParameters(reportParameters);
            rptForm.ShowDialog();

        }

        public void printEndShift(int shiftId)
        {
            string query = "SELECT * FROM vwEndShift where id = '" + shiftId + "'";
            adaptor = new SqlDataAdapter(query, adoClass.sqlcn);
            dsEndShift reports = new dsEndShift();

            try
            {
                adaptor.Fill(reports.Tables["vwEndShift"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            FormReports rptForm = new FormReports();
            rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportEndShift.rdlc";
            rptForm.mainReport.LocalReport.DataSources.Clear();
            rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", reports.Tables["vwEndShift"]));

            ReportParameter[] reportParameters = new ReportParameter[1];
            reportParameters[0] = new ReportParameter("RestName", declarations.systemOptions["name"].ToString());
            //byte[] imageBytes = (byte[])declarations.systemOptions["image"];
            //reportParameters[1] = new ReportParameter("img", Convert.ToBase64String(imageBytes));


            // ******************************************* // 
            //LocalReport report = new LocalReport();
            //string path = Application.StartupPath + @"\Reports\rptCheck.rdlc";
            //report.ReportPath = path;
            //report.DataSources.Clear();
            //report.DataSources.Add(new ReportDataSource("DataSet1", checks.Tables["dtCheck"]));
            //report.SetParameters(reportParameters);
            //PrintersClass.PrintToPrinter(report);
            // here
            // ******************************************* // 

            if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
            {

                LocalReport report = new LocalReport();
                string path = Application.StartupPath + @"\Reports\ReportEndShift.rdlc";
                report.ReportPath = path;
                report.DataSources.Clear();
                report.DataSources.Add(new ReportDataSource("DataSet1", reports.Tables["vwEndShift"]));
                report.SetParameters(reportParameters);
                PrintersClass.PrintToPrinter(report);

            }
            else
            {
                rptForm.mainReport.LocalReport.SetParameters(reportParameters);
                rptForm.ShowDialog();
            }
        }


        public void endShiftItems()
        {
            string query = "SELECT * FROM EndShiftItemsQuantity where shiftId = '" + declarations.shiftId + "'";
            adaptor = new SqlDataAdapter(query, adoClass.sqlcn);
            dsEndShiftItems reports = new dsEndShiftItems();

            try
            {
                adaptor.Fill(reports.Tables["EndShiftItemsQuantity"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            FormReports rptForm = new FormReports();
            rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportEndShiftItems.rdlc";
            rptForm.mainReport.LocalReport.DataSources.Clear();
            rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", reports.Tables["EndShiftItemsQuantity"]));

            if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
            {

                LocalReport report = new LocalReport();
                string path = Application.StartupPath + @"\Reports\ReportEndShiftItems.rdlc";
                report.ReportPath = path;
                report.DataSources.Clear();
                report.DataSources.Add(new ReportDataSource("DataSet1", reports.Tables["EndShiftItemsQuantity"]));
                PrintersClass.PrintToPrinter(report);

            }
            else
            {
                rptForm.ShowDialog();
            }
        }



        public void endShiftExpenses(int id)
        {
            string query = "SELECT * FROM EndShiftExpenses where shiftId = '" + id + "'";
            adaptor = new SqlDataAdapter(query, adoClass.sqlcn);
            dsEndShiftExpenses reports = new dsEndShiftExpenses();

            try
            {
                adaptor.Fill(reports.Tables["EndShiftExpenses"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            FormReports rptForm = new FormReports();
            rptForm.mainReport.LocalReport.ReportEmbeddedResource = "POS.Reports.ReportEndShiftExpenses.rdlc";
            rptForm.mainReport.LocalReport.DataSources.Clear();
            rptForm.mainReport.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", reports.Tables["EndShiftExpenses"]));

            if (bool.Parse(declarations.systemOptions["printToPrinter"].ToString()))
            {

                LocalReport report = new LocalReport();
                string path = Application.StartupPath + @"\Reports\ReportEndShiftExpenses.rdlc";
                report.ReportPath = path;
                report.DataSources.Clear();
                report.DataSources.Add(new ReportDataSource("DataSet1", reports.Tables["EndShiftExpenses"]));
                PrintersClass.PrintToPrinter(report);

            }
            else
            {
                rptForm.ShowDialog();
            }
        }
    }
}
