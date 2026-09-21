using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Models
{
    public class Catogory : ISqlEntity
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public required string Name { get; set; }
        //public DateTime? CreateAt { get; set; }

    }
}