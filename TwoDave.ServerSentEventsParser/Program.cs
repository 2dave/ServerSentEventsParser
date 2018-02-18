using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "data: foo\r\ndata: bar\r\n\r\nevent: next\r\n";
            var remainder = "";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out remainder);

            Console.WriteLine("Message data should be:\r\nfoo\r\nbar");
            Console.WriteLine(message.Data);

            Console.WriteLine("Remainder should be:\r\nevent: next\r\n");
            Console.WriteLine(remainder);
        }
    }
}