using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Models
{
    public class Catogory
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public required string Name { get; set; }
    }
}