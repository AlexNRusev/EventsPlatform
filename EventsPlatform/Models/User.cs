using Microsoft.AspNetCore.Identity;

namespace EventsPlatform.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBanned { get; set; }
        public virtual ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
        public virtual ICollection<Event> AppliedEvents { get; set; } = new List<Event>();
    }
}
