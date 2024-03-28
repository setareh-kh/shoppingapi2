using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public required string Name { get; set; }
        [Required]
        public required decimal TotalPrice { get; set; }
        [Required]
        public required DateTime CreateAt { get; set; }
        public  DateTime? UpdateDate { get; set; }
        
        //Fk 
        [Required]
        public required int UserId { get; set; }
        public User? User{ get; set; }

    }
}