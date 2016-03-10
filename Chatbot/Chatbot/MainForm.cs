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

        private void MainForm_Load(object sender, EventArgs e)
        {
            Process[] pname = Process.GetProcessesByName("Skype");
            if (pname.Length == 0)
            {
                MessageBox.Show("Skype is not running. Please open Skype and run the program again.", "Skype not Running", MessageBoxButtons.OK);
                System.Windows.Forms.Application.Exit();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bot AimlBot = new Bot();
            AIMLbot.User myUser = new AIMLbot.User("testUser", AimlBot);

            AimlBot.loadSettings();
            AimlBot.isAcceptingUserInput = false;
            AimlBot.loadAIMLFromFiles();
            AimlBot.isAcceptingUserInput = true;

            String input = "vagina";

            Request r = new Request(input, myUser, AimlBot);
            Result res = AimlBot.Chat(r);

            richTextBox1.AppendText(res.Output);
        }
    }
}
