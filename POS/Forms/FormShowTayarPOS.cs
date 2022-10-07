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
    public partial class FormShowTayarPOS : Form
    {
        public FormShowTayarPOS()
        {
            InitializeComponent();
        }
        private DataTable loadTable()
        {
            DataTable dt = new DataTable();

            if (adoClass.sqlcn.State != ConnectionState.Open)
            {
                adoClass.sqlcn.Open();
            }
            SqlCommand cmd = new SqlCommand("Select phone,name from Tayar", adoClass.sqlcn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            adoClass.sqlcn.Close();
            return dt;
        }
        private void FormShowTayarPOS_Load(object sender, EventArgs e)
        {
            try
            {
                dgvTayar.DataSource = loadTable();
                dgvTayar.Columns[0].HeaderText = "التليفون";
                dgvTayar.Columns[1].HeaderText = "الاسم";
            }
            catch
            {

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
