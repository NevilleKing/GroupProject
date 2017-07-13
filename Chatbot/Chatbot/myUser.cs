using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIMLbot;
using Skype4Sharp;

namespace Chatbot
{
    class myUser
    {
        public string userName;
        public RichTextBox textBox;
        public AIMLbot.User AIusr;
        public Chat chat;

        public myUser(string user, ref RichTextBox richTextBox, Bot theBot, string userFullName, Chat theChat)
        {
            userName = user;
            textBox = richTextBox;
            AIusr = new AIMLbot.User(user, theBot);
            chat = theChat;
            if (userFullName != "")
                AIusr.Predicates.updateSetting("name", userFullName);
            else
                AIusr.Predicates.updateSetting("name", user);
        }

        public myUser()
        {

        }
    }
}
