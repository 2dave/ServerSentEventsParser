using System;
using TwoDave.ServerSentEventsParser;
using Xunit;

namespace TD.SseParserTest
{
    public class ParserFixture
    {
        [Fact]
        public void BeginningParserTest()
        {
            var input = "data: This is data.\r\ndata: More data is expec";
            //string line;
            //string remainder;

            //Parser.Parse(input, ref line, out remainder);
            var line = Parser.Parse(input, out var remainder);

            Assert.Equal("data: This is data.", line);
            Assert.Equal("data: More data is expec", remainder);
        }

    }
}