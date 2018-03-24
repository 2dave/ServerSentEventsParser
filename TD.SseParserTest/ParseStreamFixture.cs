using System;
using System.Text;
using System.IO;
using TwoDave.ServerSentEventsParser;
using Xunit;

namespace TD.SseParserTest
{
    public class ParseStreamFixture
    {
        [Fact]
        public void BeginningParseStreamTest()
        {
            var s = "id:foo\r\n\r\n";
            var bytes = Encoding.UTF8.GetBytes(s);

            using (var stream = new MemoryStream(bytes))
            {
                var message = Parser.ParseStream(stream, out var remainder);
                Assert.Equal("foo", message.Id);
            }

        }

    }
}