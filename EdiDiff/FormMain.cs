using FBH.EDI.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EdiDiff
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = this.Text + " - " + version;

            ComponentInitializer();
            DiffUtil.MessageEventHandler += DiffUtil_MessageEventHandler;
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
        }

        private void FindDiffProcess(object sender, EventArgs e)
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
            if(Directory.Exists(targetFolder) == false)
            {
                MsgBox.Error($"target folder {txtTargetFolder} is not exists");
                return;

            }
            try
            {
                //리스트만들기
                List<Item850> list850 = DiffUtil.ReadExcel850(txt850);
                List<Item945> list945 = DiffUtil.ReadExcel945(txt945);
                //비교
                List<DiffItem> listResult = DiffUtil.Diff(list850, list945);


                var templateDiffPath = Path.Combine(targetFolder, CommonUtil.RandomFilename("diff.xlsx"));
                File.WriteAllBytes(templateDiffPath, Properties.Resources.diff_template);
                //엑셀만들기
                string output = DiffUtil.CreateResultExcel(templateDiffPath, listResult);
                logTextBox1.Write(output);
                tabControl1.SelectedTab = tabPage2;

            }
            catch (Exception ex)
            {
                MsgBox.Error(ex.Message);
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
