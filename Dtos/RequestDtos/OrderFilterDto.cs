namespace shoppingapi2.Dtos.RequestDtos;
public class OrderFilterDto : BaseFilterRequest
{
    public int? UserId { get; set; }

    public decimal? MinTotalPrice { get; set; }
    public decimal? MaxTotalPrice { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }

    public DateTime? FromUpdateDate { get; set; }
    public DateTime? ToUpdateDate { get; set; }
}