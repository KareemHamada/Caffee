
namespace POS.Forms
{
    partial class TablesPOSForm
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
            this.pnlRest = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pnlRest
            // 
            this.pnlRest.AutoScroll = true;
            this.pnlRest.AutoSize = true;
            this.pnlRest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRest.Location = new System.Drawing.Point(0, 0);
            this.pnlRest.Name = "pnlRest";
            this.pnlRest.Size = new System.Drawing.Size(1182, 753);
            this.pnlRest.TabIndex = 42;
            // 
            // TablesPOSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.pnlRest);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(824, 524);
            this.Name = "TablesPOSForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TablesPOSForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlRest;
    }
}