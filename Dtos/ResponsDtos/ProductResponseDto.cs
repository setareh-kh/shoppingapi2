using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Models
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }
        public required int Discount { get; set; }
        public required bool Available { get; set; }
        public required bool Active { get; set; }
        public required DateTime CreateAt { get; set; }
        public  DateTime? UpdateDate { get; set; }
        //Fk 
        public required int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}