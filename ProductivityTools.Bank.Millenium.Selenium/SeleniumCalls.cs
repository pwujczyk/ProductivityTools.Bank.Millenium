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
            var loginControl = this.Chrome.FindElement(By.Id("Millekod_txtContent"));
            loginControl.SendKeys(login);

            var loginButton = this.Chrome.FindElement(By.Id("BtnLogin"));
            loginButton.Click();

            var passwordControl = this.Chrome.FindElement(By.Id("PasswordOne_txtContent"));
            passwordControl.SendKeys(password);

            bool[] peselInputs = new bool[11];
            Func<string,bool> peselDisabled = (id) =>
              {
                  var pesel1Control = this.Chrome.FindElement(By.Id(id));
                  var disabled = pesel1Control.GetAttribute("disabled");
                  return !(disabled == "true");
              };
           ;
            peselInputs[0] = peselDisabled("PESEL_0_txtContent");
            peselInputs[1] = peselDisabled("PESEL_1_txtContent");
            peselInputs[2] = peselDisabled("PESEL_2_txtContent");
            peselInputs[3] = peselDisabled("PESEL_3_txtContent");
            peselInputs[4] = peselDisabled("PESEL_4_txtContent");
            peselInputs[5] = peselDisabled("PESEL_5_txtContent");
            peselInputs[6] = peselDisabled("PESEL_6_txtContent");
            peselInputs[7] = peselDisabled("PESEL_7_txtContent");
            peselInputs[8] = peselDisabled("PESEL_8_txtContent");
            peselInputs[9] = peselDisabled("PESEL_9_txtContent");
            peselInputs[10] = peselDisabled("PESEL_10_txtContent");


        }
    }
}
