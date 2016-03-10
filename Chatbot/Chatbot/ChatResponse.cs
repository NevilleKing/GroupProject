using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using SKYPE4COMLib;
using AIMLbot;

namespace Chatbot
{
    class ChatResponse
    {
        Bot AimlBot;
        AIMLbot.User myUser;

        public ChatResponse()
        {
            AimlBot = new Bot();
            myUser = new AIMLbot.User("testUser", AimlBot);

            AimlBot.loadSettings();
            AimlBot.isAcceptingUserInput = false;
            AimlBot.loadAIMLFromFiles();
            AimlBot.isAcceptingUserInput = true;
        }

        public string getResponse(string input)
        {

            Request r = new Request(input, myUser, AimlBot);
            Result res = AimlBot.Chat(r);

            return res.Output;
        }
    }
}
