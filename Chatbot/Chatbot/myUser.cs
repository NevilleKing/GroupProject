using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AIMLbot;

namespace Chatbot
{
    class myUser
    {
        public string userName;
        public RichTextBox textBox;
        public User AIusr;

        public myUser(string user, ref RichTextBox richTextBox, Bot theBot, string userFullName)
        {
            userName = user;
            textBox = richTextBox;
            AIusr = new AIMLbot.User(user, theBot);
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
