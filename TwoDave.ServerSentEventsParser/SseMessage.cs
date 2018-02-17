using System;
using System.IO;

namespace TwoDave.ServerSentEventsParser
{
    public class SseMessage
    {
        public string Event { get; set; }
        public string Id { get; set; }
        public string Data { get; set; }
    }
}