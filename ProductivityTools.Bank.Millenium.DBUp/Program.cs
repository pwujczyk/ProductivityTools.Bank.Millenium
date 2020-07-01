using DbUp;
using System;
using System.Reflection;

namespace ProductivityTools.Bank.Millenium.DBUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString ="Server=.\\sql2019; Database=PT.Bank.Millenium; Trusted_connection=true";
            EnsureDatabase.For.SqlDatabase(connectionString);
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
              
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
          
        }
    }
}
