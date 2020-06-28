﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ProductivityTools.Bank.Millenium.Selenium
{
    public class SeleniumCalls
    {
        IWebDriver Chrome;

        public SeleniumCalls()
        {
            ChromeOptions options = new ChromeOptions();
            //options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            //options.SetLoggingPreference(LogType.Client, LogLevel.All);
            //options.SetLoggingPreference(LogType.Driver, LogLevel.All);
            //options.SetLoggingPreference(LogType.Profiler, LogLevel.All);
            //options.SetLoggingPreference(LogType.Server, LogLevel.All);
            
            //LoggingPreferences logPrefs = new LoggingPreferences();
            //logPrefs.enable(LogType.BROWSER, Level.ALL);
            //options.setCapability(CapabilityType.LOGGING_PREFS, logPrefs);


            //options.AddArguments("start-maximized");
            //ReadOnlyDesiredCapabilities capabilities = new ReadOnlyDesiredCapabilities();

            //capabilities.etCapability(ChromeOptions.CAPABILITY, options);
            this.Chrome = new ChromeDriver(options);
        }

        public void Login(string login, string password, string Pesel)
        {
            this.Chrome.Url = Addresses.LoginPage;
            var loginControl = this.Chrome.FindElement(By.Id("Millekod_txtContent"));
            loginControl.SendKeys(login);

            var loginButton = this.Chrome.FindElement(By.Id("BtnLogin"));
            loginButton.Click();
            Thread.Sleep(2000);
            var passwordControl = this.Chrome.FindElement(By.Id("PasswordOne_txtContent"));
            passwordControl.SendKeys(password);

            bool[] peselInputs = new bool[11];
            Func<string, bool> peselEnabled = (id) =>
               {
                   var peselControl = this.Chrome.FindElement(By.Id(id));
                   var disabled = peselControl.GetAttribute("disabled");
                   return !(disabled == "true");
               };
            ;


            //peselInputs[0] = peselDisabled("PESEL_0_txtContent");
            //peselInputs[1] = peselDisabled("PESEL_1_txtContent");
            //peselInputs[2] = peselDisabled("PESEL_2_txtContent");
            //peselInputs[3] = peselDisabled("PESEL_3_txtContent");
            //peselInputs[4] = peselDisabled("PESEL_4_txtContent");
            //peselInputs[5] = peselDisabled("PESEL_5_txtContent");
            //peselInputs[6] = peselDisabled("PESEL_6_txtContent");
            //peselInputs[7] = peselDisabled("PESEL_7_txtContent");
            //peselInputs[8] = peselDisabled("PESEL_8_txtContent");
            //peselInputs[9] = peselDisabled("PESEL_9_txtContent");
            //peselInputs[10] = peselDisabled("PESEL_10_txtContent");

            for (int i = 0; i < peselInputs.Length; i++)
            {
                string id = $"PESEL_{i}_txtContent";
                if (peselEnabled(id))
                {
                    var peselControl = this.Chrome.FindElement(By.Id(id));
                    peselControl.SendKeys(Pesel[i].ToString());
                }
            }

            this.Chrome.FindElement(By.Id("BtnLogin")).Click();

            this.Chrome.Url = "https://www.bankmillennium.pl/osobiste2/Accounts/CurrentAccountDetails/Details";
        }

        public void GetData()
        {
            var history = this.Chrome.FindElement(By.LinkText("Historia"));
            history.Click();
            Thread.Sleep(2000);
            var dataRows=this.Chrome.FindElements(By.ClassName("ActionRow"));
            for (int i = 3; i < dataRows.Count; i++)
            {
                var type = dataRows[i].FindElement(By.TagName("a"));
                //var details = dataRows[i].FindElement(By.ClassName("MNCommand"));
                type.Click();
                Thread.Sleep(2000);
                var link=dataRows[i].FindElement(By.XPath("//*[text()='Typ operacji']"));
                var parent= link.FindElement(By.XPath("./../.."));
                var spans = parent.FindElements(By.TagName("span"));
                var value = spans[1];
                Console.WriteLine(value.GetAttribute("innerHTML"));
            }
            {
               
                
            }

        }

    }
}
