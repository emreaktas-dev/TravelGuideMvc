using System.ComponentModel.DataAnnotations;

namespace TravelGuideMvc.Models
{
    public class Place : BaseEntity
    {
        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(180)]
        public string Slug { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        // Konum
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Range(-180, 180)]
        public double Longitude { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [StringLength(200)]
        public string? OpeningHours { get; set; }

        [StringLength(120)]
        public string? PriceInfo { get; set; }

        [StringLength(120)]
        public string? BestTimeToVisit { get; set; }

        // FK
        public Guid CityId { get; set; }
        public City? City { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<PlaceImage> Images { get; set; } = new List<PlaceImage>();
    }
}
