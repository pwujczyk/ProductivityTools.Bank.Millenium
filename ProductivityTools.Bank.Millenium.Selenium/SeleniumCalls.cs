using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Runtime.CompilerServices;

namespace ProductivityTools.Bank.Millenium.Selenium
{
    public class SeleniumCalls
    {
        IWebDriver Chrome;

        public SeleniumCalls()
        {
            this.Chrome = new ChromeDriver();
        }

        public void Login(string login, string password, string Pesel)
        {
            this.Chrome.Url = Addresses.LoginPage;
        }
    }
}
