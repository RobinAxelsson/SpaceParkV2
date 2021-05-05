namespace SpacePark_API.Authentication
{
    public class AddSpacePortModel
    {
        public string Name { get; set; }
        public int ParkingSpots { get; set; }
        public int PriceMultiplier { get; set; }
    }
    public class DisableSpacePortModelId
    {
        public int SpacePortId { get; set; }
        public bool Enabled { get; set; }
    }
    public class DisableSpacePortModelString
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
    public class RemoveSpacePortModelId
    {
        public int SpacePortId { get; set; }
    }
    public class RemoveSpacePortModelString
    {
        public string Name { get; set; }
    }
    public class UpdateSpacePortPriceId
    {
        public int SpacePortId { get; set; }
        public double SpacePortMultiplier { get; set; }
    }
    public class UpdateSpacePortPriceString
    {
        public string Name { get; set; }
        public double SpacePortMultiplier { get; set; }
    }
    public class UpdateSpacePortNameId
    {
        public int SpacePortId { get; set; }
        public string NewSpacePortName { get; set; }
    }
    public class UpdateSpacePortNameString
    {
        public string OldSpacePortName { get; set; }
        public string NewSpacePortName { get; set; }
    }
}