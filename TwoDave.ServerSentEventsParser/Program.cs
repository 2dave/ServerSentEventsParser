using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "event: foo\r\ndata: some data\r\n\r\n";
            var remainder = "";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out remainder);

            Console.WriteLine("message.Id = {0}", message.Id);
            Console.WriteLine("message.Event = {0}", message.Data);
            Console.WriteLine("message.Data = {0}", message.Event);
            Console.WriteLine("Remainder = {0}", remainder);
        }
    }
}