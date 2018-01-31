using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "data: This is data.\r\ndata: More data is expec\r\nmoredata now";
            string line = Parser.Parse(input, out string remainder);
        }
    }
}