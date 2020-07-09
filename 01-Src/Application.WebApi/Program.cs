using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Applications.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, loggingBuilder) =>
                {
                    loggingBuilder.AddFilter("System", LogLevel.Warning);//���˵������ռ�
                    loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
                    var path = context.HostingEnvironment.ContentRootPath;
                    loggingBuilder.AddLog4Net($"{path}/log4net.config");//�����ļ�
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}