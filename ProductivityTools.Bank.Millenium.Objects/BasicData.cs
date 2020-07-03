using System;
using System.Collections.Generic;
using System.Text;

namespace ProductivityTools.Bank.Millenium.Objects
{
    public class BasicData
    {
        public int BasicDataId { get; set; }
        public DateTime Date { get; set; }
        public decimal Saldo { get; set; }
        public decimal AvailiableFunds { get; set; }
        public decimal BlockedFunds { get; set; }
        public decimal IncomingTransfers { get; set; }
    }
}
