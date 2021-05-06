namespace SpacePark_API.Authentication
{
    public class AddSpacePortModel
    {
        public string Name { get; set; }
        public int ParkingSpots { get; set; }
        public int PriceMultiplier { get; set; }
    }
    public class ChangeSpacePortAvailability
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
  
    public class RemoveSpacePortModel
    {
        public string Name { get; set; }
    }

    public class UpdateSpacePortPrice
    {
        public string Name { get; set; }
        public double SpacePortMultiplier { get; set; }
    }
   
    public class UpdateSpacePortName
    {
        public string OldSpacePortName { get; set; }
        public string NewSpacePortName { get; set; }
    }
    public class PromoteAdminModel
    {
        public string AccountName { get; set; }
    }
    public class DemoteAdminModel
    {
        public string AccountName { get; set; }
    }
}