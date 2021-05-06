namespace SpacePark_API.Authentication
{
    public class AddSpacePortModel
    {
        public string Name { get; set; }
        public int ParkingSpots { get; set; }
        public int PriceMultiplier { get; set; }
    }
    public class ChangeSpacePortAvailabilityId
    {
        public string NameOrId { get; set; }
        public bool Enabled { get; set; }
    }
  
    public class RemoveSpacePortModelId
    {
        public string NameOrId { get; set; }
    }

    public class UpdateSpacePortPriceId
    {
        public string NameOrId { get; set; }
        public double SpacePortMultiplier { get; set; }
    }
   
    public class UpdateSpacePortNameId
    {
        public string NameOrId { get; set; }
        public string NewSpacePortName { get; set; }
    }
   
}