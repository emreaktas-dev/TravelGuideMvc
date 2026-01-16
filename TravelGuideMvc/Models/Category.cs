using System.ComponentModel.DataAnnotations;

namespace TravelGuideMvc.Models
{
    public class Category : BaseEntity
    {
        [Required, StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(120)]
        public string Slug { get; set; } = string.Empty;

        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}
