using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarwarsConsoleClient.Main
{
    public class AccountManagement
    {
        private AccountManagement()
        {
            //if (ConnectionString == null)
            //    throw new Exception("The static property ConnectionString has not been assigned.",
            //        new Exception(
            //            "Please assign a value to the static property ConnectionString before calling any methods"));
        } //Instantiation. This will help us throw if ConnectionString is null.

        private List<Receipt> GetAccountReceipts(string accountName)
        {
            //var receiptList = new List<Receipt>();
            //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
            //foreach (var receipt in dbHandler.Receipts)
            //    if (receipt.Account.AccountName == accountName)
            //        receiptList.Add(receipt);
            //return receiptList;
            return new List<Receipt>();
        }

        private List<Receipt> GetAccountReceipts(int accountId)
        {
            //var receiptList = new List<Receipt>();
            //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
            //foreach (var receipt in dbHandler.Receipts)
            //    if (receipt.Account.AccountID == accountId)
            //        receiptList.Add(receipt);
            //return receiptList;
            return new List<Receipt>();
        }

        private bool Execution_Exists(User inputUser)
        {
            //var result = false;
            //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
            //foreach (var user in dbHandler.Users)
            //    if (user.Name == inputUser.Name)
            //        result = true;

            //return result;
            return false;
        }
        private List<Receipt> _GetAccountReceipts(Account account)
        {
            //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
            //return dbHandler.Receipts.Include(a => a.Account)
            //    .Where(receipt => receipt.Account.AccountName == account.AccountName).ToList();
            return new List<Receipt>();
        }


        //These methods instantiate AccountManagement and call upon the private non-static methods.
        public static IEnumerable<Receipt> GetAccountReceipts(Account account)
        {
            var am = new AccountManagement();
            return am._GetAccountReceipts(account);
        }
    }
}