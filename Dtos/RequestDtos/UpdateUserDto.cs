using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class UpdateUserDto
    {
        [Required, MaxLength(250)]
        public required string Name { get; set; }
        [Required, MaxLength(250), MinLength(8)]
        public required string Password { get; set; }

    }
}