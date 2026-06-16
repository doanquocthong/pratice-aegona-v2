namespace pratice_aegona_v2.Models.Entities
{
    public class Roles  
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // Admin, User

        public ICollection<AppUser> Users { get; set; }
    }
}
