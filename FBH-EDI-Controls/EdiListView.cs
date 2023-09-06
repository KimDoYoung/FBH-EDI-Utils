using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FBH.EDI.Common;
using System.IO;

namespace FBH.EDI.Controls
{
    public partial class EdiListView : UserControl
    {
        public static event EventHandler<MessageEventArgs> MessageEventHandler;
        
        [
            Browsable(true),
            CategoryAttribute("데이터"),
            Description("제목")
        ]
        public string Title { get { return lblTitle.Text; } set { lblTitle.Text = value; } }
        
        
        public string SourceFolder { get { return sourceFolder; } set { SetSourceFolder(value); } }


        public ListView GetListView()
        {
            return this.lvEdiFiles;
        }


        private string sourceFolder;
        private void SetSourceFolder(string sourceFolder)
        {
            this.sourceFolder = sourceFolder;
            lvEdiFiles.Items.Clear();
            if (Directory.Exists(this.sourceFolder) == false)
            {
                MessageEventHandler?.Invoke(this, new MessageEventArgs(this.sourceFolder + " is not exist!"));
            }
            var dir = this.sourceFolder;

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
                    lvEdiFiles.Items.Add(item);

                    i++;
                }
            }
        }

        public EdiListView()
        {
            InitializeComponent();
            ListViewInitialize(this.lvEdiFiles, this.Title);
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
    }
}
