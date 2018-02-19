using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Debugging and Testing ParseMessage
            //var input = "data: foo\r\ndata: bar\r\n\r\nevent: next\r\n";
            //var remainder = "";
            //SseMessage message = new SseMessage();

            //message = Parser.ParseMessage(input, out remainder);

            //Console.WriteLine("Message data should be:\r\nfoo\r\nbar");
            //Console.WriteLine(message.Data);

            //Console.WriteLine("Remainder should be:\r\nevent: next\r\n");
            //Console.WriteLine(remainder);
            #endregion

            #region Debugging and Testing ParseFile
            var path = @".\data\test.txt";
            try
            {
                var remainder = "";
                SseMessage message = new SseMessage();
                
                message = Parser.ParseFile(path, out remainder);

                Console.WriteLine("Message data should be:\r\nfoo\r\nbar");
                Console.WriteLine(message.Data);

                Console.WriteLine("Remainder should be:\r\nevent: next\r\n");
                Console.WriteLine(remainder);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find: {0}", e.FileName);
            }
            #endregion

        }
    }
}