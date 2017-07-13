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
using AIMLbot;
using System.Threading;
using Skype4Sharp;
using Skype4Sharp.Events;
using Skype4Sharp.Auth;
using Skype4Sharp.Helpers;
using Skype4Sharp.Enums;

namespace Chatbot
{
    public partial class MainForm : Form
    {
        private Skype4Sharp.Skype4Sharp mainSkype;
        private string skype_botName; // display name of local user (bot)
        ChatResponse chatbot;
        List<myUser> conversation_users;

        public MainForm()
        {
            InitializeComponent();

            bool loginFlag = false;
            string user = "";

            do
            {
                // Get login info from user
                Login userLogin = new Login();

                if (userLogin.ShowDialog() != DialogResult.OK || (userLogin.user == "" || userLogin.pass == ""))
                {
                    MessageBox.Show("You need to provide credentials to use this program.", "Skype Login Error", MessageBoxButtons.OK);
                    Environment.Exit(-1);
                }

                // create credential object
                Skype4Sharp.Auth.SkypeCredentials creds = new Skype4Sharp.Auth.SkypeCredentials(userLogin.user, userLogin.pass);

                mainSkype = new Skype4Sharp.Skype4Sharp(creds);
                if (mainSkype.Login())
                {
                    loginFlag = true;
                    user = userLogin.user;
                }
                else
                    MessageBox.Show("Login failed. Please try again.", "Skype Login Error", MessageBoxButtons.OK);

            } while (loginFlag == false);

            mainSkype.messageReceived += OnMessage;
            skype_botName = user;
            chatbot = new ChatResponse();
            conversation_users = new List<myUser>();
            mainSkype.StartPoll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        void SendMessage()
        {
            mainSkype.SendMessage(tabControl1.SelectedTab.Text, textBox1.Text);
            textBox1.Text = String.Empty;
        }


        //Will read from skype and update text box
        private void OnMessage(ChatMessage msg)
        {
            // check if we are on the main thread
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { OnMessage(msg); });
                return;
            }

            if (msg.Sender.Username != skype_botName)
            {
                myUser usr = getCurrentUser(msg.Sender.Username, msg.Sender.DisplayName);
                usr.textBox.AppendText("\n\n" + msg.Sender.Username + ": " + msg.Body);
                string resp = chatbot.getResponse(msg.Body, usr.AIusr, label2);
                msg.Chat.SendMessage(resp);
                usr.textBox.AppendText("\n\nBot: " + resp);      
            }
        }

        private myUser initUser(string user, string userFullName)
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
            conversation_users.Add(new myUser(user, ref rtb, chatbot.getBot(), userFullName));
            tabControl1.Visible = true;
            return conversation_users[conversation_users.Count-1];
        }

        private myUser getCurrentUser(string user, string userFullName)
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
                currentUsr = initUser(user, userFullName);

            return currentUsr;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendMessage();
        }
    }
}
