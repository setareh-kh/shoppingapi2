using System.ComponentModel.DataAnnotations;
using shoppingapi2.Validators;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "please enter your MobileNumber"), MaxLength(250)]
        [IsValidMobileNumber]//==[RegularExpression(@"^(\+98|0)?9\d{9}$", ErrorMessage = "The MobileNumber is not Valid")]
        public required string Mobile { get; set; }
        [Required(ErrorMessage = "please enter your password"), MaxLength(250), MinLength(8)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}