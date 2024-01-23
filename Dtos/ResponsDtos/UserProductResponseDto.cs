using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class UserProductResponseDto
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
        public required int CatogoryId { get; set; }
    }
}