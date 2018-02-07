using System;
using TwoDave.ServerSentEventsParser;
using Xunit;

namespace TD.SseParserTest
{
    public class MessageParserFixture
    {
        [Fact]
        public void BeginningMessageParserTest()
        {
            var input = "Id: 1\r\n\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal("1", message.Id);
            Assert.Equal("", remainder);
        }

        [Fact]
        public void SimilarBeginningMessageParserTest()
        {
            var input = "aaaId: 1\r\n\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal("aaa1", message.Id);
            Assert.Equal("", remainder);
        }

        [Fact]
        public void MessageParserSpecExampleTwo()
        {
            var input = "event: foo\r\ndata: some data\r\n\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal("foo", message.Event);
            Assert.Equal("some data", message.Id);
            Assert.Equal("", remainder);

        }

    }
}