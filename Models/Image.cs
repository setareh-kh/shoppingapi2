using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public required string ItemType { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        [MaxLength(400)]
        public required string Url { get; set; }
        [Required]
        public int Priority { get; set; }

    }
}