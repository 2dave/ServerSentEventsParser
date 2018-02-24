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

            using (FileStream fs = File.Create(input))
            {
                Byte[] info = Encoding.UTF8.GetBytes("data: foo\r\ndata: bar\r\n\r\nevent: next\r\n");
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
                var input = @".\shouldnotexist.txt";
                var remainder = "";
                SseMessage message = new SseMessage();

                message = Parser.ParseFile(input, out remainder);

                Assert.Equal("shouldnevergethere", message.Data);
                Assert.Equal("shouldnevergethere", remainder);

                File.Delete(input);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Could not find: {0}", e.FileName);
            }
        }

        [Fact]
        public void FileParserTest()
        {
            var input = @".\testasdf.txt";

            using (FileStream fs = File.Create(input))
            {
                Byte[] info = Encoding.UTF8.GetBytes("id: 3\r\ndata: foo\r\ndata: bar\r\n\r\nevent: next\r\n");
                fs.Write(info, 0, info.Length);
            }

            try
            {
                var remainder = "";
                SseMessage message = new SseMessage();

                message = Parser.ParseFile(input, out remainder);

                Assert.Equal("3", message.Id);
                Assert.Equal("foo\r\nbar", message.Data);
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