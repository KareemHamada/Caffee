
namespace POS.Forms
{
    partial class FormMainReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainReports));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClientsCashWithdraw = new System.Windows.Forms.Button();
            this.btnShifts = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnExpensesReport = new System.Windows.Forms.Button();
            this.btnReportStore = new System.Windows.Forms.Button();
            this.btnReportSalaries = new System.Windows.Forms.Button();
            this.btnReportAttendLeave = new System.Windows.Forms.Button();
            this.btnReportWithdrawDeposit = new System.Windows.Forms.Button();
            this.btnStoreOrderItems = new System.Windows.Forms.Button();
            this.btnReportItems = new System.Windows.Forms.Button();
            this.btnSupplierCashWithdraw = new System.Windows.Forms.Button();
            this.btnReportOverAll = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnClientsCashWithdraw, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnShifts, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOrders, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnExpensesReport, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnReportStore, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnReportSalaries, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnReportAttendLeave, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnReportWithdrawDeposit, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnStoreOrderItems, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnReportItems, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSupplierCashWithdraw, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnReportOverAll, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1402, 753);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnClientsCashWithdraw
            // 
            this.btnClientsCashWithdraw.BackColor = System.Drawing.Color.White;
            this.btnClientsCashWithdraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClientsCashWithdraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClientsCashWithdraw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClientsCashWithdraw.Image = ((System.Drawing.Image)(resources.GetObject("btnClientsCashWithdraw.Image")));
            this.btnClientsCashWithdraw.Location = new System.Drawing.Point(353, 505);
            this.btnClientsCashWithdraw.Name = "btnClientsCashWithdraw";
            this.btnClientsCashWithdraw.Size = new System.Drawing.Size(344, 245);
            this.btnClientsCashWithdraw.TabIndex = 24;
            this.btnClientsCashWithdraw.Text = "تقرير اجل و سداد عملاء";
            this.btnClientsCashWithdraw.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClientsCashWithdraw.UseVisualStyleBackColor = false;
            this.btnClientsCashWithdraw.Click += new System.EventHandler(this.btnClientsCashWithdraw_Click);
            // 
            // btnShifts
            // 
            this.btnShifts.BackColor = System.Drawing.Color.White;
            this.btnShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShifts.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShifts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnShifts.Image = ((System.Drawing.Image)(resources.GetObject("btnShifts.Image")));
            this.btnShifts.Location = new System.Drawing.Point(1053, 3);
            this.btnShifts.Name = "btnShifts";
            this.btnShifts.Size = new System.Drawing.Size(346, 245);
            this.btnShifts.TabIndex = 8;
            this.btnShifts.Text = "تقرير ورديات";
            this.btnShifts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnShifts.UseVisualStyleBackColor = false;
            this.btnShifts.Click += new System.EventHandler(this.btnShifts_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.BackColor = System.Drawing.Color.White;
            this.btnOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOrders.Image = ((System.Drawing.Image)(resources.GetObject("btnOrders.Image")));
            this.btnOrders.Location = new System.Drawing.Point(703, 3);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(344, 245);
            this.btnOrders.TabIndex = 9;
            this.btnOrders.Text = "تقرير طلبات";
            this.btnOrders.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // btnExpensesReport
            // 
            this.btnExpensesReport.BackColor = System.Drawing.Color.White;
            this.btnExpensesReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExpensesReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpensesReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnExpensesReport.Image = ((System.Drawing.Image)(resources.GetObject("btnExpensesReport.Image")));
            this.btnExpensesReport.Location = new System.Drawing.Point(353, 3);
            this.btnExpensesReport.Name = "btnExpensesReport";
            this.btnExpensesReport.Size = new System.Drawing.Size(344, 245);
            this.btnExpensesReport.TabIndex = 10;
            this.btnExpensesReport.Text = "تقرير مصروفات";
            this.btnExpensesReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExpensesReport.UseVisualStyleBackColor = false;
            this.btnExpensesReport.Click += new System.EventHandler(this.btnExpensesReport_Click);
            // 
            // btnReportStore
            // 
            this.btnReportStore.BackColor = System.Drawing.Color.White;
            this.btnReportStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportStore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnReportStore.Image = ((System.Drawing.Image)(resources.GetObject("btnReportStore.Image")));
            this.btnReportStore.Location = new System.Drawing.Point(3, 3);
            this.btnReportStore.Name = "btnReportStore";
            this.btnReportStore.Size = new System.Drawing.Size(344, 245);
            this.btnReportStore.TabIndex = 16;
            this.btnReportStore.Text = "تقرير وارد مخازن فواتير";
            this.btnReportStore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReportStore.UseVisualStyleBackColor = false;
            this.btnReportStore.Click += new System.EventHandler(this.btnReportStore_Click);
            // 
            // btnReportSalaries
            // 
            this.btnReportSalaries.BackColor = System.Drawing.Color.White;
            this.btnReportSalaries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportSalaries.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportSalaries.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnReportSalaries.Image = ((System.Drawing.Image)(resources.GetObject("btnReportSalaries.Image")));
            this.btnReportSalaries.Location = new System.Drawing.Point(1053, 254);
            this.btnReportSalaries.Name = "btnReportSalaries";
            this.btnReportSalaries.Size = new System.Drawing.Size(346, 245);
            this.btnReportSalaries.TabIndex = 17;
            this.btnReportSalaries.Text = "تقرير مرتبات";
            this.btnReportSalaries.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReportSalaries.UseVisualStyleBackColor = false;
            this.btnReportSalaries.Click += new System.EventHandler(this.btnReportSalaries_Click);
            // 
            // btnReportAttendLeave
            // 
            this.btnReportAttendLeave.BackColor = System.Drawing.Color.White;
            this.btnReportAttendLeave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportAttendLeave.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportAttendLeave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnReportAttendLeave.Image = ((System.Drawing.Image)(resources.GetObject("btnReportAttendLeave.Image")));
            this.btnReportAttendLeave.Location = new System.Drawing.Point(703, 254);
            this.btnReportAttendLeave.Name = "btnReportAttendLeave";
            this.btnReportAttendLeave.Size = new System.Drawing.Size(344, 245);
            this.btnReportAttendLeave.TabIndex = 18;
            this.btnReportAttendLeave.Text = "تقرير حضور و انصراف";
            this.btnReportAttendLeave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReportAttendLeave.UseVisualStyleBackColor = false;
            this.btnReportAttendLeave.Click += new System.EventHandler(this.btnReportAttendLeave_Click);
            // 
            // btnReportWithdrawDeposit
            // 
            this.btnReportWithdrawDeposit.BackColor = System.Drawing.Color.White;
            this.btnReportWithdrawDeposit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportWithdrawDeposit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportWithdrawDeposit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnReportWithdrawDeposit.Image = ((System.Drawing.Image)(resources.GetObject("btnReportWithdrawDeposit.Image")));
            this.btnReportWithdrawDeposit.Location = new System.Drawing.Point(353, 254);
            this.btnReportWithdrawDeposit.Name = "btnReportWithdrawDeposit";
            this.btnReportWithdrawDeposit.Size = new System.Drawing.Size(344, 245);
            this.btnReportWithdrawDeposit.TabIndex = 19;
            this.btnReportWithdrawDeposit.Text = "تقرير استلاف و سداد موظفين";
            this.btnReportWithdrawDeposit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReportWithdrawDeposit.UseVisualStyleBackColor = false;
            this.btnReportWithdrawDeposit.Click += new System.EventHandler(this.btnReportWithdrawDeposit_Click);
            // 
            // btnStoreOrderItems
            // 
            this.btnStoreOrderItems.BackColor = System.Drawing.Color.White;
            this.btnStoreOrderItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStoreOrderItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStoreOrderItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnStoreOrderItems.Image = ((System.Drawing.Image)(resources.GetObject("btnStoreOrderItems.Image")));
            this.btnStoreOrderItems.Location = new System.Drawing.Point(3, 254);
            this.btnStoreOrderItems.Name = "btnStoreOrderItems";
            this.btnStoreOrderItems.Size = new System.Drawing.Size(344, 245);
            this.btnStoreOrderItems.TabIndex = 20;
            this.btnStoreOrderItems.Text = "وارد مخازن منتجات";
            this.btnStoreOrderItems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStoreOrderItems.UseVisualStyleBackColor = false;
            this.btnStoreOrderItems.Click += new System.EventHandler(this.btnStoreOrderItems_Click);
            // 
            // btnReportItems
            // 
            this.btnReportItems.BackColor = System.Drawing.Color.White;
            this.btnReportItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnReportItems.Image = ((System.Drawing.Image)(resources.GetObject("btnReportItems.Image")));
            this.btnReportItems.Location = new System.Drawing.Point(1053, 505);
            this.btnReportItems.Name = "btnReportItems";
            this.btnReportItems.Size = new System.Drawing.Size(346, 245);
            this.btnReportItems.TabIndex = 21;
            this.btnReportItems.Text = "تقرير عناصر";
            this.btnReportItems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReportItems.UseVisualStyleBackColor = false;
            this.btnReportItems.Click += new System.EventHandler(this.btnReportItems_Click);
            // 
            // btnSupplierCashWithdraw
            // 
            this.btnSupplierCashWithdraw.BackColor = System.Drawing.Color.White;
            this.btnSupplierCashWithdraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSupplierCashWithdraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplierCashWithdraw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSupplierCashWithdraw.Image = ((System.Drawing.Image)(resources.GetObject("btnSupplierCashWithdraw.Image")));
            this.btnSupplierCashWithdraw.Location = new System.Drawing.Point(703, 505);
            this.btnSupplierCashWithdraw.Name = "btnSupplierCashWithdraw";
            this.btnSupplierCashWithdraw.Size = new System.Drawing.Size(344, 245);
            this.btnSupplierCashWithdraw.TabIndex = 22;
            this.btnSupplierCashWithdraw.Text = "تقرير اجل و سداد موردين";
            this.btnSupplierCashWithdraw.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSupplierCashWithdraw.UseVisualStyleBackColor = false;
            this.btnSupplierCashWithdraw.Click += new System.EventHandler(this.btnSupplierCashWithdraw_Click);
            // 
            // btnReportOverAll
            // 
            this.btnReportOverAll.BackColor = System.Drawing.Color.White;
            this.btnReportOverAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReportOverAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportOverAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnReportOverAll.Image = ((System.Drawing.Image)(resources.GetObject("btnReportOverAll.Image")));
            this.btnReportOverAll.Location = new System.Drawing.Point(3, 505);
            this.btnReportOverAll.Name = "btnReportOverAll";
            this.btnReportOverAll.Size = new System.Drawing.Size(344, 245);
            this.btnReportOverAll.TabIndex = 23;
            this.btnReportOverAll.Text = "تقرير شامل";
            this.btnReportOverAll.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReportOverAll.UseVisualStyleBackColor = false;
            this.btnReportOverAll.Click += new System.EventHandler(this.btnReportOverAll_Click);
            // 
            // FormMainReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1402, 753);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMainReports";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FormMainReports";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnShifts;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnExpensesReport;
        private System.Windows.Forms.Button btnReportStore;
        private System.Windows.Forms.Button btnReportSalaries;
        private System.Windows.Forms.Button btnReportAttendLeave;
        private System.Windows.Forms.Button btnReportWithdrawDeposit;
        private System.Windows.Forms.Button btnStoreOrderItems;
        private System.Windows.Forms.Button btnReportItems;
        private System.Windows.Forms.Button btnSupplierCashWithdraw;
        private System.Windows.Forms.Button btnReportOverAll;
        private System.Windows.Forms.Button btnClientsCashWithdraw;
    }
}