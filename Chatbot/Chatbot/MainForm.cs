using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SKYPE4COMLib;
using AIMLbot;

namespace Chatbot
{
    public partial class MainForm : Form
    {

        private Skype skype;
        private string skype_botName; // display name of local user (bot)
        ChatResponse chatbot;

        public MainForm()
        {
            InitializeComponent();
            skype = new Skype();
            skype.MessageStatus += OnMessage;
            skype.Attach(7, false);
            skype_botName = skype.CurrentUserHandle;
            chatbot = new ChatResponse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skype.SendMessage("vollytrolly", textBox1.Text);
            textBox1.Text = String.Empty;
        }


        //Will read from skype and update text box, but only if skype is the active window (?)
        private void OnMessage(ChatMessage msg, TChatMessageStatus status)
        {
            switch (status)
            {
                case TChatMessageStatus.cmsSent:
                    richTextBox1.AppendText("\n\nBot: " + msg.Body);
                    break;
                case TChatMessageStatus.cmsReceived:
                    richTextBox1.AppendText("\n\n" + msg.Sender.Handle + ": " + msg.Body);
                    skype.SendMessage(msg.Sender.Handle, chatbot.getResponse(msg.Body));
                    break;
                case TChatMessageStatus.cmsRead:
                case TChatMessageStatus.cmsSending:
                    // nothing
                    break;
                default:
                    richTextBox1.AppendText("\n\n [ERROR]");
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("Skype");
            if (pname.Length == 0)
            {
                MessageBox.Show("Skype is not running. Please open Skype and run the program again.", "Skype not Running", MessageBoxButtons.OK);
                System.Windows.Forms.Application.Exit();
            }

        }
    }
}
