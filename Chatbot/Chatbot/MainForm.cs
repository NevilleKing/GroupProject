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
            skype.MessageStatus += OnMessage;
            skype.Attach(7, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skype.SendMessage("vollytrolly", textBox1.Text);
            textBox1.Text = String.Empty;
        }


	//Will read from skype and update text box, but only if skype is the active window (?)
        private void OnMessage(ChatMessage msg, TChatMessageStatus status)
        {
           if (msg.Sender.Handle == "vollytrolly")
            {
                richTextBox1.AppendText("\n\nCharlie: " + msg.Body);

                if (msg.Body.Contains("Hi") || msg.Body.Contains("Hello"))
                {
                    skype.SendMessage("vollytrolly", "Hi, how are you?");
                }
                else
                {
                    skype.SendMessage("vollytrolly", "I don't understand.");

                }
            }

            else
            {
                richTextBox1.AppendText("\n\nBot: " + msg.Body);
            }
        }

    }
}
