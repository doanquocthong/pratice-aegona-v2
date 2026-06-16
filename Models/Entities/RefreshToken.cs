namespace pratice_aegona_v2.Models.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public string? Token { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }

        public Guid UserId { get; set; }

        public AppUser? User { get; set; }
    }
}