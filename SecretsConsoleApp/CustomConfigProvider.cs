using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretsConsoleApp
{
    public class CustomConfigProvider : ConfigurationProvider
    {
        public CustomConfigProvider() { }

        public override void Load()
        {
            // read in the file

            // decrypt the json file

            // set Data = decrypted key value pairs
        }
    }

}
