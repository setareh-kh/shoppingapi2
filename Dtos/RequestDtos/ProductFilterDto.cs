namespace shoppingapi2.Dtos.RequestDtos;

public class ProductFilterDto : BaseFilterRequest
{
    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? CategoryId { get; set; }
    public bool? Available { get; set; }
}
