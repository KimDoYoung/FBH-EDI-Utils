using FBH.EDI.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EdiDbUploader
{
    public partial class FormMain : Form
    {
        IConfig config = null;
        string configPath;
        public FormMain()
        {
            InitializeComponent();
            //Version
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = this.Text + " - " + version;

            //Drag and Drop
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(FormMain_DragEnter);
            this.DragDrop += new DragEventHandler(FormMain_DragDrop);

            //List View 설정
            ListViewInitialize(lvEdiExcels, "EDI excels");

            //Config 생성
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var programPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            configPath = $"{programPath}/EdiDbUpload.config";
            config = new FileConfig(configPath);

            FbhEdiDbUploader.MessageEventHandler += FbhEdiDbUploader_MessageEventHandler;
        }

        private void FbhEdiDbUploader_MessageEventHandler(object sender, MessageEventArgs e)
        {
            logBox.Write(e.Message);
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                AddFilesToListView(files);


            }
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; //마우스 변경
            }
        }

        private void ListViewInitialize(ListView listView, string text)
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

        private void btnConfig_Click(object sender, EventArgs e)
        {
            using (NetworkConfig configForm = new NetworkConfig()) {

                configForm.Host = config.Get("Host", "jskn.iptime.org");
                configForm.Port = Convert.ToInt32(config.Get("Port", "5432"));
                configForm.Database = config.Get("Database", "fbhdb");
                configForm.Username = config.Get("Username", "postgres");
                configForm.Password = config.Get("Password", "postgres");

                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    config.Set("Host", configForm.Host);
                    config.Set("Port", configForm.Port.ToString());
                    config.Set("Databse", configForm.Database);
                    config.Set("Username", configForm.Username);
                    config.Set("Password", configForm.Password);
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if (sender == btnFolderAdd) //폴더 추가
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    fbd.ShowNewFolderButton = false;
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {

                        string[] files = Directory.GetFiles(fbd.SelectedPath, "*.xlsx", SearchOption.AllDirectories);
                        AddFilesToListView(files);
                    }
                }
            }
            else if (sender == btnFileAdd)
            {
                using (OpenFileDialog fd = new OpenFileDialog())
                {
                    fd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    fd.Title = "Browse Excel Files";
                    fd.CheckFileExists = fd.CheckPathExists = true;

                    fd.Filter = "excel files (*.xlsx)|*.xlsx|pdf files (*.pdf)|*.pdf|all files (*.*)|*.*";
                    fd.FilterIndex = 0;
                    fd.RestoreDirectory = true;

                    fd.ReadOnlyChecked = true;
                    fd.ShowReadOnly = true;
                    fd.Multiselect = true;
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        AddFilesToListView(fd.FileNames);
                    }
                }
            }
            else if (sender == btnClearList)
            {
                if (lvEdiExcels.Items.Count > 0 && MsgBox.Confirm("리스트 내용을 모두 지우시겠습니까?") == DialogResult.Yes)
                {
                    lvEdiExcels.Items.Clear();
                }
            }
        }

        private void AddFilesToListView(string[] files)
        {
            HashSet<string> already = new HashSet<string>();
            int i = 0;
            for(i=0; i< lvEdiExcels.Items.Count; i++)
            {
                already.Add(lvEdiExcels.Items[i].SubItems[2].Text);
            }
            i = lvEdiExcels.Items.Count + 1;

            foreach (var file in files)
            {
                if (already.Contains(file))
                {
                    logBox.Write(file + " is already exist in list");
                    continue;
                }
                //Hidden파일이 아니고 파일이 존재해야하고 파일의 확장자가 xlsx또는 pdf여야한다
                bool isHidden = File.GetAttributes(file).HasFlag(FileAttributes.Hidden);
                bool isValid = (file.EndsWith(".xlsx") || file.EndsWith(".pdf")) && !file.StartsWith("~") && isHidden == false;
                isValid = isValid && File.Exists(file);
                
                if (isValid)
                {
                    ListViewItem item = new ListViewItem(Convert.ToString(i));
                    item.SubItems.Add(Path.GetFileName(file));
                    item.SubItems.Add(file);
                    lvEdiExcels.Items.Add(item);

                    i++;
                }
            }
        }

        private void MenuButtonClick(object sender, EventArgs e)
        {
            if (sender == mnuFileAbout)
            {
                AboutBox1 about = new AboutBox1();
                about.ShowDialog();
            }
            else if (sender == mnuFileConfig)
            {
                btnConfig_Click(sender, null);
            }
            else if (sender == mnuFileQuit)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void ButtonRunClick(object sender, EventArgs e)
        {
            if(lvEdiExcels.Items.Count  == 0)
            {
                MsgBox.Warning("리스트가 비어 있습니다");
                return;
            }
            List<string> list = new List<string>();
            for (int i = 0; i < lvEdiExcels.Items.Count; i++)
            {
                list.Add(lvEdiExcels.Items[i].SubItems[2].Text);
            }
            
            var Host = config.Get("Host");
            var Port = config.Get("Port");
            var Database= config.Get("Database");
            var Username= config.Get("Username");
            var Password= config.Get("Password");
            var url = $"Host={Host};Port={Port};Database={Database};User ID={Username};Password={Password}";
            logBox.Write("db url:" + url);
            try
            {
                var ediDbUploader = new FbhEdiDbUploader(url);
                int countOK = 0;
                int countNK = 0;
                var idx = 0;
                foreach (var ediFile in list)
                {
                    var results = ediDbUploader.Insert(ediFile);
                    foreach (var result in results)
                    {
                        if (result.StartsWith("OK"))
                        {
                            countOK++;
                            lvEdiExcels.Items[idx].BackColor = System.Drawing.Color.GreenYellow;
                        }
                        else if (result.StartsWith("NK"))
                        {
                            countNK++;
                            lvEdiExcels.Items[idx].BackColor = System.Drawing.Color.Crimson;
                        }
                        logBox.Write((idx+1) + " " +result);
                    }
                    idx++;
                }
                logBox.Write("");
                logBox.Write("---------------------------------------------------------");
                logBox.Write($"총파일갯수: {lvEdiExcels.Items.Count}, 성공갯수: {countOK}, 실패갯수 : {countNK}");
                logBox.Write("---------------------------------------------------------");
            }
            catch (Exception ex)
            {
                logBox.Write(ex.ToString());
                MsgBox.Error(ex.Message); 
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //config save
            config.Save();
        }
    }

}
