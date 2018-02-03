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

            //Parser.Parse(input, ref line, out remainder);
            var line = Parser.Parse(input, out var remainder);

            Assert.Equal("data: This is data.", line);
            Assert.Equal("data: More data is expec", remainder);
        }

        [Fact]
        public void ParserTestCustomerSpecExample()
        {
            var input = "event: partia";

            var line = Parser.Parse(input, out var remainder);

            Assert.Equal(null, line);
            Assert.Equal("event: partia", remainder);
        }

        [Fact]
        public void ParserTestBlank()
        {
            var input = "";

            var line = Parser.Parse(input, out var remainder);

            Assert.Equal(null, line);
            Assert.Equal("", remainder);
        }

    }
}