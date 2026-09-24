namespace shoppingapi2.Dtos.ResponseDtos;

public class ImageResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ItemType { get; set; } = null!;

    public int ItemId { get; set; }

    public string Url { get; set; } = null!;

    public int Priority { get; set; }
}