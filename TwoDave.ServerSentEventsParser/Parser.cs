using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    public class Parser
    {
        public static string Parse(string input, out string remainder)
        {
            // Some common line terminator unicode values:
            // \u000a - Line Feed
            // \u000b - Vertical Tab
            // \u000c - Form Feed
            // \u000d - Carriage Return

            var lfsearch = input.IndexOf('\u000a'); //first occurence 
            var crsearch = input.IndexOf('\u000d'); //first occurence
            var line = "";
            remainder = "";

            #region Block that counts LFs and CRs - not used
            //Console.WriteLine("Line Feed first occurence found at index {0}", lfsearch);
            //Console.WriteLine("Carriage return first occurence found at index {0}", crsearch);

            var lf = 0;
            var cr = 0;
            for (var i = 0; i < input.Length; i++)
            {
                char temp = input[i];

                if (temp == '\u000a')
                {
                    lf++;
                }

                if (temp == '\u000d')
                {
                    cr++;
                }
            }
            #endregion

            if (lfsearch >= 0 && crsearch >= 0)
            {
                //line = input.Remove(lfsearch);
                line = input.Remove(crsearch);
            }

            remainder = input.Remove(0, lfsearch + 1); // lfsearch = -1 when blank. is this good enough?

            if (line == "")
            {
                return null;
            }
            else
            {
                return line;
            }

            //throw new NotImplementedException();
        }
    }
}