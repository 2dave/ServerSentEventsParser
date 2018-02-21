using System;
using System.IO;
using System.Text;
using TwoDave.ServerSentEventsParser;
using Xunit;

namespace TD.SseParserTest
{
    public class FileParserFixture
    {
        [Fact]
        public void BeginningFileParserTest()
        {
            var input = @".\test.txt";

            using(FileStream fs = File.Create(input))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("data: foo\r\ndata: bar\r\n\r\nevent: next\r\n");
                fs.Write(info, 0, info.Length);
            }

            try
            {
                var remainder = "";
                SseMessage message = new SseMessage();

                message = Parser.ParseFile(input, out remainder);

                Assert.Equal("foo\r\nbar", message.Data);
                Assert.Equal("event: next\r\n", remainder);

                File.Delete(input);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find: {0}", e.FileName);
            }
        }

        [Fact]
        public void FileParserBlankTest()
        {
            try
            {
                var input = @".\test.txt"; //Does not exist
                var remainder = "";
                SseMessage message = new SseMessage();

                message = Parser.ParseFile(input, out remainder);

                Assert.Equal(null, message.Data);
                Assert.Equal("event: next\r\n", remainder);

                File.Delete(input);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find: {0}", e.FileName);
            }
        }
    }
}