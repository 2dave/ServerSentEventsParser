using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    public class Parser
    {
        public static string ParseLine(string input, out string remainder)
        {
            string line = null;
            var lfsearch = input.IndexOf('\n'); //first occurence 
            var crsearch = input.IndexOf('\r'); //first occurence

            if (lfsearch >= 0 && crsearch >= 0)
            {
                //line = input.Remove(lfsearch);
                line = input.Remove(crsearch);
            }

            remainder = input.Remove(0, lfsearch + 1);

            return line;
        }

        public static SseMessage ParseMessage(string input, out string remainder)
        {
            SseMessage message = new SseMessage();
            var lfsearch = input.IndexOf('\n'); //first occurence 
            var crsearch = input.IndexOf('\r'); //first occurence

            message.Id = "1";
            remainder = "";

            return message;
        }

    }
}