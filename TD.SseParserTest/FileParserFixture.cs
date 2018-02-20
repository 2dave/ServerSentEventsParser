using System;
using System.IO;
using TwoDave.ServerSentEventsParser;
using Xunit;

namespace TD.SseParserTest
{
    public class FileParserFixture
    {
        [Fact]
        public void BeginningFileParserTest()
        {
            // File has "data: foo\r\ndata: bar\r\n\r\nevent: next\r\n"
            
            var input = @".\data\test.txt";            
            //var input = "data: foo\r\ndata: bar\r\n\r\nevent: next\r\n";
            //System.IO.File.WriteAllText(@".\data\fileunittest1.txt", input);

            try
            {                
                var remainder = "";
                SseMessage message = new SseMessage();

                message = Parser.ParseFile(input, out remainder);

                Assert.Equal("foo\r\nbar", message.Data);
                Assert.Equal("event: next\r\n", remainder);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find: {0}", e.FileName);
            }
        }

    }
}