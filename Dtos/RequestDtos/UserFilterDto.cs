using System.ComponentModel.DataAnnotations;

namespace shoppingapi2.Dtos.RequestDtos;

public class UserFilterDto : BaseFilterRequest
{
    public string? Name { get; set; }

    public string? Mobile { get; set; }

    public byte? Type { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }
}