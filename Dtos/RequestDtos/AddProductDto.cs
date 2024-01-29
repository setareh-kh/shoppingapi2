using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using shoppingapi2.Validators;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class AddProductDto
    {
        [Required(ErrorMessage ="please enter product's name"), MaxLength(250)]
        public required string Name { get; set; }
        [Required(ErrorMessage ="please enter product's price")]
        public required int Price { get; set; }
        [Required(ErrorMessage ="please enter Quantity")]
        [DefaultValue(2)]
        public required int Quantity { get; set; }
        [Required]
        //[IsValidPercent]//=
        [Range(0, 100, ErrorMessage = "nummer should be percent")]
        [DefaultValue(0)]
        public required int Discount { get; set; }
        [Required]
        public required bool Available { get; set; }
        [Required]
        public required bool Active { get; set; }
        [Required]
        public required int CatogoryId { get; set; }
        [Required(ErrorMessage ="please upload file")]
        [AllowedExtnsions(new[] {".jpg",".jepg",".png",".bmp",".gif",".tga",".tiff",".jfif"})]
        [IsValidSizeFile(131072)]//13107=128KB
        public required IFormFile File { get; set; }
    }
}