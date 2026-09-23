using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Dtos.RequestDtos;
public class CreateOrderDto
{
    [Range(1, int.MaxValue)] public required int UserId { get; set; }
    [Required][MinLength(1)] public List<CreateOrderItemDto> Items { get; set; } = new();
}