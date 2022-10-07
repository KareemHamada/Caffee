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
    public partial class TablesPOSForm : Form
    {
        public TablesPOSForm()
        {
            InitializeComponent();
        }

        private SqlDataAdapter adapter;
        private DataTable table;
        //private Point MouseLocaion;

        public string _tableId { get; set; }
        public string _tableName { get; set; }
        private void TablesPOSForm_Load(object sender, EventArgs e)
        {
            loadTables();
        }

        private void loadTables()
        {
            //pnlRest.Controls.Clear();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from vwTablesRoom", adoClass.sqlcn);
            try
            {
                da.Fill(dt);
                DataRow[] drs = dt.Select();
                pnlRest.Controls.Clear();

                for (int i = 0; i < drs.Length; i++)
                {
                    Button catBtn = new Button();
                    //catBtn.AccessibleName = "CAT";
                    catBtn.AccessibleDescription = drs[i]["id"].ToString();
                    catBtn.Name = "btnCat" + drs[i]["id"].ToString();
                    catBtn.Text = drs[i]["name"].ToString();
                    catBtn.Size = new Size(150, 150);
                    if(drs[i]["status"].ToString() == "O")
                    {
                        catBtn.BackgroundImage = Properties.Resources.O;
                    }
                    else
                    {
                        catBtn.BackgroundImage = Properties.Resources.V;
                    }
                    
                    catBtn.BackgroundImageLayout = ImageLayout.Zoom;
                    catBtn.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular);
                    catBtn.TextAlign = ContentAlignment.BottomCenter;
                    catBtn.ImageAlign = ContentAlignment.TopCenter;
                    //catBtn.RightToLeft = RightToLeft.Yes;
                    catBtn.TextImageRelation = TextImageRelation.ImageAboveText;
                    catBtn.BackColor = Color.White;
                    catBtn.FlatStyle = FlatStyle.Flat;
                    catBtn.FlatAppearance.BorderSize = 0;
                    catBtn.Click += object_doubleClick;
                    pnlRest.Controls.Add(catBtn);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

      

        private void object_doubleClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            _tableId = button.AccessibleDescription;
            _tableName = button.Text;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
