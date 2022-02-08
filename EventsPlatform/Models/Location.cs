using System.ComponentModel.DataAnnotations.Schema;

namespace EventsPlatform.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Town { get; set; }
        public string? Street { get; set; }
        public string? OtherDetails { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
    }
}
