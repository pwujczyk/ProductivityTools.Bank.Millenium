using ProductivityTools.Bank.Millenium.Selenium;
using System;

namespace ProductivityTools.Bank.Millenium.App
{
    public class MilleniumApplication
    {
        public void Run()
        {
            SeleniumCalls calls = new SeleniumCalls();
            calls.Login(string.Empty,string.Empty,string.Empty);
        }
    }
}
