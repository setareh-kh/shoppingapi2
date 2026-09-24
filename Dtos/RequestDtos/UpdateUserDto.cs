using System.ComponentModel.DataAnnotations;
using shoppingapi2.Validators;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class UpdateUserDto
    {
        [Required, MaxLength(250)]
        public required string Name { get; set; }
        [Required(ErrorMessage = "please enter your MobileNumber"), MaxLength(250)]
        [IsValidMobileNumber]//==[RegularExpression(@"^(\+98|0)?9\d{9}$", ErrorMessage = "The MobileNumber is not Valid")]
        public required string Mobile { get; set; }
        [AllowedExtnsions(new[] { ".jpg", ".jpeg",".png", ".bmp", ".gif", ".tga", ".tiff", ".jfif" })]
        [IsValidSizeFile(131072)]//131072=128KB
        public IFormFile? Image { get; set; }
        public bool AddImage { get; set; }


    }
}