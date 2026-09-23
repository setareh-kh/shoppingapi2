using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Dtos.RequestDtos;

public class CreateOrderItemDto
{
    [Range(1, int.MaxValue)] public required int ProductId { get; set; }
    [Range(1, int.MaxValue)]public required int Quantity { get; set; }
}