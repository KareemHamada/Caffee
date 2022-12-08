
namespace POS.Forms
{
    partial class FormMainDB
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCreateNewCopy = new System.Windows.Forms.Button();
            this.btnRestoreCopy = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnCreateNewCopy, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRestoreCopy, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 906);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btnCreateNewCopy
            // 
            this.btnCreateNewCopy.BackColor = System.Drawing.Color.White;
            this.btnCreateNewCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCreateNewCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateNewCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCreateNewCopy.Image = global::POS.Properties.Resources.plus1;
            this.btnCreateNewCopy.Location = new System.Drawing.Point(595, 3);
            this.btnCreateNewCopy.Name = "btnCreateNewCopy";
            this.btnCreateNewCopy.Size = new System.Drawing.Size(586, 447);
            this.btnCreateNewCopy.TabIndex = 11;
            this.btnCreateNewCopy.Text = "انشاء نسخة احتياطية جديدة";
            this.btnCreateNewCopy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCreateNewCopy.UseVisualStyleBackColor = false;
            this.btnCreateNewCopy.Click += new System.EventHandler(this.btnCreateNewCopy_Click);
            // 
            // btnRestoreCopy
            // 
            this.btnRestoreCopy.BackColor = System.Drawing.Color.White;
            this.btnRestoreCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRestoreCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestoreCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRestoreCopy.Image = global::POS.Properties.Resources.backup;
            this.btnRestoreCopy.Location = new System.Drawing.Point(3, 3);
            this.btnRestoreCopy.Name = "btnRestoreCopy";
            this.btnRestoreCopy.Size = new System.Drawing.Size(586, 447);
            this.btnRestoreCopy.TabIndex = 12;
            this.btnRestoreCopy.Text = "استرجاع نسخة احتياطية";
            this.btnRestoreCopy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRestoreCopy.UseVisualStyleBackColor = false;
            this.btnRestoreCopy.Click += new System.EventHandler(this.btnRestoreCopy_Click);
            // 
            // FormMainDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 906);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FormMainDB";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnCreateNewCopy;
        private System.Windows.Forms.Button btnRestoreCopy;
    }
}