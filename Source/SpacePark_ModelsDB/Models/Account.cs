namespace SpacePark_ModelsDB.Models
{
    public record Account
    {
        public int AccountID { get; set; }
        public Person Person { get; set; }
        public SpaceShip SpaceShip { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
    }
}