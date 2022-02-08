using EventsPlatform.Models.Enums;

namespace EventsPlatform.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public int MaxNumberOfParticipants { get; set; }
        public EventStatus EventStatus { get; set; }
        public EventType EventType { get; set; }
        public string Details { get; set; }
        public virtual ICollection<User> RegisteredParticipants { get; set; } = new List<User>();
        public virtual ICollection<User> Organizers { get; set; } = new List<User>();
        public virtual Location Location { get; set; } = new Location();

    }
}
