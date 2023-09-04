using System.Windows.Forms;

namespace FBH.EDI.Common
{
    public class MsgBox
    {
        public static void Alert(string msg)
        {
            Show(msg);
        }
        public static void Error(string msg)
        {
            MessageBox.Show(msg, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        public static void Warning(string msg)
        {
            MessageBox.Show(msg, "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static DialogResult YesNo(string msg)
        {
            return MessageBox.Show(msg, "질문", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }
        public static DialogResult Confirm(string msg)
        {
            return MessageBox.Show(msg, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult YesNo(string msg, string title)
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DialogResult YesNoCancel(string msg, string title)
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        public static void Show(string msg)
        {
            MessageBox.Show(msg);
        }
    }

}
