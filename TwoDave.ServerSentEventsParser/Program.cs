using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @".\data\test.txt";

            try
            {
                var remainder = "";

                SseMessage message = new SseMessage();

                message = Parser.ParseFile(path, out remainder);

                Console.WriteLine("***Parsing file located at {0} ***", path);

                // Could or should this be rewritten with either null conditionals or different form -> ? :
                if (message.Id != null) //Is there a benefit to this? -> !string.IsNullOrEmpty(message.Id))
                {
                    Console.WriteLine("Message ID = {0}", message.Id);
                }
                if (message.Event != null)
                {
                    Console.WriteLine("Message Event = {0}", message.Event);
                }
                if (message.Data != null)
                {
                    Console.WriteLine("Message Data = {0}", message.Data);
                }

                Console.WriteLine("Remainder = {0}", remainder);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find: {0}", e.FileName);
            }
        }
    }
}