using ProductivityTools.Bank.Millenium.App;
using System;
using Microsoft.Extensions.Configuration;

namespace ProductivityTools.Bank.Millenium.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            var settings = configuration["Login"];
            Console.WriteLine(settings);

            MilleniumApplication app = new MilleniumApplication();
            app.Run();
            Console.ReadLine();
        }
    }
}
