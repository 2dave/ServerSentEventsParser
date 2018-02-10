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

            // Parse message line by line
            var line = ParseLine(input, out remainder);
            message.Id = line;
            //message.Event = line; // seems like a bad idea
            //message.Data = line;
            
            remainder = remainder.TrimStart(); // but will take off the /r/n - maybe  wait until end of function?
            //Console.WriteLine("raw message.Id = {0}", message.Id);
            //Console.WriteLine("raw remainder = {0}", remainder);

            //Does an Id: exist? if so clean it up and set message.Id
            if (line.StartsWith("Id:"))
            {
                var removestring = "Id:";
                int index = message.Id.IndexOf(removestring);
                int length = removestring.Length;

                var startstring = message.Id.Substring(0, index); //Getting string up until beginning of removal string 
                var endofstring = message.Id.Substring(index + length); //Getting everything after removal string
                var clean = startstring.Trim() + endofstring.Trim(); //Removing leading and trailing spaces

                message.Id = clean; 
            }

            return message;
        }

    }
}