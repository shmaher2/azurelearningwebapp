using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace VisualStudioWebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(x =>
            {
                x.AddAzureStorageCoreServices();
                x.AddAzureStorage();
                x.AddTimers();
            });

            builder.ConfigureLogging((context, b) => { b.AddConsole(); });

            var host = builder.Build();

            using (host)
            {
                 host.Run();
            }
        }
    }
}
