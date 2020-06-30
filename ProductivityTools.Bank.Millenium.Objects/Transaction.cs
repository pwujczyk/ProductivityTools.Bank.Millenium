using System;

namespace ProductivityTools.Bank.Millenium.Objects
{
    public class Transaction
    {
        public string Bank { get; set; }
        public string AccountName { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        public string SourceAccount { get; set; }
        public string DestAccount { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }

    }
}
