using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AIMLbot;

namespace Chatbot
{
    class ChatResponse
    {
        Bot AimlBot;

        public ChatResponse()
        {
            AimlBot = new Bot();

            AimlBot.loadSettings();
            AimlBot.isAcceptingUserInput = false;
            AimlBot.loadAIMLFromFiles();
            AimlBot.isAcceptingUserInput = true;
        }


        public string getResponse(string input, User theUsr)
        {
            Request r = new Request(input, theUsr, AimlBot);
            Result res = AimlBot.Chat(r);
            int x = res.Output.Length;
            Thread.Sleep(x*100);


            string output = res.Output;

            // default response
            if (output == "")
                output = "Sorry I don't understand.";

            return output;
        }

        public Bot getBot()
        {
            return AimlBot;
        }
    }
}
