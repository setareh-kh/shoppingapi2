namespace shoppingapi2.Dtos.ResponseDtos;
public class OrderDetailsResponseDto
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public OrderProductResponseDto? Product { get; set; }

    public decimal UnitPrice { get; set; }

    public int Discount { get; set; }

    public int Quantity { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime? UpdateDate { get; set; }
}
