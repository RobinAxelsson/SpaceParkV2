using Newtonsoft.Json;

namespace SpacePark_ModelsDB.Models
{
    public partial class Person
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; } //Doing string because some users has "unknown" value of mass

        [JsonProperty("hair_color")] public string HairColor { get; set; }

        [JsonProperty("skin_color")] public string SkinColor { get; set; }

        [JsonProperty("eye_color")] public string EyeColor { get; set; }

        [JsonProperty("birth_year")] public string BirthYear { get; set; }

        public string Gender { get; set; }
        public string Url { get; set; }
        public Homeworld Homeplanet { get; set; }
    }
}