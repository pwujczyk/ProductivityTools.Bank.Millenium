using System;

namespace ProductivityTools.Bank.Millenium.Objects
{
    public class Transaction
    {
        public Transaction(string augmentTransactionId)
        {
            this.AugmentTransactionId = augmentTransactionId.Trim();
        }

        public int TransactionId { get; set; }
        public string AugmentTransactionId {get;set;}

        public string Bank { get; set; }
        public string AccountName { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        public string SourceAccount { get; set; }
        public string DestAccount { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }

        public string SourceBank { get; set; }
        public string Sender { get; set; }
        public string Receipment { get; set; }

        //blik
        public string TransactionPlace { get; set; }
        //

        //karta
        public string CardNumber{ get; set; }
        public string CardOwner { get; set; }

        public string Description { get; set; }

    }
}
