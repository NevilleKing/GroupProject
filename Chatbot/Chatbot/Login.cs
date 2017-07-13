using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatbot
{
    public partial class Login : Form
    {
        public string user = "";
        public string pass = "";

        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            user = UsernameTextbox.Text;
            pass = PasswordTextbox.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
