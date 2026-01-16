using System.ComponentModel.DataAnnotations;

namespace TravelGuideMvc.Models
{
    public class CultureArticle : BaseEntity
    {
        [Required, StringLength(180)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string Slug { get; set; } = string.Empty;

        [Required, StringLength(5)]
        public string Language { get; set; } = "TR"; // TR, EN

        [Required, StringLength(60)]
        public string Category { get; set; } = "General"; // SimCard, Money, Culture...

        public string Content { get; set; } = string.Empty; // HTML/Markdown/plain

        public bool IsPublished { get; set; } = true;

        public DateTime? PublishedAt { get; set; }
    }
}
