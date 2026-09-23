namespace shoppingapi2.Dtos.ResponseDtos;
public class OrderItemResponseDto
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int UnitPrice { get; set; }

    public int Discount { get; set; }
}