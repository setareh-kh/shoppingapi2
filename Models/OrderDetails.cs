using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
                
        //Fk 
        [Required]
        public required int OrderId { get; set; }
        public Order? Order{ get; set; }
        [Required]
        public required int ProductId { get; set; }
        public Product? Product{ get; set; }
        //
        [Required]
        public required int UnitPrice { get; set; }
        [Required]
        public required int Discount { get; set; }
        [Required]
        public required int Quantity { get; set; }
        [Required]
        public required DateTime CreateAt { get; set; }
        public  DateTime? UpdateDate { get; set; }

    }
}