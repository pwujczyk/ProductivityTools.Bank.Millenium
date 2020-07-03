using Microsoft.EntityFrameworkCore;
using ProductivityTools.Bank.Millenium.Database;
using ProductivityTools.Bank.Millenium.Objects;
using ProductivityTools.DateTimeTools;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace ProductivityTools.Bank.Millenium.Commands
{
    public class BMCommands
    {
        MilleniumContext Context;
        IDateTimePT currentdate;

        public BMCommands (MilleniumContext context, IDateTimePT date)
        {
            this.Context = context;
            this.currentdate = date;
        }

        public void SaveBasicData(BasicData basicData)
        {
            basicData.Date = currentdate.Now;
            var currentRecord=this.Context.BasicData.AsNoTracking().FirstOrDefault(x => x.Date.Date == this.currentdate.Now.Date);
            if (currentRecord==null)
            {
                this.Context.BasicData.Add(basicData);
            }
            else
            {
                basicData.BasicDataId = currentRecord.BasicDataId;
                this.Context.Entry(basicData).CurrentValues.SetValues(basicData);
                this.Context.Attach(basicData);
            }
            this.Context.SaveChanges();
        }
    }
}
