using System.ComponentModel.DataAnnotations;
using shoppingapi2.Models;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class UpdateOrderDetailsDto
    {
        [Required]
        public required int OrderId { get; set; }

        [Required]
        public required int ProductId { get; set; }
        //
        [Required]
        public required int UnitPrice { get; set; }
        [Required]
        public required int Discount { get; set; }
        [Required]
        public required int Quantity { get; set; }
    }
}