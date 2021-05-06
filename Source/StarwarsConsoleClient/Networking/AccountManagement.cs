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
        #region Instantiated Methods

        #region Overloads

        private bool _Exists(User inputUser)
        {
            var result = false;
            var thread = new Thread(() => { result = Execution_Exists(inputUser); });
            thread.Start();
            thread.Join(); //By doing join it will wait for the method to finish
            return result;
        }

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

        #endregion

        //Async method. Below will call upon a private corresponding method in another thread.
        private void _Register(User inputUser, SpaceShip inputShip, string accountName, string password)
        {
            //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
            //var outputAccount = new Account
            //{
            //    AccountName = accountName,
            //    Password = PasswordHashing.HashPassword(password),
            //    User = inputUser,
            //    SpaceShip = inputShip
            //};
            //inputUser.Homeplanet = dbHandler.Homeworlds.FirstOrDefault(g => g.Name == inputUser.Homeplanet.Name) ??
            //                       inputUser.Homeplanet;
            //dbHandler.Accounts.Add(outputAccount);
            //dbHandler.SaveChanges();
        }

        private List<Receipt> _GetAccountReceipts(Account account)
        {
            //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
            //return dbHandler.Receipts.Include(a => a.Account)
            //    .Where(receipt => receipt.Account.AccountName == account.AccountName).ToList();
            return new List<Receipt>();
        }

        //Async method. Below will call upon a private corresponding method in another thread.
        private bool _Exists(string name, bool isAccountName)
        {
            var result = false;
            var thread = new Thread(() => { result = Execution_Exists(name, isAccountName); });
            thread.Start();
            thread.Join(); //By doing join it will wait for the method to finish
            return result;
        }

        //Execution method that will do the "work"
        private bool Execution_Exists(string name, bool isAccountName)
        {
            //if (isAccountName)
            //{
            //    var result = false;
            //    var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
            //    foreach (var account in dbHandler.Accounts)
            //        if (account.AccountName == name)
            //            result = true;

            //    return result;
            //}
            //else
            //{
            //    var result = false;
            //    var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
            //    foreach (var account in dbHandler.Users)
            //        if (account.Name == name)
            //            result = true;

            //    return result;
            //}
            return false;
        }

        //private Account _ValidateLogin(string accountName, string passwordInput)
        //{
        //    var account = new Account();
        //    var thread = new Thread(() =>
        //    {
        //        account = Execution_ValidateLogin(accountName, PasswordHashing.HashPassword(passwordInput));
        //    });
        //    thread.Start();
        //    thread.Join(); //By doing join it will wait for the method to finish
        //    return account;
        //}

        //private Account Execution_ValidateLogin(string accountName, string passwordInput)
        //{
        //    //Account accountHolder = null;
        //    //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
        //    //foreach (var account in dbHandler.Accounts.Include(a => a.User).Include(a => a.SpaceShip)
        //    //    .Include(h => h.User.Homeplanet))
        //    //    if (account.AccountName == accountName && account.Password == passwordInput)
        //    //        accountHolder = account;
        //    //return accountHolder;
        //    return null;
        //}

        #endregion

        #region Static Methods

        #region Private Methods

        private static (string question, string answer) GetSecurityQuestion(User inputUser)
        {
            var question = string.Empty;
            var answer = string.Empty;
            var r = new Random();
            var x = r.Next(1, 4);
            switch (x)
            {
                case 1:
                    question = "What is your hair color?";
                    answer = inputUser.HairColor;
                    break;
                case 2:
                    question = "What is your skin color?";
                    answer = inputUser.SkinColor;
                    break;
                case 3:
                    question = "What is your eye color?";
                    answer = inputUser.EyeColor;
                    break;
                case 4:
                    question = "What is your birth year?";
                    answer = inputUser.BirthYear;
                    break;
            }

            return (question, answer);
        }

        #endregion

        #region Public Methods & IEnumerables

        //These methods instantiate AccountManagement and call upon the private non-static methods.
        public static IEnumerable<Receipt> GetAccountReceipts(Account account)
        {
            var am = new AccountManagement();
            return am._GetAccountReceipts(account);
        }
        //main method
        public static async Task<Account> ValidateLoginAsync(string accountName, string passwordInput)
        {
            var Client = new HttpClient();

            var json = JsonConvert.SerializeObject(new
            {
                AccountName = accountName,
                Password = passwordInput
            });
            string token = null;
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            Account account = null;

            try
            {
                var response = await Client.PostAsync("/api/Account/Login", data).ConfigureAwait(continueOnCapturedContext: false);

                var loginResponseText = await response.Content.ReadAsStringAsync();
                var loginResponseValuePairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(loginResponseText);

                token = loginResponseValuePairs.GetValueOrDefault("token");
                account.AccountName = accountName;
                account.Password = token;
            }
            catch { account = null; }

            return account;
        }

        public static void Register(User inputUser, SpaceShip inputShip, string accountName, string password)
        {
            var am = new AccountManagement();
            am._Register(inputUser, inputShip, accountName, password);
        }

        public static void ReRegisterShip(Account account, SpaceShip ship)
        {
            //ship.SpaceShipID = account.SpaceShip.SpaceShipID;
            //account.SpaceShip = ship;

            //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
            //dbHandler.SpaceShips.Update(ship);
            //dbHandler.SaveChanges();
        }

        public static User IdentifyWithQuestion(string username, Func<string, string> getSecurityAnswer)
        {
            var inputUser = SwapiCollector.ParseUserAsync(username);
            if (inputUser == null) return null;

            var (question, answer) = GetSecurityQuestion(inputUser);
            var inputAnswer = getSecurityAnswer(question);
            if (inputAnswer.ToLower() == answer.ToLower()) return inputUser;
            return null;
        }

        public static bool Exists(string name, bool isAccountName)
        {
            var am = new AccountManagement();
            return am._Exists(name, isAccountName);
        }

        #region Overloads
        public static bool Exists(User user)
        {
            var am = new AccountManagement();
            return am._Exists(user);
        }
        #endregion


        #endregion

        #endregion
        private static class PasswordHashing
        {
            private const string Salt = "78378265240709988066";

            //Salting password to enhance security by adding data to the password before the SHA256 conversion.
            //This makes it more difficult(impossible) to datamine.
            public static string HashPassword(string input)
            {
                var hashedData = ComputeSha256Hash(input + Salt);
                return hashedData;
            }

            private static string ComputeSha256Hash(string rawData)
            {
                using var sha256Hash = SHA256.Create();
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData + Salt));
                var builder = new StringBuilder();
                foreach (var t in bytes)
                    builder.Append(t.ToString("x2"));
                return builder.ToString();
            }
        }


    }
}
