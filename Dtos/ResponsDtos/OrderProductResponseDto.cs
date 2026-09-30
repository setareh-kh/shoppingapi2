namespace shoppingapi2.Dtos.ResponseDtos;
public class OrderProductResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
}
