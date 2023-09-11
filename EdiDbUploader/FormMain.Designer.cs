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
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnConfig = new System.Windows.Forms.ToolStripButton();
            this.scBatang = new System.Windows.Forms.SplitContainer();
            this.lvEdiExcels = new System.Windows.Forms.ListView();
            this.panelLeftTop = new System.Windows.Forms.Panel();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnFileAdd = new System.Windows.Forms.Button();
            this.btnFolderAdd = new System.Windows.Forms.Button();
            this.panelRightTop = new System.Windows.Forms.Panel();
            this.btnRun = new System.Windows.Forms.Button();
            this.logBox = new FBH.EDI.Controls.LogTextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scBatang)).BeginInit();
            this.scBatang.Panel1.SuspendLayout();
            this.scBatang.Panel2.SuspendLayout();
            this.scBatang.SuspendLayout();
            this.panelLeftTop.SuspendLayout();
            this.panelRightTop.SuspendLayout();
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
            this.mnuFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(896, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileConfig,
            this.mnuFileAbout,
            this.toolStripMenuItem1,
            this.mnuFileQuit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 22);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileConfig
            // 
            this.mnuFileConfig.Name = "mnuFileConfig";
            this.mnuFileConfig.Size = new System.Drawing.Size(144, 22);
            this.mnuFileConfig.Text = "&Config";
            this.mnuFileConfig.Click += new System.EventHandler(this.MenuButtonClick);
            // 
            // mnuFileAbout
            // 
            this.mnuFileAbout.Name = "mnuFileAbout";
            this.mnuFileAbout.Size = new System.Drawing.Size(144, 22);
            this.mnuFileAbout.Text = "&About";
            this.mnuFileAbout.Click += new System.EventHandler(this.MenuButtonClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(141, 6);
            // 
            // mnuFileQuit
            // 
            this.mnuFileQuit.Name = "mnuFileQuit";
            this.mnuFileQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.mnuFileQuit.Size = new System.Drawing.Size(144, 22);
            this.mnuFileQuit.Text = "&Quit";
            this.mnuFileQuit.Click += new System.EventHandler(this.MenuButtonClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConfig});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(896, 35);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnConfig
            // 
            this.btnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConfig.Image = global::EdiDbUploader.Properties.Resources.DatabaseSettings;
            this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(32, 32);
            this.btnConfig.Text = "setting config...";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
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
            this.scBatang.SplitterDistance = 305;
            this.scBatang.TabIndex = 3;
            // 
            // lvEdiExcels
            // 
            this.lvEdiExcels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEdiExcels.HideSelection = false;
            this.lvEdiExcels.Location = new System.Drawing.Point(0, 33);
            this.lvEdiExcels.Name = "lvEdiExcels";
            this.lvEdiExcels.Size = new System.Drawing.Size(305, 359);
            this.lvEdiExcels.TabIndex = 1;
            this.lvEdiExcels.UseCompatibleStateImageBehavior = false;
            // 
            // panelLeftTop
            // 
            this.panelLeftTop.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelLeftTop.Controls.Add(this.btnClearList);
            this.panelLeftTop.Controls.Add(this.btnFileAdd);
            this.panelLeftTop.Controls.Add(this.btnFolderAdd);
            this.panelLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLeftTop.Location = new System.Drawing.Point(0, 0);
            this.panelLeftTop.Name = "panelLeftTop";
            this.panelLeftTop.Size = new System.Drawing.Size(305, 33);
            this.panelLeftTop.TabIndex = 0;
            // 
            // btnClearList
            // 
            this.btnClearList.Image = global::EdiDbUploader.Properties.Resources.DeleteListItem_16x;
            this.btnClearList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearList.Location = new System.Drawing.Point(229, 5);
            this.btnClearList.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(74, 26);
            this.btnClearList.TabIndex = 2;
            this.btnClearList.Text = "Clear";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnFileAdd
            // 
            this.btnFileAdd.Image = global::EdiDbUploader.Properties.Resources.Office_Excel_Application_16xLG;
            this.btnFileAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFileAdd.Location = new System.Drawing.Point(130, 4);
            this.btnFileAdd.Name = "btnFileAdd";
            this.btnFileAdd.Size = new System.Drawing.Size(97, 26);
            this.btnFileAdd.TabIndex = 1;
            this.btnFileAdd.Text = "File Add...";
            this.btnFileAdd.UseVisualStyleBackColor = true;
            this.btnFileAdd.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnFolderAdd
            // 
            this.btnFolderAdd.Image = global::EdiDbUploader.Properties.Resources.openfolderHS1;
            this.btnFolderAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFolderAdd.Location = new System.Drawing.Point(5, 4);
            this.btnFolderAdd.Name = "btnFolderAdd";
            this.btnFolderAdd.Size = new System.Drawing.Size(122, 26);
            this.btnFolderAdd.TabIndex = 0;
            this.btnFolderAdd.Text = "Folder Add...";
            this.btnFolderAdd.UseVisualStyleBackColor = true;
            this.btnFolderAdd.Click += new System.EventHandler(this.ButtonClick);
            // 
            // panelRightTop
            // 
            this.panelRightTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelRightTop.Controls.Add(this.btnRun);
            this.panelRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRightTop.Location = new System.Drawing.Point(0, 0);
            this.panelRightTop.Name = "panelRightTop";
            this.panelRightTop.Size = new System.Drawing.Size(587, 33);
            this.panelRightTop.TabIndex = 1;
            // 
            // btnRun
            // 
            this.btnRun.Image = global::EdiDbUploader.Properties.Resources.run;
            this.btnRun.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRun.Location = new System.Drawing.Point(2, 5);
            this.btnRun.Margin = new System.Windows.Forms.Padding(2);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(63, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Start";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.ButtonRunClick);
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Location = new System.Drawing.Point(0, 33);
            this.logBox.Margin = new System.Windows.Forms.Padding(1);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(587, 359);
            this.logBox.TabIndex = 0;
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.scBatang.Panel1.ResumeLayout(false);
            this.scBatang.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scBatang)).EndInit();
            this.scBatang.ResumeLayout(false);
            this.panelLeftTop.ResumeLayout(false);
            this.panelRightTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileConfig;
        private System.Windows.Forms.ToolStripMenuItem mnuFileAbout;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileQuit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer scBatang;
        private FBH.EDI.Controls.LogTextBox logBox;
        private System.Windows.Forms.Panel panelRightTop;
        private System.Windows.Forms.Panel panelLeftTop;
        private System.Windows.Forms.ListView lvEdiExcels;
        private System.Windows.Forms.Button btnFileAdd;
        private System.Windows.Forms.Button btnFolderAdd;
        private System.Windows.Forms.ToolStripButton btnConfig;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnClearList;
    }
}

