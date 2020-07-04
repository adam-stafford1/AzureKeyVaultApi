using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Azure.Identity;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Configuration;
using System;

namespace AzureKeyVaultApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            var uri = $"https://{Environment.GetEnvironmentVariable("KEY_VAULT_NAME")}.vault.azure.net";
            var clientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
            var clientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");

            config.AddAzureKeyVault(uri, clientId, clientSecret);

            var env = hostingContext.HostingEnvironment;

            config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                  .AddJsonFile($"{Environment.GetEnvironmentVariable("CORE_CONFIG_DIRECTORY")}appsettings.json"
                  , optional: false, reloadOnChange: true);

            config.AddXmlFile($"{Environment.GetEnvironmentVariable("CORE_CONFIG_DIRECTORY")}appsettings.xml"
                , optional: false, reloadOnChange: true);
        })
        .UseStartup<Startup>());
    }
}
