using shoppingapi2.Models;

namespace shoppingapi2.Dtos.ResponseDtos
{
    public class UserUserResponseDto
    {
       
        public required string Name { get; set; }
        public required string Mobile { get; set; }
        public string? ImageProfile { get; set; }
        
        

    }
}