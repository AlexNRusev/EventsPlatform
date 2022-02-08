using System.ComponentModel.DataAnnotations;

namespace EventsPlatform.ViewModels
{
    public class LocationViewModel
    {
        
        [Required]
        [Display(Name = "Град")]
        public string Town { get; set; }

        [Display(Name = "Улица")]
        public string? Street { get; set; }

        [Display(Name = "Други детайли за местоположението")]
        public string? OtherDetails { get; set; }
        public int EventId { get; set; }
    }
}
