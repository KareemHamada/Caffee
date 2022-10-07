
namespace POS.Forms
{
    partial class FormEndShift
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEndShift));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrintEndShift = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWared = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtShiftId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtExpenses = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(307, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 34);
            this.btnClose.TabIndex = 35;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrintEndShift
            // 
            this.btnPrintEndShift.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrintEndShift.FlatAppearance.BorderSize = 0;
            this.btnPrintEndShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintEndShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintEndShift.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintEndShift.Image")));
            this.btnPrintEndShift.Location = new System.Drawing.Point(48, 327);
            this.btnPrintEndShift.Name = "btnPrintEndShift";
            this.btnPrintEndShift.Size = new System.Drawing.Size(288, 101);
            this.btnPrintEndShift.TabIndex = 34;
            this.btnPrintEndShift.Text = "تقفيل الوردية";
            this.btnPrintEndShift.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintEndShift.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintEndShift.UseVisualStyleBackColor = true;
            this.btnPrintEndShift.Click += new System.EventHandler(this.btnPrintEndShift_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(22, 254);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(169, 34);
            this.txtTotal.TabIndex = 31;
            this.txtTotal.Text = "0";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(251, 254);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(90, 29);
            this.label4.TabIndex = 30;
            this.label4.Text = "الاجمالي :";
            // 
            // txtWared
            // 
            this.txtWared.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWared.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWared.Location = new System.Drawing.Point(22, 137);
            this.txtWared.Name = "txtWared";
            this.txtWared.ReadOnly = true;
            this.txtWared.Size = new System.Drawing.Size(169, 34);
            this.txtWared.TabIndex = 29;
            this.txtWared.Text = "0";
            this.txtWared.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(271, 137);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(70, 29);
            this.label3.TabIndex = 28;
            this.label3.Text = "الوارد :";
            // 
            // txtShiftId
            // 
            this.txtShiftId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShiftId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShiftId.Location = new System.Drawing.Point(22, 76);
            this.txtShiftId.Name = "txtShiftId";
            this.txtShiftId.ReadOnly = true;
            this.txtShiftId.Size = new System.Drawing.Size(169, 34);
            this.txtShiftId.TabIndex = 25;
            this.txtShiftId.Text = "0";
            this.txtShiftId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(232, 76);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(109, 29);
            this.label1.TabIndex = 24;
            this.label1.Text = "رقم الشيفت :";
            // 
            // txtExpenses
            // 
            this.txtExpenses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpenses.Location = new System.Drawing.Point(22, 198);
            this.txtExpenses.Name = "txtExpenses";
            this.txtExpenses.ReadOnly = true;
            this.txtExpenses.Size = new System.Drawing.Size(169, 34);
            this.txtExpenses.TabIndex = 37;
            this.txtExpenses.Text = "0";
            this.txtExpenses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(226, 200);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(115, 29);
            this.label5.TabIndex = 36;
            this.label5.Text = "المصروفات :";
            // 
            // FormEndShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(378, 456);
            this.Controls.Add(this.txtExpenses);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrintEndShift);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWared);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtShiftId);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEndShift";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormEndShift";
            this.Load += new System.EventHandler(this.FormEndShift_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrintEndShift;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtWared;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtShiftId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExpenses;
        private System.Windows.Forms.Label label5;
    }
}