using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AIMLbot;
using System.Net;

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

        // Responding to a users message
        public string getResponse(string input, User theUsr)
        {
            // Auto spell correcting
            string correctedInput = spellCorrect(input);

            // Sending input to bot
            Request r = new Request(correctedInput, theUsr, AimlBot);
            Result res = AimlBot.Chat(r);
            int x = res.Output.Length;
            Thread.Sleep(x*100);


            string output = res.Output;

            // default response
            if (output == "")
                output = "Sorry I don't understand.";

            return output;
        }

        // Uses google search "Did you mean?" function to correct spelling mistakes on user inputs by parsing the HTML for the correct spelling.
        public string spellCorrect(string input)
        {
            // Saves a google search result page for "input" as a string.
            WebClient client = new WebClient();
            string htmlCode = client.DownloadString("https://www.google.com/search?q=" + input);

            // Finds a place in the html string where the corrected spelling is (correction comes directly after "href="/search?q=".
            string placeHolder = htmlCode.Substring(htmlCode.LastIndexOf("href=\"/search?q=") + 16);
            // Gets the text between the start of "placeHolder" and "&" from the HTML, this should be the corrected spelling with added '+' signs.
            string correctedSpelling = getBetween("a" + placeHolder, "a", "&");
            // Replaces the '+' in the HTML with spaces so it can be correctly read by the bot.
            correctedSpelling.Replace('+', ' ');

            return correctedSpelling;
        }


        // Used to isolate the corrected search results in the google search results HTML
        // Given a source string to search and a starting and ending point, will return the characters between the start and end points.
        // Jara, O. (2016) Find text in string with C#. Available at: http://stackoverflow.com/questions/10709821/find-text-in-string-with-c-sharp (Accessed: 2 April 2016).
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public Bot getBot()
        {
            return AimlBot;
        }
    }
}
