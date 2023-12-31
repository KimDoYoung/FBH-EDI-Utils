﻿using FBH.EDI.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EdiDiff
{
    
    public partial class FormMain : Form
    {
        private const string Mode850945 = "Mode850945";
        private const string ModeInvoice= "Invoice";
        private const string ModeHubMerge = "HubMerge";
        private string CurrentMode = Mode850945;
        
        public FormMain()
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = this.Text + " - " + version;

            ComponentInitializer();
            EdiDiff.DiffUtil.MessageEventHandler += DiffUtil_MessageEventHandler;
        }

        private void DiffUtil_MessageEventHandler(object sender, MessageEventArgs e)
        {
            logTextBox1.Write(e.Message);
        }

        private void ComponentInitializer()
        {
            btnFileDialog850.Click += ButtonFileDialog;
            btnFileDialog945.Click += ButtonFileDialog;
            btnFindDiff.Click += FindDiffProcess;

            dgvResult.Dock = DockStyle.Fill;
            dgvResult.AllowUserToAddRows = false;
            dgvResult.AllowUserToDeleteRows = false;
            dgvResult.AllowUserToResizeRows = false;
            dgvResult.EnableHeadersVisualStyles = false;
            dgvResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResult.ShowEditingIcon = false;

            logTextBox1.Dock = DockStyle.Fill;
            tabControl1.SelectedTab = tabPage2;
#if DEBUG
            txtFile850.Text = "C:\\FBH-Work\\수량비교850-945\\850_List_2023_09_04_14_16_29.xlsx";
            txtFile945.Text = "C:\\FBH-Work\\수량비교850-945\\945_List_2023_09_04_14_17_27.xlsx\r\n";
            txtTargetFolder.Text = "C:\\FBH-Work\\수량비교850-945";
#endif
            rdo850945.CheckedChanged += RdoButtonChanged;
            rdoInvoice.CheckedChanged += RdoButtonChanged;
        }

        private void RdoButtonChanged(object sender, EventArgs e)
        {
            txtFile850.Enabled = true;
            btnFileDialog850.Enabled = true;
            
            txtFile945.Enabled = true;  
            btnFileDialog945.Enabled = true;
            
            txtTargetFolder.Enabled = true;
            btnTargetFolder.Enabled = true;
            
            btnFindDiff.Text = "Find Diff";

            if (rdo850945.Checked)
            {
                lblSrc1.Text = "850 List";
                lblSrc2.Text = "945 List";
                CurrentMode = Mode850945;
            }
            else if (rdoInvoice.Checked)
            {
                lblSrc1.Text = "Invoice";
                lblSrc2.Text = "RL Invoice";
                CurrentMode = ModeInvoice;
            }else if (rdoHubMerge.Checked)
            {
                txtFile945.Enabled = false;
                btnFileDialog945.Enabled = false;
                txtTargetFolder.Enabled = false;
                btnTargetFolder.Enabled = false;
                btnFindDiff.Text = "Merge route1,2";

                lblSrc1.Text = "Hub Route1,2 Excel";
                CurrentMode = ModeHubMerge;
            }
        }


        private void FindDiffProcess(object sender, EventArgs e)
        {

            if(CurrentMode == Mode850945)
            {
                Diff850945();
            }else if(CurrentMode == ModeInvoice)
            {
                DiffInvoice();
            }
            else if(CurrentMode == ModeHubMerge)
            {
                MergeRoute1Route2();
            }


        }

        private void MergeRoute1Route2()
        {
            var hub210Path = txtFile850.Text;
            if (File.Exists(hub210Path) == false)
            {
                MsgBox.Error($"210 Hub file : {hub210Path} is not exists");
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                logTextBox1.Write("210 route1 route2 merge after duplication removing");
                var output = DiffUtil.Hub210Merge(hub210Path);
                logTextBox1.Write($"합친 파일 : {output}");
                logTextBox1.Write("");
                logTextBox1.Write("210 route1 route2 Merge 작업이 끝났습니다.");
                logTextBox1.Write("");
            }
            catch(Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void DiffInvoice()
        {
            var invoice = txtFile850.Text;
            var rlInvoice = txtFile945.Text;
            var targetFolder = txtTargetFolder.Text;
            if (File.Exists(invoice) == false)
            {
                MsgBox.Error($"Invoice : {invoice} is not exists");
                return;
            }
            if (File.Exists(rlInvoice) == false)
            {
                MsgBox.Error($"RL Invoice : {rlInvoice} is not exists");
                return;
            }
            if (Directory.Exists(targetFolder) == false)
            {
                MsgBox.Error($"target folder {txtTargetFolder} is not exists");
                return;

            }

            string templateDiffPath = "";
            logTextBox1.Write("Invoice vs RL Invoice compare start...");
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //리스트만들기
                logTextBox1.Write($"loading {invoice} ");
                List<ItemInvoice> list850 = DiffUtil.ReadInvoice(invoice);
                logTextBox1.Write($"loading {rlInvoice} ");
                List<ItemRLinvoice> list945 = DiffUtil.ReadRlInvoice(rlInvoice);
                //비교
                List<DiffInvoiceItem> listResult = DiffUtil.DiffInvoice(list850, list945);


                templateDiffPath = Path.Combine(targetFolder, CommonUtil.RandomFilename("diffInvoice.xlsx"));
                File.WriteAllBytes(templateDiffPath, Properties.Resources.invoice_RL_invoice);
                //엑셀만들기
                logTextBox1.Write($"diff checking...");
                string output = DiffUtil.CreateResultExcelInvoice(templateDiffPath, listResult);
                logTextBox1.Write(output);
                tabControl1.SelectedTab = tabPage2;
                logTextBox1.Write("");
                logTextBox1.Write("비교 작업이 끝났습니다.");
                logTextBox1.Write("");
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
            finally
            {
                CommonUtil.DeleteFile(templateDiffPath);
                Cursor.Current = Cursors.Default;
            }
        }

        private void Diff850945()
        {
            var txt850 = txtFile850.Text;
            var txt945 = txtFile945.Text;
            var targetFolder = txtTargetFolder.Text;
            if (File.Exists(txt850) == false)
            {
                MsgBox.Error($"850 : {txt850} is not exists");
                return;
            }
            if (File.Exists(txt945) == false)
            {
                MsgBox.Error($"945 : {txt945} is not exists");
                return;
            }
            if (Directory.Exists(targetFolder) == false)
            {
                MsgBox.Error($"target folder {txtTargetFolder} is not exists");
                return;

            }

            string templateDiffPath = "";
            logTextBox1.Write("850 945 compare start...");
            try
            {
                //리스트만들기
                logTextBox1.Write($"loading {txt850} ");
                List<Item850> list850 = EdiDiff.DiffUtil.ReadExcel850(txt850);
                logTextBox1.Write($"loading {txt945} ");
                List<Item945> list945 = EdiDiff.DiffUtil.ReadExcel945(txt945);
                //비교
                List<DiffItem> listResult = EdiDiff.DiffUtil.Diff850945(list850, list945);


                templateDiffPath = Path.Combine(targetFolder, CommonUtil.RandomFilename("diff.xlsx"));
                File.WriteAllBytes(templateDiffPath, Properties.Resources.diff_template);
                //엑셀만들기
                logTextBox1.Write($"diff checking...");
                string output = EdiDiff.DiffUtil.CreateResultExcel850945(templateDiffPath, listResult);
                logTextBox1.Write(output);
                tabControl1.SelectedTab = tabPage2;
                logTextBox1.Write("");
                logTextBox1.Write("비교 작업이 끝났습니다.");
                logTextBox1.Write("");
            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
            }
            finally
            {
                CommonUtil.DeleteFile(templateDiffPath);
            }

        }

        private void ButtonFileDialog(object sender, EventArgs e)
        {
            TextBox currentTextBox = null;
            if(sender == btnFileDialog850)
            {
                currentTextBox = txtFile850;
            }else if(sender == btnFileDialog945)
            {
                currentTextBox = txtFile945;
            }else
            {
                throw new EdiException("잘못된 file dialog 호출입니다");
            }

            using (OpenFileDialog fd = new OpenFileDialog())
            {
                //fd.InitialDirectory = string.IsNullOrWhiteSpace(currentTextBox.Text) ? 
                //        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : currentTextBox.Text;
                fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                fd.Title = "Browse Excel Files";
                fd.CheckFileExists = fd.CheckPathExists = true;

                fd.Filter = "excel files (*.xlsx)|*.xlsx|all files (*.*)|*.*";
                fd.FilterIndex = 0;
                fd.RestoreDirectory = true;

                fd.ReadOnlyChecked = true;
                fd.ShowReadOnly = true;
                if ( fd.ShowDialog() == DialogResult.OK )
                {
                    currentTextBox.Text = fd.FileName;
                }
            }
        }

        private void btnTargetFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
               
                fbd.ShowNewFolderButton = false;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtTargetFolder.Text = fbd.SelectedPath;   
                }
            }
        }
    }
}
