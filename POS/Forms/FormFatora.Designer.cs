
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
            this.pnlParent = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFatoraOrderId = new System.Windows.Forms.TextBox();
            this.txtFatoraClientName = new System.Windows.Forms.TextBox();
            this.txtFatoraTotal = new System.Windows.Forms.TextBox();
            this.txtFatoraPaid = new System.Windows.Forms.TextBox();
            this.txtFatoraRemain = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlParent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlParent
            // 
            this.pnlParent.ColumnCount = 1;
            this.pnlParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlParent.Controls.Add(this.btnPrint, 0, 1);
            this.pnlParent.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.pnlParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParent.Location = new System.Drawing.Point(0, 0);
            this.pnlParent.Name = "pnlParent";
            this.pnlParent.RowCount = 2;
            this.pnlParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.16666F));
            this.pnlParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.83333F));
            this.pnlParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlParent.Size = new System.Drawing.Size(436, 462);
            this.pnlParent.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.70796F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.29203F));
            this.tableLayoutPanel1.Controls.Add(this.txtFatoraRemain, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtFatoraOrderId, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFatoraClientName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFatoraTotal, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtFatoraPaid, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(436, 365);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(290, 18);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(142, 36);
            this.label1.TabIndex = 13;
            this.label1.Text = "رقم الفاتورة :";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(316, 91);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(89, 36);
            this.label2.TabIndex = 15;
            this.label2.Text = "العميل :";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(305, 164);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(112, 36);
            this.label3.TabIndex = 17;
            this.label3.Text = "الاجمالي :";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(310, 237);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(102, 36);
            this.label4.TabIndex = 19;
            this.label4.Text = "المدفوع :";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(321, 310);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(79, 36);
            this.label5.TabIndex = 21;
            this.label5.Text = "المتبقي";
            // 
            // txtFatoraOrderId
            // 
            this.txtFatoraOrderId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFatoraOrderId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraOrderId.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraOrderId.Location = new System.Drawing.Point(3, 16);
            this.txtFatoraOrderId.Name = "txtFatoraOrderId";
            this.txtFatoraOrderId.ReadOnly = true;
            this.txtFatoraOrderId.Size = new System.Drawing.Size(280, 41);
            this.txtFatoraOrderId.TabIndex = 22;
            this.txtFatoraOrderId.Text = "0";
            this.txtFatoraOrderId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtFatoraClientName
            // 
            this.txtFatoraClientName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFatoraClientName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraClientName.Location = new System.Drawing.Point(3, 89);
            this.txtFatoraClientName.Name = "txtFatoraClientName";
            this.txtFatoraClientName.ReadOnly = true;
            this.txtFatoraClientName.Size = new System.Drawing.Size(280, 41);
            this.txtFatoraClientName.TabIndex = 23;
            this.txtFatoraClientName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtFatoraTotal
            // 
            this.txtFatoraTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFatoraTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraTotal.Location = new System.Drawing.Point(3, 162);
            this.txtFatoraTotal.Name = "txtFatoraTotal";
            this.txtFatoraTotal.ReadOnly = true;
            this.txtFatoraTotal.Size = new System.Drawing.Size(280, 41);
            this.txtFatoraTotal.TabIndex = 24;
            this.txtFatoraTotal.Text = "0";
            this.txtFatoraTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtFatoraPaid
            // 
            this.txtFatoraPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFatoraPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraPaid.Location = new System.Drawing.Point(3, 235);
            this.txtFatoraPaid.Name = "txtFatoraPaid";
            this.txtFatoraPaid.Size = new System.Drawing.Size(280, 41);
            this.txtFatoraPaid.TabIndex = 25;
            this.txtFatoraPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFatoraPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFatoraPaid_KeyPress);
            this.txtFatoraPaid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFatoraPaid_KeyUp);
            // 
            // txtFatoraRemain
            // 
            this.txtFatoraRemain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFatoraRemain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFatoraRemain.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFatoraRemain.Location = new System.Drawing.Point(3, 308);
            this.txtFatoraRemain.Name = "txtFatoraRemain";
            this.txtFatoraRemain.ReadOnly = true;
            this.txtFatoraRemain.Size = new System.Drawing.Size(280, 41);
            this.txtFatoraRemain.TabIndex = 26;
            this.txtFatoraRemain.Text = "0";
            this.txtFatoraRemain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPrint.BackgroundImage")));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Location = new System.Drawing.Point(3, 368);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(430, 91);
            this.btnPrint.TabIndex = 23;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // FormFatora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(436, 462);
            this.Controls.Add(this.pnlParent);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFatora";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormFatora_FormClosing);
         
            this.Load += new System.EventHandler(this.FormFatora_Load);
            this.pnlParent.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlParent;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtFatoraRemain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtFatoraOrderId;
        public System.Windows.Forms.TextBox txtFatoraClientName;
        public System.Windows.Forms.TextBox txtFatoraTotal;
        private System.Windows.Forms.TextBox txtFatoraPaid;
    }
}