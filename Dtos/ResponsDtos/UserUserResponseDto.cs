using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class UserUserResponseDto
    {
        [Required, MaxLength(250)]
        public required string Name { get; set; }
        [Required, MaxLength(250)]
        public required string Mobile { get; set; }
        

    }
}