using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FBH.EDI.Common;

namespace FBH.EDI.Controls
{
    public partial class LogTextBox: UserControl
    {
        public static event EventHandler<MessageEventArgs> MessageEventHandler;

        public LogTextBox()
        {
            InitializeComponent();
            btnClear.Click += ButtonClick;
            btnCopyToClipboard.Click += ButtonClick;
            btnSaveToFile.Click += ButtonClick;
        }
        public void Write(string msg)
        {
            var text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + " > " + msg + "\r\n";
            txtLog.AppendText(text);
            txtLog.ScrollToCaret();
        }
        private void ButtonClick(object sender, EventArgs e)
        {
            if(sender == btnClear)
            {
                txtLog.Text = "";
            }else if(sender == btnSaveToFile)
            {
                using (var sfd = new SaveFileDialog() { Filter = @"Log|*.log|Text file|*.txt|All files|*.*" })
                {
                    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, txtLog.Text);

                        MessageEventHandler?.Invoke(this, new MessageEventArgs($"{sfd.FileName} 에 저장되었습니다"));
                    }
                }
            }
            else if(sender == btnCopyToClipboard)
            {
                Clipboard.SetText(txtLog.Text);
                MessageEventHandler?.Invoke(this, new MessageEventArgs($"클립보드에 저장되었습니다"));
            }
        }
    }
}
