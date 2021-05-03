using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark_API.Models
{
    public partial record SpacePort
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParkingSpots { get; set; }

        public void Park(SpaceShip ship, int minutes)
        {
            //Skriv ett receipt och aktivera parkering, som i förra projektet.
        }

        public static void GetActiveParkings(int spacePortId)
        {
            //Ta in ett spaceport ID, hämta parkeringar med LINQ som matchar det inmatade ID:t
        }
    }


}
