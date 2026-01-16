using System.ComponentModel.DataAnnotations;

namespace TravelGuideMvc.Models
{
    public class PlaceImage : BaseEntity
    {
        public Guid PlaceId { get; set; }
        public Place? Place { get; set; }

        [Required, StringLength(500)]
        public string Url { get; set; } = string.Empty;

        public int Order { get; set; } = 0;
    }
}
