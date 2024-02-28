using System.ComponentModel.DataAnnotations;
using shoppingapi2.Models;

namespace shoppingapi2.Dtos.ResponseDtos
{
    public class AdminUserResponseDto
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public required string Name { get; set; }
        [Required, MaxLength(250)]
        public required string Mobile { get; set; }
        [Required, MaxLength(250), MinLength(8)]
        public required string Password { get; set; }
        public required byte Type { get; set; }// 0 is admin and 1 is user
        public Image? ImageProfile { get; set; }
    }
}