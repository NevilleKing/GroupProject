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

        public myUser(string user, ref RichTextBox richTextBox, Bot theBot)
        {
            userName = user;
            textBox = richTextBox;
            AIusr = new AIMLbot.User(user, theBot);
        }

        public myUser()
        {

        }
    }
}
