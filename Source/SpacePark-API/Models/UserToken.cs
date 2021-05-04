using System;

namespace SpacePark_API.Models
{
    public record UserToken
    {
        public Account Account { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}