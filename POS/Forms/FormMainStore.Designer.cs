
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
            this.btnItemsLowQty = new System.Windows.Forms.Button();
            this.btnCashWithdraw = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnStoreItems = new System.Windows.Forms.Button();
            this.btnAddingToStore = new System.Windows.Forms.Button();
            this.btnRelation = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnItemsLowQty, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCashWithdraw, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSuppliers, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStoreItems, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddingToStore, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnRelation, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1202, 953);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnItemsLowQty
            // 
            this.btnItemsLowQty.BackColor = System.Drawing.Color.White;
            this.btnItemsLowQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnItemsLowQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemsLowQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnItemsLowQty.Image = ((System.Drawing.Image)(resources.GetObject("btnItemsLowQty.Image")));
            this.btnItemsLowQty.Location = new System.Drawing.Point(3, 685);
            this.btnItemsLowQty.Name = "btnItemsLowQty";
            this.btnItemsLowQty.Size = new System.Drawing.Size(595, 265);
            this.btnItemsLowQty.TabIndex = 16;
            this.btnItemsLowQty.Text = "متابعة العناصر المربوطة مع المخزن";
            this.btnItemsLowQty.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnItemsLowQty.UseVisualStyleBackColor = false;
            this.btnItemsLowQty.Click += new System.EventHandler(this.btnItemsLowQty_Click);
            // 
            // btnCashWithdraw
            // 
            this.btnCashWithdraw.BackColor = System.Drawing.Color.White;
            this.btnCashWithdraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCashWithdraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCashWithdraw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCashWithdraw.Image = ((System.Drawing.Image)(resources.GetObject("btnCashWithdraw.Image")));
            this.btnCashWithdraw.Location = new System.Drawing.Point(3, 344);
            this.btnCashWithdraw.Name = "btnCashWithdraw";
            this.btnCashWithdraw.Size = new System.Drawing.Size(595, 335);
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
            this.btnSuppliers.Size = new System.Drawing.Size(595, 335);
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
            this.btnStoreItems.Size = new System.Drawing.Size(595, 335);
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
            this.btnAddingToStore.Location = new System.Drawing.Point(604, 344);
            this.btnAddingToStore.Name = "btnAddingToStore";
            this.btnAddingToStore.Size = new System.Drawing.Size(595, 335);
            this.btnAddingToStore.TabIndex = 13;
            this.btnAddingToStore.Text = "اضافة واردات";
            this.btnAddingToStore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddingToStore.UseVisualStyleBackColor = false;
            this.btnAddingToStore.Click += new System.EventHandler(this.btnAddingToStore_Click);
            // 
            // btnRelation
            // 
            this.btnRelation.BackColor = System.Drawing.Color.White;
            this.btnRelation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRelation.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRelation.Image = ((System.Drawing.Image)(resources.GetObject("btnRelation.Image")));
            this.btnRelation.Location = new System.Drawing.Point(604, 685);
            this.btnRelation.Name = "btnRelation";
            this.btnRelation.Size = new System.Drawing.Size(595, 265);
            this.btnRelation.TabIndex = 15;
            this.btnRelation.Text = "ربط العناصر مع المخزن";
            this.btnRelation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRelation.UseVisualStyleBackColor = false;
            this.btnRelation.Click += new System.EventHandler(this.btnRelation_Click);
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
        private System.Windows.Forms.Button btnRelation;
        private System.Windows.Forms.Button btnItemsLowQty;
    }
}