using System;
using System.Linq;
using System.Text;

namespace RobMen.SeverSentEventsServer
{
    public class SseMessage
    {
        public string Id { get; set; }

        public string[] Data { get; set; }

        public string Event { get; set; }

        public override string ToString()
        {
            var content = new StringBuilder();

            content.AppendLine($"event: {this.Event}");

            if (!String.IsNullOrEmpty(this.Id))
            {
                content.AppendLine($"id: {this.Id}");
            }

            if (this.Data?.Length > 0)
            {
                this.Data.Select(d => content.AppendLine($"data: {d}")).ToList();
            }

            content.AppendLine();

            return content.ToString();
        }
    }
}
