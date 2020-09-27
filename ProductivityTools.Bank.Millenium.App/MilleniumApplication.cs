using ProductivityTools.Bank.Millenium.Caller;
using ProductivityTools.Bank.Millenium.Selenium;
using ProductivityTools.BankAccounts.Contract;
using System;
using System.Collections.Generic;

namespace ProductivityTools.Bank.Millenium.App
{
    public class MilleniumApplication
    {
        SeleniumCalls seleniumCalls;
        HttpCaller BMCommands;

        public MilleniumApplication(SeleniumCalls selenium, HttpCaller bmcommands)
        {
            this.seleniumCalls = selenium;
            this.BMCommands = bmcommands;
        }

        public async void Run(string login, string password, string pesel)
        {
            //SeleniumCalls calls = new SeleniumCalls();
            seleniumCalls.Login(login, password, pesel);
            BasicData basicData= seleniumCalls.GetBasicData();
            await BMCommands.SaveBasicData(basicData);
            List<Transaction> transactions=seleniumCalls.GetTransactions();
             
            await BMCommands.SaveTransactions(transactions);
        }
    }
}
