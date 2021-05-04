using System.ComponentModel.DataAnnotations;

namespace SpacePark_API.Authentication
{
    public class ParkingModelId
    {
        [Required(ErrorMessage = "Minutes is required")]
        public double Minutes { get; set; }
        [Required(ErrorMessage = "Spaceport-name is required")]
        public int SpacePortId { get; set; }
    }  
    public class ParkingModelString
    {
        [Required(ErrorMessage = "Minutes is required")]
        public double Minutes { get; set; }
        [Required(ErrorMessage = "Spaceport-name is required")]
        public string SpacePortName { get; set; }
    }
}
