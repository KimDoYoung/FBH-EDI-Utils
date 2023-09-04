namespace EdiDiff
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFile850 = new System.Windows.Forms.TextBox();
            this.txtFile945 = new System.Windows.Forms.TextBox();
            this.btnFileDialog850 = new System.Windows.Forms.Button();
            this.btnFileDialog945 = new System.Windows.Forms.Button();
            this.btnFindDiff = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.btnTargetFolder = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panelBatang = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.logTextBox1 = new FBH_EDI_Controls.LogTextBox();
            this.panelTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelBatang.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panelTop.Controls.Add(this.tableLayoutPanel1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(943, 100);
            this.panelTop.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.03763F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.96237F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 121F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFile850, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFile945, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnFileDialog850, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnFileDialog945, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnFindDiff, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtTargetFolder, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnTargetFolder, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.09836F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.90164F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(943, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "945 List";
            // 
            // txtFile850
            // 
            this.txtFile850.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile850.Location = new System.Drawing.Point(100, 7);
            this.txtFile850.Name = "txtFile850";
            this.txtFile850.Size = new System.Drawing.Size(588, 21);
            this.txtFile850.TabIndex = 2;
            // 
            // txtFile945
            // 
            this.txtFile945.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFile945.Location = new System.Drawing.Point(100, 40);
            this.txtFile945.Name = "txtFile945";
            this.txtFile945.Size = new System.Drawing.Size(588, 21);
            this.txtFile945.TabIndex = 3;
            // 
            // btnFileDialog850
            // 
            this.btnFileDialog850.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFileDialog850.Location = new System.Drawing.Point(694, 6);
            this.btnFileDialog850.Name = "btnFileDialog850";
            this.btnFileDialog850.Size = new System.Drawing.Size(115, 23);
            this.btnFileDialog850.TabIndex = 4;
            this.btnFileDialog850.Text = "Set File...";
            this.btnFileDialog850.UseVisualStyleBackColor = true;
            // 
            // btnFileDialog945
            // 
            this.btnFileDialog945.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnFileDialog945.Location = new System.Drawing.Point(694, 40);
            this.btnFileDialog945.Name = "btnFileDialog945";
            this.btnFileDialog945.Size = new System.Drawing.Size(115, 22);
            this.btnFileDialog945.TabIndex = 5;
            this.btnFileDialog945.Text = "Set File...";
            this.btnFileDialog945.UseVisualStyleBackColor = true;
            // 
            // btnFindDiff
            // 
            this.btnFindDiff.Location = new System.Drawing.Point(815, 3);
            this.btnFindDiff.Name = "btnFindDiff";
            this.tableLayoutPanel1.SetRowSpan(this.btnFindDiff, 3);
            this.btnFindDiff.Size = new System.Drawing.Size(112, 88);
            this.btnFindDiff.TabIndex = 6;
            this.btnFindDiff.Text = "Find Diff";
            this.btnFindDiff.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "850 List";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Target folder";
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetFolder.Location = new System.Drawing.Point(100, 72);
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.Size = new System.Drawing.Size(588, 21);
            this.txtTargetFolder.TabIndex = 8;
            // 
            // btnTargetFolder
            // 
            this.btnTargetFolder.Location = new System.Drawing.Point(694, 69);
            this.btnTargetFolder.Name = "btnTargetFolder";
            this.btnTargetFolder.Size = new System.Drawing.Size(115, 22);
            this.btnTargetFolder.TabIndex = 9;
            this.btnTargetFolder.Text = "Target folder...";
            this.btnTargetFolder.UseVisualStyleBackColor = true;
            this.btnTargetFolder.Click += new System.EventHandler(this.btnTargetFolder_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Location = new System.Drawing.Point(0, 496);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(943, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panelBatang
            // 
            this.panelBatang.Controls.Add(this.tabControl1);
            this.panelBatang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBatang.Location = new System.Drawing.Point(0, 100);
            this.panelBatang.Name = "panelBatang";
            this.panelBatang.Size = new System.Drawing.Size(943, 396);
            this.panelBatang.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(943, 396);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvResult);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(935, 370);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Result";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvResult
            // 
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(76, 46);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.Size = new System.Drawing.Size(545, 330);
            this.dgvResult.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.logTextBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(935, 370);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // logTextBox1
            // 
            this.logTextBox1.Location = new System.Drawing.Point(63, 54);
            this.logTextBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logTextBox1.Name = "logTextBox1";
            this.logTextBox1.Size = new System.Drawing.Size(605, 224);
            this.logTextBox1.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 518);
            this.Controls.Add(this.panelBatang);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EdiDiff";
            this.panelTop.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelBatang.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panelBatang;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFile850;
        private System.Windows.Forms.TextBox txtFile945;
        private System.Windows.Forms.Button btnFileDialog850;
        private System.Windows.Forms.Button btnFileDialog945;
        private System.Windows.Forms.Button btnFindDiff;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTargetFolder;
        private System.Windows.Forms.Button btnTargetFolder;
        private System.Windows.Forms.TabPage tabPage2;
        private FBH_EDI_Controls.LogTextBox logTextBox1;
    }
}

