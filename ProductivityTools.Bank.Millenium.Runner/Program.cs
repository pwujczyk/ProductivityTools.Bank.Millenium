﻿using ProductivityTools.Bank.Millenium.App;
using System;
using Microsoft.Extensions.Configuration;
using ProductivityTools.MasterConfiguration;

namespace ProductivityTools.Bank.Millenium.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (String.IsNullOrWhiteSpace(environment))
                throw new ArgumentNullException("Environment not found in ASPNETCORE_ENVIRONMENT");

            Console.WriteLine("Environment: {0}", environment);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true)
                .AddMasterConfiguration(true)
                .Build();
            var settings = configuration["Login"];
            var settings2 = configuration["Login2"];
            var x = configuration.Get<Settings>();
            Console.WriteLine(settings);

            MilleniumApplication app = new MilleniumApplication();
            app.Run(x.Login, x.Password, x.Pesel);
            Console.ReadLine();
        }
    }
}
