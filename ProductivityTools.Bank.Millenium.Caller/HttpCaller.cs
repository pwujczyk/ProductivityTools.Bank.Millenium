using ProductivityTools.BankAccounts.Contract;
using ProductivityTools.SimpleHttpPostClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductivityTools.Bank.Millenium.Caller
{
    public class HttpCaller
    {
        private readonly HttpPostClient client;

        public HttpCaller()
        {
            client = new HttpPostClient();
            client.SetBaseUrl("https://localhost:44306");
        }

        public async Task SaveBasicData(BasicData basicData)
        {
            var result1 = await client.PostAsync<object>(EndpointNames.AccountControllerName, EndpointNames.BasicDataMethodName, basicData);
        }

        public async Task SaveTransactions(List<Transaction> transactions)
        {
            var result1 = await client.PostAsync<object>(EndpointNames.AccountControllerName, EndpointNames.SaveTransactionsMethodName, transactions);
        }
    }
}
