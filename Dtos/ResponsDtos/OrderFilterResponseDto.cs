using shoppingapi2.Dtos.ResponseDtos;

public class OrderFilterResponseDto
{
    public int Id { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int UserId { get; set; }

    public UserResponseDto? User { get; set; }

    public List<OrderDetailsResponseDto> OrderDetails { get; set; } = new();
}
