using EventsPlatform.Models.Enums;

namespace EventsPlatform.Dto
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public int MaxNumberOfParticipants { get; set; }
        public EventType EventType { get; set; }
        public string Details { get; set; }
        public LocationDTO Location { get; set; }
    }
}
