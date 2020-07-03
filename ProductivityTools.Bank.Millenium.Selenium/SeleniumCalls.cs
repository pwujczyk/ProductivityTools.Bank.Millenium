using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using ProductivityTools.Bank.Millenium.Objects;
using System;
using System.Data;
using System.Linq;
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

        private bool FillMillekode(string login)
        {
            var loginControl = this.Chrome.FindElement(By.Id("Millekod_txtContent"));
            foreach (var item in login)
            {
                Thread.Sleep(100);
                loginControl.SendKeys(item.ToString());
            }

            var sessionExpired = this.Chrome.FindElements(By.Id("left-side-modal-panel_Content"));
            if (sessionExpired.Count>0 && sessionExpired[0].Displayed)
            {
                sessionExpired[0].Click();
            }

            var loginButton = this.Chrome.FindElement(By.Id("BtnLogin"));
            loginButton.Click();
            var passwordControl = this.Chrome.FindElements(By.Id("PasswordOne_txtContent"));
            if (passwordControl.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FillPassword(string password)
        {
            var passwordControl = this.Chrome.FindElement(By.Id("PasswordOne_txtContent"));
            passwordControl.SendKeys(password);

        }

        private void FillPESEL(string Pesel)
        {
            bool[] peselInputs = new bool[11];
            Func<string, bool> peselEnabled = (id) =>
            {
                var peselControl = this.Chrome.FindElement(By.Id(id));
                var disabled = peselControl.GetAttribute("disabled");
                return !(disabled == "true");
            };


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
        }

        public void Login(string login, string password, string Pesel)
        {
            this.Chrome.Url = Addresses.LoginPage;
            Thread.Sleep(2000);
            while (FillMillekode(login)==false)
            {
                Thread.Sleep(1000);
            }
            FillPassword(password);
            Thread.Sleep(2000);
            FillPESEL(Pesel);
            this.Chrome.Url = "https://www.bankmillennium.pl/osobiste2/Accounts/CurrentAccountDetails/Details";
        }

        private IWebElement GetElementByInnerText(IWebElement parent, string tag, string text)
        {
            var tags = parent.FindElements(By.TagName(tag));
            foreach (var item in tags)
            {
                var x = item.GetAttribute("innerHTML");
                Console.WriteLine(x);
                if (x == text)
                {
                    return item;
                }
            }
            return null;
        }

        private void FillItem(Transaction transaction, IWebElement datarow, string name, Action<Transaction, string> setter)
        {
            string value = GetItem(datarow, name);
            setter(transaction, value);
        }



        private string GetItem(IWebElement datarow, string name, string tagname = "span", int depth = 1)
        {
            var link = GetElementByInnerText(datarow, "span", name);
            if (link != null)
            {
                var parent2 = link.FindElement(By.XPath("./../.."));
                var spans = parent2.FindElements(By.TagName(tagname));
                var value = spans[depth];
                var r = value.GetAttribute("innerHTML");
                return r;
            }
            return string.Empty;
        }

        public BasicData GetBasicData()
        {
            var result = new BasicData();

            Func<string, decimal> parse = (s) =>
               {
                   string s1 = new string(s.Where(c => char.IsDigit(c) || c==',').ToArray());
                   var r = Decimal.Parse(s1.Replace('.',','));
                   return r;
               };

            var header = this.Chrome.FindElement(By.Id("AccountDetailsHeaderPartial"));
            result.Saldo = parse(GetItem(header, "Saldo bieżące:", "span", 2));
            result.AvailiableFunds = parse(GetItem(header, "Dostępne środki:", "span", 2));
            result.BlockedFunds = parse(GetItem(header, "Zablokowane środki:", "a", 0));
            result.IncomingTransfers = parse(GetItem(header, "Transakcje przychodzące:", "span", 2));
            return result;
        }

        public void GetTransactions()
        {
            var history = this.Chrome.FindElement(By.LinkText("Historia"));
            history.Click();
            Thread.Sleep(2000);
            var tabledata = this.Chrome.FindElement(By.ClassName("Table"));
            var dataRows = tabledata.FindElements(By.ClassName("ActionRow"));
            foreach (var item in dataRows)
            {
                var augmentTransaction = item.FindElement(By.Id("AugmentTransactionId"));
                var augmentTransactionId = augmentTransaction.GetAttribute("innerHTML");

                var type = item.FindElement(By.TagName("a"));
                type.Click();

                Thread.Sleep(2000);
                var detailsRow = item.FindElement(By.XPath($"./following::tr[1]"));

                Transaction t = new Transaction(augmentTransactionId);

                FillItem(t, detailsRow, "Typ operacji", (t, s) => { t.Type = s; });
                FillItem(t, detailsRow, "Data księgowania", (t, s) => { t.Date = DateTime.Parse(s); });
                FillItem(t, detailsRow, "Z rachunku", (t, s) => { t.SourceAccount = s; });
                FillItem(t, detailsRow, "Kwota transakcji", (t, s) => { t.Value = Decimal.Parse(s.Replace("PLN", "")); });
                FillItem(t, detailsRow, "Tytuł", (t, s) => { t.Title = s; });
                FillItem(t, detailsRow, "Na rachunek", (t, s) => { t.DestAccount = s; });

                FillItem(t, detailsRow, "Bank zleceniodawcy", (t, s) => { t.SourceBank = s; });
                if (t.Type == "PŁATNOŚĆ BLIK W INTERNECIE")
                {
                    FillItem(t, detailsRow, "Płacący", (t, s) => { t.Sender = s; });
                }
                else
                {
                    FillItem(t, detailsRow, "Zleceniodawca", (t, s) => { t.Sender = s; });
                }
                FillItem(t, detailsRow, "Odbiorca", (t, s) => { t.Receipment = s; });
                FillItem(t, detailsRow, "Miejsce transakcji", (t, s) => { t.TransactionPlace = s; });

                FillItem(t, detailsRow, "Numer karty", (t, s) => { t.CardNumber = s; });
                FillItem(t, detailsRow, "Posiadacz karty", (t, s) => { t.CardOwner = s; });
            }
        }
    }
}
