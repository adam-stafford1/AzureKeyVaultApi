using Microsoft.Build.Construction;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleSecretSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = ConfigurationManager.AppSettings[$"somesetting{ConfigurationManager.AppSettings["environment"]}"];

            Console.WriteLine(data);
        }
    }
}
