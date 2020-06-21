using ProductivityTools.Bank.Millenium.App;
using System;

namespace ProductivityTools.Bank.Millenium.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MilleniumApplication app = new MilleniumApplication();
            app.Run();
            Console.ReadLine();
        }
    }
}
