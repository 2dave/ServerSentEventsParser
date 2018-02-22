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

                if (message.Id != null)
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