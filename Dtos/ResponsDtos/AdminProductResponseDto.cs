using shoppingapi2.Models;

namespace shoppingapi2.Dtos.ResponseDtos
{
    public class AdminProductResponseDto : ProductResponseDto
    {
        public required List<Image>? Images { get; set; }
    }
}