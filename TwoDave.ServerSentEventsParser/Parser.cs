using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    public class Parser
    {
        public static string ParseLine(string input, out string remainder)
        {
            string line = null;

            var crsearch = input.IndexOf('\r'); //first occurence
            var lfsearch = input.IndexOf('\n'); //first occurence 

            if (lfsearch >= 0 && crsearch >= 0)
            {
                line = input.Remove(crsearch);
                //line = input.Remove(lfsearch);
            }

            remainder = input.Remove(0, lfsearch + 1);

            return line;
        }

        public static SseMessage ParseMessage(string input, out string remainder)
        {
            SseMessage message = new SseMessage();

            #region IF I need to find the double /r/n 
            var lf = 0;
            var cr = 0;
            for (var i = 0; i < input.Length; i++)
            {
                char temp = input[i];

                if (temp == '\r')
                {
                    cr++;
                }

                if (temp == '\n')
                {
                    lf++;
                }
            }
            #endregion

            message.Id = ParseLine(input, out remainder);
            Console.WriteLine("message.Id = {0}", message.Id);

            //removing Id: from the message            
            var removestring = "Id: ";
            int index = message.Id.IndexOf(removestring);
            int length = removestring.Length;
            string startstring = message.Id.Substring(0, index);
            string endofstring = message.Id.Substring(index + length);
            string clean = startstring + endofstring;

            message.Id = clean;

            Console.WriteLine("clean = {0}", clean);

            //message.Id = "1";
            remainder = "";

            return message;
        }

    }
}