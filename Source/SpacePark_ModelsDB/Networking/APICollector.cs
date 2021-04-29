using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpacePark_ModelsDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace SpacePark_ModelsDB.Networking
{
    public static class APICollector
    {
        private static Exception ParseFailedIncorrectUrl(string url)
        {
            throw new Exception($"Parse was empty; Is the URL in correct format? Input: {url}");
        }

        #region Public Methods

        #region Overloads

        public static Person ParsePerson(Uri url)
        {
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = url;
                httpclient.DefaultRequestHeaders.Add("Person-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(url).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }

            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var parsedPerson = JsonConvert.DeserializeObject<Person>(jsonResult);
                var resultobject = JsonConvert.DeserializeObject<JObject>(jsonResult);
                foreach (var property in resultobject.Properties())
                    if (property.Name == "homeworld")
                        parsedPerson.Homeplanet = ReturnHomeplanet(property.Value.ToString());
                return parsedPerson;
            }

            throw new Exception("Parse was empty; Is the URL in correct format?");
        }

        public static Person ParsePersonAsync(Uri url) //This contains a threadstart for the private corresponding method
        {
            var person = new Person();
            var thread = new Thread(() => { person = ParsePerson(url); });
            thread.Start();
            thread.Join(); //By doing join it will wait for the method to finish
            return person;
        }

        public static SpaceShip ParseShipAsync(string model) //This contains a threadstart for the private corresponding method
        {
            var spaceShip = new SpaceShip();
            var thread = new Thread(() => { spaceShip = ParseShip(model); });
            thread.Start();
            thread.Join(); //By doing join it will wait for the method to finish
            return spaceShip;
        }

        public static SpaceShip ParseShip(Uri url)
        {
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = url;
                httpclient.DefaultRequestHeaders.Add("Person-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(url).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }

            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var parsedShip = JsonConvert.DeserializeObject<SpaceShip>(jsonResult);
                var resultobject = JsonConvert.DeserializeObject<JObject>(jsonResult);
                foreach (var property in resultobject.Properties())
                    if (property.Name == "length")
                        parsedShip.ShipLength = property.Value.ToString().Replace(",", "").Replace(".", "");
                return parsedShip;
            }

            throw new Exception("Parse was empty; Is the URL in correct format?");
        }

        #endregion

        public static Person ParsePerson(string name)
        {
            var foundPerson = false;
            var person = new Person();
            for (var i = 1; i <= 9; i++)
                if (!foundPerson)
                {
                    var tempPersons =
                        ReturnPersonsFromList("https://swapi.dev/api/people/?page=" + i);
                    foreach (var s in tempPersons)
                    {
                        if (s.Name.ToLower() != name.ToLower()) continue;
                        foundPerson = true;
                        person = s;
                        break;
                    }
                }
                else
                {
                    break;
                }

            if (foundPerson)
                return person;
            else return null;
        }

        public static Person ParsePersonAsync(string name) //This contains a threadstart for the private corresponding method
        {
            var p = new Person();
            var thread = new Thread(() => { p = ParsePerson(name); });
            thread.Start();
            thread.Join(); //By doing join it will wait for the method to finish
            return p;
        }

        public static SpaceShip ParseShip(string model)
        {
            var foundShip = false; //A check to verify a ship has been found by name
            var
                ship = new SpaceShip(); //Had to initialize because code didn't realize it was bound to the foundShip bool and i got build errors.
            for (var i = 1; i <= 4; i++)
            {
                var tempShips =
                    ReturnSpaceShipsFromList("https://swapi.dev/api/starships/?page=" + i);
                foreach (var s in tempShips)
                {
                    if (s.Model.ToLower() != model.ToLower()) continue;
                    foundShip = true;
                    ship = s;
                }
            }

            if (!foundShip)
                throw new Exception("Could not find a ship based on " + model + ". Did you enter the name correctly?");
            return ship;
        }

        public static SpaceShip ParseShipAsync(Uri url) //This contains a threadstart for the private corresponding method
        {
            var spaceShip = new SpaceShip();
            var thread = new Thread(() => { spaceShip = ParseShip(url); });
            thread.Start();
            thread.Join(); //By doing join it will wait for the method to finish
            return spaceShip;
        }

        public static SpaceShip[] ReturnShips()
        {
            var spaceShips = new List<SpaceShip>();
            for (var i = 1; i <= 4; i++)
            {
                var tempShips =
                    ReturnSpaceShipsFromList("https://swapi.dev/api/starships/?page=" + i);
                spaceShips.AddRange(tempShips);
            }

            return spaceShips.ToArray();
        }

        public static SpaceShip[] ReturnShipsAsync() //This contains a threadstart for the private corresponding method
        {
            var spaceShips = Array.Empty<SpaceShip>();
            var thread = new Thread(() => { spaceShips = ReturnShips(); });
            thread.Start();
            thread.Join(); //By doing join it will wait for the method to finish
            return spaceShips;
        }

        #endregion

        #region Private Methods & IEnumerables

        private static Homeworld ReturnHomeplanet(string url)
        {
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(url);
                httpclient.DefaultRequestHeaders.Add("Person-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(url).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }

            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var homeworld = JsonConvert.DeserializeObject<Homeworld>(jsonResult);
                return homeworld;
            }

            throw ParseFailedIncorrectUrl(url);
        }

        private static IEnumerable<SpaceShip> ReturnSpaceShipsFromList(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url));
            var spaceShips = new List<SpaceShip>();
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(url);
                httpclient.DefaultRequestHeaders.Add("Person-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(url).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }

            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var resultObjects = AllChildren(JObject.Parse(jsonResult))
                    .First(c => c.Type == JTokenType.Array && c.Path.Contains("results"))
                    .Children<JObject>();
                foreach (var result in resultObjects)
                {
                    var spaceShip = result.ToObject<SpaceShip>();
                    foreach (var property in result.Properties())
                        if (property.Name == "length")
                            try
                            {
                                spaceShip.ShipLength = property.Value.ToString().Replace(",", "");
                            }
                            catch
                            {
                                throw new Exception($"Failed to parse length.Input value: {property.Value}");
                            }

                    spaceShips.Add(spaceShip);
                }
            }
            else
            {
                throw ParseFailedIncorrectUrl(url);
            }

            return spaceShips.ToArray();
        }

        private static IEnumerable<Person> ReturnPersonsFromList(string url)
        {
            var persons = new List<Person>();
            var jsonResult = "";
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri(url);
                httpclient.DefaultRequestHeaders.Add("Person-Agent", "Anything");
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpresponse = httpclient.GetAsync(url).Result;
                httpresponse.EnsureSuccessStatusCode();
                jsonResult = httpresponse.Content.ReadAsStringAsync().Result;
            }

            if (!string.IsNullOrWhiteSpace(jsonResult))
            {
                var resultObjects = AllChildren(JObject.Parse(jsonResult))
                    .First(c => c.Type == JTokenType.Array && c.Path.Contains("results"))
                    .Children<JObject>();
                foreach (var result in resultObjects)
                {
                    var person = result.ToObject<Person>();
                    foreach (var property in result.Properties())
                        if (property.Name == "homeworld")
                            person.Homeplanet = ReturnHomeplanet(property.Value.ToString());
                    persons.Add(person);
                }
            }
            else
            {
                throw ParseFailedIncorrectUrl(url);
            }

            return persons.ToArray();
        }

        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield
                    return c;
                foreach (var cc in AllChildren(c))
                    yield
                        return cc;
            }
        }

        #endregion
    }
}