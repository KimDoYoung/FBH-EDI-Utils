using EdiName;
using EdiRename.Common;
using kr.co.kalpa.common.CommonUtil;
using System;
using System.IO;
using System.Windows.Forms;

namespace EdiRename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = "EdiRename - " + version;

            ListViewInitialize(listViewSrcFiles, "이름 바꿀 파일 목록");

            //event handler
            RenameUtils.MessageEventHandler += ExcelUtils_MessageEventHandler;

#if DEBUG
            txtSrcFolder.Text = @"C:\\FBH-Work\\Rename1";
#endif
        }

        private void buttonFolderDialog_Click(object sender, EventArgs e)
        {
            string dir = txtSrcFolder.Text;
            if (string.IsNullOrEmpty(dir))
            {
                MsgBox.Error("대상폴더가 비어 있습니다. 바꿀 파일들이 있는 폴더를 선택해 주십시오");
                return;
            }
            if (Directory.Exists(dir) == false)
            {
                MsgBox.Error("대상폴더가 존재하지 않습니다");
                return;
            }
            listViewSrcFiles.Items.Clear();

            string[] files = Directory.GetFiles(dir);
            int i = 1;
            foreach (var file in files)
            {
                bool isHidden = File.GetAttributes(file).HasFlag(FileAttributes.Hidden);
                if (file.EndsWith(".xlsx") && !file.StartsWith("~") && isHidden == false)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(i));
                    item.SubItems.Add(file.Substring(dir.Length + 1));
                    item.SubItems.Add(file);
                    listViewSrcFiles.Items.Add(item);

                    i++;
                }
            }
            if (listViewSrcFiles.Items.Count == 0)
            {
                MsgBox.Warning("대상폴더에 엑셀 파일이 없습니다");
                return;
            }
        }
        private void ListViewInitialize(System.Windows.Forms.ListView listView, string text)
        {
            listView.View = View.Details;
            listView.GridLines = true;
            listView.Columns.Add("No", 30, HorizontalAlignment.Right);
            listView.Columns.Add("File name", 500, HorizontalAlignment.Left);
            listView.Columns.Add("Full Path", 0, HorizontalAlignment.Left);
            listView.Text = text;
            listView.FullRowSelect = true;
            listView.MultiSelect = false;
        }

        private void btnRunRename_Click(object sender, EventArgs e)
        {
            
            if (listViewSrcFiles.Items.Count == 0)
            {
                MsgBox.Error("대상 파일들 목록이 비어 있습니다");
                return;
            }
            btnRunRename.Enabled = false;
            try
            {
                textBoxLog.Text = "";
                for (int i = 0; i < listViewSrcFiles.Items.Count; i++)
                {
                    string fileName = listViewSrcFiles.Items[i].SubItems[1].Text;
                    string fullPath = listViewSrcFiles.Items[i].SubItems[2].Text;
                    string basePath = Path.GetDirectoryName(fullPath);
                    
                    WriteLog($"{i + 1} : {fileName} 시작....");
                    listViewSrcFiles.Items[i].Selected = true;
                    listViewSrcFiles.Items[i].Focused = true;

                    //
                    NameProperties np = RenameUtils.ReadExcelAndExtractNameProperties(fullPath);
                    var newFilename = np.GetFilename(".xlsx");
                    

                    //폴더를 만든다.
                    var targetFolder = Path.Combine(basePath,  np.EdiTypeNo);
                    DirectoryInfo di = Directory.CreateDirectory(targetFolder);

                    //copy한다
                    var newFullPath = Path.Combine(targetFolder, newFilename);
                    System.IO.File.Copy(fullPath, newFullPath);

                    WriteLog($"{i + 1} : {Path.GetFileName(fullPath)} -> {Path.GetFileName(newFullPath)}");

                }
                WriteLog("");
                WriteLog("이름 바꾸기 작업이 끝났습니다.");
                WriteLog("");

            }
            catch (Exception ex)
            {

                MsgBox.Error(ex.Message);
                WriteLog(ex.ToString());
            }
            finally
            {
                this.listViewSrcFiles.SelectedItems.Clear();
                btnRunRename.Enabled = true;
            }
        }

        /// <summary>
        /// 메세지 핸들러 - ExcelUtil클래스에서 나오는 메세지를 로깅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelUtils_MessageEventHandler(object sender, MessageEventArgs e)
        {
            WriteLog(e.Message);
        }

        /// <summary>
        /// 로그 출력
        /// </summary>
        /// <param name="msg"></param>
        private void WriteLog(string msg)
        {
            var text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " > " + msg + "\r\n";
            textBoxLog.AppendText(text);
            textBoxLog.ScrollToCaret();
        }

        private void btnFolderSelect_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                listViewSrcFiles?.Items.Clear();
                DialogResult result = fbd.ShowDialog();

                fbd.ShowNewFolderButton = false;
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtSrcFolder.Text = fbd.SelectedPath;
                }
            }

        }
    }
}
