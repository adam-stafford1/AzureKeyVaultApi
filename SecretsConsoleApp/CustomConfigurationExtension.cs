using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsConsoleApp
{
    public static class CustomConfigurationExtensions
    {
        public static IConfigurationBuilder AddCustomConfiguration(this IConfigurationBuilder builder)
        {
            return builder.Add(new CustomConfigurationSource());
        }
    }
}
