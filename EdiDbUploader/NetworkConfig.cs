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
    public partial class NetworkConfig : Form
    {
        public NetworkConfig()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            if(sender == btnOK)
            {
                this.DialogResult = DialogResult.OK;
            }else if (sender == btnCancel)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
