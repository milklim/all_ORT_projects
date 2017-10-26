using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormClient
{
    public partial class UserNameDialog : Form
    {
        public string UsrName
        {
            get { return this.textBox1.Text; }
        }
       // Form1 chat;
        public UserNameDialog()
        {
            InitializeComponent();
            this.Activate();
           // chat = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // string name = this.textBox1.Text;
            //  chat.InitChatClient(name);
            // this.Close();
            this.DialogResult = DialogResult.OK;

        }
    }
}
