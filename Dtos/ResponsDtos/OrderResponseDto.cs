namespace shoppingapi2.Dtos.ResponseDtos;
public class OrderResponseDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime CreateAt { get; set; }

    public List<OrderItemResponseDto> Items { get; set; } = new();
}