using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Bank.Millenium.Objects
{
    public class AccountSnapshot
    {
        public string Bank { get; set; }
        public string AccountName { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string Saldo { get; set; }
    }
}
