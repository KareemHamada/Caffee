
namespace POS.Forms
{
    partial class FormMainStore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainStore));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCashWithdraw = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnStoreItems = new System.Windows.Forms.Button();
            this.btnAddingToStore = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnCashWithdraw, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSuppliers, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStoreItems, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddingToStore, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1202, 953);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnCashWithdraw
            // 
            this.btnCashWithdraw.BackColor = System.Drawing.Color.White;
            this.btnCashWithdraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCashWithdraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCashWithdraw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCashWithdraw.Image = ((System.Drawing.Image)(resources.GetObject("btnCashWithdraw.Image")));
            this.btnCashWithdraw.Location = new System.Drawing.Point(3, 479);
            this.btnCashWithdraw.Name = "btnCashWithdraw";
            this.btnCashWithdraw.Size = new System.Drawing.Size(595, 471);
            this.btnCashWithdraw.TabIndex = 14;
            this.btnCashWithdraw.Text = "اجل و سداد موردين";
            this.btnCashWithdraw.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCashWithdraw.UseVisualStyleBackColor = false;
            this.btnCashWithdraw.Click += new System.EventHandler(this.btnCashWithdraw_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.BackColor = System.Drawing.Color.White;
            this.btnSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSuppliers.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSuppliers.Image = ((System.Drawing.Image)(resources.GetObject("btnSuppliers.Image")));
            this.btnSuppliers.Location = new System.Drawing.Point(604, 3);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(595, 470);
            this.btnSuppliers.TabIndex = 11;
            this.btnSuppliers.Text = "الموردين";
            this.btnSuppliers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSuppliers.UseVisualStyleBackColor = false;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnStoreItems
            // 
            this.btnStoreItems.BackColor = System.Drawing.Color.White;
            this.btnStoreItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStoreItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStoreItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnStoreItems.Image = ((System.Drawing.Image)(resources.GetObject("btnStoreItems.Image")));
            this.btnStoreItems.Location = new System.Drawing.Point(3, 3);
            this.btnStoreItems.Name = "btnStoreItems";
            this.btnStoreItems.Size = new System.Drawing.Size(595, 470);
            this.btnStoreItems.TabIndex = 12;
            this.btnStoreItems.Text = "عناصر المخزن";
            this.btnStoreItems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStoreItems.UseVisualStyleBackColor = false;
            this.btnStoreItems.Click += new System.EventHandler(this.btnStoreItems_Click);
            // 
            // btnAddingToStore
            // 
            this.btnAddingToStore.BackColor = System.Drawing.Color.White;
            this.btnAddingToStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddingToStore.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddingToStore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnAddingToStore.Image = ((System.Drawing.Image)(resources.GetObject("btnAddingToStore.Image")));
            this.btnAddingToStore.Location = new System.Drawing.Point(604, 479);
            this.btnAddingToStore.Name = "btnAddingToStore";
            this.btnAddingToStore.Size = new System.Drawing.Size(595, 471);
            this.btnAddingToStore.TabIndex = 13;
            this.btnAddingToStore.Text = "اضافة واردات";
            this.btnAddingToStore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddingToStore.UseVisualStyleBackColor = false;
            this.btnAddingToStore.Click += new System.EventHandler(this.btnAddingToStore_Click);
            // 
            // FormMainStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1202, 953);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMainStore";
            this.Text = "FormMainStore";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCashWithdraw;
        private System.Windows.Forms.Button btnSuppliers;
        private System.Windows.Forms.Button btnStoreItems;
        private System.Windows.Forms.Button btnAddingToStore;
    }
}