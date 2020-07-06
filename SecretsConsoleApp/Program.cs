using Microsoft.Extensions.Configuration;
using System;

namespace SecretsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddCustomConfiguration()
                .AddCommandLine(args)
                .Build();

            var data = Configuration.GetValue<string>("somesetting");
        }
    }
}
