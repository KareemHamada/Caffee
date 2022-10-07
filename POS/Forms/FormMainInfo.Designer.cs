
namespace POS.Forms
{
    partial class FormMainInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMainInfo));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnTables = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnTayar = new System.Windows.Forms.Button();
            this.btnRegions = new System.Windows.Forms.Button();
            this.btnCategories = new System.Windows.Forms.Button();
            this.btnItems = new System.Windows.Forms.Button();
            this.btnExpenses = new System.Windows.Forms.Button();
            this.btnClients = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.btnTables, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnUsers, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTayar, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRegions, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCategories, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnItems, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnExpenses, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnClients, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnDeleteOrder, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1420, 800);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnTables
            // 
            this.btnTables.BackColor = System.Drawing.Color.White;
            this.btnTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTables.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTables.Image = global::POS.Properties.Resources.V;
            this.btnTables.Location = new System.Drawing.Point(3, 535);
            this.btnTables.Name = "btnTables";
            this.btnTables.Size = new System.Drawing.Size(467, 262);
            this.btnTables.TabIndex = 15;
            this.btnTables.Text = "الترابيزات";
            this.btnTables.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTables.UseVisualStyleBackColor = false;
            this.btnTables.Click += new System.EventHandler(this.btnTables_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.BackColor = System.Drawing.Color.White;
            this.btnUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnUsers.Image = ((System.Drawing.Image)(resources.GetObject("btnUsers.Image")));
            this.btnUsers.Location = new System.Drawing.Point(949, 3);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(468, 260);
            this.btnUsers.TabIndex = 7;
            this.btnUsers.Text = "المستخدمين";
            this.btnUsers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUsers.UseVisualStyleBackColor = false;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnTayar
            // 
            this.btnTayar.BackColor = System.Drawing.Color.White;
            this.btnTayar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTayar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTayar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTayar.Image = ((System.Drawing.Image)(resources.GetObject("btnTayar.Image")));
            this.btnTayar.Location = new System.Drawing.Point(476, 3);
            this.btnTayar.Name = "btnTayar";
            this.btnTayar.Size = new System.Drawing.Size(467, 260);
            this.btnTayar.TabIndex = 8;
            this.btnTayar.Text = "الطيارين";
            this.btnTayar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTayar.UseVisualStyleBackColor = false;
            this.btnTayar.Click += new System.EventHandler(this.btnTayar_Click);
            // 
            // btnRegions
            // 
            this.btnRegions.BackColor = System.Drawing.Color.White;
            this.btnRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRegions.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRegions.Image = ((System.Drawing.Image)(resources.GetObject("btnRegions.Image")));
            this.btnRegions.Location = new System.Drawing.Point(3, 3);
            this.btnRegions.Name = "btnRegions";
            this.btnRegions.Size = new System.Drawing.Size(467, 260);
            this.btnRegions.TabIndex = 9;
            this.btnRegions.Text = "المناطق";
            this.btnRegions.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRegions.UseVisualStyleBackColor = false;
            this.btnRegions.Click += new System.EventHandler(this.btnRegions_Click);
            // 
            // btnCategories
            // 
            this.btnCategories.BackColor = System.Drawing.Color.White;
            this.btnCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCategories.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCategories.Image = ((System.Drawing.Image)(resources.GetObject("btnCategories.Image")));
            this.btnCategories.Location = new System.Drawing.Point(949, 269);
            this.btnCategories.Name = "btnCategories";
            this.btnCategories.Size = new System.Drawing.Size(468, 260);
            this.btnCategories.TabIndex = 10;
            this.btnCategories.Text = "الاصناف";
            this.btnCategories.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCategories.UseVisualStyleBackColor = false;
            this.btnCategories.Click += new System.EventHandler(this.btnCategories_Click);
            // 
            // btnItems
            // 
            this.btnItems.BackColor = System.Drawing.Color.White;
            this.btnItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnItems.Image = ((System.Drawing.Image)(resources.GetObject("btnItems.Image")));
            this.btnItems.Location = new System.Drawing.Point(476, 269);
            this.btnItems.Name = "btnItems";
            this.btnItems.Size = new System.Drawing.Size(467, 260);
            this.btnItems.TabIndex = 11;
            this.btnItems.Text = "العناصر";
            this.btnItems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnItems.UseVisualStyleBackColor = false;
            this.btnItems.Click += new System.EventHandler(this.btnItems_Click);
            // 
            // btnExpenses
            // 
            this.btnExpenses.BackColor = System.Drawing.Color.White;
            this.btnExpenses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpenses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnExpenses.Image = ((System.Drawing.Image)(resources.GetObject("btnExpenses.Image")));
            this.btnExpenses.Location = new System.Drawing.Point(3, 269);
            this.btnExpenses.Name = "btnExpenses";
            this.btnExpenses.Size = new System.Drawing.Size(467, 260);
            this.btnExpenses.TabIndex = 12;
            this.btnExpenses.Text = "المصاريف";
            this.btnExpenses.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExpenses.UseVisualStyleBackColor = false;
            this.btnExpenses.Click += new System.EventHandler(this.btnExpenses_Click);
            // 
            // btnClients
            // 
            this.btnClients.BackColor = System.Drawing.Color.White;
            this.btnClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClients.Image = ((System.Drawing.Image)(resources.GetObject("btnClients.Image")));
            this.btnClients.Location = new System.Drawing.Point(949, 535);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(468, 262);
            this.btnClients.TabIndex = 13;
            this.btnClients.Text = "العملاء";
            this.btnClients.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClients.UseVisualStyleBackColor = false;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.BackColor = System.Drawing.Color.White;
            this.btnDeleteOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnDeleteOrder.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteOrder.Image")));
            this.btnDeleteOrder.Location = new System.Drawing.Point(476, 535);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(467, 262);
            this.btnDeleteOrder.TabIndex = 14;
            this.btnDeleteOrder.Text = "حذف طلبات";
            this.btnDeleteOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDeleteOrder.UseVisualStyleBackColor = false;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // FormMainInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1420, 800);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMainInfo";
            this.Text = "FormMainInfo";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnTables;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnTayar;
        private System.Windows.Forms.Button btnRegions;
        private System.Windows.Forms.Button btnCategories;
        private System.Windows.Forms.Button btnItems;
        private System.Windows.Forms.Button btnExpenses;
        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnDeleteOrder;
    }
}