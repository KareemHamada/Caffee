
namespace POS.Forms
{
    partial class FormMainEmployess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainEmployess));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEmployeesSalaries = new System.Windows.Forms.Button();
            this.btnEmp = new System.Windows.Forms.Button();
            this.btnWD = new System.Windows.Forms.Button();
            this.btnAttendingLeaving = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnEmployeesSalaries, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnEmp, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnWD, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAttendingLeaving, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1220, 1000);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnEmployeesSalaries
            // 
            this.btnEmployeesSalaries.BackColor = System.Drawing.Color.White;
            this.btnEmployeesSalaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEmployeesSalaries.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmployeesSalaries.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnEmployeesSalaries.Image = ((System.Drawing.Image)(resources.GetObject("btnEmployeesSalaries.Image")));
            this.btnEmployeesSalaries.Location = new System.Drawing.Point(3, 503);
            this.btnEmployeesSalaries.Name = "btnEmployeesSalaries";
            this.btnEmployeesSalaries.Size = new System.Drawing.Size(604, 494);
            this.btnEmployeesSalaries.TabIndex = 11;
            this.btnEmployeesSalaries.Text = "مرتبات موظفين";
            this.btnEmployeesSalaries.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEmployeesSalaries.UseVisualStyleBackColor = false;
            this.btnEmployeesSalaries.Click += new System.EventHandler(this.btnEmployeesSalaries_Click);
            // 
            // btnEmp
            // 
            this.btnEmp.BackColor = System.Drawing.Color.White;
            this.btnEmp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEmp.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnEmp.Image = ((System.Drawing.Image)(resources.GetObject("btnEmp.Image")));
            this.btnEmp.Location = new System.Drawing.Point(613, 3);
            this.btnEmp.Name = "btnEmp";
            this.btnEmp.Size = new System.Drawing.Size(604, 494);
            this.btnEmp.TabIndex = 8;
            this.btnEmp.Text = "الموظفين";
            this.btnEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEmp.UseVisualStyleBackColor = false;
            this.btnEmp.Click += new System.EventHandler(this.btnEmp_Click);
            // 
            // btnWD
            // 
            this.btnWD.BackColor = System.Drawing.Color.White;
            this.btnWD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWD.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnWD.Image = ((System.Drawing.Image)(resources.GetObject("btnWD.Image")));
            this.btnWD.Location = new System.Drawing.Point(3, 3);
            this.btnWD.Name = "btnWD";
            this.btnWD.Size = new System.Drawing.Size(604, 494);
            this.btnWD.TabIndex = 9;
            this.btnWD.Text = "استلاف و سداد موظفين";
            this.btnWD.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnWD.UseVisualStyleBackColor = false;
            this.btnWD.Click += new System.EventHandler(this.btnWD_Click);
            // 
            // btnAttendingLeaving
            // 
            this.btnAttendingLeaving.BackColor = System.Drawing.Color.White;
            this.btnAttendingLeaving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAttendingLeaving.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttendingLeaving.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnAttendingLeaving.Image = ((System.Drawing.Image)(resources.GetObject("btnAttendingLeaving.Image")));
            this.btnAttendingLeaving.Location = new System.Drawing.Point(613, 503);
            this.btnAttendingLeaving.Name = "btnAttendingLeaving";
            this.btnAttendingLeaving.Size = new System.Drawing.Size(604, 494);
            this.btnAttendingLeaving.TabIndex = 10;
            this.btnAttendingLeaving.Text = "حضور و انصراف";
            this.btnAttendingLeaving.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAttendingLeaving.UseVisualStyleBackColor = false;
            this.btnAttendingLeaving.Click += new System.EventHandler(this.btnAttendingLeaving_Click);
            // 
            // FormMainEmployess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1220, 1000);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMainEmployess";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FormMainEmployess";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnEmployeesSalaries;
        private System.Windows.Forms.Button btnEmp;
        private System.Windows.Forms.Button btnWD;
        private System.Windows.Forms.Button btnAttendingLeaving;
    }
}