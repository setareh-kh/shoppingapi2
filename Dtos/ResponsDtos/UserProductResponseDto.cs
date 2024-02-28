namespace shoppingapi2.Dtos.ResponseDtos
{
    public class UserProductResponseDto
    {
      
        public required string Name { get; set; }
       
        public required int Price { get; set; }
        
        public required int Quantity { get; set; }
        
        public required int Discount { get; set; }
        
        public required int CatogoryId { get; set; }

    }
}