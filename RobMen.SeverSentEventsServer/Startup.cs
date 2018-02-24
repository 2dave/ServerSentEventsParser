using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace RobMen.SeverSentEventsServer
{
    public class Startup
    {
        private static Random _randomNumberGenerator = new Random();
        private static int _id = 1;

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(SseResponseStreamAsync);
        }

        public async Task SseResponseStreamAsync(HttpContext context)
        {
            var applicationLifetime = context.RequestServices.GetRequiredService<IApplicationLifetime>();

            var response = context.Response;

            // Set the SSE headers in the response.
            response.ContentType = "text/event-stream";
            response.Headers["Cache-Control"] = "no-cache";
            response.Headers["Cache-Encoding"] = "identity";
            response.Headers["Access-Control-Allow-Origin"] = "*";

            var bufferingFeature = context.Features.Get<IHttpBufferingFeature>();
            bufferingFeature?.DisableResponseBuffering();

            // Create a cancellation source from the server shutdown and client disconnect cancellation tokens.
            using (var linked = CancellationTokenSource.CreateLinkedTokenSource(applicationLifetime.ApplicationStopping, context.RequestAborted))
            {
                var cancellationToken = linked.Token;

                try
                {
                    // If client sent the last event id, send it back to the client for no particular reason.
                    if (context.Request.Headers.TryGetValue("Last-Event-ID", out var lastEventId))
                    {
                        var message = new SseMessage
                        {
                            Event = "server-resumed",
                            Data = new[] { "Resuming with last event id", lastEventId.ToString() }
                        };

                        await response.WriteAsync(message.ToString(), cancellationToken).ConfigureAwait(false);
                    }

                    // Send a random message every 3 seconds until the client disconnects or the server shuts down.
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        await Task.Delay(3 * 1000).ConfigureAwait(false);

                        var message = CreateRandomMessage();

                        await response.WriteAsync(message.ToString(), cancellationToken).ConfigureAwait(false);
                    }
                }
                catch (OperationCanceledException)
                {
                    // If the client is still connected, the server is shutting down so say goodbye.
                    if (!context.RequestAborted.IsCancellationRequested)
                    {
                        var message = new SseMessage { Event = "server-shutdown" };

                        await response.WriteAsync(message.ToString(), CancellationToken.None).ConfigureAwait(false);

                        // Wait for a second to ensure the client gets the message.
                        try
                        {
                            await Task.Delay(1 * 1000, context.RequestAborted).ConfigureAwait(false);
                        }
                        catch (OperationCanceledException)
                        {
                        }
                    }
                }
            }
        }

        private static SseMessage CreateRandomMessage()
        {
            // Have an id 70% of the time
            var id = _randomNumberGenerator.Next(0, 9) > 2 ? "id-" + ++_id : null;

            var dataLength = _randomNumberGenerator.Next(0, 5);

            var data = new string[dataLength];

            for (var i = 0; i < dataLength; ++i)
            {
                data[i] = RandomString();
            }

            return new SseMessage
            {
                Event = "random-message" + _randomNumberGenerator.Next(1, 5),
                Id = id,
                Data = data,
            };
        }

        private static string RandomString()
        {
            var words = new[] { "foo", "bar", "baz", "bim", "qux", "lorem", "ipsum", "avocado", "toast" };

            var terms = new string[_randomNumberGenerator.Next(1, 10)];

            for (var i = 0; i < terms.Length; ++i)
            {
                terms[i] = words[_randomNumberGenerator.Next(0, words.Length - 1)];
            }

            return String.Join(" ", terms);
        }
    }
}
