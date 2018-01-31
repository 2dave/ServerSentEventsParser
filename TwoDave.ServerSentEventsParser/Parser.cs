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
            var lf = 0;
            var cr = 0;

            //Console.WriteLine("Line Feed first occurence found at index {0}", lfsearch);
            //Console.WriteLine("Carriage return first occurence found at index {0}", crsearch);

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

            var line = input.Remove(lfsearch);
            line = input.Remove(crsearch);

            remainder = input.Remove(0, 21);

            Console.WriteLine("Total LFs = {0}", lf);
            Console.WriteLine("Total CRs = {0}", cr);

            //Console.WriteLine("Line = {0}", line);
            //Console.WriteLine("Remainder = {0}", remainder);

            return line;
            //throw new NotImplementedException();
        }
    }
}