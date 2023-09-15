using FBH.EDI.Common;
using Npgsql;
using System;
using System.Windows.Forms;

namespace EdiDbUploader
{
    
    public partial class NetworkConfig : Form
    {
        //propeties
        public string Host { get { return txtHost.Text; }  set { txtHost.Text = value; } }
        public int Port { get { return Convert.ToInt32(txtPort.Text); } set { txtPort.Text = Convert.ToString(value); } }
        public string Database { get { return txtDatabase.Text; } set { txtDatabase.Text = value; } }
        public string Username{ get { return txtUsername.Text; } set { txtUsername.Text = value; } }
        public string Password{ get { return txtPassword.Text; } set { txtPassword.Text = value; } }
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

        private void btnConnectionTest_Click(object sender, EventArgs e)
        {
            string connString = $"Host={Host};Port={Port};Database={Database};User ID={Username};Password={Password}";
            NpgsqlConnection conn = null;
            try
            {
                conn = new NpgsqlConnection(connString);
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("Connection OK","성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("연결에 실패했습니다", "연결실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("연결에 실패했습니다:"+ex.Message, "연결실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn != null) { conn.Close(); }
            }
        }
    }
}
