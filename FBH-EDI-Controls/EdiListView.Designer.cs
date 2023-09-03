namespace FBH_EDI_Controls
{
    partial class EdiListView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlBatang = new System.Windows.Forms.Panel();
            this.lvEdiFiles = new System.Windows.Forms.ListView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlBatang.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(507, 38);
            this.pnlTop.TabIndex = 0;
            // 
            // pnlBatang
            // 
            this.pnlBatang.Controls.Add(this.lvEdiFiles);
            this.pnlBatang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBatang.Location = new System.Drawing.Point(0, 38);
            this.pnlBatang.Name = "pnlBatang";
            this.pnlBatang.Size = new System.Drawing.Size(507, 465);
            this.pnlBatang.TabIndex = 1;
            // 
            // lvEdiFiles
            // 
            this.lvEdiFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEdiFiles.HideSelection = false;
            this.lvEdiFiles.Location = new System.Drawing.Point(0, 0);
            this.lvEdiFiles.Name = "lvEdiFiles";
            this.lvEdiFiles.Size = new System.Drawing.Size(507, 465);
            this.lvEdiFiles.TabIndex = 0;
            this.lvEdiFiles.UseCompatibleStateImageBehavior = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(3, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(34, 18);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "title";
            // 
            // EdiListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBatang);
            this.Controls.Add(this.pnlTop);
            this.Name = "EdiListView";
            this.Size = new System.Drawing.Size(507, 503);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBatang.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBatang;
        private System.Windows.Forms.ListView lvEdiFiles;
        private System.Windows.Forms.Label lblTitle;
    }
}
