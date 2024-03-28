using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using shoppingapi2.Validators;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class AddOrderDto
    {
        [Required(ErrorMessage ="please enter product's name"), MaxLength(250)]
        public required string Name { get; set; }
        [Required(ErrorMessage ="please enter product's price")]
        public required decimal TotalPrice { get; set; }
        [Required]
        public required int UserId { get; set; }
    }
}