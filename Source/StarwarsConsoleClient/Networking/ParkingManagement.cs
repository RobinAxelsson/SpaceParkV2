using System;
using System.Threading;

namespace StarwarsConsoleClient.Main
{
        public class ParkingManagement
        {

            #region Static Methods

            //These methods instantiate ParkingManagement and call upon the private non-static methods.
            public static (bool isOpen, DateTime nextAvailable) CheckParkingStatus()
            {
                var pm = new ParkingManagement();
                return pm._CheckParkingStatus();
            }

            public static decimal CalculatePrice(SpaceShip ship, double minutes, double priceMultiplier)
            {
                var pm = new ParkingManagement();
                return pm._CalculatePrice(ship, minutes, priceMultiplier);
            }

            public static Receipt SendInvoice(Account account, double minutes)
            {
                var pm = new ParkingManagement();
                return pm._SendInvoice(account, minutes);
            }

            #endregion

            #region Instantiated Methods

            private (bool isOpen, DateTime nextAvailable) _CheckParkingStatus()
            {
                //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                //var ongoingParkings = new List<Receipt>();
                //foreach (var receipts in dbHandler.Receipts)
                //    if (DateTime.Parse(receipts.EndTime) > DateTime.Now)
                //        ongoingParkings.Add(receipts);
                //var nextAvailable = DateTime.Now;
                //var isOpen = false;
                //if (ongoingParkings.Count >= ParkingSlots)
                //{
                //    //Setting nextAvailable 10 years ahead so the loop will always start running.
                //    nextAvailable = DateTime.Now.AddYears(10);
                //    var cachedNow = DateTime.Now;
                //    //Caching DateTimeNow in case loops takes longer than expected, to ensure that time moving forward doesn't break the loop.
                //    foreach (var receipt in ongoingParkings)
                //    {
                //        var endTime = DateTime.Parse(receipt.EndTime);
                //        if (endTime > cachedNow && endTime < nextAvailable) nextAvailable = endTime;
                //    }
                //}
                //else
                //{
                //    isOpen = true;
                //}
                bool isOpen = true;

                return (isOpen, DateTime.Now);
            }

            private decimal _CalculatePrice(SpaceShip ship, double minutes, double priceMultiplier)
            {
                var price = double.Parse(ship.ShipLength.Replace(".", ",")) * minutes / priceMultiplier;
                return (decimal)price;
            }

            private Receipt _SendInvoice(Account account, double minutes)
            {
                var receipt = new Receipt();
                var thread = new Thread(() => { receipt = Execution_SendInvoice(account, minutes); });
                thread.Start();
                thread.Join(); //By doing join it will wait for the method to finish
                return receipt;
            }

            private Receipt Execution_SendInvoice(Account account, double minutes)
            {
                //var price = _CalculatePrice(account.SpaceShip, minutes);
                //var endTime = DateTime.Now.AddMinutes(minutes);
                //var receipt = new Receipt
                //{
                //    Account = account,
                //    Price = price,
                //    StartTime = DateTime.Now.ToString("g"),
                //    EndTime = endTime.ToString("g")
                //};
                //if (ConnectionString == null)
                //    throw new Exception("The static property ConnectionString has not been assigned.",
                //        new Exception(
                //            "Please assign a value to the static property ConnectionString before calling any methods"));
                //var dbHandler = new StarWarsContext { ConnectionString = ConnectionString };
                //dbHandler.Receipts.Update(receipt);
                //dbHandler.SaveChanges(); //TODO
                //return receipt;
                return new Receipt();
            }

            #endregion
        }
    }
