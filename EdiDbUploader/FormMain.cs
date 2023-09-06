using FBH.EDI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EdiDbUploader
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.AllowDrop = true;

            this.DragEnter += new DragEventHandler(FormMain_DragEnter);

            this.DragDrop += new DragEventHandler(FormMain_DragDrop);

            ListViewInitialize(lvEdiExcels, "EDI excels");
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
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
            NetworkConfig configForm = new NetworkConfig();
            if (configForm.ShowDialog() == DialogResult.OK)
            {

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

                    fd.Filter = "excel files (*.xlsx)|*.xlsx|all files (*.*)|*.*";
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
                if (lvEdiExcels.Items.Count > 0 && MsgBox.Confirm("리스트 내용을 모두 지우시겠습니까?") == DialogResult.OK)
                {
                    lvEdiExcels.Items.Clear();
                }
            }
        }

        private void AddFilesToListView(string[] files)
        {
            int i = lvEdiExcels.Items.Count + 1;
            foreach (var file in files)
            {
                bool isHidden = File.GetAttributes(file).HasFlag(FileAttributes.Hidden);
                if (file.EndsWith(".xlsx") && !file.StartsWith("~") && isHidden == false)
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
    }

}
