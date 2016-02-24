using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKYPE4COMLib;

namespace Chatbot
{
    public partial class MainForm : Form
    {

        private Skype skype;

        public MainForm()
        {
            InitializeComponent();
            skype = new Skype();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skype.Attach(5, true);
            skype.SendMessage("vollytrolly", "hey");
        }
    }
}
