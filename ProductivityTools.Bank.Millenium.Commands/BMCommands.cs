using ProductivityTools.Bank.Millenium.Database;
using ProductivityTools.Bank.Millenium.Objects;
using System;

namespace ProductivityTools.Bank.Millenium.Commands
{
    public class BMCommands
    {
        MilleniumContext Context;

        public BMCommands (MilleniumContext context)
        {
            this.Context = context;
        }

        public void SaveBasicData(BasicData basicData)
        {
            this.Context.BasicData.Add(basicData);
            this.Context.SaveChanges();
        }
    }
}
