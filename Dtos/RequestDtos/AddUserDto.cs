using System.ComponentModel.DataAnnotations;
using shoppingapi2.Validators;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class AddUserDto
    {
        [Required(ErrorMessage = "please enter your Name"), MaxLength(250)]
        public required string Name { get; set; }
        [Required(ErrorMessage = "please enter your MobileNumber"), MaxLength(250)]
        [IsValidMobileNumber]//==[RegularExpression(@"^(\+98|0)?9\d{9}$", ErrorMessage = "The MobileNumber is not Valid")]
        public required string Mobile { get; set; }
        [Required(ErrorMessage = "please enter your password"), MaxLength(250), MinLength(8)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public required byte Type { get; set; }// 0 is admin and 1 is user
        [Required(ErrorMessage = "please upload file")]
        [AllowedExtnsions(new[] { ".jpg", ".jpeg",".png", ".bmp", ".gif", ".tga", ".tiff", ".jfif" })]
        [IsValidSizeFile(131072)]//13107=128KB
        public required IFormFile File { get; set; }
    }
}