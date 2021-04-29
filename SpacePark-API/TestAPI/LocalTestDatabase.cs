using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SpacePark_API.Models;
using SpacePark_ModelsDB.Networking;

namespace SpaceParkTests
{
    public static class LocalTestDatabase
    {
        public static double _priceMultiplier = 10;
        public static ObservableCollection<Person> Persons = new ObservableCollection<Person>()
        {
            APICollector.ParseUser("c3po"),
            APICollector.ParseUser("lukeskywalker"),
            APICollector.ParseUser("bobafett"),
            APICollector.ParseUser("darthvader")
        };

        static LocalTestDatabase()
        {
            for (var i = 0; i <= Persons.Count - 1; i++)
            {
                Persons[i].Id = i;
            }
            Persons.CollectionChanged += ListChanged;
            //Fix userID's in list(They'll always
        }
        private static void ListChanged(object sender, NotifyCollectionChangedEventArgs args) {
            for (var i = 0; i <= Persons.Count - 1; i++)
            {
                Persons[i].Id = i;
            }
        }
    }
}