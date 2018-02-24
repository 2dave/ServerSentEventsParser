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

            //while (!(line == null || line == ""))
            //while (line != null && line != "")
            while (!string.IsNullOrEmpty(line))
            {
                if (line.StartsWith("data:"))
                {
                    if (message.Data != null)
                    {
                        message.Data += "\r\n";
                    }
                    const string removestring = "data:";
                    message.Data += (line.Substring(0 + removestring.Length)).Trim();
                }
                else if (line.StartsWith("event:"))
                {
                    const string removestring = "event:";
                    message.Event = (line.Substring(0 + removestring.Length)).Trim();
                }
                else if (line.StartsWith("id:"))
                {
                    const string removestring = "id:";
                    message.Id = line.Substring(0 + removestring.Length).Trim();
                }

                line = ParseLine(remainder, out remainder);
            }

            return message;
        }

        public static SseMessage ParseFile(string path, out string remainder)
        {
            var filemessage = File.ReadAllText(path);

            SseMessage message = new SseMessage();
            message = Parser.ParseMessage(filemessage, out remainder);

            return message;
        }
    }
}