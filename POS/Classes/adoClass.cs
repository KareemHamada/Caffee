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

        public static void setConnection()
        {

            //Data Source = DESKTOP - ADO823G; Initial Catalog = POS; Integrated Security = True
            //sqlcn = new SqlConnection(POS.Properties.Settings.Default.Connection);
            //string ds = "DESKTOP-ADO823G";
            try
            {
                //if (File.Exists(Application.StartupPath + "\\Serial\\serial.txt") == false)
                //{
                //    FormEnterSerialNumber frm = new FormEnterSerialNumber();
                //    frm.Show();
                //}


                ////////////////////////
                //StreamReader sr = new StreamReader(Application.StartupPath + "\\Tools\\serial.txt");
                //string txt = sr.ReadLine();
                //string ds = sr.ReadLine();

                //sr.Close();
                //txt = Regex.Replace(txt, @"\s+", "");
                //ds = Regex.Replace(ds, @"\s+", "");

                //sqlcn = new SqlConnection("Data Source=" + ds + ";Initial Catalog=POS;Integrated Security=True");
                ////////////////////////


                sqlcn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=POS;Integrated Security=True");

                //sqlcn = new SqlConnection(@"Data Source=DESKTOP-KE662S4;Initial Catalog=POS;Integrated Security=True");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}



