using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Dtos.RequestDtos
{
    public class CatogoryDto
    {
        [Required, MaxLength(250)]
        public required string Name { get; set; }
    }
}