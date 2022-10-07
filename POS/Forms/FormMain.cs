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

namespace POS.Forms
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private Button currentButton;
        private Form activeForm;

        

        private void FormMain_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            lblUserName.Text = declarations.userFullName;
        }

        private void OpenChildForm(Form cForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = cForm;
            ActiveButton(btnSender);
            cForm.TopLevel = false;
            cForm.FormBorderStyle = FormBorderStyle.None;
            cForm.BackColor = Color.White;
            cForm.Dock = DockStyle.Fill;
            pnlMainForm.Controls.Add(cForm);
            pnlMainForm.Tag = cForm;
            cForm.BringToFront();
            cForm.Show();
        }

        


        private void ActiveButton(object sender)
        {
            if (sender != null)
            {
                if (currentButton != (Button)sender)
                {
                    unSelectButton();
                    currentButton = (Button)sender;
                    Color color = Color.FromArgb(255, 128, 0);
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    // Microsoft Sans Serif, 12pt, style=Bold
                    currentButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
                    //pnlTitle.BackColor = color;
                    //lblTitle.Text = currentButton.Text;
                }
            }
        }

        private void unSelectButton()
        {
            foreach (Control ctr in pnlMenu.Controls)
            {
                if (ctr.GetType() == typeof(Button))
                {
                    ctr.BackColor = Color.White;
                    ctr.ForeColor = Color.Black;
                    ctr.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();

            }
        }

        private void btnMainInfo_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormMainInfo(), sender);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormMainEmployess(), sender);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormMainReports(), sender);
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormOptions(), sender);
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormMainStore(), sender);
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            FormBackupDatabasecs frm = new FormBackupDatabasecs();
            frm.Show();
        }

    }
}


