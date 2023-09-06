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
            this.scBatang = new System.Windows.Forms.SplitContainer();
            this.lvEdiExcels = new System.Windows.Forms.ListView();
            this.panelLeftTop = new System.Windows.Forms.Panel();
            this.panelRightTop = new System.Windows.Forms.Panel();
            this.logBox = new FBH.EDI.Controls.LogTextBox();
            this.btnClearList = new System.Windows.Forms.Button();
            this.btnFileAdd = new System.Windows.Forms.Button();
            this.btnFolderAdd = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.ToolStripButton();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 688);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 20, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1280, 22);
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
            this.menuStrip1.Size = new System.Drawing.Size(1280, 33);
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
            this.mnuFile.Size = new System.Drawing.Size(55, 29);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileConfig
            // 
            this.mnuFileConfig.Name = "mnuFileConfig";
            this.mnuFileConfig.Size = new System.Drawing.Size(270, 34);
            this.mnuFileConfig.Text = "&Config";
            this.mnuFileConfig.Click += new System.EventHandler(this.MenuButtonClick);
            // 
            // mnuFileAbout
            // 
            this.mnuFileAbout.Name = "mnuFileAbout";
            this.mnuFileAbout.Size = new System.Drawing.Size(270, 34);
            this.mnuFileAbout.Text = "&About";
            this.mnuFileAbout.Click += new System.EventHandler(this.MenuButtonClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(267, 6);
            // 
            // mnuFileQuit
            // 
            this.mnuFileQuit.Name = "mnuFileQuit";
            this.mnuFileQuit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.mnuFileQuit.Size = new System.Drawing.Size(270, 34);
            this.mnuFileQuit.Text = "&Quit";
            this.mnuFileQuit.Click += new System.EventHandler(this.MenuButtonClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConfig});
            this.toolStrip1.Location = new System.Drawing.Point(0, 33);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1280, 37);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // scBatang
            // 
            this.scBatang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scBatang.Location = new System.Drawing.Point(0, 70);
            this.scBatang.Margin = new System.Windows.Forms.Padding(4);
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
            this.scBatang.Size = new System.Drawing.Size(1280, 618);
            this.scBatang.SplitterDistance = 425;
            this.scBatang.SplitterWidth = 6;
            this.scBatang.TabIndex = 3;
            // 
            // lvEdiExcels
            // 
            this.lvEdiExcels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEdiExcels.HideSelection = false;
            this.lvEdiExcels.Location = new System.Drawing.Point(0, 50);
            this.lvEdiExcels.Margin = new System.Windows.Forms.Padding(4);
            this.lvEdiExcels.Name = "lvEdiExcels";
            this.lvEdiExcels.Size = new System.Drawing.Size(425, 568);
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
            this.panelLeftTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelLeftTop.Name = "panelLeftTop";
            this.panelLeftTop.Size = new System.Drawing.Size(425, 50);
            this.panelLeftTop.TabIndex = 0;
            // 
            // panelRightTop
            // 
            this.panelRightTop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelRightTop.Controls.Add(this.button1);
            this.panelRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRightTop.Location = new System.Drawing.Point(0, 0);
            this.panelRightTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelRightTop.Name = "panelRightTop";
            this.panelRightTop.Size = new System.Drawing.Size(849, 50);
            this.panelRightTop.TabIndex = 1;
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logBox.Location = new System.Drawing.Point(0, 50);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(849, 568);
            this.logBox.TabIndex = 0;
            // 
            // btnClearList
            // 
            this.btnClearList.Image = global::EdiDbUploader.Properties.Resources.DeleteListItem_16x;
            this.btnClearList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearList.Location = new System.Drawing.Point(306, 6);
            this.btnClearList.Name = "btnClearList";
            this.btnClearList.Size = new System.Drawing.Size(106, 39);
            this.btnClearList.TabIndex = 2;
            this.btnClearList.Text = "Clear";
            this.btnClearList.UseVisualStyleBackColor = true;
            this.btnClearList.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnFileAdd
            // 
            this.btnFileAdd.Image = global::EdiDbUploader.Properties.Resources.Office_Excel_Application_16xLG;
            this.btnFileAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFileAdd.Location = new System.Drawing.Point(166, 6);
            this.btnFileAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnFileAdd.Name = "btnFileAdd";
            this.btnFileAdd.Size = new System.Drawing.Size(139, 39);
            this.btnFileAdd.TabIndex = 1;
            this.btnFileAdd.Text = "File Add...";
            this.btnFileAdd.UseVisualStyleBackColor = true;
            this.btnFileAdd.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnFolderAdd
            // 
            this.btnFolderAdd.Image = global::EdiDbUploader.Properties.Resources.openfolderHS1;
            this.btnFolderAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFolderAdd.Location = new System.Drawing.Point(7, 6);
            this.btnFolderAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnFolderAdd.Name = "btnFolderAdd";
            this.btnFolderAdd.Size = new System.Drawing.Size(158, 39);
            this.btnFolderAdd.TabIndex = 0;
            this.btnFolderAdd.Text = "Folder Add...";
            this.btnFolderAdd.UseVisualStyleBackColor = true;
            this.btnFolderAdd.Click += new System.EventHandler(this.ButtonClick);
            // 
            // button1
            // 
            this.button1.Image = global::EdiDbUploader.Properties.Resources.run;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(3, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnConfig
            // 
            this.btnConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnConfig.Image = global::EdiDbUploader.Properties.Resources.DatabaseSettings;
            this.btnConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(34, 32);
            this.btnConfig.Text = "setting config...";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 710);
            this.Controls.Add(this.scBatang);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClearList;
    }
}

