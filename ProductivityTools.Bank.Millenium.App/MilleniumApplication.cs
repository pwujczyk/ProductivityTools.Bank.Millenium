using ProductivityTools.Bank.Millenium.Commands;
using ProductivityTools.Bank.Millenium.Objects;
using ProductivityTools.Bank.Millenium.Selenium;
using System;
using System.Collections.Generic;

namespace ProductivityTools.Bank.Millenium.App
{
    public class MilleniumApplication
    {
        SeleniumCalls seleniumCalls;
        BMCommands BMCommands;

        public MilleniumApplication(SeleniumCalls selenium, BMCommands bmcommands)
        {
            this.seleniumCalls = selenium;
            this.BMCommands = bmcommands;
        }

        public void Run(string login, string password, string pesel)
        {
            //SeleniumCalls calls = new SeleniumCalls();
            seleniumCalls.Login(login, password, pesel);
            BasicData basicData= seleniumCalls.GetBasicData();
            BMCommands.SaveBasicData(basicData);
            List<Transaction> transactions=seleniumCalls.GetTransactions();
             
            BMCommands.SaveTransactions(transactions);
        }
    }
}
