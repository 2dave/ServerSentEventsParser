using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //var input = "data: This is data.\r\ndata: More data is expec";
            //var line = Parser.ParseLine(input, out var remainder);

            //Console.WriteLine("Input = {0}", line);
            //Console.WriteLine("Remainder = {0}", remainder);

            var input = "aaaId: 1\r\n\r\notherstuff";
            var remainder = "";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out remainder);

            Console.WriteLine("message.Id = {0}", message.Id);
            Console.WriteLine("Remainder = {0}", remainder);
        }
    }
}