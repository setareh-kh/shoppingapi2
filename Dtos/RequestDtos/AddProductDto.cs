using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class AddProductDto
    {
        [Required, MaxLength(250)]
        public required string Name { get; set; }
        [Required]
        public required int Price { get; set; }
        [Required]
        public required int Quantity { get; set; }
        [Required]
        public required int Discount { get; set; }
        [Required]
        public required bool Available { get; set; }
        [Required]
        public required bool Active { get; set; }
        [Required]
        public required int CatogoryId { get; set; }
        public required IFormFile File { get; set; }
    }
}