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
            //var remainder = "";
            SseMessage message = new SseMessage();

            message = Parser.ParseMessage(input, out var remainder);
            
            Assert.Equal("1", message.Id);
            Assert.Equal("", remainder);
        }      


    }
}