using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public required string Name { get; set; }
        [Required]
        public required int Price { get; set; }
        [Required]
        public required int Quantity { get; set; }
        [Required]
        public required int Discount { get; set; }
        [Required]
        public required bool Available { get; set; }
        [Required]
        public required bool Active { get; set; }
        [Required]
        public required DateTime CreateAt { get; set; }
        public  DateTime? UpdateDate { get; set; }
        //Fk 
        [Required]
        public required int CatogoryId { get; set; }
        public Catogory? Catogory { get; set; }

    }
}