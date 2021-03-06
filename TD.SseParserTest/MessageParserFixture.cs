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
            var input = "id: 1\r\n\r\n";
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

            Assert.Equal(null, message.Id);
            Assert.Equal("", remainder);
        }

        [Fact]
        public void MessageParserTestForEvent()
        {
            var input = "event: test\r\n\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal("test", message.Event);
            Assert.Equal("", remainder);
        }

        [Fact]
        public void MessageParserTestForData()
        {
            var input = "data: test\r\n\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal("test", message.Data);
            Assert.Equal("", remainder);
        }

        [Fact]
        public void MessageParserSpecExampleTwo()
        {
            var input = "event: foo\r\ndata: some data\r\n\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal("foo", message.Event);
            Assert.Equal("some data", message.Data);
            Assert.Equal("", remainder);
        }

        [Fact]
        public void MessageParserTestIgnoreColon()
        {
            var input = ": Stuff:data should ignore\r\n\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal(null, message.Id);
            Assert.Equal(null, message.Data);
            Assert.Equal(null, message.Event);
            Assert.Equal("", remainder);
        }

        [Fact]
        public void MessageParserTestOneColonLine()
        {
            var input = ":Stuff to Ignore\r\nid: 2\r\ndata: stuff\r\n\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal("2", message.Id);
            Assert.Equal("stuff", message.Data);
            Assert.Equal(null, message.Event);
            Assert.Equal("", remainder);
        }

        [Fact]
        public void MessageParserTestSpacesDoNotMatterAfterProperties()
        {
            var input = ":Stuff to Ignore\r\nid: 2\r\ndata:stuff\r\n\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal("2", message.Id);
            Assert.Equal("stuff", message.Data);
            Assert.Equal(null, message.Event);
            Assert.Equal("", remainder);
        }

        [Fact]
        public void MessageParserTestSpecExampleThree()
        {
            var input = "data: foo\r\ndata: bar\r\n\r\nevent: next\r\n";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);

            Assert.Equal("foo\r\nbar", message.Data);
            Assert.Equal("event: next\r\n", remainder);
        }
    }
}