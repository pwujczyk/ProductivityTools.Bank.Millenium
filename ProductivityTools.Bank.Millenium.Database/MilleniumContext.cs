using Microsoft.EntityFrameworkCore;
using o=ProductivityTools.Bank.Millenium.Objects;
using System;
using System.Transactions;

namespace ProductivityTools.Bank.Millenium.Database
{
    public class MilleniumContext
    {
        DbSet<o.Transaction> Transactions { get; set; }
        DbSet<o.AccountSnapshot> AccountSnapshot { get; set; }
    }
}
