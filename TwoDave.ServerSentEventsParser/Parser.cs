using System;
using System.IO;
using System.Text;

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

        public static SseMessage ParseStream(Stream stream, out string remainder)
        {
            byte[] bytes = new byte[4 * 1024];
            var read = stream.Read(bytes, 0, bytes.Length); // tells the actual number bytes

            string s = Encoding.UTF8.GetString(bytes, 0, read);


            SseMessage message = Parser.ParseMessage(s, out remainder);

            return message;
        }

        public static SseMessage ParseFile(string path, out string remainder) //take out remainder and return an array of SseMessages
        {
            // refactor to allow for multiple messages
            using (var stream = File.OpenRead(path))
            {
                SseMessage message = Parser.ParseStream(stream, out remainder);
                return message;

            }
        }
    }
}