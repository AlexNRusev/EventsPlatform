using EventsPlatform.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EventsPlatform.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Име")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Името на събитието трябва да е по-дълго от 4 символа!")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Начална дата")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime EventStartDate { get; set; }

        [Required]
        [Display(Name = "Крайна дата")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime EventEndDate { get; set; }

        [Required]
        [Display(Name = "Максимален брой участници")]
        public int MaxNumberOfParticipants { get; set; }

        [Required]
        [Display(Name = "Вид събитие")]
        public EventType EventType { get; set; }

        [Required]
        [Display(Name = "Детайли")]
        public string Details { get; set; }

        public LocationViewModel? Location { get; set; }
    }
}
