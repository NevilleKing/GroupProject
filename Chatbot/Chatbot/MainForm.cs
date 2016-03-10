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
using System.Threading;

namespace Chatbot
{
    public partial class MainForm : Form
    {

        private Skype skype;
        private string skype_botName; // display name of local user (bot)
        ChatResponse chatbot;
        List<myUser> conversation_users;

        public MainForm()
        {
            InitializeComponent();
            skype = new Skype();
            skype.MessageStatus += OnMessage;
            skype.Attach(7, false);
            skype_botName = skype.CurrentUserHandle;
            chatbot = new ChatResponse();
            conversation_users = new List<myUser>();
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
                case TChatMessageStatus.cmsReceived:
                    myUser usr = getCurrentUser(msg.Sender.Handle);
                    usr.textBox.AppendText("\n\n" + msg.Sender.Handle + ": " + msg.Body);
                    string resp = chatbot.getResponse(msg.Body, usr.AIusr);
                    skype.SendMessage(msg.Sender.Handle, resp);
                    usr.textBox.AppendText("\n\nBot: " + resp);
                    break;
                case TChatMessageStatus.cmsSent:
                case TChatMessageStatus.cmsRead:
                case TChatMessageStatus.cmsSending:
                    // nothing
                    break;
                default:
                    MessageBox.Show("ERROR");
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
            } else
            {
                skype.Client.Focus();
            }

        }

        private myUser initUser(string user)
        {
            TabPage tp;
            if (conversation_users.Count == 0)
            {
                tp = tabControl1.TabPages[0];
            } else
            {
                tp = new TabPage();
            }
            RichTextBox rtb = new RichTextBox();
            rtb.Parent = tp;
            rtb.Dock = DockStyle.Fill;
            tp.Text = user;
            if (conversation_users.Count != 0)
                tabControl1.TabPages.Add(tp);
            conversation_users.Add(new myUser(user, ref rtb, chatbot.getBot()));
            tabControl1.Visible = true;
            return conversation_users[conversation_users.Count-1];
        }

        private myUser getCurrentUser(string user)
        {
            myUser currentUsr = new myUser();
            bool found = false;
            foreach (myUser usr in conversation_users)
            {
                if (usr.userName == user)
                {
                    found = true;
                    currentUsr = usr;
                    break;
                }
            }

            if (!found)
                currentUsr = initUser(user);

            return currentUsr;
        }
    }
}
