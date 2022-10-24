
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
            this.pnlParent = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrintEndShift = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtShiftId = new System.Windows.Forms.TextBox();
            this.txtWared = new System.Windows.Forms.TextBox();
            this.txtExpenses = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.pnlParent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlParent
            // 
            this.pnlParent.ColumnCount = 1;
            this.pnlParent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlParent.Controls.Add(this.btnPrintEndShift, 0, 1);
            this.pnlParent.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.pnlParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlParent.Location = new System.Drawing.Point(0, 0);
            this.pnlParent.Name = "pnlParent";
            this.pnlParent.RowCount = 2;
            this.pnlParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.16666F));
            this.pnlParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.83333F));
            this.pnlParent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlParent.Size = new System.Drawing.Size(452, 456);
            this.pnlParent.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.70796F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.29203F));
            this.tableLayoutPanel1.Controls.Add(this.txtTotal, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtShiftId, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtWared, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtExpenses, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(452, 360);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnPrintEndShift
            // 
            this.btnPrintEndShift.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrintEndShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintEndShift.FlatAppearance.BorderSize = 0;
            this.btnPrintEndShift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintEndShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintEndShift.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintEndShift.Image")));
            this.btnPrintEndShift.Location = new System.Drawing.Point(3, 363);
            this.btnPrintEndShift.Name = "btnPrintEndShift";
            this.btnPrintEndShift.Size = new System.Drawing.Size(446, 90);
            this.btnPrintEndShift.TabIndex = 35;
            this.btnPrintEndShift.Text = "تقفيل الوردية";
            this.btnPrintEndShift.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrintEndShift.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrintEndShift.UseVisualStyleBackColor = true;
            this.btnPrintEndShift.Click += new System.EventHandler(this.btnPrintEndShift_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(306, 27);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(136, 36);
            this.label1.TabIndex = 25;
            this.label1.Text = "رقم الشيفت :";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(330, 117);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(87, 36);
            this.label3.TabIndex = 29;
            this.label3.Text = "الوارد :";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(302, 207);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(144, 36);
            this.label5.TabIndex = 37;
            this.label5.Text = "المصروفات :";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(318, 297);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(112, 36);
            this.label4.TabIndex = 38;
            this.label4.Text = "الاجمالي :";
            // 
            // txtShiftId
            // 
            this.txtShiftId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShiftId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShiftId.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShiftId.Location = new System.Drawing.Point(3, 24);
            this.txtShiftId.Name = "txtShiftId";
            this.txtShiftId.ReadOnly = true;
            this.txtShiftId.Size = new System.Drawing.Size(290, 41);
            this.txtShiftId.TabIndex = 39;
            this.txtShiftId.Text = "0";
            this.txtShiftId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWared
            // 
            this.txtWared.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWared.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWared.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWared.Location = new System.Drawing.Point(3, 114);
            this.txtWared.Name = "txtWared";
            this.txtWared.ReadOnly = true;
            this.txtWared.Size = new System.Drawing.Size(290, 41);
            this.txtWared.TabIndex = 40;
            this.txtWared.Text = "0";
            this.txtWared.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtExpenses
            // 
            this.txtExpenses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpenses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExpenses.Location = new System.Drawing.Point(3, 204);
            this.txtExpenses.Name = "txtExpenses";
            this.txtExpenses.ReadOnly = true;
            this.txtExpenses.Size = new System.Drawing.Size(290, 41);
            this.txtExpenses.TabIndex = 41;
            this.txtExpenses.Text = "0";
            this.txtExpenses.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(3, 294);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(290, 41);
            this.txtTotal.TabIndex = 42;
            this.txtTotal.Text = "0";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FormEndShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(452, 456);
            this.Controls.Add(this.pnlParent);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEndShift";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormEndShift_Load);
            this.pnlParent.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlParent;
        private System.Windows.Forms.Button btnPrintEndShift;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtShiftId;
        public System.Windows.Forms.TextBox txtWared;
        private System.Windows.Forms.TextBox txtExpenses;
    }
}