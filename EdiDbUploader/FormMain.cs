using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }


}
