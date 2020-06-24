using ProductivityTools.Bank.Millenium.Selenium;
using System;

namespace ProductivityTools.Bank.Millenium.App
{
    public class MilleniumApplication
    {
        public void Run(string login, string password, string pesel)
        {
            SeleniumCalls calls = new SeleniumCalls();
            calls.Login(login, password, pesel);
        }
    }
}
