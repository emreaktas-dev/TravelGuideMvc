using System.ComponentModel.DataAnnotations;
using TravelGuideMvc.Models.Enums;

namespace TravelGuideMvc.Models
{
    public class ServicePoint : BaseEntity
    {
        public ServicePointType Type { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(40)]
        public string? Phone { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Range(-180, 180)]
        public double Longitude { get; set; }

        // City (opsiyonel ama filtre için iyi)
        public Guid CityId { get; set; }
        public City? City { get; set; }
    }
}
