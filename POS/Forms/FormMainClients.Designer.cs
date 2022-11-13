
namespace POS.Forms
{
    partial class FormMainClients
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClients = new System.Windows.Forms.Button();
            this.btnClientsCashWithdraw = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnClients, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClientsCashWithdraw, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 906);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnClients
            // 
            this.btnClients.BackColor = System.Drawing.Color.White;
            this.btnClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClients.Image = global::POS.Properties.Resources.man;
            this.btnClients.Location = new System.Drawing.Point(595, 3);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(586, 447);
            this.btnClients.TabIndex = 11;
            this.btnClients.Text = "العملاء";
            this.btnClients.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClients.UseVisualStyleBackColor = false;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // btnClientsCashWithdraw
            // 
            this.btnClientsCashWithdraw.BackColor = System.Drawing.Color.White;
            this.btnClientsCashWithdraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClientsCashWithdraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClientsCashWithdraw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClientsCashWithdraw.Image = global::POS.Properties.Resources.money;
            this.btnClientsCashWithdraw.Location = new System.Drawing.Point(3, 3);
            this.btnClientsCashWithdraw.Name = "btnClientsCashWithdraw";
            this.btnClientsCashWithdraw.Size = new System.Drawing.Size(586, 447);
            this.btnClientsCashWithdraw.TabIndex = 12;
            this.btnClientsCashWithdraw.Text = "اجل و سداد عملاء";
            this.btnClientsCashWithdraw.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClientsCashWithdraw.UseVisualStyleBackColor = false;
            this.btnClientsCashWithdraw.Visible = false;
            this.btnClientsCashWithdraw.Click += new System.EventHandler(this.btnClientsCashWithdraw_Click);
            // 
            // FormMainClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 906);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormMainClients";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnClientsCashWithdraw;
    }
}