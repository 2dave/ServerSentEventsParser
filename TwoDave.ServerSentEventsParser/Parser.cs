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

            var line = ParseLine(input, out remainder);

            //Debug
            var numlines = 0;

            //Parse the message line by line until nothing is left
            while (line != null)
            {
                //Debug - number of lines
                numlines++;
                Console.WriteLine("Number of Lines Processed: {0}", numlines);

                //Does an Id: exist? if so clean it up and set message.Id
                if (line.StartsWith("id:"))
                {
                    message.Id = line;

                    var removestring = "id:";
                    var clean = (message.Id.Substring(0 + removestring.Length)).Trim();

                    message.Id = clean;
                }

                if (line.StartsWith("event:"))
                {
                    message.Event = line;

                    var removestring = "event:";
                    var clean = (message.Event.Substring(0 + removestring.Length)).Trim();

                    message.Event = clean;
                }

                if (line.StartsWith("data:"))
                {
                    message.Data = line;

                    var removestring = "data:";
                    var clean = (message.Data.Substring(0 + removestring.Length)).Trim();

                    message.Data = clean;
                }

                // Parsed the first line of the message now continue             
                line = ParseLine(remainder, out remainder);
            }
            
            return message;
        }
    }
}