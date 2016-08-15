using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RazorPages.Todo.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureServices(services => {
                    services.AddMvcCore()
                            .AddJsonFormatters();
                })
                .ConfigureLogging(loggerFactory => {
                    loggerFactory.AddConsole();
                })
                .Configure(app => {
                    app.UseMvc();
                })
                .Build();

            host.Run();
        }
    }
}
