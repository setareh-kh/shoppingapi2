using shoppingapi2.Models;

namespace shoppingapi2.Dtos.ResponseDtos
{
    public class AdminUserResponseDto:UserResponseDto
    {
        public Image? ImageProfile { get; set; }
    }
}