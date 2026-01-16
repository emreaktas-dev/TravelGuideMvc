using System.ComponentModel.DataAnnotations;

namespace TravelGuideMvc.Models
{
    public class EmergencyContact : BaseEntity
    {
        [Required, StringLength(120)]
        public string Title { get; set; } = string.Empty;   // Ambulans, Polis...

        [Required, StringLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Notes { get; set; }

        public int Priority { get; set; } = 0;
    }
}
