
namespace POS.Forms
{
    partial class FormFatora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFatora));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.txtFatoraRemain = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFatoraPaid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFatoraTotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFatoraClientName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFatoraOrderId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.txtFatoraRemain);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtFatoraPaid);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtFatoraTotal);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtFatoraClientName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtFatoraOrderId);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 462);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(306, 11);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 34);
            this.btnClose.TabIndex = 23;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Location = new System.Drawing.Point(84, 377);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(160, 74);
            this.btnPrint.TabIndex = 22;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // txtFatoraRemain
            // 
            this.txtFatoraRemain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraRemain.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraRemain.Location = new System.Drawing.Point(21, 316);
            this.txtFatoraRemain.Name = "txtFatoraRemain";
            this.txtFatoraRemain.ReadOnly = true;
            this.txtFatoraRemain.Size = new System.Drawing.Size(169, 34);
            this.txtFatoraRemain.TabIndex = 21;
            this.txtFatoraRemain.Text = "0";
            this.txtFatoraRemain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(264, 316);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(64, 29);
            this.label5.TabIndex = 20;
            this.label5.Text = "المتبقي";
            // 
            // txtFatoraPaid
            // 
            this.txtFatoraPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraPaid.Location = new System.Drawing.Point(21, 254);
            this.txtFatoraPaid.Name = "txtFatoraPaid";
            this.txtFatoraPaid.Size = new System.Drawing.Size(169, 34);
            this.txtFatoraPaid.TabIndex = 19;
            this.txtFatoraPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFatoraPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFatoraPaid_KeyPress);
            this.txtFatoraPaid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFatoraPaid_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(247, 254);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(81, 29);
            this.label4.TabIndex = 18;
            this.label4.Text = "المدفوع :";
            // 
            // txtFatoraTotal
            // 
            this.txtFatoraTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraTotal.Location = new System.Drawing.Point(21, 193);
            this.txtFatoraTotal.Name = "txtFatoraTotal";
            this.txtFatoraTotal.ReadOnly = true;
            this.txtFatoraTotal.Size = new System.Drawing.Size(169, 34);
            this.txtFatoraTotal.TabIndex = 17;
            this.txtFatoraTotal.Text = "0";
            this.txtFatoraTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(238, 191);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(90, 29);
            this.label3.TabIndex = 16;
            this.label3.Text = "الاجمالي :";
            // 
            // txtFatoraClientName
            // 
            this.txtFatoraClientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraClientName.Location = new System.Drawing.Point(21, 130);
            this.txtFatoraClientName.Name = "txtFatoraClientName";
            this.txtFatoraClientName.ReadOnly = true;
            this.txtFatoraClientName.Size = new System.Drawing.Size(169, 34);
            this.txtFatoraClientName.TabIndex = 15;
            this.txtFatoraClientName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(257, 128);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(71, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "العميل :";
            // 
            // txtFatoraOrderId
            // 
            this.txtFatoraOrderId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraOrderId.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraOrderId.Location = new System.Drawing.Point(21, 76);
            this.txtFatoraOrderId.Name = "txtFatoraOrderId";
            this.txtFatoraOrderId.ReadOnly = true;
            this.txtFatoraOrderId.Size = new System.Drawing.Size(169, 34);
            this.txtFatoraOrderId.TabIndex = 13;
            this.txtFatoraOrderId.Text = "0";
            this.txtFatoraOrderId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(214, 76);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(114, 29);
            this.label1.TabIndex = 12;
            this.label1.Text = "رقم الفاتورة :";
            // 
            // FormFatora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(378, 462);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormFatora";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormFatora";
            this.Load += new System.EventHandler(this.FormFatora_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox txtFatoraRemain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFatoraPaid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtFatoraOrderId;
        public System.Windows.Forms.TextBox txtFatoraTotal;
        public System.Windows.Forms.TextBox txtFatoraClientName;
        public System.Windows.Forms.Panel panel1;
    }
}