#region Using

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

#endregion

namespace BERlogic.CallCenter
{
    internal class Program
    {
        internal static void Main(string[] args) => BuildWebHost(args).Run();

        private static IWebHost BuildWebHost(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build())
            .UseKestrel()
            .UseStartup<Startup>()
            .Build();
    }
}
