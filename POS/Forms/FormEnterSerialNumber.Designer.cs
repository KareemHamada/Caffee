
namespace POS.Forms
{
    partial class FormEnterSerialNumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEnterSerialNumber));
            this.btnClose = new System.Windows.Forms.Button();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtDS = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(413, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(38, 39);
            this.btnClose.TabIndex = 13;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSerialNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNumber.Location = new System.Drawing.Point(93, 39);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new System.Drawing.Size(279, 41);
            this.txtSerialNumber.TabIndex = 14;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(165, 183);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(160, 45);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtDS
            // 
            this.txtDS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDS.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDS.Location = new System.Drawing.Point(92, 111);
            this.txtDS.Name = "txtDS";
            this.txtDS.Size = new System.Drawing.Size(279, 41);
            this.txtDS.TabIndex = 16;
            // 
            // FormEnterSerialNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(463, 262);
            this.Controls.Add(this.txtDS);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEnterSerialNumber";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormEnterSerialNumber";
            this.Load += new System.EventHandler(this.FormEnterSerialNumber_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtDS;
    }
}