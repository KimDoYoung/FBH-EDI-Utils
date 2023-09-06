namespace EdiDbUploader
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.scBatang = new System.Windows.Forms.SplitContainer();
            this.lvEdiExcels = new System.Windows.Forms.ListView();
            this.panelLeftTop = new System.Windows.Forms.Panel();
            this.btnFileAdd = new System.Windows.Forms.Button();
            this.btnFolderAdd = new System.Windows.Forms.Button();
            this.logBox = new FBH.EDI.Controls.LogTextBox();
            this.panelRightTop = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scBatang)).BeginInit();
            this.scBatang.Panel1.SuspendLayout();
            this.scBatang.Panel2.SuspendLayout();
            this.scBatang.SuspendLayout();
            this.panelLeftTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Location = new System.Drawing.Point(0, 451);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(896, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(896, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolStripMenuItem1,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.configToolStripMenuItem.Text = "&Config";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(107, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(896, 35);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(32, 32);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // scBatang
            // 
            this.scBatang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBatang.Location = new System.Drawing.Point(0, 59);
            this.scBatang.Name = "scBatang";
            // 
            // scBatang.Panel1
            // 
            this.scBatang.Panel1.Controls.Add(this.lvEdiExcels);
            this.scBatang.Panel1.Controls.Add(this.panelLeftTop);
            // 
            // scBatang.Panel2
            // 
            this.scBatang.Panel2.Controls.Add(this.logBox);
            this.scBatang.Panel2.Controls.Add(this.panelRightTop);
            this.scBatang.Size = new System.Drawing.Size(896, 392);
            this.scBatang.SplitterDistance = 298;
            this.scBatang.TabIndex = 3;
            // 
            // lvEdiExcels
            // 
            this.lvEdiExcels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEdiExcels.HideSelection = false;
            this.lvEdiExcels.Location = new System.Drawing.Point(0, 33);
            this.lvEdiExcels.Name = "lvEdiExcels";
            this.lvEdiExcels.Size = new System.Drawing.Size(298, 359);
            this.lvEdiExcels.TabIndex = 1;
            this.lvEdiExcels.UseCompatibleStateImageBehavior = false;
            // 
            // panelLeftTop
            // 
            this.panelLeftTop.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelLeftTop.Controls.Add(this.btnFileAdd);
            this.panelLeftTop.Controls.Add(this.btnFolderAdd);
            this.panelLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLeftTop.Location = new System.Drawing.Point(0, 0);
            this.panelLeftTop.Name = "panelLeftTop";
            this.panelLeftTop.Size = new System.Drawing.Size(298, 33);
            this.panelLeftTop.TabIndex = 0;
            // 
            // btnFileAdd
            // 
            this.btnFileAdd.Location = new System.Drawing.Point(108, 4);
            this.btnFileAdd.Name = "btnFileAdd";
            this.btnFileAdd.Size = new System.Drawing.Size(97, 26);
            this.btnFileAdd.TabIndex = 1;
            this.btnFileAdd.Text = "File Add...";
            this.btnFileAdd.UseVisualStyleBackColor = true;
            // 
            // btnFolderAdd
            // 
            this.btnFolderAdd.Location = new System.Drawing.Point(8, 4);
            this.btnFolderAdd.Name = "btnFolderAdd";
            this.btnFolderAdd.Size = new System.Drawing.Size(94, 26);
            this.btnFolderAdd.TabIndex = 0;
            this.btnFolderAdd.Text = "Folder Add...";
            this.btnFolderAdd.UseVisualStyleBackColor = true;
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Location = new System.Drawing.Point(0, 33);
            this.logBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(594, 359);
            this.logBox.TabIndex = 0;
            // 
            // panelRightTop
            // 
            this.panelRightTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRightTop.Location = new System.Drawing.Point(0, 0);
            this.panelRightTop.Name = "panelRightTop";
            this.panelRightTop.Size = new System.Drawing.Size(594, 33);
            this.panelRightTop.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 473);
            this.Controls.Add(this.scBatang);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EdiDbUploader";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.scBatang.Panel1.ResumeLayout(false);
            this.scBatang.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scBatang)).EndInit();
            this.scBatang.ResumeLayout(false);
            this.panelLeftTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.SplitContainer scBatang;
        private FBH.EDI.Controls.LogTextBox logBox;
        private System.Windows.Forms.Panel panelRightTop;
        private System.Windows.Forms.Panel panelLeftTop;
        private System.Windows.Forms.ListView lvEdiExcels;
        private System.Windows.Forms.Button btnFileAdd;
        private System.Windows.Forms.Button btnFolderAdd;
    }
}

