using shoppingapi2.Models;

namespace shoppingapi2.Dtos.ResponseDtos
{
    public class AdminProductResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int Price { get; set; }
        public required int Quantity { get; set; }
        public required int Discount { get; set; }
        public required bool Available { get; set; }
        public required bool Active { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateDate { get; set; }
        public required int CategoryId { get; set; }
        public required List<Image>? Images { get; set; }
    }
}