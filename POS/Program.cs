using POS.Classes;
using POS.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace POS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]


        static void Main()
        {

            if (File.Exists(Application.StartupPath + "\\Serial\\serial.txt") == false)
            {
                Application.Run(new FormEnterSerialNumber());

            }

            else
            {
                adoClass.setConnection();
                Application.SetCompatibleTextRenderingDefault(false);

                try
                {
                    ClassSerialNumber sn = new ClassSerialNumber();
                    string serialNumber = Regex.Replace(sn.GetSerialNumber(@"C:"), @"\s+", "");

                    StreamReader sr = new StreamReader(Application.StartupPath + "\\serial\\serial.txt");
                    string txt = sr.ReadLine();

                    sr.Close();
                    txt = Regex.Replace(txt, @"\s+", "");

                    if (txt == serialNumber)
                    {

                        FormStartUp frmStartUP = new FormStartUp();
                        if (frmStartUP.ShowDialog() == DialogResult.OK)
                        {
                            FormLogin frmLogin = new FormLogin();
                            if (frmLogin.ShowDialog() == DialogResult.OK)
                            {
                                Application.EnableVisualStyles();
                                if (declarations.privilege == "Admin")
                                {
                                    Application.Run(new FormMain());
                                }
                                else
                                {
                                    SqlCommand cmd;
                                    try
                                    {
                                        if (adoClass.sqlcn.State != ConnectionState.Open)
                                        {
                                            adoClass.sqlcn.Open();
                                        }
                                        DataTable dt = new DataTable();
                                        cmd = new SqlCommand("Select TOP 1 * from Shifts order by id DESC", adoClass.sqlcn);
                                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                                        da.Fill(dt);

                                        if (dt.Rows.Count > 0)
                                        {

                                            DataRow row = dt.Rows[0];
                                            object dateTimeStart = row["dateTimeStart"];
                                            object dateTimeEnd = row["dateTimeEnd"];
                                            if (dateTimeStart != DBNull.Value && dateTimeEnd != DBNull.Value)
                                            {
                                                string query = "Insert into Shifts (userId,dateTimeStart) values (@userId,@dateTimeStart); ";
                                                query += "SELECT @shiftId = SCOPE_IDENTITY(); ";
                                                cmd = new SqlCommand(query, adoClass.sqlcn);

                                                cmd.Parameters.Add("@shiftId", SqlDbType.Int);

                                                cmd.Parameters.AddWithValue("@userId", declarations.userid);
                                                cmd.Parameters.AddWithValue("@dateTimeStart", DateTime.Now);
                                                // shift id
                                                cmd.Parameters["@shiftId"].Direction = ParameterDirection.Output;

                                                cmd.ExecuteNonQuery();


                                                declarations.shiftId = int.Parse(cmd.Parameters["@shiftId"].Value.ToString());
                                                MessageBox.Show("لقد بدات وردية جديدة");
                                                Application.Run(new FormPOSResponsive());
                                                

                                            }
                                            else
                                            {
                                                declarations.shiftId = int.Parse(row["id"].ToString());
                                                Application.Run(new FormPOSResponsive());
                                            }
                                        }
                                        else
                                        {
                                            cmd = new SqlCommand("Insert into Shifts (userId,dateTimeStart) values (@userId,@dateTimeStart)", adoClass.sqlcn);
                                            cmd.Parameters.AddWithValue("@userId", declarations.userid);
                                            cmd.Parameters.AddWithValue("@dateTimeStart", DateTime.Now);
                                            cmd.ExecuteNonQuery();


                                            declarations.shiftId = 1;
                                            Application.Run(new FormPOSResponsive());
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
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("خطا في تشغيل البرنامج الرجاء الاتصال علي الشركة");
                        Application.Exit();
                    }




                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //MessageBox.Show("خطا في تشغيل البرنامج الرجاء الاتصال علي الشركة");
                    Application.Exit();
                }
            }
        }



    }
}

