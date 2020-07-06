using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsConsoleApp
{
    public class CustomConfigurationSource : IConfigurationSource
    {
        public CustomConfigurationSource() { }


        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new CustomConfigProvider();
        }
    }
}
