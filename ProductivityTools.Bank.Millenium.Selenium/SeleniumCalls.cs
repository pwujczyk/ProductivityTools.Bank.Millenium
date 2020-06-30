using OpenQA.Selenium;
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

        private IWebElement GetElementByInnerText(IWebElement parent, string tag, string text)
        {
            var tags = parent.FindElements(By.TagName(tag));
            foreach (var item in tags)
            {
                if (item.GetAttribute("innerHTML") == text)
                {
                    return item;
                }
            }
            throw new Exception($"No element with text {text}");
        }

        public string GetItem(IWebElement datarow, string name)
        {
          //  Console.WriteLine("================================");
            Console.WriteLine(datarow.GetAttribute("innerHTML"));
           // Console.WriteLine("================================");

            var link = GetElementByInnerText(datarow, "span", "Kwota transakcji");
           

          //  Console.WriteLine("==================LINK==============");
            Console.WriteLine(link.GetAttribute("innerHTML"));
           // Console.WriteLine("=================ENDLINK===============");
            if (link != null)
            {
                var parent1 = link.FindElement(By.XPath("./.."));
               // Console.WriteLine("==================parent1==============");
                Console.WriteLine(parent1.GetAttribute("innerHTML"));
               //Console.WriteLine("=================ENDpARENT===============");
                var parent2 = parent1.FindElement(By.XPath("./.."));
              //  Console.WriteLine("==================parent2==============");
                Console.WriteLine(parent2.GetAttribute("innerHTML"));
               // Console.WriteLine("=================ENDpARENT===============");

               


                var spans = parent2.FindElements(By.TagName("span"));
                var value = spans[1];
            //    Console.WriteLine("==================value==============");
                Console.WriteLine(value.GetAttribute("innerHTML"));
             //   Console.WriteLine("=================ENDvalue===============");
                var r = value.GetAttribute("innerHTML");
                return r;
            }
            return string.Empty;
        }

        public void GetData()
        {
            var history = this.Chrome.FindElement(By.LinkText("Historia"));
            history.Click();
            Thread.Sleep(2000);
            var tabledata = this.Chrome.FindElement(By.ClassName("Table"));
            var dataRows = tabledata.FindElements(By.ClassName("ActionRow"));
            foreach (var item in dataRows)
            {

                //}
                //for (int i = 3; i < dataRows.Count; i++)
                //{

                var type = item.FindElement(By.TagName("a"));
                Console.WriteLine(item.GetAttribute("innerHTML"));
                //var details = dataRows[i].FindElement(By.ClassName("MNCommand"));
                type.Click();

                Thread.Sleep(2000);

                var detailsRow = item.FindElement(By.XPath($"./following::tr[1]"));
                Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                Console.WriteLine(detailsRow.GetAttribute("innerHTML"));
                Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                Console.WriteLine(GetItem(detailsRow, "Typ operacji"));
                // Console.WriteLine(GetItem(detailsRow, "gowania"));
                //Console.WriteLine(GetItem(detailsRow, "Z rachunku"));
                //Console.WriteLine(GetItem(detailsRow, "Kwota transakcji"));
                //Console.WriteLine(GetItem(detailsRow, "Tytuł"));
                //Console.WriteLine(GetItem(detailsRow, "Na rachunek"));
                //Console.WriteLine(GetItem(detailsRow, "Odbiorca"));
            }
        }

    }
}
